using System;
using System.Collections.Generic;

namespace com.wer.sc.data.utils
{
    /// <summary>
    /// 交易时间工具类
    /// </summary>
    public class TradingTimeUtils
    {
        public static KLineDataTimeInfo GetKLineDataTimeInfo(IList<ITradingTime> tradingTimeList, KLinePeriod klinePeriod)
        {
            return new KLineDataTimeInfo(GetKLineTimeList(tradingTimeList, klinePeriod), klinePeriod);
        }

        public static IList<double[]> GetKLineTimeList(IList<ITradingTime> tradingTimeList, KLinePeriod klinePeriod)
        {
            List<double[]> klineTimeList = new List<double[]>();
            for (int i = 0; i < tradingTimeList.Count; i++)
            {
                klineTimeList.AddRange(GetKLineTimeList(tradingTimeList[i], klinePeriod));
            }
            return klineTimeList;
        }

        public static KLineDataTimeInfo GetKLineDataTimeInfo(IList<double[]>[] tradingPeriodArr, KLinePeriod klinePeriod)
        {
            return new KLineDataTimeInfo(GetKLineTimeList(tradingPeriodArr, klinePeriod), klinePeriod);
        }

        public static KLineDataTimeInfo GetKLineDataTimeInfo(IList<double[]> tradingPeriod, KLinePeriod klinePeriod)
        {
            return new KLineDataTimeInfo(GetKLineTimeList(tradingPeriod, klinePeriod), klinePeriod);
        }

        /// <summary>
        /// 获得交易时间
        /// </summary>
        /// <param name="tradingTime"></param>
        /// <param name="klinePeriod"></param>
        /// <returns></returns>
        public static IList<double[]> GetKLineTimeList(ITradingTime tradingTime, KLinePeriod klinePeriod)
        {
            return GetKLineTimeList(tradingTime.TradingPeriods, klinePeriod);
        }

        public static IList<double[]> GetKLineTimeList(IList<double[]>[] tradingPeriodArr, KLinePeriod klinePeriod)
        {
            List<double[]> klineTimeList = new List<double[]>();
            for (int i = 0; i < tradingPeriodArr.Length; i++)
            {
                klineTimeList.AddRange(GetKLineTimeList(tradingPeriodArr[i], klinePeriod));
            }
            return klineTimeList;
        }

        /// <summary>
        /// 计算指定交易时间段内指定周期的K线的每个bar的起止时间
        /// </summary>
        /// <param name="tradingPeriod"></param>
        /// <param name="klinePeriod"></param>
        /// <param name="splitPeriod"></param>
        /// <returns></returns>
        public static List<double[]> GetKLineTimeList(IList<double[]> tradingPeriod, KLinePeriod klinePeriod)
        {
            List<double[]> klineTimeList = new List<double[]>();

            int offset = 0;
            for (int i = 0; i < tradingPeriod.Count; i++)
            {
                offset = GetTimeArr(tradingPeriod[i], klinePeriod, klineTimeList, offset);
            }
            return klineTimeList;
        }

        private static int GetTimeArr(double[] tradingPeriod, KLinePeriod period, List<double[]> timeList, int offset)
        {
            double currentTime = tradingPeriod[0];
            double endTime = tradingPeriod[1];

            if (offset != 0)
            {
                currentTime = TimeUtils.AddMinutes(currentTime, offset);
                //修正上一个时间段的结束时间
                if (timeList.Count != 0)
                {
                    timeList[timeList.Count - 1][1] = currentTime;
                }
            }

            double lastTime = currentTime;
            while (currentTime < endTime)
            {
                lastTime = currentTime;
                currentTime = TimeUtils.AddTime(currentTime, period.Period, period.PeriodType);
                if (currentTime >= endTime)
                {
                    TimeSpan timeSpan = TimeUtils.Substract(currentTime, endTime);
                    offset = timeSpan.Hours * 60 + timeSpan.Minutes;
                    currentTime = endTime;
                }
                timeList.Add(new double[] { Math.Round(lastTime, 6), Math.Round(currentTime, 6) });
            }
            return offset;
        }
    }
}