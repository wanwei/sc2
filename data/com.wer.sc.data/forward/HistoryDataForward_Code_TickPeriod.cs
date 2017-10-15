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
    internal class HistoryDataForward_Code_TickPeriod : IHistoryDataForward_Code
    {
        private IDataPackage_Code dataPackage;

        private IList<KLinePeriod> allKLinePeriods;

        private IList<int> tradingDays;

        private Dictionary<KLinePeriod, IKLineData_RealTime> dic_Period_KLineData = new Dictionary<KLinePeriod, IKLineData_RealTime>();

        private ITickData_Extend currentTickData;

        private int currentTradingDayIndex = 0;

        private ForwardPeriod forwardPeriod;

        private IKLineData_RealTime mainKlineData;

        private TimeLineData_RealTime currentTimeLineData;

        private float lastEndPrice;

        private List<ForwardOnbar_Info> barFinishedInfos = new List<ForwardOnbar_Info>();

        private ForwardOnBarArgument onBarArgument;

        public HistoryDataForward_Code_TickPeriod(IDataPackage_Code dataPackage, IList<KLinePeriod> periods, KLinePeriod forwardPeriod)
        {
            this.allKLinePeriods = periods;
            Dictionary<KLinePeriod, IKLineData_RealTime> allKLineData = dataPackage.CreateKLineData_RealTimes(periods);
            IList<int> tradingDays = dataPackage.GetTradingDays();
            this.onBarArgument = new ForwardOnBarArgument(barFinishedInfos);
            Init(dataPackage, allKLineData, tradingDays, forwardPeriod);
        }

        private void Init(IDataPackage_Code dataPackage, Dictionary<KLinePeriod, IKLineData_RealTime> allKLineData, IList<int> tradingDays, KLinePeriod forwardPeriod)
        {
            this.dataPackage = dataPackage;
            this.dic_Period_KLineData = allKLineData;
            this.tradingDays = tradingDays;
            this.forwardPeriod = new ForwardPeriod(true, forwardPeriod);
            InitData();

            timer.Elapsed += Timer_Elapsed;
            timer.AutoReset = true;
        }

        private void InitData()
        {
            int currentTradingDay = tradingDays[0];
            this.currentTickData = dataPackage.GetTickData(currentTradingDay);
            foreach (KLinePeriod period in dic_Period_KLineData.Keys)
            {
                IKLineData_RealTime klineData = dic_Period_KLineData[period];
                klineData.ChangeCurrentBar(GetKLineBar(currentTickData));
                if (period == forwardPeriod.KlineForwardPeriod)
                    mainKlineData = klineData;
            }

            //初始化分时线
            //int lastTradingDay = dataReader.TradingDayReader.GetPrevTradingDay(currentTradingDay);
            //IKLineData lastDayklineData = dataReader.KLineDataReader.GetData(code, lastTradingDay, lastTradingDay, KLinePeriod.KLinePeriod_1Day);
            //if (lastDayklineData.Length == 0)
            //    lastEndPrice = currentTickData.Arr_Price[0];
            //else
            //    lastEndPrice = lastDayklineData.End;
            this.lastEndPrice = dataPackage.GetLastEndPrice(currentTradingDay);
            ITimeLineData timeLineData = dataPackage.GetTimeLineData(currentTradingDay);
            this.currentTimeLineData = new TimeLineData_RealTime(timeLineData);
            this.currentTimeLineData.SetRealTimeData(GetTimeLineBar(currentTickData, lastEndPrice));
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
            get { return dataPackage.Code; }
        }

        public double Time
        {
            get
            {
                return currentTickData.Time;
            }
        }

        public ITickData GetTickData()
        {
            return currentTickData;
        }

        public IKLineData GetKLineData(KLinePeriod klinePeriod)
        {
            if (dic_Period_KLineData.ContainsKey(klinePeriod))
                return dic_Period_KLineData[klinePeriod];
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
                return tradingDays[0];
            }
        }

        public int EndDate
        {
            get
            {
                return tradingDays[tradingDays.Count - 1];
            }
        }

        public IDataPackage_Code DataPackage
        {
            get
            {
                return this.dataPackage;
            }
        }

        public bool Forward()
        {
            if (isEnd)
                return false;

            if (currentTickData == null || currentTickData.BarPos + 1 >= currentTickData.Length)
            {
                bool forwardNextDay = ForwardNextDay();
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
                    if (period.PeriodType < KLineTimeType.DAY || period == KLinePeriod.KLinePeriod_1Day)
                    {
                        dic_Period_KLineData[period].ResetCurrentBar();
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
                OnTick(this, currentTickData, currentTickData.BarPos);
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
                    for (int i = 0; i < allKLinePeriods.Count; i++)
                    {
                        KLinePeriod period = allKLinePeriods[i];
                        bool isPeriodEnd = dic_KLinePeriod_IsEnd[period];
                        if (isPeriodEnd)
                        {
                            IKLineData klineData = dic_Period_KLineData[period];
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
            ITickData_Extend tickData = ForwardTickData();
            if (tickData == null)
            {
                isEnd = true;
                return false;
            }
            this.lastEndPrice = currentTickData.Arr_Price[currentTickData.Length - 1];
            this.currentTickData = tickData;

            foreach (KLinePeriod period in dic_Period_KLineData.Keys)
            {
                IKLineData_RealTime klineData = dic_Period_KLineData[period];
                ForwardNextDay_KLine(klineData, period);
            }
            ForwardNextDay_TimeLine();
            return true;
        }

        private ITickData_Extend ForwardTickData()
        {
            this.currentTradingDayIndex++;
            if (this.currentTradingDayIndex >= tradingDays.Count)
                return null;
            int currentTradingDay = tradingDays[currentTradingDayIndex];
            return dataPackage.GetTickData(currentTradingDay);
        }

        private void ForwardNextDay_KLine(IKLineData_RealTime klineData, KLinePeriod period)
        {
            if (period.PeriodType >= KLineTimeType.DAY)
            {
                double day = currentTickData.TradingDay;
                int nextKLineIndex = FindNextKLineIndex(klineData, day);
                if (nextKLineIndex != klineData.BarPos)
                {
                    dic_KLinePeriod_IsEnd[period] = true;
                    klineData.ChangeCurrentBar(GetKLineBar(currentTickData), nextKLineIndex);
                }
                else
                {
                    dic_KLinePeriod_IsEnd[period] = false;
                    klineData.ChangeCurrentBar(GetKLineBar(klineData, currentTickData));
                }
                return;
            }

            ITickBar tickBar = currentTickData.GetCurrentBar();
            int nextbarPos = klineData.BarPos + 1;
            klineData.ChangeCurrentBar(GetKLineBar(tickBar), nextbarPos);
        }

        private void ForwardNextDay_TimeLine()
        {
            int currentTradingDay = currentTickData.TradingDay;
            TimeLineData timeLineData = (TimeLineData)dataPackage.GetTimeLineData(currentTradingDay);

            float lastEndPrice = this.currentTimeLineData.Arr_Price[currentTimeLineData.Length - 1];
            TimeLineData_RealTime timeLineData_RealTime = new TimeLineData_RealTime(timeLineData);
            timeLineData_RealTime.SetRealTimeData(GetTimeLineBar(currentTickData, lastEndPrice));
            this.currentTimeLineData = timeLineData_RealTime;
        }

        private void ForwardToday()
        {
            int prevMainBarPos = mainKlineData.BarPos;
            foreach (KLinePeriod period in dic_Period_KLineData.Keys)
            {
                IKLineData_RealTime klineData = dic_Period_KLineData[period];
                ForwardToday_KLineData(klineData, period);
            }
            ForwardToday_TimeLineData(currentTimeLineData);
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
            ITickBar nextTickBar = currentTickData.GetBar(currentTickData.BarPos + 1);
            //日线，肯定不会跳到下一个bar
            if (period == KLinePeriod.KLinePeriod_1Day)
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
            double endTime = klineData.GetEndTime(barPos);
            if (klineData.IsTradingTimeEnd(barPos))
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

        private void ForwardToday_TimeLineData(TimeLineData_RealTime timeLineData)
        {
            ITickBar nextTickBar = currentTickData.GetBar(currentTickData.BarPos + 1);
            int nextTimeLineBarPos = timeLineData.BarPos + 1;
            if (nextTimeLineBarPos >= timeLineData.Length)
            {
                TimeLineBar timeLineBar = GetTimeLineBar(timeLineData, nextTickBar, lastEndPrice);
                timeLineData.SetRealTimeData(timeLineBar, timeLineData.BarPos);
                return;
            }
            else
            {
                double nextTime = timeLineData.Arr_Time[nextTimeLineBarPos];
                TimeLineBar timeLineBar;
                if (nextTickBar.Time >= nextTime)
                {
                    timeLineBar = GetTimeLineBar(nextTickBar, lastEndPrice);
                    timeLineData.SetRealTimeData(timeLineBar, nextTimeLineBarPos);
                }
                else
                {
                    timeLineBar = GetTimeLineBar(timeLineData, nextTickBar, lastEndPrice);
                    timeLineData.SetRealTimeData(timeLineBar, timeLineData.BarPos);
                }
            }
        }

        public void NavigateTo(double time)
        {
            //TODO IDataNavigate_Code返回的是ITickData，需要变成ITickData_Extend
            //IDataNavigate_Code dataNav = DataNavigateFactory.CreateDataNavigate(dataPackage, time);
            //this.currentTimeLineData = (TimeLineData_RealTime)dataNav.GetTimeLineData();
            //ITickData tickData = dataNav.GetTickData();

            //this.currentTickData = (TickData)dataNav.GetTickData();
            //KLinePeriod[] periods = this.dic_Period_KLineData.Keys.ToArray();
            //for (int i = 0; i < periods.Length; i++)
            //{
            //    KLinePeriod period = periods[i];
            //    this.dic_Period_KLineData[period] = (IKLineData_RealTime)dataNav.GetKLineData(period);
            //}
        }

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
            bool canForward = true;
            if (this.currentTickData == null)
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
            return currentTimeLineData;
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