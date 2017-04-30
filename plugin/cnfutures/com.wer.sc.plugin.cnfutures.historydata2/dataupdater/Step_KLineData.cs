using com.wer.sc.data;
using com.wer.sc.data.reader;
using com.wer.sc.plugin.cnfutures.historydata.dataloader;
using com.wer.sc.utils.update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin.cnfutures.historydata.dataupdater
{
    public class Step_KLineData : IStep
    {
        private string code;

        private List<int> dates;

        private IDataLoader dataLoader;

        public Step_KLineData(string code, List<int> dates, IDataLoader dataLoader)
        {
            this.code = code;
            this.dates = dates;
            this.dataLoader = dataLoader;
        }

        public int ProgressStep
        {
            get
            {
                return dates.Count * 5;
            }
        }

        public string StepDesc
        {
            get
            {
                return "更新" + GetDesc();
            }
        }
        private string GetDesc()
        {
            return code + "的K线数据：" + dates[0] + "-" + dates[dates.Count - 1];
        }

        public string Proceed()
        {
            ITradingDayReader openDateReader = this.dataLoader.LoadTradingDayReader();
            KLineDataLastEndInfo lastEndInfo;// = GetLastEndInfo(dates[0]);
            IKLineData lastKLineData = null;

            for (int i = 0; i < dates.Count; i++)
            {
                int date = dates[i];
                if (i == 0)
                {
                    lastEndInfo = GetLastEndInfo(date);
                }
                else
                {
                    if (openDateReader.GetTradingDayIndex(date) - openDateReader.GetTradingDayIndex(dates[i - 1]) == 1)
                    {
                        lastEndInfo.lastEndPrice = lastKLineData.Arr_End[lastKLineData.Length - 1];
                        lastEndInfo.lastEndHold = lastKLineData.Arr_Hold[lastKLineData.Length - 1];
                    }
                    else
                    {
                        lastEndInfo = GetLastEndInfo(date);
                    }
                }
                Step_KLineData_OneDay step_klineData = new Step_KLineData_OneDay(code, date, KLinePeriod.KLinePeriod_1Minute, dataLoader, lastEndInfo.lastEndPrice, lastEndInfo.lastEndHold);
                step_klineData.Proceed();
                lastKLineData = step_klineData.KlineData;
            }
            return "更新完毕" + GetDesc();
        }

        private KLineDataLastEndInfo GetLastEndInfo(int date)
        {
            int prevDate = this.dataLoader.LoadTradingDayReader().GetPrevTradingDay(date);
            IKLineData lastKLineData = this.dataLoader.LoadUpdatedKLineData(code, prevDate, KLinePeriod.KLinePeriod_1Minute);
            float lastEndPrice = lastKLineData != null ? lastKLineData.Arr_End[lastKLineData.Length - 1] : -1;
            int lastEndHold = lastKLineData != null ? lastKLineData.Arr_Hold[lastKLineData.Length - 1] : -1;
            return new KLineDataLastEndInfo(lastEndPrice, lastEndHold);
        }

        public override string ToString()
        {
            return StepDesc;
        }
    }

    struct KLineDataLastEndInfo
    {
        public float lastEndPrice;

        public int lastEndHold;

        public KLineDataLastEndInfo(float lastEndPrice, int lastEndHold)
        {
            this.lastEndPrice = lastEndPrice;
            this.lastEndHold = lastEndHold;
        }
    }
}