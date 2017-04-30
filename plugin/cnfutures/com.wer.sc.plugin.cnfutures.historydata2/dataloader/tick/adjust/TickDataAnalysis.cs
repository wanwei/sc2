using com.wer.sc.data;
using com.wer.sc.data.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin.cnfutures.historydata.dataloader.tick.adjust
{
    /// <summary>
    /// 正确数据：
    /// 早9点晚9点
    ///     
    /// 一些典型数据错误：
    /// 1.时间出现偏移，如20100104的m05，出现41秒偏移。
    /// 2.开盘时间出现大量重复，收盘却提前，如20071017，提前45秒收盘
    /// 3.开盘有一个时间点莫名其妙提前了很多，如20061229，提前了快3分钟
    /// 
    /// </summary>
    public class TickDataAnalysis
    {
        public static List<TickInfo_Period> Analysis(TickData data, List<double[]> openTime)
        {
            return Analysis(data, openTime, 2);
        }

        public static List<TickInfo_Period> Analysis(TickData data, List<double[]> openTime, int countEverySecond)
        {
            List<TickInfo_Period> periods = GetPeriods(data, openTime);
            setAdjustInfo(periods, data, countEverySecond);
            return periods;
        }

        public static List<TickInfo_Period> GetPeriods(TickData data, List<double[]> openTime)
        {
            int date = data.TradingDay;
            List<TickInfo_Period> adjustInfos = new List<TickInfo_Period>();
            int currentPeriodIndex = 0;

            TickInfo_Period period = NewPeriod(openTime, currentPeriodIndex, 0);
            adjustInfos.Add(period);

            /*
             * 这里处理在10:15分之前或是整个上午一笔交易都没有的情况
             */
            double firstTime = data.arr_time[0];
            for (int i = 0; i < openTime.Count; i++)
            {
                if (date + openTime[i][1] < firstTime)
                {
                    currentPeriodIndex++;

                    adjustInfos[adjustInfos.Count - 1].StartIndex = -1;
                    adjustInfos[adjustInfos.Count - 1].EndIndex = -1;

                    period = NewPeriod(openTime, currentPeriodIndex, 0);
                    adjustInfos.Add(period);
                }
            }

            //TODO
            //最后一个周期直接加，
            double periodSplit = date + GetPeriodSplit(openTime, currentPeriodIndex);
            for (int currentIndex = 1; currentIndex < data.Length; currentIndex++)
            {
                if (data.arr_time[currentIndex - 1] < periodSplit
                    && data.arr_time[currentIndex] > periodSplit)
                {
                    currentPeriodIndex++;
                    if (adjustInfos.Count != 0)
                        //设置上一个adjustinfo的结束index
                        adjustInfos[adjustInfos.Count - 1].EndIndex = currentIndex - 1;

                    period = NewPeriod(openTime, currentPeriodIndex, currentIndex);
                    adjustInfos.Add(period);

                    if (currentPeriodIndex == openTime.Count - 1)
                    {
                        adjustInfos[adjustInfos.Count - 1].EndIndex = data.Length - 1;
                        break;
                    }
                    periodSplit = date + GetPeriodSplit(openTime, currentPeriodIndex);
                }
            }
            adjustInfos[adjustInfos.Count - 1].EndIndex = data.Length - 1;
            return adjustInfos;
        }

        private static TickInfo_Period NewPeriod(List<double[]> openTime, int currentPeriodIndex, int currentTickIndex)
        {
            TickInfo_Period period = new TickInfo_Period();
            period.StartTime = openTime[currentPeriodIndex][0];
            period.EndTime = openTime[currentPeriodIndex][1];
            period.StartIndex = currentTickIndex;
            period.PeriodIndex = currentPeriodIndex;
            return period;
        }

        private static double GetPeriodSplit(List<double[]> periods, int currentIndex)
        {
            return periods[currentIndex][1] + 0.0005;
        }

        private static void setAdjustInfo(List<TickInfo_Period> periods, TickData data, int countEverySecond)
        {
            for (int i = 0; i < periods.Count; i++)
            {
                setAdjustInfo(periods[i], data, countEverySecond);
            }
        }

        private static void setAdjustInfo(TickInfo_Period period, TickData data, int countEverySecond)
        {
            if (period.StartIndex == -1 || period.EndIndex == -1)
                return;

            setStartAdjustInfo(period, data, countEverySecond);
            setEndAdjustInfo(period, data, countEverySecond);
        }

        private static void setStartAdjustInfo(TickInfo_Period period, TickData data, int countEverySecond)
        {
            TickPeriodAdjustInfo adjustInfo = period.adjustInfo;
            int startIndex = period.StartIndex;
            if (period.StartTime == 0.09 || period.StartTime == 0.21)
            {
                startIndex++;
                adjustInfo.IsOpen = true;
            }

            int startRepeatIndex = FindStartTimeIndex(period, data, startIndex);
            if (startRepeatIndex >= 0)
            {
                int startRepeatTimes = FindStartRepeatTimes(startRepeatIndex, data);
                if (startRepeatTimes > countEverySecond)
                {
                    adjustInfo.StartRepeatIndex = startRepeatIndex;
                    adjustInfo.StartRepeatTimes = startRepeatTimes;
                    adjustInfo.StartRepeat = true;
                }
            }
            double time = Math.Round(period.StartTime + data.TradingDay, 6);
            TimeSpan span = TimeUtils.Substract(data.arr_time[startIndex], time);
            int timeDif = span.Minutes * 60 + span.Seconds;
            //开盘提前超过1分钟，认为该开盘时间可能有误
            if (timeDif < -60)
                adjustInfo.StartErrorData = true;
            adjustInfo.StartOffset = timeDif;
        }

        private static void setEndAdjustInfo(TickInfo_Period period, TickData data, int countEverySecond)
        {
            TickPeriodAdjustInfo adjustInfo = period.adjustInfo;
            int endIndex = period.EndIndex;

            int endRepeatIndex = FindEndTimeIndex(period, data);
            if (endRepeatIndex > 0)
            {
                int endRepeatTimes = FindEndRepeatTimes(endRepeatIndex, data);
                if (endRepeatTimes > countEverySecond)
                {
                    adjustInfo.EndRepeatIndex = endRepeatIndex;
                    adjustInfo.EndRepeatTimes = endRepeatTimes;
                    adjustInfo.EndRepeat = true;
                }
            }
            double time = Math.Round(period.EndTime + data.TradingDay, 6);
            TimeSpan span = TimeUtils.Substract(data.arr_time[endIndex], time);
            int timeDif = span.Minutes * 60 + span.Seconds;
            //收盘晚收超过1分钟，认为该收盘时间可能有误
            if (timeDif > 60)
                adjustInfo.EndErrorData = true;
            adjustInfo.EndOffset = timeDif;
        }

        private static int FindStartTimeIndex(TickInfo_Period period, TickData data, int startIndex)
        {
            double time = Math.Round(period.StartTime + data.TradingDay, 6);
            int index = startIndex;
            //int index = period.StartIndex;
            double currentTime = data.arr_time[index];
            while (currentTime < time)
            {
                index++;
                currentTime = data.arr_time[index];
            }
            if (currentTime == time)
                return index;
            return -1;
        }

        private static int FindStartRepeatTimes(int startIndex, TickData data)
        {
            int repeatTimes = 1;
            double time = data.arr_time[startIndex];
            for (int i = startIndex + 1; i < data.Length; i++)
            {
                if (data.arr_time[i] == time)
                    repeatTimes++;
                else
                    break;
            }
            return repeatTimes;
        }

        private static int FindEndTimeIndex(TickInfo_Period period, TickData data)
        {
            double time = Math.Round(period.EndTime + data.TradingDay, 6);
            int index = period.EndIndex;
            double currentTime = data.arr_time[index];
            while (currentTime > time)
            {
                index--;
                currentTime = data.arr_time[index];
            }
            if (currentTime == time)
                return index;
            return -1;
        }

        private static int FindEndRepeatTimes(int endIndex, TickData data)
        {
            int repeatTimes = 1;
            double time = data.arr_time[endIndex];
            for (int i = endIndex - 1; i > data.Length; i++)
            {
                if (data.arr_time[i] == time)
                    repeatTimes++;
                else
                    break;
            }
            return repeatTimes;
        }
    }

}
