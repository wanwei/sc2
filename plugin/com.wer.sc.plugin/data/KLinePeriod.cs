using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data
{
    /// <summary>
    /// K线周期类
    /// 如1分钟、5分钟、日线都用该类表示
    /// </summary>
    public class KLinePeriod : IComparable<KLinePeriod>
    {
        //public const int TYPE_SECOND = 0;

        //public const int TYPE_MINUTE = 1;

        //public const int TYPE_HOUR = 2;

        //public const int TYPE_DAY = 3;

        //public const int TYPE_WEEK = 4;

        //public const int TYPE_MILESECOND = 5;

        private KLineTimeType periodType;

        public KLineTimeType PeriodType
        {
            get
            {
                return periodType;
            }

            set
            {
                periodType = value;
            }
        }

        public int Period;

        public KLinePeriod()
        {

        }

        public KLinePeriod(KLineTimeType periodType, int period)
        {
            this.PeriodType = periodType;
            this.Period = period;
        }

        public String ToEngString()
        {
            return Period.ToString() + PeriodType;
        }

        public override String ToString()
        {
            return Period + TimeTypeToString(periodType);
        }

        public static string TimeTypeToString(KLineTimeType timeType)
        {
            switch (timeType)
            {
                case KLineTimeType.SECOND:
                    return "秒钟";
                case KLineTimeType.MINUTE:
                    return "分钟";
                case KLineTimeType.HOUR:
                    return "小时";
                case KLineTimeType.DAY:
                    return "天";
                case KLineTimeType.WEEK:
                    return "周";
            }
            return "";
        }

        public override bool Equals(object obj)
        {
            if (!(obj is KLinePeriod))
                return false;
            KLinePeriod period = (KLinePeriod)obj;
            if (this.PeriodType == period.PeriodType && this.Period == period.Period)
                return true;
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return Period * 10 + (int)PeriodType;
        }

        public int CompareTo(KLinePeriod other)
        {
            if (this.periodType == other.periodType)
                return this.Period.CompareTo(other.Period);
            else
                return this.periodType.CompareTo(other.periodType);
        }

        private static KLinePeriod period_5second = new KLinePeriod(KLineTimeType.SECOND, 5);
        private static KLinePeriod period_1minute = new KLinePeriod(KLineTimeType.MINUTE, 1);
        private static KLinePeriod period_5minute = new KLinePeriod(KLineTimeType.MINUTE, 5);
        private static KLinePeriod period_15minute = new KLinePeriod(KLineTimeType.MINUTE, 15);
        private static KLinePeriod period_Hour = new KLinePeriod(KLineTimeType.HOUR, 1);
        private static KLinePeriod period_Day = new KLinePeriod(KLineTimeType.DAY, 1);

        public static KLinePeriod KLinePeriod_5Second
        {
            get { return period_5second; }
        }

        public static KLinePeriod KLinePeriod_1Minute
        {
            get { return period_1minute; }
        }

        public static KLinePeriod KLinePeriod_5Minute
        {
            get { return period_5minute; }
        }

        public static KLinePeriod KLinePeriod_15Minute
        {
            get { return period_15minute; }
        }

        public static KLinePeriod KLinePeriod_1Hour
        {
            get { return period_Hour; }
        }

        public static KLinePeriod KLinePeriod_1Day
        {
            get { return period_Day; }
        }
    }
}