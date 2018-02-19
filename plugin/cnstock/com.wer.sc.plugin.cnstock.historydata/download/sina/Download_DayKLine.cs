using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace com.wer.sc.plugin.cnstock.historydata.download.sina
{
    public class Download_DayKLine
    {
        public static List<string[]> RequestCode(string code, int lastUpdatedDate)
        {
            return RequestData(code, false, lastUpdatedDate);
        }

        public static List<string[]> RequestIndex(string code,int lastUpdatedDate)
        {
            return RequestData(code, true, lastUpdatedDate);
        }

        /// <summary>
        /// 数据格式：
        /// 
        //20180205,3411.670,3487.721,3487.497,3406.241,21767375400,252198769276
        //20180206,3418.010,3440.124,3370.652,3364.216,28055547700,318818541762
        //20180207,3412.744,3425.538,3309.260,3304.007,26093971400,295264429885
        //20180208,3281.046,3307.162,3262.050,3225.712,20126249900,222348685287
        //20180209,3172.851,3180.110,3129.851,3062.743,25638917600,272095086839
        /// </summary>
        /// <param name="code"></param>
        /// <param name="lastUpdatedDate"></param>
        /// <returns></returns>
        public static List<string[]> RequestData(string code, bool isIndex, int lastUpdatedDate)
        {
            JiduData jiduData = RequestData_Jidu(code, isIndex);
            List<string[]> klineData = new List<string[]>();

            bool continueReq = AddKLineData(klineData, jiduData.KLineData, lastUpdatedDate);
            if (!continueReq)
                return klineData;

            if (jiduData.Jidu > 1)
            {
                int firstyear = jiduData.years[0];
                continueReq = RequestData(klineData, code, isIndex, firstyear, jiduData.Jidu - 1, lastUpdatedDate);
                if (!continueReq)
                    return klineData;
            }
            for (int i = 1; i < jiduData.years.Count; i++)
            {
                int year = jiduData.years[i];
                if (year < 2007)
                    break;
                continueReq = RequestData(klineData, code, isIndex, year, 4, lastUpdatedDate);
                if (!continueReq)
                    break;
            }
            return klineData;
        }

        //private static List<string[]> RequestData(string code, bool isIndex, int lastUpdatedDate)
        //{
        //    JiduData jiduData = RequestData_Jidu(code, isIndex);
        //    List<string[]> klineData = new List<string[]>();

        //    bool continueReq = AddKLineData(klineData, jiduData.KLineData, lastUpdatedDate);
        //    if (!continueReq)
        //        return klineData;

        //    if (jiduData.Jidu > 1)
        //    {
        //        int firstyear = jiduData.years[0];
        //        continueReq = RequestData(klineData, code, isIndex, firstyear, jiduData.Jidu - 1, lastUpdatedDate);
        //        if (!continueReq)
        //            return klineData;
        //    }
        //    for (int i = 1; i < jiduData.years.Count; i++)
        //    {
        //        int year = jiduData.years[i];
        //        continueReq = RequestData(klineData, code, isIndex, year, 4, lastUpdatedDate);
        //        if (!continueReq)
        //            break;
        //    }
        //    return klineData;
        //}

        //private static List<string[]> RequestData(string code, int lastUpdatedDate, JiduData jiduData)
        //{
        //    List<string[]> klineData = new List<string[]>();

        //    bool continueReq = AddKLineData(klineData, jiduData.KLineData, lastUpdatedDate);
        //    if (!continueReq)
        //        return klineData;

        //    if (jiduData.Jidu > 1)
        //    {
        //        int firstyear = jiduData.years[0];
        //        continueReq = RequestIndex(klineData, code, firstyear, jiduData.Jidu - 1, lastUpdatedDate);
        //        if (!continueReq)
        //            return klineData;
        //    }
        //    for (int i = 1; i < jiduData.years.Count; i++)
        //    {
        //        int year = jiduData.years[i];
        //        continueReq = RequestIndex(klineData, code, year, 4, lastUpdatedDate);
        //        if (!continueReq)
        //            break;
        //    }
        //    return klineData;
        //}

        public static bool RequestData(List<string[]> klineData, string code, bool isIndex, int year, int startJidu, int lastUpdatedDate)
        {
            for (int i = startJidu; i > 0; i--)
            {
                Thread.Sleep(2000);
                JiduData jiduData = RequestData_Jidu(code, isIndex, year, i);
                if (jiduData == null)
                    continue;
                List<string[]> arr = jiduData.KLineData;
                bool isContinue = AddKLineData(klineData, arr, lastUpdatedDate);
                if (!isContinue)
                    return false;
            }
            return true;
        }

        private static bool AddKLineData(List<string[]> klineData, List<string[]> arr, int lastUpdatedDate)
        {
            int insertStartIndex = GetLastUpdatedIndex(arr, lastUpdatedDate);
            if (insertStartIndex < 0)
                klineData.InsertRange(0, arr);
            else
            {
                int len = arr.Count - insertStartIndex;
                if (len > 0)
                    klineData.InsertRange(0, arr.GetRange(insertStartIndex, len));
                return false;
            }
            return true;
        }

        private static int GetLastUpdatedIndex(List<string[]> arr, int lastUpdatedDate)
        {
            if (lastUpdatedDate < 0)
                return -1;
            int startDate = int.Parse(arr[0][0]);
            if (startDate > lastUpdatedDate)
                return -1;
            for (int i = 0; i < arr.Count; i++)
            {
                int date = int.Parse(arr[i][0]);
                if (date > lastUpdatedDate)
                    return i;
            }
            return arr.Count;
        }        

        public static JiduData RequestData_Jidu(string code, bool isIndex)
        {
            string url;
            if (isIndex)
                url = "http://vip.stock.finance.sina.com.cn/corp/go.php/vMS_MarketHistory/stockid/" + code.Substring(2, 6) + "/type/S.phtml";
            else
                url = "http://vip.stock.finance.sina.com.cn/corp/go.php/vMS_MarketHistory/stockid/" + code.Substring(2, 6) + ".phtml";
            return RequestJiduData(url);
        }

        public static JiduData RequestData_Jidu(string code, bool isIndex, int year, int jidu)
        {
            string url;
            if (isIndex)
                url = "http://vip.stock.finance.sina.com.cn/corp/go.php/vMS_MarketHistory/stockid/" + code.Substring(2, 6) + "/type/S.phtml?year=" + year + "&jidu=" + jidu;
            else
                url = "http://vip.stock.finance.sina.com.cn/corp/go.php/vMS_MarketHistory/stockid/" + code.Substring(2, 6) + ".phtml?year=" + year + "&jidu=" + jidu;
            return RequestJiduData(url);
        }

        private static JiduData RequestJiduData(string url)
        {
            JiduData data = new JiduData();
            string content = HttpUtils.HttpGetUrl(url, "gbk");
            HttpComp_Select compSelect = HttpParser.ParseSelectByName(content, "year");
            for (int i = 0; i < compSelect.Values.Count; i++)
            {
                data.years.Add(int.Parse(compSelect.Values[i]));
            }

            HttpComp_Table compTable = HttpParser.ParseTableById(content, "FundHoldSharesTable");
            if (compTable != null)
            {
                for (int i = compTable.Rows.Count - 1; i > 1; i--)
                {
                    List<string> row = compTable.Rows[i];
                    string[] arr = parseKLineDataRow(row);
                    if (arr == null)
                        continue;
                    data.KLineData.Add(arr);
                }
            }
            return data;
        }

        private static string[] parseKLineDataRow(List<string> row)
        {
            if (row.Count < 7)
                return null;
            string[] arr = new string[row.Count + 1];
            arr[0] = ParseDate(row[0]);
            arr[1] = ParseValue(row[1]);
            arr[2] = ParseValue(row[2]);
            arr[3] = ParseValue(row[3]);
            arr[4] = ParseValue(row[4]);
            arr[5] = ParseValue(row[5]);
            arr[6] = ParseValue(row[6]);
            arr[7] = "0";
            return arr;
        }

        private static string ParseDate(string td_Date)
        {
            int start = td_Date.IndexOf("date=") + "date=".Length;
            string dateStr = td_Date.Substring(start, 10);
            return dateStr.Substring(0, 4) + dateStr.Substring(5, 2) + dateStr.Substring(8, 2);
        }

        private static string ParseValue(string td_Value)
        {
            string tag = "center\">";
            int start = td_Value.IndexOf(tag) + tag.Length;
            int end = td_Value.IndexOf("<", start);
            return td_Value.Substring(start, end - start);
        }
    }

    public class JiduData
    {

        public List<string[]> KLineData = new List<string[]>();

        public List<int> years = new List<int>();

        public int Jidu
        {
            get
            {
                if (KLineData.Count == 0)
                    return -1;
                string date = KLineData[0][0];

                int dateInt = int.Parse(date);
                int day = dateInt % 10000;
                day = day / 100;
                if (day <= 3)
                    return 1;
                if (day <= 6)
                    return 2;
                if (day <= 9)
                    return 3;
                return 4;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Jidu).Append("\r\n");
            for (int i = 0; i < KLineData.Count; i++)
            {
                sb.Append(ListUtils.ToString(KLineData[i]));
                sb.Append("\r\n");
            }
            return sb.ToString();
        }
    }
}