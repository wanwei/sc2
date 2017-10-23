using com.wer.sc.data;
using com.wer.sc.data.datapackage;
using com.wer.sc.data.navigate;
using com.wer.sc.data.reader;
using com.wer.sc.data.realtime;
using com.wer.sc.data.transfer;
using com.wer.sc.data.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace com.wer.sc.data.forward
{
    /// <summary>
    /// 该类用于数据的tick前进
    /// </summary>
    internal class HistoryDataForward_Code_TickForward : IDataForward_Code
    {
        private DataForForward_Code forwardData;

        private float lastEndPrice;

        private ForwardPeriod forwardPeriod;

        private IKLineData_RealTime mainKlineData;

        private List<ForwardOnbar_Info> barFinishedInfos = new List<ForwardOnbar_Info>();

        private ForwardOnBarArgument onBarArgument;

        private IDataPackage_Code dataPackage;
        public HistoryDataForward_Code_TickForward(IDataPackage_Code dataPackage, ForwardReferedPeriods referedPeriods, ForwardPeriod forwardPeriod)
        {
            this.dataPackage = dataPackage;
            this.forwardData = new DataForForward_Code(dataPackage, referedPeriods);
            this.forwardPeriod = forwardPeriod;

            this.onBarArgument = new ForwardOnBarArgument(barFinishedInfos);
            Init();
        }

        private void Init()
        {
            InitData();
            timer.Elapsed += Timer_Elapsed;
            timer.AutoReset = true;
        }

        private void InitData()
        {
            forwardData.TradingDay = forwardData.StartDate;
            foreach (KLinePeriod period in forwardData.ReferedKLinePeriods)
            {
                IKLineData_RealTime klineData = forwardData.GetKLineData(period);
                klineData.ChangeCurrentBar(GetKLineBar(forwardData.CurrentTickData));
                if (period.Equals(forwardPeriod.KlineForwardPeriod))
                    this.mainKlineData = klineData;
            }
            if (this.forwardData.UseTimeLineData)
            {
                lastEndPrice = dataPackage.GetLastEndPrice(forwardData.StartDate);
                this.forwardData.CurrentTimeLineData.ChangeCurrentBar(GetTimeLineBar(forwardData.CurrentTickData, lastEndPrice));
            }
        }

        private static KLineBar GetKLineBar(ITickBar tickBar)
        {
            return KLineUtils.GetKLineBar(tickBar);
        }

        private static KLineBar GetKLineBar(IKLineBar klineBar, ITickBar tickBar)
        {
            return KLineUtils.GetKLineBar(klineBar, tickBar);
        }

        private static TimeLineBar GetTimeLineBar(ITickBar tickBar, float lastEndPrice)
        {
            return TimeLineUtils.GetTimeLineBar(tickBar, lastEndPrice);
        }

        private static TimeLineBar GetTimeLineBar(ITimeLineBar klineBar, ITickBar tickBar, float lastEndPricce)
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

        public void AttachOtherData(string code, ForwardReferedPeriods referedPeriods)
        {

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

        public IRealTimeDataReader AttachedDataReader
        {
            get
            {
                return null;
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
                }
                if (forwardNextDay)
                    DealEvents();
                return forwardNextDay;
            }
            else
                isDayStart = false;

            ForwardToday();
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
                OnTick(this, new ForwardOnTickArgument(forwardData.CurrentTickData, forwardData.CurrentTickData.BarPos));
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

            this.lastEndPrice = forwardData.CurrentTickData.Arr_Price[forwardData.CurrentTickData.Length - 1];
            this.forwardData.TradingDay = tradingDay;

            foreach (KLinePeriod period in forwardData.ReferedKLinePeriods)
            {
                IKLineData_RealTime klineData = forwardData.GetKLineData(period);
                ForwardNextDay_KLine(klineData, period);
            }

            ForwardNextDay_TimeLine();
            return true;
        }

        private void ForwardNextDay_KLine(IKLineData_RealTime klineData, KLinePeriod period)
        {
            //if (period.PeriodType >= KLineTimeType.DAY)
            //{
            //    double day = navigateData.CurrentTickData.TradingDay;
            //    int nextKLineIndex = FindNextKLineIndex(klineData, day);
            //    if (nextKLineIndex != klineData.BarPos)
            //    {
            //        dic_KLinePeriod_IsEnd[period] = true;
            //        klineData.ChangeCurrentBar(GetKLineBar(navigateData.CurrentTickData), nextKLineIndex);
            //    }
            //    else
            //    {
            //        dic_KLinePeriod_IsEnd[period] = false;
            //        klineData.ChangeCurrentBar(GetKLineBar(klineData, navigateData.CurrentTickData));
            //    }
            //    return;
            //}

            ITickBar tickBar = forwardData.CurrentTickData.GetCurrentBar();
            int nextbarPos = klineData.BarPos + 1;
            klineData.ChangeCurrentBar(GetKLineBar(tickBar), nextbarPos);
        }

        private void ForwardNextDay_TimeLine()
        {
            if (!forwardData.UseTimeLineData)
                return;

            ITimeLineBar timeLineBar = GetTimeLineBar(forwardData.CurrentTickData, lastEndPrice);
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

        private double GetEndTime(IKLineData_RealTime klineData, int barPos)
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

        private int FindNextKLineIndex(IKLineData_RealTime klineData, double time)
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

            //if (barPos >= klineData.Length)
            return barPos - 1;
            //if (barPos != prevBarPos && klineData.Arr_Time[barPos] != time)
            //    barPos--;

            //return barPos < 0 ? 0 : barPos;
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
                TimeLineBar timeLineBar = GetTimeLineBar(timeLineData, nextTickBar, lastEndPrice);
                timeLineData.ChangeCurrentBar(timeLineBar, timeLineData.BarPos);
                return;
            }
            else
            {
                double nextTime = timeLineData.Arr_Time[nextTimeLineBarPos];
                TimeLineBar timeLineBar;
                if (nextTickBar.Time >= nextTime)
                {
                    timeLineBar = GetTimeLineBar(nextTickBar, lastEndPrice);
                    timeLineData.ChangeCurrentBar(timeLineBar, nextTimeLineBarPos);
                }
                else
                {
                    timeLineBar = GetTimeLineBar(timeLineData, nextTickBar, lastEndPrice);
                    timeLineData.ChangeCurrentBar(timeLineBar, timeLineData.BarPos);
                }
            }
        }

        public void NavigateTo(double time)
        {
            //TODO IDataNavigate_Code返回的是ITickData，需要变成ITickData_Extend
            //DataNavigate_Code dataNav = new DataNavigate_Code(this.DataPackage, time);
            //if (navigateData.UseTimeLineData)
            //    this.navigateData.CurrentTimeLineData = (TimeLineData_RealTime)dataNav.GetTimeLineData();
            //if (navigateData.UseTickData)
            //    this.navigateData.CurrentTickData = dataNav.GetTickData_Extend();

            //KLinePeriod[] periods = this.dic_Period_KLineData.Keys.ToArray();
            //for (int i = 0; i < periods.Length; i++)
            //{
            //    KLinePeriod period = periods[i];
            //    this.dic_Period_KLineData[period] = (IKLineData_RealTime)dataNav.GetKLineData(period);
            //}
        }

        public event DelegateOnNavigateTo OnNavigateTo;

        private System.Timers.Timer timer = new System.Timers.Timer(250);

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
}