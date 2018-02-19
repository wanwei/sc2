using com.wer.sc.data;
using com.wer.sc.data.utils;
using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace com.wer.sc.plugin.cnstock.historydata.download.sina
{
    public class Download_Sina
    {
        private string path;

        private DownloadPathUtils pathutils;

        public Download_Sina(string path)
        {
            this.path = path;
            this.pathutils = new DownloadPathUtils(path);
        }

        public void Download(List<string> codes)
        {
            DownloadDates();
            for (int i = 0; i < codes.Count; i++)
            {
                Download(codes[i]);
            }
        }

        public List<int> GetTradingDays()
        {
            string tradingdaypath = pathutils.GetPath_Tradingday();
            return CsvUtils_TradingDay.Load(tradingdaypath);
        }

        public void DownloadDates()
        {
            string tradingdaypath = pathutils.GetPath_Tradingday();
            int tradingDay = -1;
            List<int> tradingDays = null;
            if (File.Exists(tradingdaypath))
            {
                tradingDays = CsvUtils_TradingDay.Load(tradingdaypath);
                tradingDay = tradingDays[tradingDays.Count - 1];
            }

            List<string[]> arr = Download_DayKLine.RequestIndex("sh000001", tradingDay);
            if (tradingDays == null)
                tradingDays = new List<int>(arr.Count);
            for (int i = 0; i < arr.Count; i++)
            {
                try
                {
                    tradingDays.Add(int.Parse(arr[i][0]));
                }
                catch (Exception e)
                {
                    Console.WriteLine(arr[i]);
                    Console.WriteLine(e.Message);
                }
            }
            CsvUtils_TradingDay.Save(tradingdaypath, tradingDays);
        }

        public void Download(string code)
        {
            IKLineData klineData = DownloadKLine(code);
            HashSet<int> set = GetTickDays(code);
            for (int i = 0; i < klineData.Length; i++)
            {
                int date = (int)klineData.Arr_Time[i];
                if (date < 20070101)
                    continue;
                if (set.Contains(date))
                    continue;

                List<string[]> arr = Download_Tick.Request(code, date);
                string path = pathutils.GetTickPath(code, date);
                SaveData(path, arr);
                Thread.Sleep(2000);
            }
        }

        public HashSet<int> GetTickDays(string code)
        {
            string path = pathutils.GetTickPath(code);
            FileUtils.EnsureDirExist(path);
            string[] files = Directory.GetFiles(path, "*.csv");
            HashSet<int> set = new HashSet<int>();
            for (int i = 0; i < files.Length; i++)
            {
                string file = files[i];
                int startDateIndex = file.IndexOf('_') + 1;
                int date = int.Parse(file.Substring(startDateIndex, 8));
                set.Add(date);
            }
            return set;
        }

        public List<int> GetTickDayList(string code)
        {
            string path = pathutils.GetTickPath(code);
            FileUtils.EnsureDirExist(path);
            string[] files = Directory.GetFiles(path, "*.csv");
            List<int> list = new List<int>();
            for (int i = 0; i < files.Length; i++)
            {
                string file = files[i];
                int startDateIndex = file.IndexOf('_') + 1;
                int date = int.Parse(file.Substring(startDateIndex, 8));
                list.Add(date);
            }
            return list;
        }

        public List<string[]> GetTickData(string code, int date)
        {
            string path = pathutils.GetTickPath(code, date);
            string[] lines = File.ReadAllLines(path);
            List<string[]> tickData = new List<string[]>();
            for (int i = 0; i < lines.Length; i++)
                tickData.Add(lines[i].Split(','));
            return tickData;
        }

        private IKLineData DownloadKLine(string code)
        {
            string path = pathutils.GetKLinePath(code);
            if (File.Exists(path))
            {
                IKLineData klineData = CsvUtils_KLineData.Load(path);
                int lastUpdatedDate = (int)klineData.Arr_Time[klineData.Length - 1];
                AppendKLineData(code, path, lastUpdatedDate);
            }
            else
                AppendKLineData(code, path, -1);
            return CsvUtils_KLineData.Load(path);
        }

        private static void AppendKLineData(string code, string path, int lastUpdatedDate)
        {
            List<string[]> arr = Download_DayKLine.RequestCode(code, lastUpdatedDate);
            SaveData(path, arr);
        }

        public static void SaveData(string path, List<string[]> arr)
        {
            List<string> contents = new List<string>(arr.Count);
            for (int i = 0; i < arr.Count; i++)
            {
                contents.Add(ListUtils.ToString(arr[i]));
            }
            FileUtils.EnsureParentDirExist(path);
            //FileUtils.EnsureDirExist(path);
            File.AppendAllLines(path, contents);
        }
    }
}
