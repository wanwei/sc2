using com.wer.sc.data;
using com.wer.sc.data.reader;
using com.wer.sc.data.realtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.data.datapackage;

namespace com.wer.sc.data.forward
{
    /// <summary>
    /// 该类用于K线数据
    /// </summary>
    internal class HistoryDataForward_Code_KLinePeriod : IHistoryDataForward_Code
    {
        private string code;

        private IDataPackage_Code dataPackage;

        private IList<KLinePeriod> periods;

        private IKLineData_RealTime mainKLineData;

        private Dictionary<KLinePeriod, IKLineData_RealTime> dic_Period_KLineData = new Dictionary<KLinePeriod, IKLineData_RealTime>();

        private ITimeLineData timeLineData;

        private ForwardPeriod forwardPeriod;

        private Dictionary<KLinePeriod, bool> dic_KLinePeriod_IsEnd = new Dictionary<KLinePeriod, bool>();

        private List<ForwardOnbar_Info> barFinishedInfos = new List<ForwardOnbar_Info>();

        private ForwardOnBarArgument onBarArgument;

        private int currentTradingDay;

        public HistoryDataForward_Code_KLinePeriod(IDataPackage_Code dataPackage, IList<KLinePeriod> periods, KLinePeriod mainKLinePeriod)
        {
            this.code = dataPackage.Code;
            this.periods = periods;
            this.dataPackage = dataPackage;
            this.dic_Period_KLineData = this.dataPackage.CreateKLineData_RealTimes(periods);
            this.mainKLineData = this.dic_Period_KLineData[mainKLinePeriod];
            this.currentTradingDay = dataPackage.StartDate;
            this.timeLineData = dataPackage.GetTimeLineData(dataPackage.StartDate);
            this.forwardPeriod = new ForwardPeriod(false, mainKLineData.Period);
            this.onBarArgument = new ForwardOnBarArgument(this.barFinishedInfos);
            InitKLine();
        }

        private void InitKLine()
        {
            foreach (KLinePeriod period in dic_Period_KLineData.Keys)
            {
                IKLineData_RealTime klineData = dic_Period_KLineData[period];
                //主K线最后前进
                if (klineData == mainKLineData)
                {
                    dic_KLinePeriod_IsEnd[period] = true;
                    continue;
                }
                klineData.ChangeCurrentBar(mainKLineData);
            }
        }

        public string Code
        {
            get { return code; }
        }

        public double Time
        {
            get
            {
                return mainKLineData.Time;
            }
        }

        public IKLineData GetKLineData()
        {
            return mainKLineData;
        }

        public IKLineData GetKLineData(KLinePeriod klinePeriod)
        {
            if (dic_Period_KLineData.ContainsKey(klinePeriod))
                return dic_Period_KLineData[klinePeriod];
            return null;
        }

        #region 当前的前进信息

        private bool isEnd;

        public bool IsEnd
        {
            get { return isEnd; }
        }

        private bool isDayStart;

        /// <summary>
        /// 是否是一天的开始
        /// </summary>
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

        /// <summary>
        /// 是否是一个交易时段的开始
        /// </summary>
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

        #endregion

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
                return dataPackage;
            }
        }

        public bool Forward()
        {
            if (isEnd)
                return false;

            double prevTime = this.Time;
            foreach (KLinePeriod period in dic_Period_KLineData.Keys)
            {
                IKLineData_RealTime klineData = dic_Period_KLineData[period];
                //主K线最后前进
                if (klineData == mainKLineData)
                    continue;
                ForwardKLineData(klineData);
            }
            ForwardTimeLineData();
            mainKLineData.BarPos++;

            DealTimeInfo();

            //如果是最后一个bar，设置结束标志
            if (mainKLineData.BarPos >= mainKLineData.Length - 1)
                isEnd = true;

            foreach (KLinePeriod period in dic_Period_KLineData.Keys)
            {
                IKLineData_RealTime klineData = dic_Period_KLineData[period];
                if (isEnd)
                    dic_KLinePeriod_IsEnd[period] = true;
                else if (klineData.BarPos == klineData.Length - 1)
                    continue;
                else if (period.PeriodType >= KLineTimeType.DAY)
                {
                    if (period == KLinePeriod.KLinePeriod_1Day)
                    {
                        if (isDayEnd)
                            dic_KLinePeriod_IsEnd[period] = true;
                        else
                            dic_KLinePeriod_IsEnd[period] = false;
                    }
                }
                else
                {
                    double nextMainTime = mainKLineData.Arr_Time[mainKLineData.BarPos + 1];
                    double nextKLineTime = klineData.Arr_Time[klineData.BarPos + 1];
                    if (nextMainTime == nextKLineTime)
                        dic_KLinePeriod_IsEnd[period] = true;
                    else
                        dic_KLinePeriod_IsEnd[period] = false;
                }
            }

            if (OnBar != null)
            {
                barFinishedInfos.Clear();
                for (int i = 0; i < periods.Count; i++)
                {
                    KLinePeriod period = periods[i];
                    if (dic_KLinePeriod_IsEnd[period]) {
                        IKLineData_RealTime klineData = dic_Period_KLineData[period];
                        barFinishedInfos.Add(new ForwardOnbar_Info(klineData, klineData.BarPos));
                    }
                }
                OnBar(this, onBarArgument);
            }
            if (OnRealTimeChanged != null)
                OnRealTimeChanged(this, new RealTimeChangedArgument(prevTime, this.Time, this));
            return true;
        }

        private void DealTimeInfo()
        {
            if (mainKLineData.IsTradingTimeStart(mainKLineData.BarPos))
                isTradingTimeStart = true;
            else
                isTradingTimeStart = false;

            if (mainKLineData.IsTradingTimeEnd(mainKLineData.BarPos))
                isTradingTimeEnd = true;
            else
                isTradingTimeEnd = false;

            if (mainKLineData.IsDayStart(mainKLineData.BarPos))
                isDayStart = true;
            else
                isDayStart = false;

            if (mainKLineData.IsDayEnd(mainKLineData.BarPos))
                isDayEnd = true;
            else
                isDayEnd = false;
        }

        private void ForwardKLineData(IKLineData_RealTime klineData)
        {
            IKLineBar nextMainBar = mainKLineData.GetBar(mainKLineData.BarPos + 1);
            int currentBarPos = klineData.BarPos;
            int nextBarPos = currentBarPos + 1;
            if (nextBarPos >= klineData.Length)
                ForwardBar_CurrentPeriod(klineData, nextMainBar);
            else
            {
                double nextTime = klineData.Arr_Time[nextBarPos];
                double nextMainTime = mainKLineData.Arr_Time[mainKLineData.BarPos + 1];
                if (nextMainTime >= nextTime)
                    ForwardBar_NextPeriod(klineData, nextMainBar);
                else
                    ForwardBar_CurrentPeriod(klineData, nextMainBar);
            }
        }

        private void ForwardBar_NextPeriod(IKLineData_RealTime klineData, IKLineBar klineBar)
        {
            klineData.BarPos++;
            klineData.ChangeCurrentBar(klineBar);
        }

        private void ForwardBar_CurrentPeriod(IKLineData_RealTime klineData, IKLineBar klineBar)
        {
            double time = klineBar.Time;

            IKLineBar oldbar = klineData.GetBar(klineData.BarPos);

            KLineBar bar = new KLineBar();
            bar.Time = klineBar.Time;
            bar.Start = oldbar.Start;
            bar.High = klineBar.High > oldbar.High ? klineBar.High : oldbar.High;
            bar.Low = klineBar.Low < oldbar.Low ? klineBar.Low : oldbar.Low;
            bar.End = klineBar.End;
            bar.Mount = oldbar.Mount + klineBar.Mount;
            bar.Money = oldbar.Money + klineBar.Money;
            bar.Hold = klineBar.Hold;

            klineData.ChangeCurrentBar(bar);
        }

        private void ForwardTimeLineData()
        {
            if (timeLineData.BarPos >= timeLineData.Length - 1)
            {
                int nextTradingDay = DataPackage.GetTradingDayReader().GetNextTradingDay(this.currentTradingDay);
                if (nextTradingDay < 0)
                    return;
                this.currentTradingDay = nextTradingDay;
                this.timeLineData = DataPackage.GetTimeLineData(this.currentTradingDay);
            }

            if (mainKLineData.BarPos >= mainKLineData.Length - 1)
                return;
            double nextTime_KLine = mainKLineData.Arr_Time[mainKLineData.BarPos + 1];
            int nextTimeLineBarPos = timeLineData.BarPos;
            double nextTime_TimeLine = timeLineData.Arr_Time[nextTimeLineBarPos];
            while (nextTime_TimeLine < nextTime_KLine && nextTimeLineBarPos < timeLineData.Length - 1)
            {
                nextTimeLineBarPos++;
                nextTime_TimeLine = timeLineData.Arr_Time[nextTimeLineBarPos];
            }
            timeLineData.BarPos = nextTimeLineBarPos;
        }

        public void NavigateTo(double time)
        {

        }

        /// <summary>
        /// 自动前进
        /// </summary>
        public void Play()
        {
            //play模式只是为了还原当时场景
            //所以按照K线周期前进不需要支持play
        }

        /// <summary>
        /// 停止自动前进
        /// </summary>
        public void Pause()
        {

        }

        public ITimeLineData GetTimeLineData()
        {
            return timeLineData;
        }

        public ITickData GetTickData()
        {
            return null;
        }

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