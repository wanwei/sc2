using System;
using System.Collections.Generic;

namespace com.wer.sc.data.utils
{
    /// <summary>
    /// 交易时间工具类
    /// </summary>
    public class TradingTimeUtils
    {
        /// <summary>
        /// 得到多天的交易时间详细信息
        /// </summary>
        /// <param name="tradingPeriod"></param>
        /// <param name="klinePeriod"></param>
        /// <returns></returns>
        public static KLineDataTimeInfo GetKLineDataTimeInfo(IList<double[]>[] tradingPeriodArr, KLinePeriod klinePeriod)
        {
            List<double[]> klineTimeList = new List<double[]>();
            ISet<int> set_PeriodEnd = new HashSet<int>();
            ISet<int> set_DayEnd = new HashSet<int>();
            List<int> tradingDays = new List<int>();
            for (int i = 0; i < tradingPeriodArr.Length; i++)
            {
                IList<double[]> tradingPeriod = tradingPeriodArr[i];
                GetKLineTimeDataInfo(tradingPeriod, klinePeriod, klineTimeList, set_PeriodEnd, set_DayEnd);
                tradingDays.Add((int)tradingPeriod[tradingPeriod.Count - 1][1]);
            }
            List<int> periodEnds = new List<int>(set_PeriodEnd);
            periodEnds.Sort();
            List<int> dayEnds = new List<int>(set_DayEnd);
            dayEnds.Sort();
            return new KLineDataTimeInfo(klineTimeList, periodEnds, tradingDays, dayEnds, klinePeriod);
        }

        /// <summary>
        /// 得到一天内交易时间详细信息
        /// </summary>
        /// <param name="tradingPeriod"></param>
        /// <param name="klinePeriod"></param>
        /// <returns></returns>
        public static KLineDataTimeInfo GetKLineDataTimeInfo(IList<double[]> tradingPeriod, KLinePeriod klinePeriod)
        {
            List<double[]> klineTimeList = new List<double[]>();
            ISet<int> set_PeriodEnd = new HashSet<int>();
            ISet<int> set_DayEnd = new HashSet<int>();

            GetKLineTimeDataInfo(tradingPeriod, klinePeriod, klineTimeList, set_PeriodEnd, set_DayEnd);
            List<int> periodEndBarPoses = new List<int>(set_PeriodEnd);
            periodEndBarPoses.Sort();
            List<int> dayEndBarposes = new List<int>(set_DayEnd);
            dayEndBarposes.Sort();
            IList<int> tradingDays = new List<int>();
            tradingDays.Add((int)tradingPeriod[tradingPeriod.Count - 1][1]);
            return new KLineDataTimeInfo(klineTimeList, periodEndBarPoses, tradingDays, dayEndBarposes, klinePeriod);
        }

        private static void GetKLineTimeDataInfo(IList<double[]> tradingPeriod, KLinePeriod klinePeriod, List<double[]> klineTimeList, ISet<int> set_PeriodEnd, ISet<int> set_DayEnd)
        {
            int offset = 0;
            for (int i = 0; i < tradingPeriod.Count; i++)
            {
                offset = GetTimeArr_Full(tradingPeriod[i], klinePeriod, klineTimeList, offset);
                set_PeriodEnd.Add(klineTimeList.Count - 1);
            }
            set_DayEnd.Add(klineTimeList.Count - 1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tradingPeriod"></param>
        /// <param name="klinePeriod"></param>
        /// <param name="splitPeriod"></param>
        /// <returns></returns>
        public static List<double[]> GetKLineTimeList_Full(IList<double[]> tradingPeriod, KLinePeriod klinePeriod)
        {
            List<double[]> klineTimeList = new List<double[]>();

            int offset = 0;
            for (int i = 0; i < tradingPeriod.Count; i++)
            {
                offset = GetTimeArr_Full(tradingPeriod[i], klinePeriod, klineTimeList, offset);
            }
            return klineTimeList;
        }

        private static int GetTimeArr_Full(double[] tradingPeriod, KLinePeriod period, List<double[]> timeList, int offset)
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

        ///// <summary>
        ///// 得到当日的所有K线时间列表
        ///// </summary>
        ///// <param name="openDate">今日</param>
        ///// <param name="lastOpenDate">上一个开盘日</param>
        ///// <param name="tradingSession">交易时段</param>
        ///// <param name="targetPeriod"></param>
        ///// <returns></returns>
        //public static List<double> GetKLineTimeList(int date, int lastOpenDate, List<double[]> tradingSession, KLinePeriod targetPeriod)
        //{
        //    bool overNight = IsOverNight(tradingSession);
        //    bool isWeekStart = IsWeekStart(date, lastOpenDate);

        //    KLineOpenPeriods dayOpenTime = TradingTimeUtils.GetKLineTimeList_DayOpenTime(tradingSession, targetPeriod);
        //    List<double> klineFullTimes = new List<double>(dayOpenTime.KlineTimes.Count);

        //    List<double> klineTimes = dayOpenTime.KlineTimes;
        //    if (overNight)
        //    {
        //        bool isFistOpenTimeOverNight = IsOpenTimeOverNight(tradingSession[0]);
        //        if (isWeekStart)
        //        {
        //            int firstDate = lastOpenDate;
        //            int secondDate = (int)TimeUtils.AddDays(firstDate, 1);

        //            AddFullKLineTimes(klineFullTimes, firstDate, klineTimes, 0, dayOpenTime.OverNightIndex - 1);
        //            AddFullKLineTimes(klineFullTimes, secondDate, klineTimes, dayOpenTime.OverNightIndex, dayOpenTime.SplitIndeies[1] - 1);
        //            AddFullKLineTimes(klineFullTimes, date, klineTimes, dayOpenTime.SplitIndeies[1], dayOpenTime.KlineTimes.Count - 1);
        //        }
        //        else
        //        {
        //            int firstDate = lastOpenDate;
        //            AddFullKLineTimes(klineFullTimes, firstDate, dayOpenTime.KlineTimes, 0, dayOpenTime.OverNightIndex - 1);
        //            AddFullKLineTimes(klineFullTimes, date, dayOpenTime.KlineTimes, dayOpenTime.OverNightIndex, dayOpenTime.KlineTimes.Count - 1);
        //        }
        //    }
        //    else
        //    {
        //        AddFullKLineTimes(klineFullTimes, date, dayOpenTime.KlineTimes, 0, dayOpenTime.KlineTimes.Count - 1);
        //    }

        //    return klineFullTimes;
        //}

        /////// <summary>
        /////// 得到一天内K线的每一根柱子的时间
        /////// </summary>
        /////// <param name="openDateReader"></param>
        /////// <param name="openTimes"></param>
        /////// <param name="targetPeriod"></param>
        /////// <returns></returns>
        ////public static List<double> GetKLineTimeList(int date, ITradingDayReader openDateReader, List<double[]> openTimes, KLinePeriod targetPeriod)
        ////{
        ////    if (!openDateReader.IsOpen(date))
        ////        throw new ArgumentException(date + "不开盘");

        ////    int lastOpenDate = openDateReader.GetPrevOpenDate(date);
        ////    return GetKLineTimeList(date, lastOpenDate, openTimes, targetPeriod);
        ////}

        //private static void AddFullKLineTimes(List<double> klineFullTimes, int date, List<double> klineTimes, int startIndex, int endIndex)
        //{
        //    for (int i = startIndex; i <= endIndex; i++)
        //    {
        //        klineFullTimes.Add(date + klineTimes[i]);
        //    }
        //}

        //private static bool IsOpenTimeOverNight(double[] openTime)
        //{
        //    return openTime[0] < openTime[1];
        //}

        //private static bool IsWeekStart(int date, int lastDate)
        //{
        //    if (date - lastDate > 1)
        //        return true;
        //    return false;
        //}

        //private static bool IsOverNight(List<double[]> openTimes)
        //{
        //    double lastTime = 0;
        //    for (int i = 0; i < openTimes.Count; i++)
        //    {
        //        double[] openTime = openTimes[i];
        //        if (openTime[0] < lastTime)
        //            return true;
        //        if (openTime[1] < openTime[0])
        //            return true;
        //        lastTime = openTime[1];
        //    }
        //    return false;
        //}

        //private static bool isDayChange(double time1, double time2)
        //{
        //    if (time2 < time1)
        //        return true;
        //    if (time1 < 0.06 && time2 > 0.08)
        //        return true;
        //    return false;
        //}

        ///// <summary>
        ///// 根据起止时间获得一个当日k线数组
        ///// </summary>
        ///// <param name="tradingTimeList"></param>
        ///// <returns></returns>
        //public static List<double> GetKLineTimeList(List<double[]> tradingTimeList, KLinePeriod period)
        //{
        //    KLineOpenPeriods dayOpenTime = new KLineOpenPeriods();
        //    double offset = 0;
        //    for (int i = 0; i < tradingTimeList.Count; i++)
        //    {
        //        if (offset != 0)
        //        {
        //            //两次开盘时间间隔超过4小时，则重新开始一个周期
        //            if (tradingTimeList[i][0] - tradingTimeList[i - 1][1] >= 0.04)
        //                offset = 0;
        //        }
        //        offset = GetTimeArr(dayOpenTime, tradingTimeList[i], period, offset);
        //    }
        //    return dayOpenTime.KlineTimes;
        //}

        //internal static KLineOpenPeriods GetKLineTimeList_DayOpenTime(List<double[]> openTimeList, KLinePeriod period)
        //{
        //    KLineOpenPeriods dayOpenTime = new KLineOpenPeriods();
        //    double offset = 0;
        //    for (int i = 0; i < openTimeList.Count; i++)
        //    {
        //        if (offset != 0)
        //        {
        //            //两次开盘时间间隔超过4小时，则重新开始一个周期
        //            if (openTimeList[i][0] - openTimeList[i - 1][1] >= 0.04)
        //                offset = 0;
        //        }

        //        if (i != 0)
        //        {
        //            if (openTimeList[i][0] < openTimeList[i - 1][1])
        //            {
        //                dayOpenTime.OverNightIndex = dayOpenTime.KlineTimes.Count;
        //            }
        //        }
        //        dayOpenTime.SplitIndeies.Add(dayOpenTime.KlineTimes.Count);
        //        offset = GetTimeArr(dayOpenTime, openTimeList[i], period, offset);
        //    }
        //    return dayOpenTime;
        //}

        //private static double GetTimeArr(KLineOpenPeriods dayOpenTime, double[] openTime, KLinePeriod period, double offset)
        //{
        //    List<double> times = dayOpenTime.KlineTimes;
        //    double currentTime = 20100101 + openTime[0] + offset;
        //    double endTime = 20100101 + openTime[1];

        //    //看是否隔夜
        //    if (endTime < currentTime)
        //        endTime += 1;

        //    bool overNight = false;

        //    while (currentTime < endTime)
        //    {
        //        double time = currentTime - 20100101;
        //        if (time >= 1)
        //            time -= 1;
        //        times.Add(Math.Round(time, 6));
        //        currentTime = TimeUtils.AddTime(currentTime, period.Period, period.PeriodType);
        //        if (!overNight && currentTime >= 20100102)
        //        {
        //            dayOpenTime.OverNightIndex = times.Count;
        //            overNight = true;
        //        }
        //    }

        //    return currentTime - endTime;
        //}
    }

    //internal class KLineOpenPeriods
    //{
    //    private List<int> splitIndeies = new List<int>();
    //    private List<double> klineTimes = new List<double>();

    //    private int overNightIndex = -1;

    //    /// <summary>
    //    /// 得到停牌后开盘K线Bar的Index
    //    /// </summary>
    //    public List<int> SplitIndeies
    //    {
    //        get
    //        {
    //            return splitIndeies;
    //        }
    //    }

    //    public int OverNightIndex
    //    {
    //        get
    //        {
    //            return overNightIndex;
    //        }
    //        set
    //        {
    //            overNightIndex = value;
    //        }
    //    }

    //    /// <summary>
    //    /// 得到所有的K线时间
    //    /// </summary>
    //    public List<double> KlineTimes
    //    {
    //        get
    //        {
    //            return klineTimes;
    //        }
    //    }
    //}
}