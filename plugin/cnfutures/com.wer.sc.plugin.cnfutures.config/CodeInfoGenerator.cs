﻿using com.wer.sc.data;
using com.wer.sc.data.utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin.cnfutures.config
{
    public class CodeInfoGenerator
    {
        private int startYear = 2004;
        private int endYear = 2017;

        private Dictionary<string, CodeInfo> dic_OldCode_CodeInfo = new Dictionary<string, CodeInfo>();

        private Dictionary<string, CodeInfo> dic_Code_OldCodeInfo = new Dictionary<string, CodeInfo>();

        private List<CodeInfo> instruments;

        private List<CodeInfo> generatedCodes;

        private DataLoader_InstrumentInfo loader;

        public CodeInfoGenerator(string pluginPath)
        {
            PathUtils pathUtils = new PathUtils(pluginPath);
            this.instruments = CsvUtils_Code.LoadByContent(File.ReadAllText(pathUtils.InstrumentPath));
            this.loader = new DataLoader_InstrumentInfo(pluginPath);
            generatedCodes = new List<CodeInfo>();
            for (int i = 0; i < instruments.Count; i++)
            {
                CodeInfo codeInfo = instruments[i];
                generatedCodes.AddRange(GetCodes(codeInfo, startYear, endYear));
            }                       
        }

        private List<CodeInfo> GetCodes(CodeInfo code, int startYear, int endYear)
        {
            List<CodeInfo> codes = new List<CodeInfo>();
            string codeId = code.Code;
            bool isIndex = codeId.EndsWith("MI") || codeId.EndsWith("13");
            if (isIndex)
            {
                CodeInfo codeInfo = new CodeInfo(code.Code, code.Name, code.Catelog, -1, -1, loader.GetBelongMarket(codeId));
                if (codeId.EndsWith("13"))
                    codeInfo.Code = codeId.Substring(0, codeId.Length - 2) + "0000";
                codes.Add(codeInfo);
                dic_Code_OldCodeInfo.Add(codeInfo.Code, code);
                return codes;
            }

            for (int i = startYear; i <= endYear; i++)
            {
                CodeInfo codeInfo = GetCodes(code, i);
                if (codeInfo != null)
                {
                    codes.Add(codeInfo);
                    dic_Code_OldCodeInfo.Add(codeInfo.Code, code);               
                }
            }
            return codes;
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
            int month = int.Parse(codeId.Substring(codeId.Length - 2, 2));
            newCode.Start = GetEndDay(year - 1, month) + 1;
            newCode.End = GetEndDay(year, month);
            newCode.Exchange = loader.GetBelongMarket(codeId);
            return newCode;
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
            int month = int.Parse(codeId.Substring(codeId.Length - 2, 2));
            newCode.Start = GetEndDay(year - 2, month) + 1;
            newCode.End = GetEndDay(year, month);
            newCode.Exchange = loader.GetBelongMarket(codeId);
            return newCode;
        }

        private int GetEndDay(int year, int month)
        {
            string str = year.ToString() + (month < 10 ? ("0" + month) : month.ToString()) + "01";
            DateTime dt = DateTime.ParseExact(str, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
            DayOfWeek dayOfWeek = dt.DayOfWeek;
            //如果是周末
            if (dayOfWeek == DayOfWeek.Saturday || dayOfWeek == DayOfWeek.Sunday)
            {
                int day = 7 + 1 - (int)dayOfWeek + 2 * 7 + 5;
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

        public List<CodeInfo> GenerateCodes()
        {
            return generatedCodes;
        }

        public CodeInfo GetOldCodeInfo(String code)
        {
            if (dic_Code_OldCodeInfo.ContainsKey(code))
                return dic_Code_OldCodeInfo[code];
            return null;
        }
    }
}
