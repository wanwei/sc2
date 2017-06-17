using com.wer.sc.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin.cnfutures.config
{
    public class CodeInfoUtils
    {
        public const string EXCHANGE_DL = "DL";
        public const string EXCHANGE_SQ = "SQ";
        public const string EXCHANGE_ZZ = "ZZ";

        //双年合约
        //SR从2009年3月17日第一次出现双年合约
        public static string[] TWOYEARS_VARIETY = new string[] { "A", "BU", "SR", "WS" };

        /// <summary>
        /// 得到期货合约对应的品种
        /// </summary>
        /// <param name="codeId"></param>
        /// <returns></returns>
        public static string GetVariety(string codeId)
        {
            return new CodeIdParser(codeId).VarietyId;
            //if (codeId == null)
            //    return null;
            //string ucodeId = codeId.ToUpper();
            //if (ucodeId.EndsWith("MI"))
            //    return ucodeId.Substring(0, ucodeId.Length - 2);

            //int index = ucodeId.Length - 1;
            //int result;
            //bool isNumber = int.TryParse(ucodeId[index].ToString(), out result);
            //while (isNumber && index > 0)
            //{
            //    index--;
            //    isNumber = int.TryParse(ucodeId[index].ToString(), out result);
            //}
            //string variety = ucodeId.Substring(0, index + 1);

            //if (variety.EndsWith("X") || variety.EndsWith("Y"))
            //{
            //    if (variety.Length > 1)
            //        return variety.Substring(0, variety.Length - 1);
            //}

            //return variety;
        }

        ///// <summary>
        ///// 分解codeid
        ///// 如ax01，分解成[a,x,01]
        ///// </summary>
        ///// <param name="codeId"></param>
        ///// <returns></returns>
        //public static string[] SplitCodeId(String codeId)
        //{
        //    string[] resultArr = new string[3];

        //    if (codeId == null)
        //        return null;
        //    string ucodeId = codeId.ToUpper();
        //    if (ucodeId.EndsWith("MI"))
        //    {
        //        resultArr[0] = ucodeId.Substring(0, ucodeId.Length - 2);
        //        resultArr[1] = "";
        //        resultArr[2] = "MI";
        //        return resultArr;
        //    }

        //    int index = ucodeId.Length - 1;
        //    int result;
        //    bool isNumber = int.TryParse(ucodeId[index].ToString(), out result);
        //    while (isNumber && index > 0)
        //    {
        //        index--;
        //        isNumber = int.TryParse(ucodeId[index].ToString(), out result);
        //    }
        //    string variety = ucodeId.Substring(0, index + 1);
        //    string suffix = ucodeId.Substring(index + 1, ucodeId.Length - index - 1);

        //    if (variety.EndsWith("X") || variety.EndsWith("Y"))
        //    {
        //        if (variety.Length > 1)
        //        {
        //            resultArr[0] = variety;
        //            resultArr[1] = variety.Substring(variety.Length - 1, 1);
        //            resultArr[2] = suffix;
        //            return resultArr;
        //        }
        //    }

        //    resultArr[0] = variety;
        //    resultArr[1] = "";
        //    resultArr[2] = suffix;
        //    return resultArr;
        //}

        public static string GetComplexCodeId(CodeIdParser parser, int tradingDay)
        {
            string suffix = parser.Suffix;
            if (suffix == "13")
                return parser.VarietyId + "0000";
            if (suffix == "MI")
                return parser.VarietyId + "MI";

            int contractMonth = parser.Month;
            if (contractMonth < 0)
                return null;
            int tradingMonth = (tradingDay % 10000) / 100;
            int tradingYear = (tradingDay / 10000) % 100;

            //双年合约
            if (parser.TwoYearTag != null && parser.TwoYearTag != "")
            {
                bool isEven = tradingYear % 2 == 0;
                if (isEven && parser.TwoYearTag == CodeIdParser.TWOYEARTAG_ODD)
                    return GetComplexCodeIdInternal(parser, tradingYear + 1);
                if (!isEven && parser.TwoYearTag == CodeIdParser.TWOYEARTAG_EVEN)
                    return GetComplexCodeIdInternal(parser, tradingYear + 1);

                if (IsLateThan(contractMonth, 2000 + tradingYear, tradingMonth, tradingDay))
                    return GetComplexCodeIdInternal(parser, tradingYear + 2);
                return GetComplexCodeIdInternal(parser, tradingYear);
            }
            else
            {
                return GetComplexCodeId(parser, contractMonth, tradingYear, tradingMonth, tradingDay);
            }
        }

        public static string GetComplexCodeId(String codeId, int tradingDay)
        {
            return GetComplexCodeId(new CodeIdParser(codeId), tradingDay);
        }

        private static string GetComplexCodeId(CodeIdParser parser, int contractMonth, int tradingYear, int tradingMonth, int tradingDay)
        {
            if (IsLateThan(contractMonth, 2000 + tradingYear, tradingMonth, tradingDay))
                return GetComplexCodeIdInternal(parser, tradingYear + 1);
            return GetComplexCodeIdInternal(parser, tradingYear);
        }

        private static String GetComplexCodeIdInternal(CodeIdParser parser, int tradingYear)
        {
            return parser.VarietyId + ((tradingYear < 10) ? "0" + tradingYear.ToString() : tradingYear.ToString()) + parser.Suffix;
        }

        private static bool IsLateThan(int contractMonth, int tradingYear, int tradingMonth, int tradingDay)
        {
            if (tradingMonth > contractMonth)
                return true;
            else if (tradingMonth < contractMonth)
                return false;
            else
            {
                int endDay = GetEndDay(tradingYear, contractMonth);
                if (tradingDay > endDay)
                    return true;
                return false;
            }
        }

        /// <summary>
        /// 由期货现在的带年份的合约ID换成老的不带年份的合约ID
        /// 如由M1605得到M05
        /// </summary>
        /// <param name="codeInfo"></param>
        /// <returns></returns>
        public static string GetSimpleCodeId(string codeId)
        {
            if (codeId == null)
                return null;
            string newCodeId = codeId.ToUpper();
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
                CodeIdParser parser = new CodeIdParser(codeId);
                //string[] codeArr = SplitCodeId(codeId);
                string variety = parser.VarietyId;
                string suffix = parser.Suffix;
                if (IsTwoYearsVariety(variety))
                {
                    int p = int.Parse(suffix.Substring(0, 2));
                    if (p % 2 == 1)
                        return variety + "Y" + suffix.Substring(2, 2);
                    return variety + "X" + suffix.Substring(2, 2);
                }
                return variety + suffix.Substring(2, 2);
            }
        }

        public static bool IsTwoYearsVariety(string variety)
        {
            for (int i = 0; i < TWOYEARS_VARIETY.Length; i++)
            {
                if (variety.ToUpper().Equals(TWOYEARS_VARIETY[i]))
                    return true;
            }
            return false;
        }

        public static CodeInfo GetCodeInfo(CodeIdParser parser, DataLoader_Variety dataLoader_Variety)
        {
            string varietyId = parser.VarietyId;
            string suffix = parser.Suffix;
            int year = parser.Year;
            int month = parser.Month;

            VarietyInfo varietyInfo = dataLoader_Variety.GetVariety(varietyId);
            if (varietyInfo == null)
                return null;
            string codeId = parser.CodeId;
            string codeName = varietyInfo.Name + suffix;
            int start = parser.StartDay;
            int end = parser.EndDay;
            string exchange = varietyInfo.Exchange;
            string varietyName = varietyInfo.Name;
            CodeInfo code = new CodeInfo(parser.CodeId, codeName, varietyId, varietyName, start, end, exchange, "");
            code.ServerCode = GetServerCode(parser.CodeId, exchange);
            code.ShortCode = parser.ShortCode;
            return code;
        }

        public static CodeInfo GetCodeInfo(String newCodeId, DataLoader_Variety dataLoader_Variety)
        {
            return GetCodeInfo(new CodeIdParser(newCodeId), dataLoader_Variety);
        }

        /// <summary>
        /// 传入代码，品种名称和交易所，得到CodeInfo
        /// </summary>
        /// <param name="codeId"></param>
        /// <param name="varietyName"></param>
        /// <param name="exchange"></param>
        /// <returns></returns>
        //public static CodeInfo GetCodeInfo(string codeId, string varietyName, string exchange)
        //{
        //    return GetCodeInfo(codeId, SplitCodeId(codeId), varietyName, exchange);
        //}

        public static CodeInfo GetCodeInfo(string codeId, string[] codeIdSplit, string varietyName, string exchange)
        {
            if (codeIdSplit[1] != "")
            {
                return GetTwoYearCodes(codeId, codeIdSplit, varietyName, exchange);
            }
            string varietyId = codeIdSplit[0];
            string suffix = codeIdSplit[2];
            int year = 2000 + int.Parse(suffix.Substring(suffix.Length - 4, 2));
            int month = int.Parse(suffix.Substring(suffix.Length - 2, 2));
            string codeName = varietyName + suffix.Substring(suffix.Length - 4, 4);
            int start = GetEndDay(year - 1, month) + 1;
            int end = GetEndDay(year, month);
            CodeInfo code = new CodeInfo(codeId, codeName, varietyId, varietyName, start, end, exchange, "");
            code.ServerCode = GetServerCode(codeId, exchange);
            //code.ShortCode = codeIdSplit[0]
            return code;
        }

        private static CodeInfo GetTwoYearCodes(string codeId, string[] codeIdSplit, string varietyName, string exchange)
        {
            string codeTwoYearTag = codeIdSplit[1];
            bool isOddContract = codeTwoYearTag.EndsWith("y") || codeTwoYearTag.EndsWith("Y");
            bool isEvenContract = codeTwoYearTag.EndsWith("x") || codeTwoYearTag.EndsWith("X");
            if (!isOddContract && !isEvenContract)
                return null;
            string suffix = codeIdSplit[2];
            int year = 2000 + int.Parse(suffix.Substring(suffix.Length - 4, 2));

            if (isOddContract && year % 2 == 0)
                return null;
            if (isEvenContract && year % 2 == 1)
                return null;

            int subYear = year - 2000;
            codeTwoYearTag = codeTwoYearTag.Substring(0, codeTwoYearTag.Length - 1);
            string codeSuffix = ((subYear < 10) ? ("0" + subYear.ToString()) : subYear.ToString()) + codeId.Substring(codeId.Length - 2, 2);

            CodeInfo newCode = new CodeInfo();
            newCode.Code = codeTwoYearTag + codeSuffix;
            newCode.Name = varietyName + codeSuffix;
            newCode.Catelog = codeIdSplit[0];
            newCode.CatelogName = varietyName;
            int month = int.Parse(codeId.Substring(codeId.Length - 2, 2));
            newCode.Start = GetEndDay(year - 2, month) + 1;
            newCode.End = GetEndDay(year, month);
            newCode.Exchange = exchange;
            newCode.ServerCode = GetServerCode(codeId, exchange);
            return newCode;
        }

        public static string GetServerCode(string newCodeId, string exchange)
        {
            if (exchange == EXCHANGE_ZZ)
            {
                int cutIndex = newCodeId.Length - 4;
                return newCodeId.Substring(0, cutIndex) + newCodeId.Substring(cutIndex + 1, 3);
            }
            else
                return newCodeId;
        }

        /// <summary>
        /// 得到合约的交割日
        /// 一般默认是该月的第三个星期五
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
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
    }

    public class CodeIdParser
    {
        /*
         * SR双年合约开始于2008年下半年，数据中最开始有是20090317
         * 20090317前都算SRY合约
            SR1001	20090317-201001
            SR1003	20090317-201003
            SR1005	20090317-201005
            SR1007	20090317-201007
            SR1009	20090317-201009

            SR0905	200705-200905
            SR0907	200707-200905
            SR0909	200709-200909
            SR0911	200711-200911
         */

        /// <summary>
        /// 偶数年份用X表示
        /// </summary>
        public const string TWOYEARTAG_EVEN = "X";

        /// <summary>
        /// 奇数年份用Y表示
        /// </summary>
        public const string TWOYEARTAG_ODD = "Y";

        private string codeId;

        private string serverCodeId;

        private string varietyId;

        private string twoYearTag;

        private string suffix;

        private int year = -1;

        private int month = -1;

        public int Year
        {
            get
            {
                if (year != -1)
                    return year;
                if (suffix.Length < 4 || suffix == "0000")
                {
                    year = 0;
                    return year;
                }
                year = 2000 + int.Parse(suffix.Substring(0, 2));
                return year;
            }
        }

        public int Month
        {
            get
            {
                if (suffix == "13" || suffix == "0000" || suffix == "MI")
                {
                    month = 0;
                    return month;
                }
                if (suffix == null || suffix.Length < 2)
                    return -1;
                month = int.Parse(suffix.Substring(suffix.Length - 2, 2));
                if (month > 13)
                    return -1;
                return month;
            }
        }

        public int EndDay
        {
            get
            {
                if (this.Year <= 0 || this.Month <= 0)
                    return 0;

                return CodeInfoUtils.GetEndDay(Year, Month);
            }
        }

        public int StartDay
        {
            get
            {
                if (this.Year <= 0 || this.Month <= 0)
                    return 0;

                if (this.TwoYearTag == null || this.twoYearTag == "")
                    return CodeInfoUtils.GetEndDay(Year - 1, Month) + 2;
                if (varietyId == "SR")
                {
                    if (TwoYearTag == TWOYEARTAG_EVEN && Year == 2010)
                        return 20090317;
                }
                return CodeInfoUtils.GetEndDay(Year - 2, Month) + 2;
            }
        }

        public string CodeId
        {
            get { return codeId; }
        }

        public string ServerCodeId
        {
            get
            {
                return serverCodeId;
            }
        }

        public string VarietyId
        {
            get
            {
                return varietyId;
            }
        }

        public string TwoYearTag
        {
            get
            {
                return twoYearTag;
            }
        }

        public string Suffix
        {
            get
            {
                return suffix;
            }
        }

        public bool IsComplexCode
        {
            get
            {
                return suffix.Length == 4;
            }
        }

        public string ShortCode
        {
            get
            {
                if (suffix == "0000")
                    return varietyId + "13";
                if (suffix == "MI")
                    return varietyId + "MI";
                return varietyId + TwoYearTag + Suffix.Substring(Suffix.Length - 2, 2);
            }
        }

        public bool IsIndex
        {
            get
            {
                return suffix == "0000" || suffix == "MI";
            }
        }

        public CodeIdParser(string codeId)
        {
            if (codeId == null)
                return;

            string ucodeId = codeId.ToUpper();
            this.codeId = ucodeId;
            this.serverCodeId = codeId;
            if (ucodeId.EndsWith("MI"))
            {
                this.varietyId = ucodeId.Substring(0, ucodeId.Length - 2);
                this.twoYearTag = "";
                this.suffix = "MI";
                return;
            }

            int index = ucodeId.Length - 1;
            int result;
            bool isNumber = int.TryParse(ucodeId[index].ToString(), out result);
            while (isNumber && index > 0)
            {
                index--;
                isNumber = int.TryParse(ucodeId[index].ToString(), out result);
            }
            string variety = ucodeId.Substring(0, index + 1);
            string suffix = ucodeId.Substring(index + 1, ucodeId.Length - index - 1);

            if (variety.EndsWith("X") || variety.EndsWith("Y"))
            {
                if (variety.Length > 1)
                {
                    this.varietyId = variety.Substring(0, variety.Length - 1);
                    this.twoYearTag = variety.Substring(variety.Length - 1, 1);
                    this.suffix = suffix;
                    return;
                }
            }

            this.varietyId = variety;
            //处理郑州交易所代码，证交所传入的代码为FG701，这里统一转换为FG1701
            if (suffix.Length == 3)
            {
                this.suffix = "1" + suffix;
                this.codeId = varietyId + this.suffix;
            }
            else
                this.suffix = suffix;
            //if(this.suffix.Length)
            if (CodeInfoUtils.IsTwoYearsVariety(this.varietyId))
            {
                this.twoYearTag = this.Year % 2 == 1 ? TWOYEARTAG_ODD : TWOYEARTAG_EVEN;
            }
            else
                this.twoYearTag = "";
        }
    }
}