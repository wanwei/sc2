using com.wer.sc.data.reader;
using System;
using System.Collections.Generic;
using com.wer.sc.data;
using com.wer.sc.data.realtime;
using com.wer.sc.strategy;
using System.Timers;
using com.wer.sc.data.datapackage;

namespace com.wer.sc.data.forward
{
    /// <summary>
    /// 实时数据读取器，用作策略的历史数据回测
    /// </summary>
    public class HistoryDataForward_Code : IHistoryDataForward_Code
    {
        private IDataPackage_Code dataPackage;

        private ForwardReferedPeriods referedPeriods;

        private ForwardPeriod forwardPeriod;

        private IHistoryDataForward_Code historyDataForward;

        public HistoryDataForward_Code(IDataReader dataReader, string code, HistoryDataForwardArguments args)
        {
            IDataPackage_Code dataPackage = DataPackageFactory.CreateDataPackage(dataReader, code, args.StartDate, args.EndDate);
            ForwardReferedPeriods referedPeriods = args.ReferedPeriods;
            ForwardPeriod forwardPeriod = new ForwardPeriod(args.IsTickForward, args.ForwardKLinePeriod);
            this.Init(dataPackage, referedPeriods, forwardPeriod);
        }        

        public HistoryDataForward_Code(IDataPackage_Code dataPackage, ForwardReferedPeriods referedPeriods, ForwardPeriod forwardPeriod)
        {
            Init(dataPackage, referedPeriods, forwardPeriod);
        }

        private void Init(IDataPackage_Code dataPackage, ForwardReferedPeriods referedPeriods, ForwardPeriod forwardPeriod)
        {
            this.dataPackage = dataPackage;
            this.referedPeriods = referedPeriods;
            this.forwardPeriod = forwardPeriod;

            Dictionary<KLinePeriod, KLineData_RealTime> allKLineData = new Dictionary<KLinePeriod, KLineData_RealTime>();
            for (int i = 0; i < referedPeriods.UsedKLinePeriods.Count; i++)
            {
                KLinePeriod period = referedPeriods.UsedKLinePeriods[i];
                IKLineData klineData = this.dataPackage.GetKLineData(period);
                KLineData_RealTime klineData_RealTime = new KLineData_RealTime(klineData);
                allKLineData.Add(period, klineData_RealTime);
            }

            //ITimeLineData timelineData = this.dataReader.TimeLineDataReader.GetData(code, startDate);
            //this.timeLineData_RealTime = new TimeLineData_RealTime(timelineData);

            IList<int> allTradingDays = dataPackage.GetTradingDays();
            if (forwardPeriod.IsTickForward)
            {
                //this.historyDataForward = new HistoryDataForward_Code_TickPeriod(dataReader, code, allKLineData, allTradingDays, forwardPeriod.KlineForwardPeriod);
                this.historyDataForward = new HistoryDataForward_Code_TickPeriod(dataPackage, allKLineData, allTradingDays, forwardPeriod.KlineForwardPeriod);
            }
            else
            {
                KLinePeriod mainPeriod = forwardPeriod.KlineForwardPeriod;
                KLineData_RealTime mainKLineData = allKLineData[mainPeriod];
                this.historyDataForward = new HistoryDataForward_Code_KLinePeriod(Code, mainKLineData, allKLineData);
            }

            this.historyDataForward.OnRealTimeChanged += HistoryDataForward_OnRealTimeChanged;
            this.historyDataForward.OnTick += KlineDataForward_OnTick;
            this.historyDataForward.OnBar += KlineDataForward_OnBar;
        }

        private void HistoryDataForward_OnRealTimeChanged(object sender, RealTimeChangedArgument e)
        {
            if (this.OnRealTimeChanged != null)
                this.OnRealTimeChanged(this, e);
        }

        private void KlineDataForward_OnBar(object sender, IKLineData klineData, int index)
        {
            if (OnBar != null)
                OnBar(this, klineData, index);
        }

        private void KlineDataForward_OnTick(object sender, ITickData tickData, int index)
        {
            if (OnTick != null)
                OnTick(this, tickData, index);
        }

        public double Time
        {
            get
            {
                return historyDataForward.Time;
            }
        }

        public string Code
        {
            get { return this.dataPackage.Code; }
        }

        public IKLineData GetKLineData()
        {
            return null;
        }

        public IKLineData GetKLineData(KLinePeriod period)
        {
            //return allKLineData[period];
            return historyDataForward.GetKLineData(period);
        }

        public ITickData GetTickData()
        {
            return historyDataForward.GetTickData();
        }

        public ITimeLineData GetTimeLineData()
        {
            return historyDataForward.GetTimeLineData();
        }

        public bool Forward()
        {
            return historyDataForward.Forward();
        }

        public void NavigateTo(double time)
        {
            this.historyDataForward.NavigateTo(time);
        }

        /// <summary>
        /// 自动前进
        /// </summary>
        public void Play()
        {
            //只有按tick前进才能进入play模式
            historyDataForward.Play();
        }

        /// <summary>
        /// 停止自动前进
        /// </summary>
        public void Pause()
        {
            historyDataForward.Pause();
        }

        public bool IsEnd
        {
            get { return historyDataForward.IsEnd; }
        }

        public bool IsDayEnd
        {
            get { return historyDataForward.IsDayEnd; }
        }

        public bool IsPeriodEnd(KLinePeriod klinePeriod)
        {
            return historyDataForward.IsPeriodEnd(klinePeriod);
        }

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
                return this.dataPackage;
            }
        }

        public event DelegateOnTick OnTick;

        public event DelegateOnBar OnBar;

        public event DelegateOnRealTimeChanged OnRealTimeChanged;
    }
}