using com.wer.sc.data.store;
using com.wer.sc.data.store.file;
using com.wer.sc.data.utils;
using com.wer.sc.plugin;
using com.wer.sc.utils.update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.update
{
    public class Step_UpdateKLineData : IStep
    {
        private string code;

        private int startDate;

        private int endDate;

        private KLinePeriod period;

        private IPlugin_HistoryData historyData;

        private IKLineDataStore klineDataStore;

        public Step_UpdateKLineData(string code, int startDate, int endDate, KLinePeriod period, IPlugin_HistoryData historyData, IKLineDataStore klineDataStore)
        {
            this.code = code;
            this.startDate = startDate;
            this.endDate = endDate;
            this.period = period;
            this.historyData = historyData;
            this.klineDataStore = klineDataStore;
        }

        public int ProgressStep
        {
            get
            {
                if (period.PeriodType == KLineTimeType.DAY)
                    return 3;

                return 3 * TimeUtils.Substract(endDate, startDate).Days;
            }
        }

        public string StepDesc
        {
            get
            {
                return "更新" + code + "的" + startDate + "-" + endDate + "的" + period + "K线数据";
            }
        }

        public override string ToString()
        {
            return StepDesc;
        }

        public string Proceed()
        {
            //Console.WriteLine(code + "," + startDate + "," + endDate + period);
            IKLineData klineData = historyData.GetKLineData(code, startDate, endDate, period);
            klineDataStore.Append(code, period, klineData);
            return StepDesc + "完毕";
        }
    }
}