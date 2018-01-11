using com.wer.sc.data.realtime;
using com.wer.sc.data.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.datapackage
{
    public class RealTimeDataPackageTimeChangeUtils
    {
        public static void ChangeTime_TickData(ITickData_Extend tickData, double time)
        {
            if (tickData.Time == time)
                return;
            int index = TimeIndeierUtils.IndexOfTime_Tick(tickData, time, true);
            tickData.BarPos = index < 0 ? 0 : index;
        }

        public static void ChangeTime_KLineData(IKLineData_RealTime klineData_RealTime, int date, double time, ITickData_Extend tickData)
        {
            KLinePeriod klinePeriod = klineData_RealTime.Period;
            int klineIndex = IndexOfTime(klineData_RealTime, klinePeriod, time, date);
            //if(tickData.TradingDay !=date)
            //    tickData = 
            int tickIndex = TimeIndeierUtils.IndexOfTime_Tick(tickData, time, true);
            if (IsPeriodEnd(klineData_RealTime, klineIndex, tickData, tickIndex))
            {
                klineData_RealTime.BarPos = klineIndex;
                klineData_RealTime.ResetCurrentBar();
                return;
            }
            int startTickIndex = GetStartTickIndex(klineData_RealTime, tickData, klinePeriod, klineIndex);
            KLineBar klineBar = GetKLineBar(tickData, startTickIndex, tickIndex);
            klineData_RealTime.ChangeCurrentBar(klineBar, klineIndex);
        }

        private static int GetStartTickIndex(IKLineData_RealTime klineData_RealTime, ITickData_Extend tickData, KLinePeriod klinePeriod, int klineIndex)
        {
            int startTickIndex;
            if (klinePeriod.PeriodType == KLineTimeType.DAY)
                startTickIndex = 0;
            else
            {
                double klineTime = klineData_RealTime.BarPos == klineIndex ? klineData_RealTime.GetCurrentBar_Original().Time : klineData_RealTime.Arr_Time[klineIndex];
                startTickIndex = TimeIndeierUtils.IndexOfTime_Tick(tickData, klineTime, true);
                if (klineData_RealTime.IsTradingTimeStart(klineIndex))
                {
                    while (!tickData.IsTradingTimeStart(startTickIndex))
                    {
                        startTickIndex--;
                    }
                }
            }

            return startTickIndex;
        }

        private static bool IsPeriodEnd(IKLineData_RealTime klineData_RealTime, int klineIndex, ITickData_Extend tickData, int tickIndex)
        {
            if (tickIndex == tickData.Length - 1)
                return true;
            double tickTime = tickData.Time;
            double nextTickTime = tickData.Arr_Time[tickIndex + 1];

            if (klineIndex >= klineData_RealTime.Length - 1)
                return false;

            double klineTime = klineData_RealTime.Arr_Time[klineIndex + 1];
            return tickTime < klineTime && nextTickTime >= klineTime;
        }

        private static int IndexOfTime(IKLineData_RealTime klineData, KLinePeriod klinePeriod, double time, int date)
        {
            if (klinePeriod.PeriodType == KLineTimeType.DAY)
            {
                return TimeIndeierUtils.IndexOfTime_KLine(klineData.GetKLineData_Original(), date);
            }
            else
            {
                int index = TimeIndeierUtils.IndexOfTime_KLine(klineData, time);
                if (klineData.IsTradingTimeEnd(index))
                {
                    double endTime = klineData.GetEndTime(index);
                    if (index >= klineData.Length - 1)
                        return index;
                    double nextStartTime = klineData.Arr_Time[index + 1];
                    double middleTime = (endTime + nextStartTime) / 2;
                    if (time < middleTime)
                        return index;
                    return index + 1;
                }
                return index;
            }
        }

        private static KLineBar GetKLineBar(ITickData tickData, int startIndex, int endIndex)
        {
            KLineBar klineBar = KLineUtils.GetKLineBar(tickData.GetBar(startIndex));
            for (int i = startIndex + 1; i <= endIndex; i++)
            {
                klineBar = KLineUtils.GetKLineBar(klineBar, tickData.GetBar(i));
            }
            return klineBar;
        }

        public static void ChangeTime_TimeLineData(ITimeLineData_RealTime timeLineData, double time, ITickData_Extend tickData)
        {
            //if (timeLineData.Time == time)
            //    return;
            int timeLineIndex = GetTimeLineIndex(timeLineData, time);
            int tickIndex = TimeIndeierUtils.IndexOfTime_Tick(tickData, time);
            double klineTime = timeLineData.Arr_Time[timeLineIndex];
            int startTickIndex = GetStartTickIndex(timeLineData, tickData, timeLineIndex);

            TimeLineBar klineBar = GetTimeLineBar(tickData, startTickIndex, tickIndex, timeLineData.YesterdayEnd);
            timeLineData.ChangeCurrentBar(klineBar, timeLineIndex);
        }

        private static int GetTimeLineIndex(ITimeLineData_RealTime timeLineData, double time)
        {
            if (time < timeLineData.Arr_Time[0])
                return 0;
            return TimeIndeierUtils.IndexOfTime_TimeLine(timeLineData, time);
        }

        private static int GetStartTickIndex(ITimeLineData_RealTime timeLineData, ITickData_Extend tickData, int timeLineIndex)
        {
            double klineTime = timeLineData.BarPos == timeLineIndex ? timeLineData.GetCurrentBar_Original().Time : timeLineData.Arr_Time[timeLineIndex];
            int startTickIndex;
            startTickIndex = TimeIndeierUtils.IndexOfTime_Tick(tickData, klineTime, true);
            if (timeLineData.IsTradingTimeStart(timeLineIndex))
            {
                while (!tickData.IsTradingTimeStart(startTickIndex))
                {
                    startTickIndex--;
                }
            }
            return startTickIndex;
        }

        private static TimeLineBar GetTimeLineBar(ITickData tickData, int startIndex, int endIndex, float lastEndPrice)
        {
            TimeLineBar timeLineBar = TimeLineUtils.GetTimeLineBar(tickData.GetBar(startIndex), lastEndPrice);
            for (int i = startIndex + 1; i <= endIndex; i++)
            {
                timeLineBar = TimeLineUtils.GetTimeLineBar(timeLineBar, tickData.GetBar(i), lastEndPrice);
            }
            return timeLineBar;
        }
    }
}
