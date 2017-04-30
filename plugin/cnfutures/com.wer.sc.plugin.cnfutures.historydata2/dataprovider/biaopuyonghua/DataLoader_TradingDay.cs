using com.wer.sc.data.reader;
using com.wer.sc.data.reader.cache;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin.cnfutures.historydata.dataloader.biaopuyonghua
{
    /// <summary>
    /// 交易日信息的原始数据装载
    /// </summary>
    public class DataLoader_TradingDay
    {
        private ITradingDayReader openDateReader;
        private List<int> tradingDays;
        private String srcDataPath;

        public DataLoader_TradingDay(string srcDataPath)
        {
            this.srcDataPath = srcDataPath;
        }

        public List<int> GetTradingDays()
        {
            initTradingDays();
            return this.tradingDays;
        }

        private void initTradingDays()
        {
            if (this.tradingDays != null)
                return;
            String path = srcDataPath + "\\DL";
            this.tradingDays = GetTradingDays(path);
            this.openDateReader = new TradingDayCache(tradingDays);
        }

        public ITradingDayReader GetTradingDayReader()
        {
            initTradingDays();
            return openDateReader;
        }

        public static List<int> GetTradingDays(string path)
        {
            if (!Directory.Exists(path))
                return new List<int>();
            String[] dirs = Directory.GetDirectories(path);
            List<int> tradingDays = new List<int>();
            foreach (String dir in dirs)
            {
                int openDate;
                int index = dir.LastIndexOf('\\');
                bool isInt = int.TryParse(dir.Substring(index + 1), out openDate);
                if (isInt)
                    tradingDays.Add(openDate);
            }
            return tradingDays;
        }
    }
}
