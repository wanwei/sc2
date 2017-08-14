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

namespace com.wer.sc.data.forward.impl
{
    /// <summary>
    /// 该类用于K线数据
    /// </summary>
    public class HistoryDataForward_Code_TickPeriod : IHistoryDataForward_Code
    {
        private IDataPackage dataPackage;

        private IList<int> tradingDays;

        private Dictionary<KLinePeriod, KLineData_RealTime> dic_Period_KLineData = new Dictionary<KLinePeriod, KLineData_RealTime>();

        private TickData currentTickData;

        private int currentTradingDayIndex = 0;

        private Dictionary<KLinePeriod, KLineData_DaySplitter> dic_Period_DaySplitter = new Dictionary<KLinePeriod, KLineData_DaySplitter>();

        private ForwardPeriod forwardPeriod;

        private KLineData_RealTime mainKlineData;

        private TimeLineData_RealTime currentTimeLineData;

        private float lastEndPrice;

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="mainKLineData"></param>
        /// <param name="allKLineData"></param>
        //public HistoryDataForward_Code_TickPeriod(IDataReader dataReader, string code, Dictionary<KLinePeriod, KLineData_RealTime> allKLineData, IList<int> tradingDays, KLinePeriod forwardPeriod)
        //{
        //    this.dic_Period_KLineData = allKLineData;
        //    this.dataReader = dataReader;
        //    this.code = code;
        //    this.tradingDays = tradingDays;
        //    this.forwardPeriod = new ForwardPeriod(true, forwardPeriod);

        //    InitDaySplitter(dataReader, code);
        //    InitData();

        //    timer.Elapsed += Timer_Elapsed;
        //    timer.AutoReset = true;
        //}

        public HistoryDataForward_Code_TickPeriod(IDataReader dataReader, string code, Dictionary<KLinePeriod, KLineData_RealTime> allKLineData, IList<int> tradingDays, KLinePeriod forwardPeriod)
        {
            IDataPackage dataPackage = DataPackageFactory.CreateDataPackage(dataReader, code, tradingDays[0], tradingDays[tradingDays.Count - 1]);
            Init(dataPackage, allKLineData, tradingDays, forwardPeriod);
        }

        public HistoryDataForward_Code_TickPeriod(IDataPackage dataPackage, Dictionary<KLinePeriod, KLineData_RealTime> allKLineData, IList<int> tradingDays, KLinePeriod forwardPeriod)
        {
            Init(dataPackage, allKLineData, tradingDays, forwardPeriod);
        }

        private void Init(IDataPackage dataPackage, Dictionary<KLinePeriod, KLineData_RealTime> allKLineData, IList<int> tradingDays, KLinePeriod forwardPeriod)
        {
            this.dataPackage = dataPackage;
            this.dic_Period_KLineData = allKLineData;
            this.tradingDays = tradingDays;
            this.forwardPeriod = new ForwardPeriod(true, forwardPeriod);

            InitDaySplitter(dataPackage, dataPackage.Code);
            InitData();

            timer.Elapsed += Timer_Elapsed;
            timer.AutoReset = true;
        }

        private void InitDaySplitter(IDataPackage dataReader, string code)
        {
            ITradingSessionReader_Instrument sessionReader = dataReader.GetTradingSessionReader();
            this.dic_Period_DaySplitter = new Dictionary<KLinePeriod, KLineData_DaySplitter>();
            foreach (KLinePeriod period in dic_Period_KLineData.Keys)
            {
                KLineData_RealTime klineData = dic_Period_KLineData[period];
                KLineData_DaySplitter daySplitter = new KLineData_DaySplitter(klineData, sessionReader);
                dic_Period_DaySplitter.Add(period, daySplitter);
            }
        }

        private void InitData()
        {
            int currentTradingDay = tradingDays[0];
            this.currentTickData = (TickData)dataPackage.GetTickData(currentTradingDay);
            foreach (KLinePeriod period in dic_Period_KLineData.Keys)
            {
                KLineData_RealTime klineData = dic_Period_KLineData[period];
                klineData.SetRealTimeData(GetKLineBar(currentTickData));
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

        private bool isDayEnd;

        public bool IsDayEnd
        {
            get
            {
                return isDayEnd;
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

        public IDataPackage DataPackage
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
                }
                DealEvents();
                return forwardNextDay;
            }

            ForwardToday();
            if (currentTickData.BarPos + 1 >= currentTickData.Length)
            {
                isDayEnd = true;
                KLinePeriod[] periods = dic_KLinePeriod_IsEnd.Keys.ToArray<KLinePeriod>();
                for (int i = 0; i < periods.Length; i++)
                {
                    KLinePeriod period = periods[i];
                    if (period.PeriodType < KLineTimeType.DAY)
                    {
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
            if (OnTick != null)
                OnTick(this, currentTickData, currentTickData.BarPos);
            if (OnBar != null && forwardPeriod.KlineForwardPeriod != null)
            {
                bool isForwardPeriodEnd = dic_KLinePeriod_IsEnd[forwardPeriod.KlineForwardPeriod];
                if (isForwardPeriodEnd)
                    OnBar(this, mainKlineData, mainKlineData.BarPos);
            }
        }

        private bool ForwardNextDay()
        {
            TickData tickData = ForwardTickData();
            if (tickData == null)
            {
                isEnd = true;
                return false;
            }
            this.lastEndPrice = currentTickData.Arr_Price[currentTickData.Length - 1];
            this.currentTickData = tickData;
            this.currentTimeLineData = GetTodayTimeLineData();

            foreach (KLinePeriod period in dic_Period_KLineData.Keys)
            {
                KLineData_RealTime klineData = dic_Period_KLineData[period];
                ForwardNextDay_KLine(klineData, period);
            }
            return true;
        }

        private TickData ForwardTickData()
        {
            this.currentTradingDayIndex++;
            if (this.currentTradingDayIndex >= tradingDays.Count)
                return null;
            int currentTradingDay = tradingDays[currentTradingDayIndex];
            return (TickData)dataPackage.GetTickData(currentTradingDay);
        }

        private TimeLineData_RealTime GetTodayTimeLineData()
        {
            int currentTradingDay = tradingDays[currentTradingDayIndex];
            TimeLineData timeLineData = (TimeLineData)dataPackage.GetTimeLineData(currentTradingDay);
            return new TimeLineData_RealTime(timeLineData);
        }

        private void ForwardNextDay_KLine(KLineData_RealTime klineData, KLinePeriod period)
        {
            if (period.PeriodType >= KLineTimeType.DAY)
            {
                double day = currentTickData.TradingDay;
                int nextKLineIndex = FindNextKLineIndex(klineData, day);
                if (nextKLineIndex != klineData.BarPos)
                {
                    dic_KLinePeriod_IsEnd[period] = true;
                    klineData.SetRealTimeData(GetKLineBar(currentTickData), nextKLineIndex);
                }
                else
                {
                    dic_KLinePeriod_IsEnd[period] = false;
                    klineData.SetRealTimeData(GetKLineBar(klineData, currentTickData));
                }
                return;
            }

            ITickBar tickBar = currentTickData.GetCurrentBar();
            KLineData_DaySplitter daySplitter = dic_Period_DaySplitter[period];
            daySplitter.NextDay();
            //klineData.BarPos = daySplitter.CurrentDayKLineIndex;
            ForwardKLine_NextPeriod(klineData, daySplitter.CurrentDayKLineIndex, tickBar);
        }

        private void ForwardToday()
        {
            foreach (KLinePeriod period in dic_Period_KLineData.Keys)
            {
                KLineData_RealTime klineData = dic_Period_KLineData[period];
                ForwardToday_KLineData(klineData, period);
            }
            ForwardToday_TimeLineData(currentTimeLineData);
            currentTickData.BarPos++;
        }

        private void ForwardToday_KLineData(KLineData_RealTime klineData, KLinePeriod period)
        {
            ITickBar nextTickBar = currentTickData.GetBar(currentTickData.BarPos + 1);
            //日线，肯定不会跳到下一个bar
            if (period == KLinePeriod.KLinePeriod_1Day)
            {
                dic_KLinePeriod_IsEnd[period] = false;
                klineData.SetRealTimeData(GetKLineBar(klineData, nextTickBar));
                return;
            }
            double time = nextTickBar.Time;
            int nextKLineIndex = FindNextKLineIndex(klineData, time);
            if (nextKLineIndex == klineData.BarPos)
            {
                dic_KLinePeriod_IsEnd[period] = false;
                klineData.SetRealTimeData(GetKLineBar(klineData, nextTickBar));
            }
            else
            {
                dic_KLinePeriod_IsEnd[period] = true;
                klineData.SetRealTimeData(GetKLineBar(nextTickBar), nextKLineIndex);
            }
        }

        private void ForwardKLine_NextPeriod(KLineData_RealTime klineData, int newBarPos, ITickBar tickBar)
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
            klineData.SetRealTimeData(bar);
        }

        private int FindNextKLineIndex(KLineData_RealTime klineData, double time)
        {
            int barPos = klineData.BarPos;
            while (barPos < klineData.Length && klineData.Arr_Time[barPos] < time)
            {
                barPos++;
            }
            if (barPos >= klineData.Length)
                return barPos - 1;
            if (barPos > 0 && klineData.Arr_Time[barPos] != time)
                barPos--;

            return barPos < 0 ? 0 : barPos;
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
            IDataNavigate_Code dataNav = DataNavigateFactory.CreateDataNavigate(dataPackage, time);
            this.currentTimeLineData = (TimeLineData_RealTime)dataNav.GetTimeLineData();
            this.currentTickData = (TickData)dataNav.GetTickData();
            KLinePeriod[] periods = this.dic_Period_KLineData.Keys.ToArray();
            for (int i = 0; i < periods.Length; i++)
            {
                KLinePeriod period = periods[i];
                this.dic_Period_KLineData[period] = (KLineData_RealTime)dataNav.GetKLineData(period);
            }
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
            return null;
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
    }
}
