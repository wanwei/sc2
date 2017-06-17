using com.wer.sc.data;
using com.wer.sc.data.utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin.cnfutures.config
{
    internal class CodeInfoGenerator
    {
        private int startYear = 2004;
        private int endYear = 2018;

        //private Dictionary<string, CodeInfo> dic_OldCode_CodeInfo = new Dictionary<string, CodeInfo>();

        private Dictionary<string, CodeInfo> dic_Code_OldCodeInfo = new Dictionary<string, CodeInfo>();

        //private List<CodeInfo> instruments;

        private List<CodeInfo> generatedCodes;

        private Dictionary<String, CodeInfo> dic_Id_CodeInfo = new Dictionary<string, CodeInfo>();

        private Dictionary<String, CodeInfo> dic_ServerId_CodeInfo = new Dictionary<string, CodeInfo>();

        private Dictionary<String, List<CodeInfo>> dic_Variety_Codes = new Dictionary<string, List<CodeInfo>>();

        //private CodeInfoLoader codeInfoLoader;

        private DataLoader_InstrumentInfo dataLoader_CodeInfo;

        public DataLoader_InstrumentInfo DataLoader_CodeInfo
        {
            get { return dataLoader_CodeInfo; }
        }

        public CodeInfoGenerator(string pluginPath)
        {
            this.dataLoader_CodeInfo = new DataLoader_InstrumentInfo(pluginPath);

            CodeInfoLoader codeInfoLoader = new CodeInfoLoader(pluginPath);
            this.generatedCodes = codeInfoLoader.LoadCodes();
            if (this.generatedCodes == null)
                GenerateCodes(pluginPath);
            InitCache();
        }

        private void InitCache()
        {
            for (int i = 0; i < generatedCodes.Count; i++)
            {
                CodeInfo code = generatedCodes[i];

                dic_Id_CodeInfo.Add(code.Code.ToUpper(), code);

                //初始化servercode
                //serverCode表示服务器上认定的ID，郑州所比较特别
                //如rm1709，郑州所为rm709表示17年9月合约，这导致servercode重名
                //rm0709和rm1709合约的servercode重名
                string serverCodeKey = code.ServerCode.ToUpper();
                if (dic_ServerId_CodeInfo.ContainsKey(serverCodeKey))
                {
                    dic_ServerId_CodeInfo.Remove(serverCodeKey);
                }
                dic_ServerId_CodeInfo.Add(code.ServerCode.ToUpper(), code);

                //初始化品种和合约的关系
                if (dic_Variety_Codes.ContainsKey(code.Catelog))
                {
                    dic_Variety_Codes[code.Catelog.ToUpper()].Add(code);
                }
                else
                {
                    List<CodeInfo> codes = new List<CodeInfo>();
                    codes.Add(code);
                    dic_Variety_Codes.Add(code.Catelog.ToUpper(), codes);
                }

                //初始化用新id编号找老id编号
                string oldCodeId = GetOldCodeId(code);
                dic_Code_OldCodeInfo.Add(code.Code, dataLoader_CodeInfo.GetInstrument(oldCodeId));
            }
        }

        private string GetOldCodeId(CodeInfo codeInfo)
        {
            string newCodeId = codeInfo.Code;
            if (newCodeId.EndsWith("0000"))
            {
                return newCodeId.Substring(0, newCodeId.Length - 4) + "13";
            }
            else if (newCodeId.EndsWith("MI"))
            {
                return newCodeId;
            }
            else
            {
                int len = newCodeId.Length;
                //处理两年合约的情况
                if (codeInfo.End - codeInfo.Start > 15000)
                {
                    int e = (codeInfo.End / 10000) % 2;
                    if (e == 1)
                        return newCodeId.Substring(0, len - 4) + "Y" + newCodeId.Substring(len - 2, 2);
                    return newCodeId.Substring(0, len - 4) + "X" + newCodeId.Substring(len - 2, 2); ;
                }
                return newCodeId.Substring(0, len - 4) + newCodeId.Substring(len - 2, 2);
            }
        }

        private void GenerateCodes(string pluginPath)
        {
            PathUtils pathUtils = new PathUtils(pluginPath);
            List<CodeInfo> instruments = this.dataLoader_CodeInfo.GetAllInstruments();
            //this.instruments = CsvUtils_Code.LoadByContent(File.ReadAllText(pathUtils.InstrumentPath));
            this.generatedCodes = new List<CodeInfo>();
            for (int i = 0; i < instruments.Count; i++)
            {
                CodeInfo codeInfo = instruments[i];
                generatedCodes.AddRange(GetCodes(codeInfo, startYear, endYear));
            }
        }

        private string GetCatelogName(string code)
        {
            return dataLoader_CodeInfo.GetVarietyNameByCode(code.ToUpper());
        }

        private List<CodeInfo> GetCodes(CodeInfo code, int startYear, int endYear)
        {
            List<CodeInfo> codes = new List<CodeInfo>();
            string codeId = code.Code;
            bool isIndex = codeId.EndsWith("MI") || codeId.EndsWith("13");
            if (isIndex)
            {
                string catelogName = GetCatelogName(code.Code);
                CodeInfo codeInfo = new CodeInfo(code.Code, code.Name, code.Catelog, catelogName, -1, -1, dataLoader_CodeInfo.GetBelongMarket(codeId), codeId);
                if (codeId.EndsWith("13"))
                {
                    codeInfo.Code = codeId.Substring(0, codeId.Length - 2) + "0000";
                    codeInfo.ServerCode = codeInfo.Code;
                }
                codes.Add(codeInfo);
                //dic_Code_OldCodeInfo.Add(codeInfo.Code.ToUpper(), code);
                return codes;
            }

            for (int i = startYear; i <= endYear; i++)
            {
                CodeInfo codeInfo = GetCodes(code, i);
                if (codeInfo != null)
                {
                    codes.Add(codeInfo);
                    //dic_Code_OldCodeInfo.Add(codeInfo.Code.ToUpper(), code);
                }
            }
            return codes;
        }

        public static CodeInfo GetCodes(string codeId, string varietyName, string exchange)
        {
            string varietyId = GetVarietyId(codeId);
            int year = 2000 + int.Parse(codeId.Substring(codeId.Length - 4, 2));
            int month = int.Parse(codeId.Substring(codeId.Length - 2, 2));
            string codeName = varietyName + codeId.Substring(codeId.Length - 4, 4);
            int start = GetEndDay(year - 1, month) + 1;
            int end = GetEndDay(year, month);
            CodeInfo code = new CodeInfo(codeId, codeName, varietyId, varietyName, start, end, exchange, "");
            GetServerCode(code);
            return code;
        }

        public static String GetVarietyId(string codeId)
        {
            string varietyId = codeId.Substring(0, codeId.Length - 4);
            return varietyId;
        }

        private CodeInfo GetCodes(CodeInfo code, int year)
        {
            if (code.Code.StartsWith("SR"))
                return null;
            bool isTwoYearCode;
            CodeInfo twoYearCode = GetTwoYearCodes(code, year, out isTwoYearCode);
            if (isTwoYearCode)
                return twoYearCode;

            string codeId = code.Code;
            string codePrefix = codeId.Substring(0, codeId.Length - 2);
            int subYear = year - 2000;
            string codeSuffix = ((subYear < 10) ? ("0" + subYear.ToString()) : subYear.ToString()) + codeId.Substring(codeId.Length - 2, 2);

            CodeInfo newCode = new CodeInfo();
            newCode.Code = codePrefix + codeSuffix;
            newCode.Name = code.Name.Substring(0, code.Name.Length - 2) + codeSuffix;
            newCode.Catelog = code.Catelog;
            newCode.CatelogName = GetCatelogName(code.Code);
            int month = int.Parse(codeId.Substring(codeId.Length - 2, 2));
            newCode.Start = GetEndDay(year - 1, month) + 1;
            newCode.End = GetEndDay(year, month);
            newCode.Exchange = dataLoader_CodeInfo.GetBelongMarket(codeId);
            GetServerCode(newCode);
            return newCode;
        }

        private static void GetServerCode(CodeInfo newCode)
        {
            if (newCode.Exchange == "ZZ")
            {
                int cutIndex = newCode.Code.Length - 4;
                newCode.ServerCode = newCode.Code.Substring(0, cutIndex) + newCode.Code.Substring(cutIndex + 1, 3);
            }
            else
                newCode.ServerCode = newCode.Code;
        }

        private CodeInfo GetTwoYearCodes(CodeInfo code, int year, out bool isTwoYearCode)
        {
            isTwoYearCode = false;
            string codeId = code.Code;
            string codePrefix = codeId.Substring(0, codeId.Length - 2);
            bool isOddContract = codePrefix.EndsWith("y") || codePrefix.EndsWith("Y");
            bool isEvenContract = codePrefix.EndsWith("x") || codePrefix.EndsWith("X");
            if (!isOddContract && !isEvenContract)
                return null;
            //豆油合约是y01,y05...，要把这些合约找出来。
            if (codeId.Length == 3)
                return null;

            isTwoYearCode = true;
            if (isOddContract && year % 2 == 0)
                return null;
            if (isEvenContract && year % 2 == 1)
                return null;

            int subYear = year - 2000;
            codePrefix = codePrefix.Substring(0, codePrefix.Length - 1);
            string codeSuffix = ((subYear < 10) ? ("0" + subYear.ToString()) : subYear.ToString()) + codeId.Substring(codeId.Length - 2, 2);

            CodeInfo newCode = new CodeInfo();
            newCode.Code = codePrefix + codeSuffix;
            newCode.Name = code.Name.Substring(0, code.Name.Length - 2) + codeSuffix;
            newCode.Catelog = code.Catelog;
            newCode.CatelogName = GetCatelogName(code.Code);
            int month = int.Parse(codeId.Substring(codeId.Length - 2, 2));
            newCode.Start = GetEndDay(year - 2, month) + 1;
            newCode.End = GetEndDay(year, month);
            newCode.Exchange = dataLoader_CodeInfo.GetBelongMarket(codeId);
            GetServerCode(newCode);
            return newCode;
        }

        public static int GetEndDay(int year, int month)
        {
            string str = year.ToString() + (month < 10 ? ("0" + month) : month.ToString()) + "01";
            DateTime dt = DateTime.ParseExact(str, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
            DayOfWeek dayOfWeek = dt.DayOfWeek;
            //如果是周末
            if (dayOfWeek == DayOfWeek.Saturday || dayOfWeek == DayOfWeek.Sunday)
            {
                int day = 7 + 1 - (int)dayOfWeek + 2 * 7 + 5;
                //星期天是0
                if (dayOfWeek == DayOfWeek.Sunday)
                    day -= 7;
                return year * 10000 + month * 100 + day;
            }
            else
            {
                int day = 7 + 1 - (int)dayOfWeek + 7 + 5;
                return year * 10000 + month * 100 + day;
            }
        }

        private string GetCodeId(string code, int year)
        {
            year -= 2000;
            return code.Substring(0, code.Length - 2)
                + ((year < 10) ? ("0" + year.ToString()) : year.ToString()) + code.Substring(code.Length - 2, 2);
        }

        private int GetEnd(int year, int month)
        {
            DateTime dt1 = Convert.ToDateTime(year + "-" + month + "-1");
            DayOfWeek day = dt1.DayOfWeek;
            int d = 7 - (int)day;
            return year * 10000 + month * 100 + 14 + d;
        }

        public List<CodeInfo> GetAllOldCodes()
        {
            return dataLoader_CodeInfo.GetAllInstruments();
        }

        public List<CodeInfo> GetAllInstruments()
        {
            return generatedCodes;
        }

        public CodeInfo GetInstrument(String instrument)
        {
            string key = instrument.ToUpper();
            if (dic_Id_CodeInfo.ContainsKey(key))
                //return dicInstruments[instrument];
                return dic_Id_CodeInfo[key];
            return null;
        }

        public CodeInfo GetInstrumentByServerId(String instrument)
        {
            string key = instrument.ToUpper();
            if (dic_ServerId_CodeInfo.ContainsKey(key))
                //return dicInstruments[instrument];
                return dic_ServerId_CodeInfo[key];
            return null;
        }

        public List<CodeInfo> GetInstruments(String variety)
        {
            return dic_Variety_Codes[variety.ToUpper()];
        }

        public String GetBelongMarketByCode(String code)
        {
            return dataLoader_CodeInfo.GetBelongMarket(GetOldCodeInfo(code).Code);
        }

        public String GetBelongMarketByVariety(String variety)
        {
            return dataLoader_CodeInfo.GetMarketByVariety(variety);
        }

        public string GetMarketByVariety(string variety)
        {
            return dataLoader_CodeInfo.GetMarketByVariety(variety);
        }

        public String GetVariety(String code)
        {
            return dataLoader_CodeInfo.GetVariety(GetOldCodeInfo(code).Code);
        }

        public String GetVarietyName(String variety)
        {
            return dataLoader_CodeInfo.GetVarietyName(variety);
        }

        public CodeInfo GetOldCodeInfo(String code)
        {
            if (dic_Code_OldCodeInfo.ContainsKey(code.ToUpper()))
                return dic_Code_OldCodeInfo[code.ToUpper()];
            return null;
        }
    }
}
