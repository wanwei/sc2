using com.wer.sc.data;
using com.wer.sc.data.reader.cache;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin.cnfutures.historydata.dataupdater
{
    public class UpdatedDataLoader
    {
        private Plugin_HistoryData_CnFutures plugin_HistoryData;

        private TradingDayCache tradingDayCache;

        private CodeInfoCache codeInfoCache;

        public UpdatedDataLoader() : this(null)
        {
        }

        public UpdatedDataLoader(string csvDataPath)
        {
            this.plugin_HistoryData = new Plugin_HistoryData_CnFutures(null, csvDataPath);
            this.tradingDayCache = new TradingDayCache(plugin_HistoryData.GetTradingDays());
            this.codeInfoCache = new CodeInfoCache(plugin_HistoryData.GetInstruments());
        }

        public string GetUpdatedDataPath()
        {
            return plugin_HistoryData.GetCsvDataPath();
        }

        public TradingDayCache GetTradingDayCache()
        {
            return tradingDayCache;
        }

        /// <summary>
        /// 得到已更新的合约缓存
        /// </summary>
        /// <returns></returns>
        public CodeInfoCache GetCodeCache()
        {
            return codeInfoCache;
        }

        public List<TradingSession> GetTradingSessions(String code)
        {
            return plugin_HistoryData.GetTradingSessions(code);
        }

        /// <summary>
        /// 得到一个合约或品种更新了K线的交易日
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public List<int> GetKLineDataTradingDays(string code)
        {
            string path = plugin_HistoryData.GetCsvDataPath() + "\\" + code + "\\kline\\1minute\\";
            return GetTradingDaysByPath(path);
        }

        public List<int> GetTickDataTradingDays(string code)
        {
            string path = plugin_HistoryData.GetCsvDataPath() + "\\" + code + "\\tick\\";
            return GetTradingDaysByPath(path);
        }

        private static List<int> GetTradingDaysByPath(string path)
        {
            DirectoryInfo dir = new DirectoryInfo(path);
            if (!dir.Exists)
                return new List<int>();
            FileInfo[] files = dir.GetFiles();
            List<int> days = new List<int>(files.Length);
            for (int i = 0; i < files.Length; i++)
            {
                FileInfo f = files[i];
                int index = f.Name.LastIndexOf('.');
                try
                {
                    int day = int.Parse(f.Name.Substring(index - 8, 8));
                    days.Add(day);
                }
                catch (Exception e)
                {
                    continue;
                }
            }
            return days;
        }

        public ITickData GetTickData(String code, int date)
        {
            return plugin_HistoryData.GetTickData(code, date);
        }

        public IKLineData GetKLineData(String code, int date)
        {
            return plugin_HistoryData.GetKLineData(code, date, KLinePeriod.KLinePeriod_1Minute);
        }
    }
}