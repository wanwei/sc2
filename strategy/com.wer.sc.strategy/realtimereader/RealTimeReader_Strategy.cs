using com.wer.sc.data.reader;
using System;
using System.Collections.Generic;
using com.wer.sc.data;
using com.wer.sc.data.realtime;

namespace com.wer.sc.strategy.realtimereader
{
    /// <summary>
    /// 实时数据读取器，用作策略的历史数据回测
    /// </summary>
    public class RealTimeReader_Strategy : IRealTimeDataReader
    {

        private IDataReader dataReader;

        private string code;

        private int startDate;

        private int endDate;

        private StrategyReferedPeriods referedPeriods;

        private IKLineDataForward klineDataForward;

        public RealTimeReader_Strategy(IDataReader dataReader, string code, int startDate, int endDate, StrategyReferedPeriods referedPeriods)
        {
            this.dataReader = dataReader;
            this.code = code;
            this.startDate = startDate;
            this.endDate = endDate;
            this.referedPeriods = referedPeriods;
        }

        public void SetForwardPeriod(bool isTickForward, KLinePeriod forwardKLinePeriod)
        {

        }

        public double Time
        {
            get
            {
                return -1;
            }
        }

        public string GetCode()
        {
            return code;
        }

        public IKLineData GetKLineData(KLinePeriod period)
        {
            return null;
        }

        public ITickData GetTickData()
        {
            return null;
        }

        public ITimeLineData GetTimeLineData()
        {
            return null;
        }

        public bool Forward()
        {
            return true;
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

        //void OnTick(IRealTimeDataReader currentData);

        //void OnBar(IRealTimeDataReader currentData);

        public event DelegateOnTick OnTick;

        public event DelegateOnBar OnBar;
    }

    public delegate void DelegateOnTick(object sender, ITickData tickData, int index);

    public delegate void DelegateOnBar(object sender, IKLineData klineData, int index);
}