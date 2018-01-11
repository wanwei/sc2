using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.datapackage
{
    /// <summary>
    /// 实时数据包
    /// </summary>
    public class RealTimeDataPackage_Code : IRealTimeDataPackage_Code
    {
        //实时数据包所对应的普通数据包
        private IDataPackage_Code dataPackage_Code;

        //该周期内所有K线数据
        private Dictionary<KLinePeriod, IKLineData_RealTime> dic_Period_KLineData = new Dictionary<KLinePeriod, IKLineData_RealTime>();

        //当前交易日的tick数据
        private ITickData_Extend currentTickData;

        //当前交易日的分时数据
        private ITimeLineData_RealTime currentTimeLineData;

        private double time;

        private int tradingDay;

        public RealTimeDataPackage_Code(IDataPackage_Code dataPackage_Code, double time)
        {
            this.dataPackage_Code = dataPackage_Code;
            this.ChangeTime(time);
        }

        public double Time
        {
            get
            {
                return time;
            }
        }

        public void ChangeTime(double time)
        {
            int tradingDay = dataPackage_Code.GetTradingTimeReader().GetRecentTradingDay(time);
            if (!dataPackage_Code.GetTradingDayReader().IsTrade(tradingDay))
            {
                tradingDay = dataPackage_Code.GetTradingTimeReader().GetRecentTradingDay(time, true);
                //if (!dataPackage.GetTradingDayReader().IsTrade(tradingDay))
            }
            this.time = time;
            this.tradingDay = tradingDay;
        }

        public int TradingDay
        {
            get
            {
                return tradingDay;
            }
        }

        public string Code
        {
            get
            {
                return dataPackage_Code.Code;
            }
        }

        public int StartDate
        {
            get
            {
                return dataPackage_Code.StartDate;
            }
        }

        public int EndDate
        {
            get
            {
                return dataPackage_Code.EndDate;
            }
        }

        public IList<int> GetTradingDays()
        {
            return dataPackage_Code.GetTradingDays();
        }

        public IKLineData_Extend GetKLineData(KLinePeriod klinePeriod)
        {
            if (klinePeriod.PeriodType == KLineTimeType.SECOND)
            {
                IKLineData_RealTime klineData_Second = dataPackage_Code.CreateKLineData_RealTime_Second(tradingDay, klinePeriod);
                RealTimeDataPackageTimeChangeUtils.ChangeTime_KLineData(klineData_Second, tradingDay, time, GetTickData());
                return klineData_Second;
            }

            IKLineData_RealTime klineData;
            if (this.dic_Period_KLineData.ContainsKey(klinePeriod))
            {
                klineData = this.dic_Period_KLineData[klinePeriod];
            }
            else
            {
                klineData = dataPackage_Code.CreateKLineData_RealTime(klinePeriod);
                this.dic_Period_KLineData.Add(klinePeriod, klineData);
            }
            RealTimeDataPackageTimeChangeUtils.ChangeTime_KLineData(klineData, tradingDay, time, GetTickData());
            return klineData;
        }

        public ITimeLineData_Extend GetTimeLineData()
        {
            if (currentTimeLineData == null || currentTimeLineData.Date != tradingDay)
            {
                currentTimeLineData = dataPackage_Code.CreateTimeLineData_RealTime(tradingDay);
            }
            RealTimeDataPackageTimeChangeUtils.ChangeTime_TimeLineData(currentTimeLineData, time, GetTickData());
            return currentTimeLineData;
        }

        public ITickData_Extend GetTickData()
        {
            if (currentTickData == null || currentTickData.TradingDay != tradingDay)
            {
                currentTickData = dataPackage_Code.GetTickData(tradingDay);
            }
            RealTimeDataPackageTimeChangeUtils.ChangeTime_TickData(currentTickData, time);
            return currentTickData;
        }
    }
}
