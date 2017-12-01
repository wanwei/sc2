using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.forward.utils
{
    public class ForwardDataIndeier
    {
        private bool isTickForward;

        private DataForForward_Code dataForForward;

        private KLineToTickIndeier klineToTickIndeier;

        private KLineToKLineIndeier klineToKlineIndeier;

        private TimeLineToKLineIndeier timeToKLineIndeier;

        public ForwardDataIndeier(DataForForward_Code dataForForward)
        {
            this.dataForForward = dataForForward;
            if (this.dataForForward.ReferedKLinePeriods.Count > 1)
            {
                List<IKLineData_Extend> indexKLines = new List<IKLineData_Extend>();
                for (int i = 0; i < this.dataForForward.ReferedKLinePeriods.Count; i++)
                {
                    KLinePeriod klinePeriod = this.dataForForward.ReferedKLinePeriods[i];
                    if (this.dataForForward.MainKLinePeriod == klinePeriod)
                        continue;
                    indexKLines.Add(this.dataForForward.GetKLineData(klinePeriod));
                }
                this.klineToKlineIndeier = new KLineToKLineIndeier(dataForForward.MainKLine, indexKLines);
            }
            if (this.dataForForward.UseTickData)
            {
                int tradingDay = dataForForward.TradingDay;
                ITickData_Extend tickData = this.dataForForward.CurrentTickData;
                this.klineToTickIndeier = new KLineToTickIndeier(tickData, dataForForward.MainKLine);
            }
            if (this.dataForForward.UseTimeLineData)
            {
                int tradingDay = dataForForward.TradingDay;
                ITimeLineData_Extend timeLineData = this.dataForForward.CurrentTimeLineData;
                this.timeToKLineIndeier = new TimeLineToKLineIndeier(dataForForward.MainKLine, timeLineData);
            }
        }

        public void ChangeTradingDay(int tradingDay)
        {
            if (this.dataForForward.TradingDay != tradingDay)
                this.dataForForward.TradingDay = tradingDay;
            if (this.klineToTickIndeier != null)
                this.klineToTickIndeier.ChangeTradingDay(this.dataForForward.CurrentTickData);
            if (this.timeToKLineIndeier != null)
                this.timeToKLineIndeier.ChangeTradingDay(this.dataForForward.CurrentTimeLineData);
        }

        public int GetMainKLineBarPosIfFinished(int tickBarPos, out int lastMainKLineBarPos)
        {
            return this.klineToTickIndeier.GetKLineBarPosIfFinished(tickBarPos, out lastMainKLineBarPos);
        }

        public int GetOtherKLineBarPosIfFinished(int mainBarPos, KLinePeriod klinePeriod)
        {
            if (this.klineToKlineIndeier == null)
                return -1;
            return this.klineToKlineIndeier.GetOtherKLineBarPosIfFinished(mainBarPos, klinePeriod);
        }

        public int GetTimeLineBarPosIfFinished(int tickBarPos, out int lastTimeLineBarPos)
        {
            if (this.dataForForward.MainKLinePeriod.Equals(KLinePeriod.KLinePeriod_1Minute))
            {
                int lastMainBarPos;
                int mainBarPos = GetMainKLineBarPosIfFinished(tickBarPos, out lastMainBarPos);
                if (mainBarPos < 0)
                {
                    lastTimeLineBarPos = -1;
                    return -1;
                }
                int timeLineBarPos = this.timeToKLineIndeier.GetTimeLineBarPosIfFinished(mainBarPos);
                if (lastMainBarPos < 0)
                    lastTimeLineBarPos = -1;
                else
                    lastTimeLineBarPos = timeLineBarPos + lastMainBarPos - mainBarPos;
                return timeLineBarPos;
            }
            throw new ApplicationException("要支持分时线必须要以1分钟线作为主周期");
        }

        public int GetTimeLineBarPosIfFinished(int mainBarPos)
        {
            if (this.dataForForward.MainKLinePeriod.Equals(KLinePeriod.KLinePeriod_1Minute))
            {        
                return this.timeToKLineIndeier.GetTimeLineBarPosIfFinished(mainBarPos);
            }
            throw new ApplicationException("要支持分时线必须要以1分钟线作为主周期");
        }
    }
}