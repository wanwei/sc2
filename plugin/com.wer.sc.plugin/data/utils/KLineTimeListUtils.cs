using com.wer.sc.data.reader;
using com.wer.sc.data.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.utils
{
    /// <summary>
    /// 获取一天中开盘时间的工具类
    /// </summary>
    class KLineTimeListUtils
    {
        /// <summary>
        /// 得到当日的所有K线时间列表
        /// </summary>
        /// <param name="openDate">今日</param>
        /// <param name="lastOpenDate">上一个开盘日</param>
        /// <param name="tradingSession">交易时段</param>
        /// <param name="targetPeriod"></param>
        /// <returns></returns>
        public static List<double> GetKLineTimeList(int date, int lastOpenDate, List<double[]> tradingSession, KLinePeriod targetPeriod)
        {
            bool overNight = IsOverNight(tradingSession);
            bool isWeekStart = IsWeekStart(date, lastOpenDate);

            KLineOpenPeriods dayOpenTime = TradingTimeUtils.GetKLineTimeList_DayOpenTime(tradingSession, targetPeriod);
            List<double> klineFullTimes = new List<double>(dayOpenTime.KlineTimes.Count);

            List<double> klineTimes = dayOpenTime.KlineTimes;
            if (overNight)
            {
                bool isFistOpenTimeOverNight = IsOpenTimeOverNight(tradingSession[0]);
                if (isWeekStart)
                {
                    int firstDate = lastOpenDate;
                    int secondDate = (int)TimeUtils.AddDays(firstDate, 1);

                    AddFullKLineTimes(klineFullTimes, firstDate, klineTimes, 0, dayOpenTime.OverNightIndex - 1);
                    AddFullKLineTimes(klineFullTimes, secondDate, klineTimes, dayOpenTime.OverNightIndex, dayOpenTime.SplitIndeies[1] - 1);
                    AddFullKLineTimes(klineFullTimes, date, klineTimes, dayOpenTime.SplitIndeies[1], dayOpenTime.KlineTimes.Count - 1);
                }
                else
                {
                    int firstDate = lastOpenDate;
                    AddFullKLineTimes(klineFullTimes, firstDate, dayOpenTime.KlineTimes, 0, dayOpenTime.OverNightIndex - 1);
                    AddFullKLineTimes(klineFullTimes, date, dayOpenTime.KlineTimes, dayOpenTime.OverNightIndex, dayOpenTime.KlineTimes.Count - 1);
                }
            }
            else
            {
                AddFullKLineTimes(klineFullTimes, date, dayOpenTime.KlineTimes, 0, dayOpenTime.KlineTimes.Count - 1);
            }

            return klineFullTimes;
        }

        /// <summary>
        /// 得到一天内K线的每一根柱子的时间
        /// </summary>
        /// <param name="openDateReader"></param>
        /// <param name="openTimes"></param>
        /// <param name="targetPeriod"></param>
        /// <returns></returns>
        public static List<double> GetKLineTimeList(int date, ITradingDayReader openDateReader, List<double[]> openTimes, KLinePeriod targetPeriod)
        {
            if (!openDateReader.IsTrade(date))
                throw new ArgumentException(date + "不开盘");

            int lastOpenDate = openDateReader.GetPrevTradingDay(date);
            return GetKLineTimeList(date, lastOpenDate, openTimes, targetPeriod);
        }

        private static void AddFullKLineTimes(List<double> klineFullTimes, int date, List<double> klineTimes, int startIndex, int endIndex)
        {
            for (int i = startIndex; i <= endIndex; i++)
            {
                klineFullTimes.Add(date + klineTimes[i]);
            }
        }

        private static bool IsOpenTimeOverNight(double[] openTime)
        {
            return openTime[0] < openTime[1];
        }

        private static bool IsWeekStart(int date, int lastDate)
        {
            if (date - lastDate > 1)
                return true;
            return false;
        }

        private static bool IsOverNight(List<double[]> openTimes)
        {
            double lastTime = 0;
            for (int i = 0; i < openTimes.Count; i++)
            {
                double[] openTime = openTimes[i];
                if (openTime[0] < lastTime)
                    return true;
                if (openTime[1] < openTime[0])
                    return true;
                lastTime = openTime[1];
            }
            return false;
        }

        private static bool isDayChange(double time1, double time2)
        {
            if (time2 < time1)
                return true;
            if (time1 < 0.06 && time2 > 0.08)
                return true;
            return false;
        }
    }
}
