using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.realtime
{
    public class KLineDataExtend_RealTime : KLineData_RealTime, IKLineData_RealTime
    {
        //private IKLineData_Extend klineData_Extend;

        protected IKLineData_Extend klineData_Extend
        {
            get { return (IKLineData_Extend)this.klineData; }
        }

        public int TradingDay
        {
            get
            {
                return klineData_Extend.TradingDay;
            }
        }

        public KLineDataExtend_RealTime(IKLineData_Extend klineData) : base(klineData)
        {
            //this.klineData_Extend = klineData;
        }

        public override int BarPos
        {
            get
            {
                return klineData_Extend.BarPos;
            }

            set
            {
                klineData_Extend.BarPos = value;
            }
        }

        public IKLineData_Extend GetKLineData_Original()
        {
            return klineData_Extend;
        }

        public double GetKLinePeriodEndTime(int barPos)
        {
            return klineData_Extend.GetKLinePeriodEndTime(barPos);
        }

        public IList<int> GetAllTradingDayEndBarPoses()
        {
            return klineData_Extend.GetAllTradingDayEndBarPoses();
        }

        public IList<int> GetAllTradingTimeEndBarPoses()
        {
            return klineData_Extend.GetAllTradingTimeEndBarPoses();
        }

        public bool IsDayEnd(int barPos)
        {
            return klineData_Extend.IsDayEnd(barPos);
        }

        public bool IsDayStart(int barPos)
        {
            return klineData_Extend.IsDayStart(barPos);
        }

        public bool IsTradingPeriodEnd(int barPos)
        {
            return klineData_Extend.IsTradingPeriodEnd(barPos);
        }

        public bool IsTradingPeriodStart(int barPos)
        {
            return klineData_Extend.IsTradingPeriodStart(barPos);
        }

        public IList<int> GetAllTradingDays()
        {
            return klineData_Extend.GetAllTradingDays();
        }

        public int GetDayStartBarPosByTradingDay(int tradingDay)
        {
            return klineData_Extend.GetDayStartBarPosByTradingDay(tradingDay);
        }

        public int GetDayEndBarPosByTradingDay(int tradingDay)
        {
            return klineData_Extend.GetDayEndBarPosByTradingDay(tradingDay);
        }

        public int GetDayBarCount()
        {
            return klineData_Extend.GetDayBarCount();
        }

        public int GetDayStartBarPos()
        {
            return klineData_Extend.GetDayStartBarPos();
        }

        public int GetDayStartBarPos(int barPos)
        {
            return klineData_Extend.GetDayStartBarPos(barPos);
        }

        public int GetDayEndBarPos()
        {
            return klineData_Extend.GetDayEndBarPos();
        }

        public int GetDayEndBarPos(int barPos)
        {
            return klineData_Extend.GetDayEndBarPos(barPos);
        }

        public ITradingTime GetTradingTime()
        {
            return klineData_Extend.GetTradingTime();
        }

        public ITradingTime GetTradingTime(int barPos)
        {
            return klineData_Extend.GetTradingTime(barPos);
        }

        public bool IsDayStart()
        {
            return klineData_Extend.IsDayStart();
        }

        public bool IsDayEnd()
        {
            return klineData_Extend.IsDayEnd();
        }

        public double[] GetTradingPeriods()
        {
            return klineData_Extend.GetTradingPeriods();
        }

        public double[] GetTradingPeriods(int barPos)
        {
            return klineData_Extend.GetTradingPeriods(barPos);
        }

        public int GetTradingPeriodsBarCount()
        {
            return klineData_Extend.GetTradingPeriodsBarCount();
        }

        public int GetTradingPeriodsBarCount(int barPos)
        {
            return klineData_Extend.GetTradingPeriodsBarCount(barPos);
        }

        public int GetTradingPeriodsStartBarPos()
        {
            return klineData_Extend.GetTradingPeriodsStartBarPos();
        }

        public int GetTradingPeriodsStartBarPos(int barPos)
        {
            return klineData_Extend.GetTradingPeriodsStartBarPos(barPos);
        }

        public int GetTradingPeriodsEndBarPos()
        {
            return klineData_Extend.GetTradingPeriodsEndBarPos();
        }

        public int GetTradingPeriodsEndBarPos(int barPos)
        {
            return klineData_Extend.GetTradingPeriodsEndBarPos(barPos);
        }

        public int GetIndexInTradingPeriods()
        {
            return klineData_Extend.GetIndexInTradingPeriods();
        }

        public int GetIndexInTradingPeriods(int barPos)
        {
            return klineData_Extend.GetIndexInTradingPeriods(barPos);
        }

        public int GetTradingPeriodsIndexInTradingDay()
        {
            return klineData_Extend.GetTradingPeriodsIndexInTradingDay();
        }

        public int GetTradingPeriodsIndexInTradingDay(int barPos)
        {
            return klineData_Extend.GetTradingPeriodsIndexInTradingDay(barPos);
        }

        public bool IsTradingPeriodStart()
        {
            return klineData_Extend.IsTradingPeriodStart();
        }

        public bool IsTradingPeriodEnd()
        {
            return klineData_Extend.IsTradingPeriodEnd();
        }

        public TimeSpan GetTimeToKLinePeriodEnd()
        {
            return klineData_Extend.GetTimeToKLinePeriodEnd();
        }

        public int HolidayDayCount()
        {
            return klineData_Extend.HolidayDayCount();
        }

        public int HolidayDayCount(int barPos)
        {
            return klineData_Extend.HolidayDayCount(barPos);
        }

        public int HaltDayCount()
        {
            return klineData_Extend.HaltDayCount();
        }

        public int HaltDayCount(int barPos)
        {
            return klineData_Extend.HaltDayCount(barPos);
        }
    }
}