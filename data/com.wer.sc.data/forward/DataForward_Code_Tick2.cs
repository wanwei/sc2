using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using com.wer.sc.data.datapackage;
using com.wer.sc.data.reader;
using com.wer.sc.data.forward.utils;
using com.wer.sc.data.utils;
using com.wer.sc.utils;
using com.wer.sc.data.navigate;
using System.Timers;

namespace com.wer.sc.data.forward
{
    public class DataForward_Code_Tick2 : IDataForward_Code
    {
        private bool isInit = false;

        private IDataCenter dataCenter;
        private IDataPackage_Code dataPackage;
        private ForwardReferedPeriods referedPeriods;
        private ForwardPeriod forwardPeriod;

        private DataForForward_Code dataForForward_Code;
        private ForwardDataIndeier forwardDataIndeier;

        private Dictionary<string, DataForward_AttachCode_Tick> dic_Code_DataForward = new Dictionary<string, DataForward_AttachCode_Tick>();

        private Dictionary<KLinePeriod, bool> dic_KLinePeriod_IsEnd = new Dictionary<KLinePeriod, bool>();
        private bool isTimeLineEnd = false;

        private IForwardOnBarArgument onBarArgument;
        private List<IForwardKLineBarInfo> barFinishedInfos = new List<IForwardKLineBarInfo>();

        public DataForward_Code_Tick2(IDataCenter dataCenter)
        {
            this.dataCenter = dataCenter;
        }

        public DataForward_Code_Tick2(IDataCenter dataCenter, IDataPackage_Code dataPackage, ForwardReferedPeriods referedPeriods, ForwardPeriod forwardPeriod)
        {
            this.dataCenter = dataCenter;
            this.dataPackage = dataPackage;
            this.referedPeriods = referedPeriods;
            this.forwardPeriod = forwardPeriod;
            this.dataForForward_Code = new DataForForward_Code(dataPackage, referedPeriods);
        }

        public bool Forward()
        {
            if (!isInit)
            {
                PrepareData();
                isInit = true;
                return true;
            }
            if (isEnd)
                return false;

            //ITickData currentTickData = dataForForward_Code.CurrentTickData;
            if (IsDayEnd)
            {
                return Forward_NextDay();
            }
            ForwardToday();
            return true;
        }

        private bool IsCurrentDayEnd()
        {
            ITickData currentTickData = dataForForward_Code.CurrentTickData;
            return currentTickData == null || currentTickData.BarPos + 1 >= currentTickData.Length;
        }

        private void PrepareData()
        {
            this.dataForForward_Code.TradingDay = dataPackage.StartDate;
            this.forwardDataIndeier = new ForwardDataIndeier(dataForForward_Code);
            this.onBarArgument = new ForwardOnBarArgument(barFinishedInfos, this);
            foreach (KLinePeriod period in referedPeriods.UsedKLinePeriods)
            {
                IKLineData_RealTime klineData = this.dataForForward_Code.GetKLineData(period);
                klineData.ChangeCurrentBar(KLineUtils.GetKLineBar(dataForForward_Code.CurrentTickData));
            }
            if (dataForForward_Code.UseTimeLineData)
            {
                ITimeLineData_RealTime timeLineData = dataForForward_Code.CurrentTimeLineData;
                timeLineData.ChangeCurrentBar(TimeLineUtils.GetTimeLineBar(dataForForward_Code.CurrentTickData, timeLineData.YesterdayEnd));
            }
        }

        #region forwardToday

        private bool ForwardToday()
        {
            /*
             * Forward顺序：
             * 1.更新tick数据，前进1格
             * 2.更新mainkline数据，确定是否完成了当前bar
             * 3.更新其它的kline数据，根据主K线数据确认是否完成当前bar
             * 4.更新分时线数据
             * 5.如果发现至少一个主周期内没有出现tick数据，则将前进K线数据
             */
            if (isTradingTimeEnd)
                isTradingTimeStart = true;
            else
                isTradingTimeStart = false;
            ForwardTickData();

            int lastMainPosIfFinished;
            int firstMainPosIfFinished = GetMainKLineForwardBarPos(out lastMainPosIfFinished);
            ForwardKLine(firstMainPosIfFinished);
            ForwardToday_TimeLineData(firstMainPosIfFinished);

            if (IsCurrentTradingTimeEnd())
                isTradingTimeEnd = true;
            else
                isTradingTimeEnd = false;
            isDayStart = false;
            if (IsCurrentDayEnd())
                isDayEnd = true;
            DealEvent_Today();

            //处理有1分钟都没有tick数据的情况
            if (lastMainPosIfFinished > 0 && lastMainPosIfFinished > firstMainPosIfFinished)
            {

            }
            return false;
        }

        private void ForwardKLine(int firstMainPosIfFinished)
        {
            bool isMainBarFinished = firstMainPosIfFinished >= 0;
            ForwardKLine(dataForForward_Code.MainKLine, isMainBarFinished, IsPeriodEnd(dataForForward_Code.MainKLinePeriod));

            foreach (KLinePeriod period in dataForForward_Code.ReferedKLinePeriods)
            {
                IKLineData_RealTime klineData = dataForForward_Code.GetKLineData(period);
                if (klineData.Period.Equals(dataForForward_Code.MainKLinePeriod))
                    continue;
                int klineBarPos = forwardDataIndeier.GetOtherKLineBarPosIfFinished(firstMainPosIfFinished, period);
                bool isBarFinished = klineBarPos >= 0;
                ForwardKLine(klineData, isBarFinished, IsPeriodEnd(period));
            }
        }

        private bool IsCurrentTradingTimeEnd()
        {
            if (IsPeriodEnd(dataForForward_Code.MainKLinePeriod))
            {
                IKLineData_RealTime mainKlineData = dataForForward_Code.MainKLine;
                if (mainKlineData.IsTradingTimeEnd(mainKlineData.BarPos))
                    return true;
            }
            return false;
        }

        private void ForwardTickData()
        {
            ITickData_Extend tickData = dataForForward_Code.CurrentTickData;
            tickData.BarPos += 1;
        }

        private int GetMainKLineForwardBarPos(out int lastMainKLineBarPos)
        {
            int tickBarPos = dataForForward_Code.CurrentTickData.BarPos;
            return forwardDataIndeier.GetMainKLineBarPosIfFinished(tickBarPos, out lastMainKLineBarPos);
        }

        private void ForwardKLine(IKLineData_RealTime klineData, bool isBarFinished, bool isBarStart)
        {
            ITickBar tickBar = dataForForward_Code.CurrentTickData;
            if (isBarFinished)
                klineData.ResetCurrentBar();
            else if (isBarStart)
            {
                KLineBar klineBar = KLineUtils.GetKLineBar(tickBar);
                klineData.ChangeCurrentBar(klineBar, klineData.BarPos + 1);
            }
            else
            {
                KLineBar klineBar = KLineUtils.GetKLineBar(klineData, tickBar);
                klineData.ChangeCurrentBar(klineBar);
            }
            dic_KLinePeriod_IsEnd[klineData.Period] = isBarFinished;
        }

        private void ForwardToday_TimeLineData(int mainBarPosIfFinished)
        {
            if (!dataForForward_Code.UseTimeLineData)
                return;
            ITickBar tickBar = dataForForward_Code.CurrentTickData;
            ITimeLineData_RealTime timeLineData = dataForForward_Code.CurrentTimeLineData;
            //该时段已经结束，跳到下一时段
            if (isTimeLineEnd)
            {
                if (mainBarPosIfFinished > 0)
                {
                    timeLineData.ResetCurrentBar();
                    timeLineData.BarPos = forwardDataIndeier.GetTimeLineBarPosIfFinished(mainBarPosIfFinished);
                    return;
                }
                TimeLineBar timeLineBar2 = TimeLineUtils.GetTimeLineBar(tickBar, timeLineData.YesterdayEnd);
                int barP = forwardDataIndeier.GetTimeLineBarPosIfFinished(mainBarPosIfFinished);
                timeLineData.ChangeCurrentBar(timeLineBar2, barP);
                //timeLineData.ChangeCurrentBar(timeLineBar2, timeLineData.BarPos + 1);
                isTimeLineEnd = false;
                return;
            }

            TimeLineBar timeLineBar = TimeLineUtils.GetTimeLineBar(timeLineData, tickBar, timeLineData.YesterdayEnd);
            timeLineData.ChangeCurrentBar(timeLineBar);

            if (mainBarPosIfFinished > 0)
                isTimeLineEnd = true;
        }

        private void DealEvent_Today()
        {
            if (OnRealTimeChanged != null)
                OnRealTimeChanged(this, new RealTimeChangedArgument(-1, Time, this));
            DealEvent_OnTick();
            DealEvent_OnBar();
        }

        private void DealEvent_OnBar()
        {
            if (OnBar != null && forwardPeriod.KlineForwardPeriod != null)
            {
                bool isForwardPeriodEnd = IsPeriodEnd(forwardPeriod.KlineForwardPeriod);
                if (isForwardPeriodEnd)
                {
                    barFinishedInfos.Clear();
                    for (int i = 0; i < dataForForward_Code.ReferedKLinePeriods.Count; i++)
                    {
                        KLinePeriod period = dataForForward_Code.ReferedKLinePeriods[i];
                        bool isPeriodEnd = IsPeriodEnd(period);
                        if (isPeriodEnd)
                        {
                            IKLineData_Extend klineData = dataForForward_Code.GetKLineData(period);
                            barFinishedInfos.Add(new ForwardOnbar_Info(klineData, klineData.BarPos));
                        }
                    }
                    OnBar(this, onBarArgument);
                }
            }
        }

        private void DealEvent_OnTick()
        {
            if (OnTick != null)
                OnTick(this, new ForwardOnTickArgument(dataForForward_Code.CurrentTickData, dataForForward_Code.CurrentTickData.BarPos, this));
        }

        #endregion

        #region ForwardNextDay

        private bool Forward_NextDay()
        {
            //判断是否是最后一个交易日
            int tradingDay = dataForForward_Code.GetNextTradingDay();
            if (!dataForForward_Code.TradingDayReader.IsTrade(tradingDay))
            {
                isEnd = true;
                return false;
            }

            isTradingTimeStart = true;
            isTradingTimeEnd = false;
            isDayEnd = false;
            isDayStart = true;

            //切换交易日
            ChangeTradingDay(tradingDay);

            int lastMainKLineBarPos;
            int firstMainBarPos = this.forwardDataIndeier.GetMainKLineBarPosIfFinished(0, out lastMainKLineBarPos);
            ForwardKLine(firstMainBarPos);
            ForwardToday_TimeLineData(firstMainBarPos);
            DealEvent_Today();
            return true;
        }

        private void ChangeTradingDay(int tradingDay)
        {
            this.dataForForward_Code.TradingDay = tradingDay;
            this.forwardDataIndeier.ChangeTradingDay(tradingDay);
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
            klineData.ChangeCurrentBar(KLineUtils.GetKLineBar(tickBar), nextbarPos);
        }

        private static void ForwardNextDay_TimeLine(DataForForward_Code forwardData)
        {
            if (!forwardData.UseTimeLineData)
                return;

            //ITimeLineBar timeLineBar = GetTimeLineBar(forwardData.CurrentTickData, forwardData.CurrentTimeLineData.YesterdayEnd);
            //forwardData.CurrentTimeLineData.ChangeCurrentBar(timeLineBar);
        }

        #endregion

        public void NavigateTo(double time)
        {
            DataForNavigate_Code dataForNav = DataForNavigate_Code.Create(this.dataForForward_Code);
            DataNavigate_Code dataNav = new DataNavigate_Code(dataForNav);
            dataNav.NavigateTo(time);

            if (dataForForward_Code.UseTimeLineData)
                this.dataForForward_Code.CurrentTimeLineData = (ITimeLineData_RealTime)dataNav.GetTimeLineData();
            if (dataForForward_Code.UseTickData)
                this.dataForForward_Code.CurrentTickData = (ITickData_Extend)dataNav.GetTickData();

            foreach (KLinePeriod period in this.dataForForward_Code.ReferedKLinePeriods)
            {
                this.dataForForward_Code.SetKLineData(period, (IKLineData_RealTime)dataNav.GetKLineData(period));
            }

        }

        public event DelegateOnTick OnTick;
        public event DelegateOnBar OnBar;
        public event DelegateOnRealTimeChanged OnRealTimeChanged;


        private System.Timers.Timer timer;// = new System.Timers.Timer(250);
        private double forwardTime;
        private int mileSecondCount = 0;

        public void Play()
        {
            if (timer == null)
            {
                timer = new System.Timers.Timer(250);
                timer.Elapsed += Timer_Elapsed;
                timer.AutoReset = true;
            }
            this.forwardTime = GetTickData().Time;
            this.timer.Enabled = true;
            this.timer.Start();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            ITickData_Extend currentTickData = dataForForward_Code.CurrentTickData;
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

        public void Pause()
        {
            this.timer.Enabled = false;
            this.forwardTime = -1;
        }

        #region 基本属性

        public string Code
        {
            get
            {
                return dataPackage.Code;
            }
        }

        public int StartDate
        {
            get
            {
                return dataPackage.StartDate;
            }
        }

        public int EndDate
        {
            get
            {
                return dataPackage.EndDate;
            }
        }

        public IDataPackage_Code DataPackage
        {
            get
            {
                return this.dataPackage;
            }
        }

        public ForwardPeriod ForwardPeriod
        {
            get
            {
                return forwardPeriod;
            }
        }

        #endregion

        #region RealTimeDataReader

        public IKLineData GetKLineData()
        {
            return dataForForward_Code.MainKLine;
        }

        public IKLineData GetKLineData(KLinePeriod period)
        {
            return dataForForward_Code.GetKLineData(period);
        }

        public ITimeLineData GetTimeLineData()
        {
            return dataForForward_Code.CurrentTimeLineData;
        }

        public ITickData GetTickData()
        {
            return dataForForward_Code.CurrentTickData;
        }

        #endregion

        #region 标记所在位置的属性

        private bool isEnd;

        private bool isDayStart;

        private bool isDayEnd;

        private bool isTradingTimeStart;

        private bool isTradingTimeEnd;

        public bool IsEnd
        {
            get
            {
                return isEnd;
            }
        }

        public bool IsPeriodEnd(KLinePeriod period)
        {
            if (this.dic_KLinePeriod_IsEnd.ContainsKey(period))
                return dic_KLinePeriod_IsEnd[period];
            return false;
        }

        public bool IsDayStart
        {
            get
            {
                return isDayStart;
            }
        }

        public bool IsDayEnd
        {
            get
            {
                return isDayEnd;
            }
        }

        public bool IsTradingTimeStart
        {
            get
            {
                return isTradingTimeStart;
            }
        }

        public bool IsTradingTimeEnd
        {
            get
            {
                return isTradingTimeEnd;
            }
        }

        public double Time
        {
            get
            {
                return dataForForward_Code.Time;
            }
        }

        public float Price
        {
            get
            {
                return dataForForward_Code.Price;
            }
        }

        #endregion

        #region 附加Code

        public void AttachOtherData(string code)
        {
            if (dic_Code_DataForward.ContainsKey(code))
                return;
            int startDate = dataForForward_Code.StartDate;
            int endDate = dataForForward_Code.EndDate;
            IDataPackage_Code dataPackage_AttachCode = dataCenter.DataPackageFactory.CreateDataPackage_Code(code, startDate, endDate);
            DataForward_AttachCode_Tick dataForward_AttachCode = new DataForward_AttachCode_Tick(dataPackage_AttachCode, this.dataForForward_Code.ReferedPeriods);
            this.dic_Code_DataForward.Add(code, dataForward_AttachCode);
            dataForward_AttachCode.ForwardNextDay(dataForForward_Code.TradingDay, this.Time);
        }

        public IRealTimeDataReader_Code GetAttachedDataReader(string code)
        {
            throw new NotImplementedException();
        }

        public List<string> GetAttachedCodes()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region 保存装载

        public void Save(XmlElement xmlElem)
        {
            xmlElem.SetAttribute("forwardType", "tick");
            xmlElem.SetAttribute("time", Time.ToString());
            this.forwardPeriod.Save(xmlElem);
            this.dataForForward_Code.Save(xmlElem);
            string[] keies = this.dic_Code_DataForward.Keys.ToArray<string>();
            xmlElem.SetAttribute("attachedCodes", ListUtils.ToString(keies));
        }

        public void Load(XmlElement xmlElem)
        {
            double time = double.Parse(xmlElem.GetAttribute("time"));
            this.forwardPeriod = new ForwardPeriod();
            this.forwardPeriod.Load(xmlElem);
            this.dataForForward_Code = new DataForForward_Code(dataCenter);
            this.dataForForward_Code.Load(xmlElem);
            this.NavigateTo(time);
        }

        #endregion
    }
}
