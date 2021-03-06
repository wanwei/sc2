﻿using com.wer.sc.data.reader;
using System;
using System.Collections.Generic;
using com.wer.sc.data;
using com.wer.sc.data.realtime;
using com.wer.sc.strategy;
using com.wer.sc.data.forward;
using com.wer.sc.data.datapackage;

namespace com.wer.sc.strategy.realtimereader
{
    /// <summary>
    /// 实时数据读取器，用作策略的历史数据回测
    /// </summary>
    public class RealTimeReader_Strategy : IHistoryDataForward_Code
    {
        private IDataPackage_Code dataPackage;

        private string code;

        private int startDate;

        private int endDate;

        private ForwardReferedPeriods referedPeriods;

        private IHistoryDataForward_Code klineDataForward;

        private Dictionary<KLinePeriod, KLineData_RealTime> allKLineData;

        private ForwardPeriod forwardPeriod;

        public RealTimeReader_Strategy(IDataPackage_Code dataPackage, ForwardReferedPeriods referedPeriods, ForwardPeriod forwardPeriod)
        {
            this.dataPackage = dataPackage;
            this.code = dataPackage.Code;
            this.startDate = dataPackage.StartDate;
            this.endDate = dataPackage.EndDate;
            this.referedPeriods = referedPeriods;
            this.forwardPeriod = forwardPeriod;

            this.allKLineData = new Dictionary<KLinePeriod, KLineData_RealTime>();
            for (int i = 0; i < referedPeriods.UsedKLinePeriods.Count; i++)
            {
                KLinePeriod period = referedPeriods.UsedKLinePeriods[i];
                IKLineData klineData = dataPackage.GetKLineData(period);
                KLineData_RealTime klineData_RealTime = new KLineData_RealTime(klineData);
                allKLineData.Add(period, klineData_RealTime);
            }

            IList<int> allTradingDays = dataPackage.GetTradingDays();
            if (forwardPeriod.IsTickForward)
            {
                this.klineDataForward = new HistoryDataForward_Code_TickPeriod(dataPackage, allKLineData, allTradingDays, forwardPeriod.KlineForwardPeriod);
            }
            else
            {
                KLinePeriod mainPeriod = forwardPeriod.KlineForwardPeriod;
                KLineData_RealTime mainKLineData = allKLineData[mainPeriod];
                this.klineDataForward = new HistoryDataForward_Code_KLinePeriod(code, mainKLineData, allKLineData);
            }

            this.klineDataForward.OnRealTimeChanged += KlineDataForward_OnRealTimeChanged;
            this.klineDataForward.OnTick += KlineDataForward_OnTick;
            this.klineDataForward.OnBar += KlineDataForward_OnBar;

            //this.klineDataForward = HistoryDataForwardFactory.CreateHistoryDataForward_Code(dataPackage, referedPeriods, forwardPeriod);
            //this.klineDataForward.OnTick += KlineDataForward_OnTick;
            //this.klineDataForward.OnBar += KlineDataForward_OnBar;
        }


        //public RealTimeReader_Strategy(IDataReader dataReader, RealTimeReader_StrategyArguments args)
        //{
        //    //this.dataPackage = DataPackageFactory.CreateDataPackage(dataReader,)
        //    this.dataReader = dataReader;
        //    this.code = args.Code;
        //    this.startDate = args.StartDate;
        //    this.endDate = args.EndDate;
        //    this.referedPeriods = args.ReferedPeriods;
        //    this.forwardPeriod = new ForwardPeriod(args.IsTickForward, args.ForwardKLinePeriod);

        //    this.allKLineData = new Dictionary<KLinePeriod, KLineData_RealTime>();
        //    for (int i = 0; i < referedPeriods.UsedKLinePeriods.Count; i++)
        //    {
        //        KLinePeriod period = referedPeriods.UsedKLinePeriods[i];
        //        IKLineData klineData = this.dataReader.KLineDataReader.GetData(code, startDate, endDate, period);
        //        KLineData_RealTime klineData_RealTime = new KLineData_RealTime(klineData);
        //        allKLineData.Add(period, klineData_RealTime);
        //    }

        //    IList<int> allTradingDays = dataReader.TradingDayReader.GetTradingDays(startDate, endDate);
        //    if (args.IsTickForward)
        //    {
        //        this.klineDataForward = new HistoryDataForward_Code_TickPeriod(dataReader, code, allKLineData, allTradingDays, args.ForwardKLinePeriod);
        //    }
        //    else
        //    {
        //        KLinePeriod mainPeriod = args.ForwardKLinePeriod;
        //        KLineData_RealTime mainKLineData = allKLineData[mainPeriod];
        //        this.klineDataForward = new HistoryDataForward_Code_KLinePeriod(code, mainKLineData, allKLineData);
        //    }

        //    this.klineDataForward.OnTick += KlineDataForward_OnTick;
        //    this.klineDataForward.OnBar += KlineDataForward_OnBar;
        //}

        private void KlineDataForward_OnRealTimeChanged(object sender, RealTimeChangedArgument e)
        {
            if (OnRealTimeChanged != null)
                OnRealTimeChanged(this, e);
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
                return klineDataForward.Time;
            }
        }

        public string Code
        {
            get { return code; }
        }

        public IKLineData GetKLineData()
        {
            return null;
        }

        public IKLineData GetKLineData(KLinePeriod period)
        {
            return allKLineData[period];
        }

        public ITickData GetTickData()
        {
            return klineDataForward.GetTickData();
        }

        public ITimeLineData GetTimeLineData()
        {
            return klineDataForward.GetTimeLineData();
        }

        public bool Forward()
        {
            return klineDataForward.Forward();
        }

        public bool IsEnd
        {
            get { return klineDataForward.IsEnd; }
        }

        public bool IsDayEnd
        {
            get { return klineDataForward.IsDayEnd; }
        }

        public bool IsPeriodEnd(KLinePeriod klinePeriod)
        {
            return klineDataForward.IsPeriodEnd(klinePeriod);
        }

        public void NavigateTo(double time)
        {
            throw new NotImplementedException();
        }

        public void Play()
        {

        }

        public void Pause()
        {

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
                return startDate;
            }
        }

        public int EndDate
        {
            get
            {
                return endDate;
            }
        }

        public IDataPackage_Code DataPackage
        {
            get
            {
                return dataPackage;
            }
        }

        public event DelegateOnTick OnTick;

        public event DelegateOnBar OnBar;

        public event DelegateOnRealTimeChanged OnRealTimeChanged;
    }
}