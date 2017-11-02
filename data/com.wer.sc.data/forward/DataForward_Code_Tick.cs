using com.wer.sc.data;
using com.wer.sc.data.datapackage;
using com.wer.sc.data.navigate;
using com.wer.sc.data.reader;
using com.wer.sc.data.realtime;
using com.wer.sc.data.transfer;
using com.wer.sc.data.utils;
using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Xml;

namespace com.wer.sc.data.forward
{
    /// <summary>
    /// 该类用于数据的tick前进
    /// </summary>
    internal class DataForward_Code_Tick : IDataForward_Code
    {
        private IDataCenter dataCenter;

        private DataForForward_Code forwardData;

        private ForwardPeriod forwardPeriod;

        private IKLineData_RealTime mainKlineData;

        private List<IForwardOnbar_Info> barFinishedInfos = new List<IForwardOnbar_Info>();

        private IForwardOnBarArgument onBarArgument;

        private Dictionary<string, DataForward_AttachCode_Tick> dic_Code_DataForward = new Dictionary<string, DataForward_AttachCode_Tick>();

        public DataForward_Code_Tick(IDataCenter dataCenter)
        {
            this.dataCenter = dataCenter;
        }

        public DataForward_Code_Tick(IDataCenter dataCenter, IDataPackage_Code dataPackage, ForwardReferedPeriods referedPeriods, ForwardPeriod forwardPeriod)
        {
            this.dataCenter = dataCenter;
            this.forwardData = new DataForForward_Code(dataPackage, referedPeriods);
            this.forwardPeriod = forwardPeriod;            
            Init();
            InitData();
        }

        private void Init()
        {
            this.onBarArgument = new ForwardOnBarArgument(barFinishedInfos, this);
            foreach (KLinePeriod period in forwardData.ReferedKLinePeriods)
            {
                IKLineData_RealTime klineData = forwardData.GetKLineData(period);
                if (period.Equals(forwardPeriod.KlineForwardPeriod))
                    this.mainKlineData = klineData;
            }
            timer.Elapsed += Timer_Elapsed;
            timer.AutoReset = true;            
        }

        private void InitData()
        {
            this.forwardData.TradingDay = forwardData.StartDate;
            foreach (KLinePeriod period in forwardData.ReferedKLinePeriods)
            {
                IKLineData_RealTime klineData = forwardData.GetKLineData(period);
                klineData.ChangeCurrentBar(GetKLineBar(forwardData.CurrentTickData));
            }
            if (this.forwardData.UseTimeLineData)
            {
                float lastEndPrice = this.forwardData.CurrentTimeLineData.YesterdayEnd; //dataPackage.GetLastEndPrice(forwardData.StartDate);
                this.forwardData.CurrentTimeLineData.ChangeCurrentBar(GetTimeLineBar(forwardData.CurrentTickData, lastEndPrice));
            }
            foreach (string code in dic_Code_DataForward.Keys)
            {
                dic_Code_DataForward[code].ForwardNextDay(forwardData.TradingDay, this.Time);
            }
        }

        internal static KLineBar GetKLineBar(ITickBar tickBar)
        {
            return KLineUtils.GetKLineBar(tickBar);
        }

        internal static KLineBar GetKLineBar(IKLineBar klineBar, ITickBar tickBar)
        {
            return KLineUtils.GetKLineBar(klineBar, tickBar);
        }

        internal static TimeLineBar GetTimeLineBar(ITickBar tickBar, float lastEndPrice)
        {
            return TimeLineUtils.GetTimeLineBar(tickBar, lastEndPrice);
        }

        internal static TimeLineBar GetTimeLineBar(ITimeLineBar klineBar, ITickBar tickBar, float lastEndPricce)
        {
            return TimeLineUtils.GetTimeLineBar(klineBar, tickBar, lastEndPricce);
        }

        public string Code
        {
            get { return forwardData.Code; }
        }

        public double Time
        {
            get
            {
                return forwardData.CurrentTickData.Time;
            }
        }

        public float Price
        {
            get
            {
                return forwardData.CurrentTickData.Price;
            }
        }

        public double GetNextTime()
        {
            ITickData currentTickData = forwardData.CurrentTickData;
            if (currentTickData.BarPos >= currentTickData.Length - 1)
                return -1;
            return currentTickData.Arr_Time[currentTickData.BarPos + 1];
        }

        public ITickData GetTickData()
        {
            return forwardData.CurrentTickData;
        }

        public IKLineData GetKLineData(KLinePeriod klinePeriod)
        {
            return forwardData.GetKLineData(klinePeriod);
        }

        public void AttachOtherData(string code)
        {
            if (dic_Code_DataForward.ContainsKey(code))
                return;
            int startDate = forwardData.StartDate;
            int endDate = forwardData.EndDate;
            IDataPackage_Code dataPackage_AttachCode = dataCenter.DataPackageFactory.CreateDataPackage_Code(code, startDate, endDate);
            DataForward_AttachCode_Tick dataForward_AttachCode = new DataForward_AttachCode_Tick(dataPackage_AttachCode, this.forwardData.ReferedPeriods);
            this.dic_Code_DataForward.Add(code, dataForward_AttachCode);
            dataForward_AttachCode.ForwardNextDay(forwardData.TradingDay, this.Time);
        }

        public List<string> GetAttachedCodes()
        {
            return dic_Code_DataForward.Keys.ToList<String>();
        }

        public IRealTimeDataReader_Code GetAttachedDataReader(string code)
        {
            if (this.dic_Code_DataForward.ContainsKey(code))
                return this.dic_Code_DataForward[code];
            return null;
        }

        private bool isEnd;

        public bool IsEnd
        {
            get { return isEnd; }
        }

        private bool isDayStart;

        public bool IsDayStart
        {
            get
            {
                return isDayStart;
            }
        }

        private bool isDayEnd;

        public bool IsDayEnd
        {
            get
            {
                return isDayEnd;
            }
        }

        private bool isTradingTimeStart;

        public bool IsTradingTimeStart
        {
            get
            {
                return isTradingTimeStart;
            }
        }

        private bool isTradingTimeEnd;

        public bool IsTradingTimeEnd
        {
            get
            {
                return isTradingTimeEnd;
            }
        }

        /// <summary>
        /// 每次前进的周期
        /// </summary>
        public ForwardPeriod ForwardPeriod
        {
            get
            {
                return forwardPeriod;
            }
        }

        public int StartDate
        {
            get
            {
                return forwardData.StartDate;
            }
        }

        public int EndDate
        {
            get
            {
                return forwardData.EndDate;
            }
        }

        public IDataPackage_Code DataPackage
        {
            get
            {
                return this.forwardData.DataPackage;
            }
        }

        public bool Forward()
        {
            if (isEnd)
                return false;

            ITickData currentTickData = forwardData.CurrentTickData;
            if (currentTickData == null || currentTickData.BarPos + 1 >= currentTickData.Length)
            {
                bool forwardNextDay = ForwardNextDay();
                currentTickData = forwardData.CurrentTickData;
                if (currentTickData != null)
                {
                    if (currentTickData.BarPos + 1 < currentTickData.Length)
                    {
                        isDayEnd = false;
                    }
                    isDayStart = true;
                    this.isTradingTimeStart = true;
                    this.isTradingTimeEnd = false;
                    ForwardNextDay_AttachCode(currentTickData.TradingDay);
                }
                if (forwardNextDay)
                    DealEvents();
                return forwardNextDay;
            }
            else
                isDayStart = false;

            ForwardToday();
            ForwardToday_AttachCode();
            if (currentTickData.BarPos + 1 >= currentTickData.Length)
            {
                isDayEnd = true;
                KLinePeriod[] periods = dic_KLinePeriod_IsEnd.Keys.ToArray<KLinePeriod>();
                for (int i = 0; i < periods.Length; i++)
                {
                    KLinePeriod period = periods[i];
                    if (period.PeriodType < KLineTimeType.DAY || period.Equals(KLinePeriod.KLinePeriod_1Day))
                    {
                        forwardData.GetKLineData(period).ResetCurrentBar();
                        dic_KLinePeriod_IsEnd[periods[i]] = true;
                    }
                }
            }
            else
                isDayEnd = false;
            DealEvents();
            return true;
        }

        private void ForwardToday_AttachCode()
        {
            foreach (string code in dic_Code_DataForward.Keys)
            {
                dic_Code_DataForward[code].ForwardToday(Time);
            }
        }

        private void ForwardNextDay_AttachCode(int tradingDay)
        {
            foreach (string code in dic_Code_DataForward.Keys)
            {
                dic_Code_DataForward[code].ForwardNextDay(tradingDay, Time);
            }
        }

        private void DealEvents()
        {
            if (OnRealTimeChanged != null)
                OnRealTimeChanged(this, new RealTimeChangedArgument(-1, Time, this));
            if (isDayEnd)
            {
                DealEvent_OnTick();
                DealEvent_OnBar();
            }
            else
            {
                DealEvent_OnBar();
                DealEvent_OnTick();
            }
        }

        private void DealEvent_OnTick()
        {
            if (OnTick != null)
                OnTick(this, new ForwardOnTickArgument(forwardData.CurrentTickData, forwardData.CurrentTickData.BarPos, this));
        }

        private void DealEvent_OnBar()
        {
            if (isDayStart)
                return;
            if (OnBar != null && forwardPeriod.KlineForwardPeriod != null)
            {
                bool isForwardPeriodEnd = dic_KLinePeriod_IsEnd[forwardPeriod.KlineForwardPeriod];
                if (isForwardPeriodEnd)
                {
                    barFinishedInfos.Clear();
                    for (int i = 0; i < forwardData.ReferedKLinePeriods.Count; i++)
                    {
                        KLinePeriod period = forwardData.ReferedKLinePeriods[i];
                        bool isPeriodEnd = dic_KLinePeriod_IsEnd[period];
                        if (isPeriodEnd)
                        {
                            IKLineData klineData = forwardData.GetKLineData(period);
                            int barPos = klineData.BarPos - 1;
                            if (IsDayEnd)
                                barPos++;
                            barFinishedInfos.Add(new ForwardOnbar_Info(klineData, barPos));
                        }
                    }
                    //OnBar(this, mainKlineData, mainKlineData.BarPos);
                    OnBar(this, onBarArgument);
                }
            }
        }

        private bool ForwardNextDay()
        {
            int tradingDay = forwardData.GetNextTradingDay();
            if (!forwardData.TradingDayReader.IsTrade(tradingDay))
            {
                isEnd = true;
                return false;
            }

            //this.lastEndPrice = forwardData.CurrentTickData.Arr_Price[forwardData.CurrentTickData.Length - 1];
            ForwardNextDay(forwardData, tradingDay);
            return true;
        }

        internal static void ForwardNextDay(DataForForward_Code forwardData, int tradingDay)
        {
            forwardData.TradingDay = tradingDay;
            foreach (KLinePeriod period in forwardData.ReferedKLinePeriods)
            {
                IKLineData_RealTime klineData = forwardData.GetKLineData(period);
                ForwardNextDay_KLine(forwardData, klineData, period);
            }
            ForwardNextDay_TimeLine(forwardData);
        }

        private static void ForwardNextDay_KLine(DataForForward_Code forwardData, IKLineData_RealTime klineData, KLinePeriod period)
        {
            ITickBar tickBar = forwardData.CurrentTickData.GetCurrentBar();
            int nextbarPos = klineData.BarPos + 1;
            //TODO 这样nextday算法还是不够准确
            //while (!klineData.IsDayStart(nextbarPos))
            //{
            //    nextbarPos = klineData.BarPos + 1;
            //}
            klineData.ChangeCurrentBar(GetKLineBar(tickBar), nextbarPos);
        }

        private static void ForwardNextDay_TimeLine(DataForForward_Code forwardData)
        {
            if (!forwardData.UseTimeLineData)
                return;

            ITimeLineBar timeLineBar = GetTimeLineBar(forwardData.CurrentTickData, forwardData.CurrentTimeLineData.YesterdayEnd);
            forwardData.CurrentTimeLineData.ChangeCurrentBar(timeLineBar);
        }

        private void ForwardToday()
        {
            int prevMainBarPos = mainKlineData.BarPos;
            foreach (KLinePeriod period in forwardData.ReferedKLinePeriods)
            {
                IKLineData_RealTime klineData = forwardData.GetKLineData(period);
                ForwardToday_KLineData(klineData, period);
            }

            ForwardToday_TimeLineData(forwardData.CurrentTimeLineData);
            ITickData_Extend currentTickData = forwardData.CurrentTickData;
            currentTickData.BarPos++;

            if (currentTickData.IsTradingTimeEnd(currentTickData.BarPos))
                this.isTradingTimeEnd = true;
            else
                this.isTradingTimeEnd = false;

            if (currentTickData.IsTradingTimeStart(currentTickData.BarPos))
                this.isTradingTimeStart = true;
            else
                this.isTradingTimeStart = false;
        }

        private void ForwardToday_KLineData(IKLineData_RealTime klineData, KLinePeriod period)
        {
            ITickData_Extend currentTickData = forwardData.CurrentTickData;
            ITickBar nextTickBar = currentTickData.GetBar(currentTickData.BarPos + 1);
            //日线，肯定不会跳到下一个bar
            if (period.Equals(KLinePeriod.KLinePeriod_1Day))
            {
                dic_KLinePeriod_IsEnd[period] = false;
                klineData.ChangeCurrentBar(GetKLineBar(klineData, nextTickBar));
                return;
            }
            double nextTickTime = nextTickBar.Time;
            int nextKLineIndex = FindNextKLineIndex(klineData, nextTickTime);
            if (nextKLineIndex == klineData.BarPos)
            {
                dic_KLinePeriod_IsEnd[period] = false;
                klineData.ChangeCurrentBar(GetKLineBar(klineData, nextTickBar));
            }
            else
            {
                dic_KLinePeriod_IsEnd[period] = true;
                klineData.ChangeCurrentBar(GetKLineBar(nextTickBar), nextKLineIndex);
            }
        }

        private void ForwardKLine_NextPeriod(IKLineData_RealTime klineData, int newBarPos, ITickBar tickBar)
        {
            KLineBar bar = new KLineBar();
            bar.Time = tickBar.Time;
            bar.Start = tickBar.Price;
            bar.High = tickBar.Price;
            bar.Low = tickBar.Price;
            bar.End = tickBar.Price;
            bar.Money = tickBar.Mount * tickBar.Price;
            bar.Mount = tickBar.Mount;
            bar.Hold = tickBar.Hold;
            klineData.BarPos = newBarPos;
            klineData.ChangeCurrentBar(bar);
        }

        private static double GetEndTime(IKLineData_RealTime klineData, int barPos)
        {
            if (klineData.Period.PeriodType >= KLineTimeType.DAY)
                return klineData.Time;
            double endTime = klineData.GetEndTime(barPos);
            if (barPos < klineData.Length - 1 && klineData.IsTradingTimeEnd(barPos))
            {
                endTime = (endTime + klineData.Arr_Time[barPos + 1]) / 2;
            }
            return endTime;
        }

        //private static double GetEndTime(ITimeLineData_RealTime klineData, int barPos)
        //{
        //    //double endTime = klineData.GetEndTime(barPos);
        //    //if (barPos < klineData.Length - 1 && klineData.IsTradingTimeEnd(barPos))
        //    //{
        //    //    endTime = (endTime + klineData.Arr_Time[barPos + 1]) / 2;
        //    //}
        //    //return endTime;
        //    return -1;
        //}

        internal static int FindNextKLineIndex(IKLineData_RealTime klineData, double time)
        {
            int prevBarPos = klineData.BarPos;
            int barPos = prevBarPos;
            if (barPos == klineData.Length - 1)
                return barPos;

            while (barPos < klineData.Length)
            {
                double currentEndTime = GetEndTime(klineData, barPos);
                if (currentEndTime > time)
                {
                    return barPos;
                }
                barPos++;
            }

            return barPos - 1;
        }

        internal static int FindNextTimeLineIndex(ITimeLineData_RealTime timelineData, double time)
        {
            int prevBarPos = timelineData.BarPos;
            int barPos = prevBarPos;
            if (barPos == timelineData.Length - 1)
                return barPos;

            while (barPos < timelineData.Length)
            {
                double startTime = timelineData.Arr_Time[barPos];
                if (barPos != 0 && timelineData.IsTradingTimeStart(barPos))
                    startTime = timelineData.Arr_Time[barPos - 1];
                if (startTime > time)
                {
                    return barPos;
                }
                barPos++;
            }

            return barPos - 1;
        }

        private void ForwardToday_TimeLineData(ITimeLineData_RealTime timeLineData)
        {
            if (timeLineData == null)
                return;
            ITickData_Extend currentTickData = forwardData.CurrentTickData;
            ITickBar nextTickBar = currentTickData.GetBar(currentTickData.BarPos + 1);
            int nextTimeLineBarPos = timeLineData.BarPos + 1;
            if (nextTimeLineBarPos >= timeLineData.Length)
            {
                TimeLineBar timeLineBar = GetTimeLineBar(timeLineData, nextTickBar, timeLineData.YesterdayEnd);
                timeLineData.ChangeCurrentBar(timeLineBar, timeLineData.BarPos);
                return;
            }
            else
            {
                double nextTime = timeLineData.Arr_Time[nextTimeLineBarPos];
                TimeLineBar timeLineBar;
                if (nextTickBar.Time >= nextTime)
                {
                    timeLineBar = GetTimeLineBar(nextTickBar, timeLineData.YesterdayEnd);
                    timeLineData.ChangeCurrentBar(timeLineBar, nextTimeLineBarPos);
                }
                else
                {
                    timeLineBar = GetTimeLineBar(timeLineData, nextTickBar, timeLineData.YesterdayEnd);
                    timeLineData.ChangeCurrentBar(timeLineBar, timeLineData.BarPos);
                }
            }
        }

        public void NavigateTo(double time)
        {
            DataForNavigate_Code dataForNav = DataForNavigate_Code.Create(this.forwardData);
            DataNavigate_Code dataNav = new DataNavigate_Code(dataForNav);
            dataNav.NavigateTo(time);

            if (forwardData.UseTimeLineData)
                this.forwardData.CurrentTimeLineData = (ITimeLineData_RealTime)dataNav.GetTimeLineData();
            if (forwardData.UseTickData)
                this.forwardData.CurrentTickData = (ITickData_Extend)dataNav.GetTickData();

            foreach (KLinePeriod period in this.forwardData.ReferedKLinePeriods)
            {
                this.forwardData.SetKLineData(period, (IKLineData_RealTime)dataNav.GetKLineData(period));
            }
        }

        private System.Timers.Timer timer = new System.Timers.Timer(250);
        //private System.Timers.Timer timer = new System.Timers.Timer(5000);

        private double forwardTime;

        private int mileSecondCount = 0;

        /// <summary>
        /// 自动前进
        /// </summary>
        public void Play()
        {
            //this.Forward();
            this.forwardTime = GetTickData().Time;
            //this.NavigateTo(forwardTime);
            this.timer.Enabled = true;
            this.timer.Start();
            //this.Timer_Elapsed(this, null);
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            ITickData_Extend currentTickData = forwardData.CurrentTickData;
            bool canForward = true;
            if (currentTickData == null)
            {
                this.Pause();
                return;
            }
            int barPos = currentTickData.BarPos;
            int nextBarPos = barPos + 1;
            if (nextBarPos >= currentTickData.Length)
            {
                canForward = this.Forward();
            }
            else
            {
                mileSecondCount++;
                if (mileSecondCount == 4)
                {
                    forwardTime = TimeUtils.AddSeconds(forwardTime, 1);
                    mileSecondCount = 0;
                }
                if (forwardTime >= currentTickData.Arr_Time[nextBarPos])
                {
                    canForward = this.Forward();
                }
            }
            if (!canForward)
                this.Pause();
        }

        /// <summary>
        /// 停止自动前进
        /// </summary>
        public void Pause()
        {
            this.timer.Enabled = false;
            this.forwardTime = -1;
        }

        public ITimeLineData GetTimeLineData()
        {
            return forwardData.CurrentTimeLineData;
        }

        public IKLineData GetKLineData()
        {
            return mainKlineData;
        }

        private Dictionary<KLinePeriod, bool> dic_KLinePeriod_IsEnd = new Dictionary<KLinePeriod, bool>();

        public bool IsPeriodEnd(KLinePeriod klinePeriod)
        {
            bool isPeriodEnd = false;
            dic_KLinePeriod_IsEnd.TryGetValue(klinePeriod, out isPeriodEnd);
            return isPeriodEnd;
        }

        public void Save(XmlElement xmlElem)
        {
            xmlElem.SetAttribute("forwardType", "tick");
            xmlElem.SetAttribute("time", Time.ToString());
            this.forwardPeriod.Save(xmlElem);
            this.forwardData.Save(xmlElem);
            string[] keies = this.dic_Code_DataForward.Keys.ToArray<string>();
            xmlElem.SetAttribute("attachedCodes", ListUtils.ToString(keies));
        }

        public void Load(XmlElement xmlElem)
        {
            double time = double.Parse(xmlElem.GetAttribute("time"));
            this.forwardPeriod = new ForwardPeriod();
            this.forwardPeriod.Load(xmlElem);
            this.forwardData = new DataForForward_Code(dataCenter);
            this.forwardData.Load(xmlElem);
            this.Init();
            this.NavigateTo(time);
        }

        /// <summary>
        /// 
        /// </summary>
        public event DelegateOnTick OnTick;

        /// <summary>
        /// 
        /// </summary>
        public event DelegateOnBar OnBar;

        public event DelegateOnRealTimeChanged OnRealTimeChanged;
    }

    class DataForward_AttachCode_Tick : IRealTimeDataReader_Code
    {
        private double time;

        private IDataPackage_Code dataPackage;

        private ForwardReferedPeriods referedPeriods;

        private DataForForward_Code dataForForward;

        public event DelegateOnRealTimeChanged OnRealTimeChanged;

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
                return dataForForward.CurrentTickData.Price;
            }
        }

        public DataForward_AttachCode_Tick(IDataPackage_Code dataPackage, ForwardReferedPeriods referedPeriods)
        {
            this.dataPackage = dataPackage;
            this.referedPeriods = referedPeriods;
            this.dataForForward = new DataForForward_Code(dataPackage, referedPeriods);
        }

        public void ForwardToday(double time)
        {
            this.time = time;

            int prevTickIndex = dataForForward.CurrentTickData.BarPos;
            ForwardToday_Tick(dataForForward.CurrentTickData, time);
            foreach (KLinePeriod period in dataForForward.ReferedKLinePeriods)
            {
                IKLineData_RealTime klineData = dataForForward.GetKLineData(period);
                ForwardToday_KLine(klineData, time, dataForForward.CurrentTickData, prevTickIndex);
            }
            ForwardToday_TimeLine(dataForForward.CurrentTimeLineData, time, dataForForward.CurrentTickData, prevTickIndex);
        }

        private void ForwardToday_Tick(ITickData tickData, double time)
        {
            tickData.BarPos = FindNextTickIndex(tickData, tickData.BarPos, time);
        }

        private int FindNextTickIndex(ITickData tickData, int startTickIndex, double time)
        {
            int barPos = tickData.BarPos;
            while (barPos < tickData.Length && tickData.Arr_Time[barPos] < time)
            {
                barPos++;
            }
            if (barPos == tickData.Length)
                barPos--;
            return barPos;
        }

        private void ForwardToday_KLine(IKLineData_RealTime klineData, double time, ITickData tickData, int prevTickIndex)
        {
            if (klineData.Period.Equals(KLinePeriod.KLinePeriod_1Day))
            {
                klineData.ChangeCurrentBar(KLineUtils.GetKLineBar(klineData, tickData, prevTickIndex, tickData.BarPos));
                return;
            }

            int nextKLineIndex = DataForward_Code_Tick.FindNextKLineIndex(klineData, time);
            if (nextKLineIndex == klineData.BarPos)
            {
                klineData.ChangeCurrentBar(KLineUtils.GetKLineBar(klineData, tickData, prevTickIndex, tickData.BarPos));
            }
            else
            {
                double periodStartTime = klineData.Arr_Time[nextKLineIndex];
                int startTickIndex = FindNextTickIndex(tickData, prevTickIndex, time);
                klineData.ChangeCurrentBar(KLineUtils.GetKLineBar(tickData, startTickIndex, tickData.BarPos), nextKLineIndex);
            }
        }

        private void ForwardToday_TimeLine(ITimeLineData_RealTime timeLineData, double time, ITickData tickData, int prevTickIndex)
        {
            if (timeLineData == null)
                return;
            int nextTimeLineIndex = DataForward_Code_Tick.FindNextTimeLineIndex(timeLineData, time);
            if (nextTimeLineIndex == timeLineData.BarPos)
            {
                timeLineData.ChangeCurrentBar(TimeLineUtils.GetTimeLineBar(timeLineData, tickData, prevTickIndex, tickData.BarPos, timeLineData.YesterdayEnd));
            }
            else
            {
                double periodStartTime = timeLineData.Arr_Time[nextTimeLineIndex];
                int startTickIndex = FindNextTickIndex(tickData, prevTickIndex, time);
                timeLineData.ChangeCurrentBar(TimeLineUtils.GetTimeLineBar(tickData, startTickIndex, tickData.BarPos, timeLineData.YesterdayEnd), nextTimeLineIndex);
            }
        }

        public void ForwardNextDay(int tradingDay, double time)
        {
            DataForward_Code_Tick.ForwardNextDay(this.dataForForward, tradingDay);
        }

        public IKLineData GetKLineData(KLinePeriod period)
        {
            return dataForForward.GetKLineData(period);
        }

        public bool IsPeriodEnd(KLinePeriod period)
        {
            return false;
        }

        public ITimeLineData GetTimeLineData()
        {
            return dataForForward.CurrentTimeLineData;
        }

        public ITickData GetTickData()
        {
            return dataForForward.CurrentTickData;
        }
    }
}