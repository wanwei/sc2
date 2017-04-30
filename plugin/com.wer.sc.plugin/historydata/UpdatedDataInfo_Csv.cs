using com.wer.sc.data;
using com.wer.sc.plugin.historydata.utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin.historydata
{
    /// <summary>
    /// 历史数据信息装载
    /// 该类用来得到当前历史数据更新状况
    /// </summary>
    public class UpdatedInfo_Csv : IUpdatedDataInfo
    {
        private String csvDataPath;

        public UpdatedInfo_Csv(String csvDataPath)
        {
            this.csvDataPath = csvDataPath;
        }

        /// <summary>
        /// 得到单支股票或期货已更新的所有tick数据日期
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public List<int> GetOpenDates_TickData(string code)
        {
            return GetOpenDates(GetTickDataPath(code));
        }

        /// <summary>
        /// 得到最新的tick数据更新日期
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public int GetLastOpenDate_TickData(string code)
        {
            return GetLastOpenDate(GetTickDataPath(code));
        }

        private string GetTickDataPath(string code)
        {
            return csvDataPath + "\\" + code + "\\tick\\";
        }

        /// <summary>
        /// 得到单支股票或期货已更新的所有K线数据日期
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public List<int> GetOpenDates_KLineData(string code, KLinePeriod period)
        {
            return GetOpenDates(GetKLineDataPath(code, period));
        }

        /// <summary>
        /// 得到最新的K线数据更新日期
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public int GetLastOpenDate_KLineData(string code, KLinePeriod period)
        {
            return GetLastOpenDate(GetKLineDataPath(code, period));
        }

        private string GetKLineDataPath(string code, KLinePeriod period)
        {
            return csvDataPath + "\\" + code + "\\kline\\" + period.ToEngString() + "\\";
        }

        private static int GetLastOpenDate(string path)
        {
            if (!Directory.Exists(path))
                return -1;
            String[] files = Directory.GetFiles(path);
            int lastOpenDate = -1;
            foreach (String file in files)
            {
                int openDate;
                int index = file.LastIndexOf('_');
                bool isInt = int.TryParse(file.Substring(index + 1, 8), out openDate);
                if (isInt && openDate > lastOpenDate)
                {
                    lastOpenDate = openDate;
                }
            }
            return lastOpenDate;
        }

        /// <summary>
        /// 得到该目录下数据的所有已更新的日期
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private static List<int> GetOpenDates(string path)
        {
            if (!Directory.Exists(path))
                return new List<int>();
            String[] files = Directory.GetFiles(path);
            List<int> openDates = new List<int>();
            foreach (String file in files)
            {
                int openDate;
                int index = file.LastIndexOf('_');
                bool isInt = int.TryParse(file.Substring(index + 1, 8), out openDate);
                if (isInt)
                    openDates.Add(openDate);
            }
            return openDates;
        }
    }
}