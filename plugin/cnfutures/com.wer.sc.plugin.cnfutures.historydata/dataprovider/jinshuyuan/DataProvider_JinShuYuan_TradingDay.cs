using com.wer.sc.data.reader;
using com.wer.sc.data.reader.cache;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin.cnfutures.historydata.dataprovider.jinshuyuan
{
    /// <summary>
    /// 交易日信息的原始数据装载
    /// </summary>
    public class DataProvider_JinShuYuan_TradingDay
    {
        private ITradingDayReader openDateReader;
        private List<int> tradingDays;
        private String srcDataPath;

        public DataProvider_JinShuYuan_TradingDay(string srcDataPath)
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

            DirectoryInfo srcPathDir = new DirectoryInfo(srcDataPath);
            DirectoryInfo[] dirs = srcPathDir.GetDirectories();
            this.tradingDays = new List<int>();
            for (int i = 0; i < dirs.Length; i++)
            {
                DirectoryInfo dir = dirs[i];
                int month = int.Parse(dir.Name);
                GetTradingDays(month, tradingDays);
            }

            //String path = srcDataPath + "\\DL";
            //this.tradingDays = GetTradingDays(path);
            this.openDateReader = new TradingDayCache(tradingDays);
        }

        private void GetTradingDays(int month, List<int> tradingDays)
        {
            string tickPath = srcDataPath + "\\" + month + "\\dc\\";

            string prefix = "a主力连续_*";
            DirectoryInfo dir = new DirectoryInfo(tickPath);
            FileInfo[] files = dir.GetFiles(prefix);
            for (int i = 0; i < files.Length; i++)
            {
                string fileName = files[i].Name;
                int dotIndex = fileName.IndexOf('.');
                int tradingDay = int.Parse(fileName.Substring(dotIndex - 8, 8));
                tradingDays.Add(tradingDay);
            }
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
