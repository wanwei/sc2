using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.utils
{
    public class TimeUtils
    {
        private static String GetZeroStr(int len)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < len; i++)
            {
                sb.Append("0");
            }
            return sb.ToString();
        }

        public static DateTime ConvertToDateTime(double time)
        {
            String timeStr = time.ToString();
            if (timeStr.Length < 15)
                timeStr = timeStr + GetZeroStr(15 - timeStr.Length);

            String timeFormat = timeStr.Substring(0, 4) + "-" + timeStr.Substring(4, 2) + "-" + timeStr.Substring(6, 2)
                + " " + timeStr.Substring(9, 2) + ":" + timeStr.Substring(11, 2) + ":" + timeStr.Substring(13, 2);
            
            return Convert.ToDateTime(timeFormat);
        }

        public static double ConvertToDoubleTime(DateTime dt)
        {
            return Double.Parse(string.Format("{0:yyyyMMdd.HHmmss}", dt));
        }

        //public static double ConvertToDoubleTimeLong(DateTime dt)
        //{
        //    return Double.Parse(string.Format("{0:yyyyMMdd.HHmmssfff}", dt));
        //}


        //public static double AddMileSeconds(double time, int value)
        //{
        //    return ConvertToDoubleTimeLong(ConvertToDateTime(time).AddMilliseconds(value));
        //}

        public static double AddSeconds(double time, int value)
        {
            return ConvertToDoubleTime(ConvertToDateTime(time).AddSeconds(value));
        }

        public static double AddMinutes(double time, int value)
        {
            return ConvertToDoubleTime(ConvertToDateTime(time).AddMinutes(value));
        }

        public static double AddHours(double time, int value)
        {
            return ConvertToDoubleTime(ConvertToDateTime(time).AddHours(value));
        }

        public static double AddDays(double time, int value)
        {
            return ConvertToDoubleTime(ConvertToDateTime(time).AddDays(value));
        }

        public static double AddTime(double time, int value, KLineTimeType timeType)
        {
            switch (timeType)
            {
                case KLineTimeType.SECOND:
                    return AddSeconds(time, value);
                case KLineTimeType.MINUTE:
                    return AddMinutes(time, value);
                case KLineTimeType.HOUR:
                    return AddHours(time, value);
                case KLineTimeType.DAY:
                    return AddDays(time, value);
            }
            return time;
        }

        public static TimeSpan Substract(double time1, double time2)
        {
            if (time1 < 1 && time2 < 1)
            {
                time1 += 20100101;
                time2 += 20100101;
            }
            DateTime t1 = ConvertToDateTime(time1);
            DateTime t2 = ConvertToDateTime(time2);
            return t1.Subtract(t2);
        }

        ///// <summary>
        ///// 根据起止时间获得一个当日k线数组
        ///// </summary>
        ///// <param name="openTimeList"></param>
        ///// <returns></returns>
        //public static List<double> GetKLineTimes(List<double[]> openTimeList, KLinePeriod period)
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
        //        offset = GetTimeArr(dayOpenTime, openTimeList[i], period, offset);
        //    }
        //    return dayOpenTime.KlineTimes;
        //}

        //public static KLineOpenPeriods GetKLineTimes_DayOpenTime(List<double[]> openTimeList, KLinePeriod period)
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
        //        currentTime = AddTime(currentTime, period.Period, period.PeriodType);
        //        if (!overNight && currentTime >= 20100102)
        //        {
        //            dayOpenTime.OverNightIndex = times.Count;
        //            overNight = true;
        //        }
        //    }

        //    return currentTime - endTime;
        //}

        //public static int GetDate(DateTime dateTime)
        //{
        //    int d = dateTime.Year * 10000 + dateTime.Month * 100 + dateTime.Day;
        //    return d;
        //}

        //public static double GetTime(DateTime dateTime)
        //{
        //    int date = GetDate(dateTime);
        //    double second = dateTime.Hour * 10000 + dateTime.Minute * 100 + dateTime.Second;
        //    return date + Math.Round(second / 1000000, 6);
        //}
    }
}