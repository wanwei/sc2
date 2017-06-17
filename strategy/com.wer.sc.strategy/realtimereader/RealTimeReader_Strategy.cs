using com.wer.sc.data.reader;
using System;
using System.Collections.Generic;
using com.wer.sc.data;

namespace com.wer.sc.strategy.realtimereader
{
    /// <summary>
    /// 历史数据加载
    /// </summary>
    public class RealTimeReader_Strategy : IRealTimeDataReader
    {
        private string code;

        private int startDate;

        private int endDate;

        private Dictionary<KLinePeriod, IKLineData> dic_Period_KLineData = new Dictionary<KLinePeriod, IKLineData>();

        private bool isTickForward;

        private KLinePeriod forwardPeriod;

        private IDataReader dataReader;

        public RealTimeReader_Strategy(IDataReader dataReader, string code, int startDate, int endDate, StrategyReferdPeriods referedPeriods)
        {
            List<KLinePeriod> klinePeriods = referedPeriods.UsedKLinePeriods;
            for (int i = 0; i < klinePeriods.Count; i++)
            {
                KLinePeriod klinePeriod = klinePeriods[i];
                IKLineData klineData = dataReader.KLineDataReader.GetData(code, startDate, endDate, klinePeriod);
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
            throw new NotImplementedException();
        }

        public ITickData GetTickData()
        {
            throw new NotImplementedException();
        }

        public ITimeLineData GetTimeLineData()
        {
            throw new NotImplementedException();
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