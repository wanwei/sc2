using com.wer.sc.data.reader;
using System;
using System.Collections.Generic;
using com.wer.sc.data;

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

        private IList<int> tradingDays;

        private Dictionary<KLinePeriod, IKLineData> dic_Period_KLineData = new Dictionary<KLinePeriod, IKLineData>();

        private ITickData lastTickData;

        private ITickData currentTickData;

        private ITimeLineData timeLineData;

        private double time;

        private bool isTickForward;

        private KLinePeriod forwardPeriod;

        public RealTimeReader_Strategy(IDataReader dataReader, string code, int startDate, int endDate, StrategyReferdPeriods referedPeriods)
        {
            this.dataReader = dataReader;
            this.code = code;
            this.startDate = startDate;
            this.endDate = endDate;

            List<KLinePeriod> klinePeriods = referedPeriods.UsedKLinePeriods;
            for (int i = 0; i < klinePeriods.Count; i++)
            {
                KLinePeriod klinePeriod = klinePeriods[i];
                IKLineData klineData = dataReader.KLineDataReader.GetData(code, startDate, endDate, klinePeriod);
                dic_Period_KLineData.Add(klinePeriod, klineData);
            }

            if (referedPeriods.UseTickData)
            {
                this.tradingDays = dataReader.TradingDayReader.GetTradingDays(startDate, endDate);
                currentTickData = dataReader.TickDataReader.GetTickData(code, tradingDays[0]);
            }

            if (referedPeriods.isReferTimeLineData)
            {
                //TODO
                timeLineData = null;
            }
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
            return dic_Period_KLineData[period];
        }

        public ITickData GetTickData()
        {
            return currentTickData;
        }

        public ITimeLineData GetTimeLineData()
        {
            return timeLineData;
        }

        #region 前进

        public void SetForwardPeriod(bool isTickForward, KLinePeriod forwardKLinePeriod)
        {
            this.isTickForward = isTickForward;
            this.forwardPeriod = forwardKLinePeriod;
        }

        public void Forward()
        {

        }

        public bool IsEnd
        {
            get { return false; }
        }

        //void OnTick(IRealTimeDataReader currentData);

        //void OnBar(IRealTimeDataReader currentData);

        public event DelegateOnTick OnTick;

        public event DelegateOnBar OnBar;

        #endregion
    }

    public delegate void DelegateOnTick(object sender, ITickData tickData, int index);

    public delegate void DelegateOnBar(object sender, IKLineData klineData, int index);
}