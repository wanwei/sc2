using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.utils.update;
using com.wer.sc.data;
using com.wer.sc.data.reader;
using com.wer.sc.data.utils;
using com.wer.sc.plugin.cnstock.historydata.download.sina;

namespace com.wer.sc.plugin.cnstock.historydata.dataupdater
{
    public class Step_TradingTime : IStep
    {
        private string code;

        private bool forceUpdate;

        private Download_Sina downloader;

        public Step_TradingTime(Download_Sina download, string code)
        {
            this.code = code;
            this.downloader = download;
        }

        public int ProgressStep
        {
            get
            {
                return 10;
            }
        }

        public string StepDesc
        {
            get
            {
                return "更新" + code + "的所有交易时间";
            }
        }

        public string Proceed()
        {
            string path = DataConst.CSVPATH + code + "\\" + code + @"_tradingtime.csv";
            //A0000_tradingtime.csv
            List<int> days = this.downloader.GetTickDayList(code);
            List<ITradingTime> tradingTimes = new List<ITradingTime>(days.Count);
            for (int i = 0; i < days.Count; i++)
            {
                int day = days[i];
                TradingTime tt = GetTradingTime(day);
                tradingTimes.Add(tt);
            }
            CsvUtils_TradingTime.Save(path, tradingTimes);
            return "更新完成" + code + "的交易时间";
        }

        public static TradingTime GetTradingTime(int day)
        {
            List<double[]> tradingPeriods = new List<double[]>();
            tradingPeriods.Add(new double[] { day + 0.093, day + 0.113 });
            tradingPeriods.Add(new double[] { day + 0.13, day + 0.15 });
            TradingTime tt = new TradingTime(day, tradingPeriods);
            return tt;
        }
    }
}
