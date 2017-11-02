using com.wer.sc.data.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.forward
{
    public class DataForwardUtils_Tick
    {
        //public static void ForwardToday_KLineData(IKLineData_RealTime klineData, KLinePeriod period, ITickData tickData, int currentTickIndex, int nextTickIndex)
        //{
        //    ITickData_Extend currentTickData = forwardData.CurrentTickData;
        //    ITickBar nextTickBar = currentTickData.GetBar(currentTickData.BarPos + 1);
        //    //日线，肯定不会跳到下一个bar
        //    if (period.Equals(KLinePeriod.KLinePeriod_1Day))
        //    {
        //        dic_KLinePeriod_IsEnd[period] = false;
        //        klineData.ChangeCurrentBar(GetKLineBar(klineData, nextTickBar));
        //        return;
        //    }
        //    double nextTickTime = nextTickBar.Time;
        //    int nextKLineIndex = FindNextKLineIndex(klineData, nextTickTime);
        //    if (nextKLineIndex == klineData.BarPos)
        //    {
        //        dic_KLinePeriod_IsEnd[period] = false;
        //        klineData.ChangeCurrentBar(GetKLineBar(klineData, nextTickBar));
        //    }
        //    else
        //    {
        //        dic_KLinePeriod_IsEnd[period] = true;
        //        klineData.ChangeCurrentBar(GetKLineBar(nextTickBar), nextKLineIndex);
        //    }

        //}

        public static void ForwardToday_TimeLineData(ITimeLineData_RealTime timeLineData, ITickData tickData, int currentTickIndex, int nextTickIndex)
        {

        }

        public static void ForwardToday_TickData(ITickData klineData)
        {

        }

        public static void ForwardNextDay(DataForForward_Code forwardData, int tradingDay)
        {
            forwardData.TradingDay = tradingDay;
            foreach (KLinePeriod period in forwardData.ReferedKLinePeriods)
            {
                IKLineData_RealTime klineData = forwardData.GetKLineData(period);
                ForwardNextDay_KLine(forwardData, klineData, period);
            }
            ForwardNextDay_TimeLine(forwardData);
        }

        private static void ForwardNextDay_KLine(DataForForward_Code forwardData, IKLineData_RealTime klineData, KLinePeriod period)
        {
            ITickBar tickBar = forwardData.CurrentTickData.GetCurrentBar();
            int nextbarPos = klineData.BarPos + 1;
            //TODO 这样nextday算法还是不够准确
            while (!klineData.IsDayStart(nextbarPos))
            {
                nextbarPos = klineData.BarPos + 1;
            }
            klineData.ChangeCurrentBar(GetKLineBar(tickBar), nextbarPos);
        }

        private static void ForwardNextDay_TimeLine(DataForForward_Code forwardData)
        {
            if (!forwardData.UseTimeLineData)
                return;

            ITimeLineBar timeLineBar = GetTimeLineBar(forwardData.CurrentTickData, forwardData.CurrentTimeLineData.YesterdayEnd);
            forwardData.CurrentTimeLineData.ChangeCurrentBar(timeLineBar);
        }

        private static KLineBar GetKLineBar(ITickBar tickBar)
        {
            return KLineUtils.GetKLineBar(tickBar);
        }

        private static KLineBar GetKLineBar(IKLineBar klineBar, ITickBar tickBar)
        {
            return KLineUtils.GetKLineBar(klineBar, tickBar);
        }

        private static TimeLineBar GetTimeLineBar(ITickBar tickBar, float lastEndPrice)
        {
            return TimeLineUtils.GetTimeLineBar(tickBar, lastEndPrice);
        }

        private static TimeLineBar GetTimeLineBar(ITimeLineBar klineBar, ITickBar tickBar, float lastEndPricce)
        {
            return TimeLineUtils.GetTimeLineBar(klineBar, tickBar, lastEndPricce);
        }
    }
}
