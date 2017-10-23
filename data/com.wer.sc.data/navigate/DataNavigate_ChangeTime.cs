using com.wer.sc.data.realtime;
using com.wer.sc.data.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.navigate
{
    public class DataNavigate_ChangeTime
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
            if (klineData_RealTime.Time == time)
                return;
            KLinePeriod klinePeriod = klineData_RealTime.Period;
            int klineIndex = IndexOfTime(klineData_RealTime, klinePeriod, time, date);

            int tickIndex = TimeIndeierUtils.IndexOfTime_Tick(tickData, time, true);
            if (IsPeriodEnd(klineData_RealTime, klineIndex, tickData, tickIndex))
            {
                klineData_RealTime.ResetCurrentBar();
                return;
            }
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
            KLineBar klineBar = GetKLineBar(tickData, startTickIndex, tickIndex);
            klineData_RealTime.ChangeCurrentBar(klineBar, klineIndex);
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
            if (timeLineData.Time == time)
                return;
            int timeLineIndex = TimeIndeierUtils.IndexOfTime_TimeLine(timeLineData, time);
            int tickIndex = TimeIndeierUtils.IndexOfTime_Tick(tickData, time);
            double klineTime = timeLineData.Arr_Time[timeLineIndex];
            int startTickIndex = TimeIndeierUtils.IndexOfTime_Tick(tickData, klineTime);

            TimeLineBar klineBar = GetTimeLineBar(tickData, startTickIndex, tickIndex, timeLineData.YesterdayEnd);
            timeLineData.ChangeCurrentBar(klineBar, timeLineIndex);
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
