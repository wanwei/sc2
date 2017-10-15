using com.wer.sc.data.datapackage;
using com.wer.sc.data.forward;
using com.wer.sc.data.reader;
using com.wer.sc.data.utils;
using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.navigate
{
    /// <summary>
    /// 数据导航实现类
    /// </summary>
    public class DataNavigate_Code : IDataNavigate_Code
    {
        private IDataPackage_Code dataPackage;

        private int startDate;

        private int endDate;

        private string code;

        private double time;

        //当前tick数据
        private DataNavigate_Code_Tick navigate_Tick;

        //当前的k线数据
        private Dictionary<KLinePeriod, DataNavigate_Code_KLine> dicNavigateKLine = new Dictionary<KLinePeriod, DataNavigate_Code_KLine>();

        //当前分时线数据
        private DataNavigate_Code_TimeLine navigate_TimeLine;

        public DataNavigate_Code(IDataPackage_Code dataPackage, double time)
        {
            this.dataPackage = dataPackage;
            this.NavigateTo(time);
        }

        public DataNavigate_Code(IDataReader dataReader, string code, double time)
        {
            int openDate = dataReader.CreateTradingTimeReader(code).GetRecentTradingDay(time);
            ITradingDayReader tradingDayReader = dataReader.TradingDayReader;

            int index = dataReader.TradingDayReader.GetTradingDayIndex(openDate);
            //默认取前100天，后50天数据
            int startIndex = index - 100;
            startIndex = startIndex < 0 ? 0 : startIndex;
            int startDate = tradingDayReader.GetTradingDay(startIndex);

            int endIndex = index + 50;
            if (endIndex >= tradingDayReader.GetAllTradingDays().Count)
                endIndex = tradingDayReader.GetAllTradingDays().Count - 1;
            int endDate = tradingDayReader.GetTradingDay(endIndex);

            this.dataPackage = DataPackageFactory.CreateDataPackage(dataReader, code, startDate, endDate);
            this.NavigateTo(time);
        }

        public DataNavigate_Code(IDataReader dataReader, string code, double time, int startDate, int endDate)
        {
            this.dataPackage = DataPackageFactory.CreateDataPackage(dataReader, code, startDate, endDate);
            this.code = code;
            this.startDate = startDate;
            this.endDate = endDate;
            this.NavigateTo(time);
        }

        //private double GetNavTime(double time)
        //{

        //}

        public bool NavigateTo(double time)
        {
            double prevTime = this.time;
            bool timeChage = this.time == time;
            this.time = time;
            if (timeChage)
            {
                if (OnRealTimeChanged != null)
                    OnRealTimeChanged(this, new RealTimeChangedArgument(prevTime, time, this));
                if (OnNavigateTo != null)
                    OnNavigateTo(this, new DataNavigateEventArgs(prevTime, time));
            }
            int tradingDay = this.dataPackage.GetTradingTimeReader().GetRecentTradingDay(time);
            return tradingDay >= dataPackage.StartDate && tradingDay <= dataPackage.EndDate;
        }

        public bool IsPeriodEnd(KLinePeriod period)
        {
            return false;
        }

        /// <summary>
        /// 前进
        /// </summary>
        /// <returns></returns>
        public bool Forward(KLinePeriod forwardPeriod)
        {
            IKLineData klineData = GetKLineData(forwardPeriod);
            int nextBarPos = klineData.BarPos + 1;
            if (nextBarPos >= klineData.Length)
            {
                ITickData tickData = GetTickData();
                if (tickData.BarPos == tickData.Length - 1)
                    return false;
                double endtime = tickData.Arr_Time[tickData.Length - 1];
                NavigateTo(endtime);
                return true;
            }
            double time = klineData.Arr_Time[nextBarPos];
            NavigateTo(time);
            return true;
        }

        /// <summary>
        /// 后退
        /// </summary>
        /// <param name="forwardPeriod"></param>
        /// <returns></returns>
        public bool Backward(KLinePeriod forwardPeriod)
        {
            IKLineData klineData = GetKLineData(forwardPeriod);
            int prevBarPos = klineData.BarPos - 1;
            if (prevBarPos < 0)
                return false;
            double time = klineData.Arr_Time[prevBarPos];
            NavigateTo(time);
            return true;
        }

        public double Time
        {
            get
            {
                return time;
            }
        }

        public string Code
        {
            get
            {
                return this.dataPackage.Code;
            }
        }

        public IDataPackage_Code DataPackage
        {
            get
            {
                return this.dataPackage;
            }
        }

        public IKLineData GetKLineData(KLinePeriod period)
        {
            if (!dicNavigateKLine.ContainsKey(period))
            {
                DataNavigate_Code_KLine navigate = new DataNavigate_Code_KLine(dataPackage, time, period);
                dicNavigateKLine.Add(period, navigate);
            }
            DataNavigate_Code_KLine navigate_KLine = dicNavigateKLine[period];
            if (time != navigate_KLine.Time)
                navigate_KLine.ChangeTime(time);
            return navigate_KLine.GetKLineData();
        }

        public ITickData GetTickData()
        {
            if (navigate_Tick == null)
            {
                navigate_Tick = new DataNavigate_Code_Tick(dataPackage, time);
                return navigate_Tick.GetTickData();
            }
            if (time != navigate_Tick.Time)
                navigate_Tick.ChangeTime(time);
            return navigate_Tick.GetTickData();
        }

        public ITimeLineData GetTimeLineData()
        {
            if (navigate_TimeLine == null)
            {
                navigate_TimeLine = new DataNavigate_Code_TimeLine(dataPackage, time);
                return navigate_TimeLine.GetTimeLineData();
            }
            if (time != navigate_TimeLine.Time)
                navigate_TimeLine.ChangeTime(time);
            return navigate_TimeLine.GetTimeLineData();
        }

        public event DelegateOnNavigateTo OnNavigateTo;

        public event DelegateOnRealTimeChanged OnRealTimeChanged;
    }
}