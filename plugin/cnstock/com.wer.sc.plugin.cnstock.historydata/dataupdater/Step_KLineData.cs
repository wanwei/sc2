using com.wer.sc.data;
using com.wer.sc.data.transfer;
using com.wer.sc.data.utils;
using com.wer.sc.utils.update;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin.cnstock.historydata.dataupdater
{
    public class Step_KLineData : IStep
    {
        private string code;

        public Step_KLineData(string code)
        {
            this.code = code;
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
                return "更新K线数据";
            }
        }

        public string Proceed()
        {
            string klinePath = KLinePath();
            ISet<int> days = updatedKLine();
            Step_TickData_Code step = new Step_TickData_Code(code);
            List<int> tickDays = step.GetTickDayList(code);
            for (int i = 0; i < tickDays.Count; i++)
            {
                int tickDay = tickDays[i];
                if (days.Contains(tickDay))
                    continue;
                Proceed(tickDay);
            }
            return "";
        }

        private void Proceed(int date)
        {
            ITickData tickData = CsvUtils_TickData.Load(Step_TickData_Code.GetTickPath(code, date));
            //tick数据没有，则不生成对应K线数据
            if (tickData == null)
            {
                return;
            }

            IList<double[]> tradingPeriod = Step_TradingTime.GetTradingTime(date).TradingPeriods;
            IKLineData klineData = DataTransfer_Tick2KLine.Transfer(tickData, tradingPeriod, KLinePeriod.KLinePeriod_1Minute, 0, 0);

            string path = DataConst.CSVPATH + code + "\\kline\\1MINUTE\\" + code + "_1MINUTE_" + date + ".csv";
            CsvUtils_KLineData.Save(path, klineData);
        }

        private ISet<int> updatedKLine()
        {
            string tickPath = KLinePath();
            if (!Directory.Exists(tickPath))
                return new HashSet<int>();
            string[] files = Directory.GetFiles(tickPath, "*.csv");
            HashSet<int> set = new HashSet<int>();
            for (int i = 0; i < files.Length; i++)
            {
                string file = files[i];
                int startDateIndex = file.LastIndexOf('_') + 1;
                int date = int.Parse(file.Substring(startDateIndex, 8));
                set.Add(date);
            }
            return set;
        }

        private string KLinePath()
        {
            string path = DataConst.CSVPATH + code + "\\kline\\1MINUTE\\";
            return path;
        }
    }
}
