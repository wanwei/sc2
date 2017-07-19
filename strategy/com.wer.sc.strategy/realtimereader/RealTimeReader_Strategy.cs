using com.wer.sc.data.reader;
using System;
using System.Collections.Generic;
using com.wer.sc.data;
using com.wer.sc.data.realtime;
using com.wer.sc.strategy;
using com.wer.sc.data.forward;
using com.wer.sc.data.forward.impl;

namespace com.wer.sc.strategy.realtimereader
{
    /// <summary>
    /// 实时数据读取器，用作策略的历史数据回测
    /// </summary>
    public class RealTimeReader_Strategy : IHistoryDataForward_Code
    {
        private IDataReader dataReader;

        private string code;

        private int startDate;

        private int endDate;

        private StrategyReferedPeriods referedPeriods;

        private IHistoryDataForward_Code klineDataForward;

        private Dictionary<KLinePeriod, KLineData_RealTime> allKLineData;

        private ForwardPeriod forwardPeriod;

        public RealTimeReader_Strategy(IDataReader dataReader, RealTimeReader_StrategyArguments args)
        {
            this.dataReader = dataReader;
            this.code = args.Code;
            this.startDate = args.StartDate;
            this.endDate = args.EndDate;
            this.referedPeriods = args.ReferedPeriods;
            this.forwardPeriod = new ForwardPeriod(args.IsTickForward, args.ForwardKLinePeriod);

            this.allKLineData = new Dictionary<KLinePeriod, KLineData_RealTime>();
            for (int i = 0; i < referedPeriods.UsedKLinePeriods.Count; i++)
            {
                KLinePeriod period = referedPeriods.UsedKLinePeriods[i];
                IKLineData klineData = this.dataReader.KLineDataReader.GetData(code, startDate, endDate, period);
                KLineData_RealTime klineData_RealTime = new KLineData_RealTime(klineData);
                allKLineData.Add(period, klineData_RealTime);
            }

            IList<int> allTradingDays = dataReader.TradingDayReader.GetTradingDays(startDate, endDate);
            if (args.IsTickForward)
            {
                this.klineDataForward = new HistoryDataForward_Code_TickPeriod(dataReader, code, allKLineData, allTradingDays, args.ForwardKLinePeriod);
            }
            else
            {
                KLinePeriod mainPeriod = args.ForwardKLinePeriod;
                KLineData_RealTime mainKLineData = allKLineData[mainPeriod];
                this.klineDataForward = new HistoryDataForward_Code_KLinePeriod(dataReader, code, mainKLineData, allKLineData);
            }

            this.klineDataForward.OnTick += KlineDataForward_OnTick;
            this.klineDataForward.OnBar += KlineDataForward_OnBar;
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

        public ForwardPeriod ForwardPeriod
        {
            get
            {
                return forwardPeriod;
            }
        }

        public event DelegateOnTick OnTick;

        public event DelegateOnBar OnBar;
    }
}