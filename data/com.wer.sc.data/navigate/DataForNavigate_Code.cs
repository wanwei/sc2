using com.wer.sc.data.forward;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.data.datapackage;
using com.wer.sc.data.realtime;
using com.wer.sc.data.utils;

namespace com.wer.sc.data.navigate
{
    public class DataForNavigate_Code
    {
        public static DataForNavigate_Code Create(DataForForward_Code dataForForward)
        {
            DataForNavigate_Code dataForNav = new DataForNavigate_Code(dataForForward.DataPackage, dataForForward.Time);
            foreach (KLinePeriod klinePeriod in dataForForward.ReferedKLinePeriods)
            {
                IKLineData_RealTime klineData = dataForForward.GetKLineData(klinePeriod);
                dataForNav.dic_Period_KLineData.Add(klinePeriod, klineData);
            }
            return dataForNav;
        }

        private double time;

        //当前交易日
        private int tradingDay;

        //数据包
        private IDataPackage_Code dataPackage;

        //该周期内所有K线数据
        private Dictionary<KLinePeriod, IKLineData_RealTime> dic_Period_KLineData = new Dictionary<KLinePeriod, IKLineData_RealTime>();

        //当前交易日的tick数据
        private ITickData_Extend currentTickData;

        //当前交易日的分时数据
        protected ITimeLineData_RealTime currentTimeLineData;

        public DataForNavigate_Code(IDataPackage_Code dataPackage, double time)
        {
            this.dataPackage = dataPackage;
            this.NavigateTo(time);
        }

        public IDataPackage_Code DataPackage
        {
            get { return this.dataPackage; }
        }

        public bool NavigateTo(double time)
        {
            int tradingDay = dataPackage.GetTradingTimeReader().GetRecentTradingDay(time);
            if (!dataPackage.GetTradingDayReader().IsTrade(tradingDay))
            {
                tradingDay = dataPackage.GetTradingTimeReader().GetRecentTradingDay(time, true);
                if (!dataPackage.GetTradingDayReader().IsTrade(tradingDay))
                    return false;
            }
            this.time = time;
            this.tradingDay = tradingDay;
            return true;
        }


        /// <summary>
        /// 得到代码
        /// </summary>
        /// <returns></returns>
        public string Code
        {
            get
            {
                return dataPackage.Code;
            }
        }

        public double Time
        {
            get
            {
                return time;
            }
        }

        public float Price
        {
            get
            {
                return this.GetTickData().Price;
            }
        }

        /// <summary>
        /// 得到指定周期的K线
        /// </summary>
        /// <param name="period"></param>
        /// <returns></returns>
        public IKLineData GetKLineData(KLinePeriod klinePeriod)
        {
            IKLineData_RealTime klineData;
            if (this.dic_Period_KLineData.ContainsKey(klinePeriod))
            {
                klineData = this.dic_Period_KLineData[klinePeriod];
            }
            else
            {
                klineData = dataPackage.CreateKLineData_RealTime(klinePeriod); //new KLineDataExtend_RealTime(this.dataPackage.GetKLineData(klinePeriod));
                this.dic_Period_KLineData.Add(klinePeriod, klineData);
            }
            DataNavigate_ChangeTime.ChangeTime_KLineData(klineData, tradingDay, time, GetTickData());
            return klineData;
        }

        /// <summary>
        /// 得到当前的分时线
        /// </summary>
        /// <returns></returns>
        public ITimeLineData GetTimeLineData()
        {
            if (currentTimeLineData == null || currentTimeLineData.Date != tradingDay)
            {
                currentTimeLineData = dataPackage.CreateTimeLineData_RealTime(tradingDay);
            }
            DataNavigate_ChangeTime.ChangeTime_TimeLineData(currentTimeLineData, time, GetTickData());
            return currentTimeLineData;
        }

        /// <summary>
        /// 得到今日的TICK数据
        /// </summary>
        /// <returns></returns>
        public ITickData_Extend GetTickData()
        {
            if (currentTickData == null || currentTickData.TradingDay != tradingDay)
            {
                currentTickData = dataPackage.GetTickData(tradingDay);
            }
            DataNavigate_ChangeTime.ChangeTime_TickData(currentTickData, time);
            return currentTickData;
        }
    }
}