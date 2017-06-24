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
    public class RealTimeReader_Strategy2 : IRealTimeDataReader
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

        private int currentTradingDay;

        private double time;

        private bool isTickForward;

        private KLinePeriod forwardPeriod;

        private bool isEnd = false;

        private bool isDayEnd = false;

        public RealTimeReader_Strategy2(IDataReader dataReader, string code, int startDate, int endDate, StrategyReferedPeriods referedPeriods)
        {
            this.dataReader = dataReader;
            this.code = code;
            this.startDate = startDate;
            this.endDate = endDate;

            this.tradingDays = dataReader.TradingDayReader.GetTradingDays(startDate, endDate);
            if (this.tradingDays.Count == 0)
                return;
            this.currentTradingDay = this.tradingDays[0];

            List<KLinePeriod> klinePeriods = referedPeriods.UsedKLinePeriods;
            for (int i = 0; i < klinePeriods.Count; i++)
            {
                KLinePeriod klinePeriod = klinePeriods[i];
                InitKLineData(dataReader, code, startDate, endDate, klinePeriod);
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

        private IKLineData InitKLineData(IDataReader dataReader, string code, int startDate, int endDate, KLinePeriod klinePeriod)
        {
            IKLineData klineData = dataReader.KLineDataReader.GetData(code, startDate, endDate, klinePeriod);
            KLineData_RealTime klineData_RealTime = new KLineData_RealTime(klineData);
            dic_Period_KLineData.Add(klinePeriod, klineData_RealTime);
            return klineData;
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
            if (dic_Period_KLineData.ContainsKey(period))
                return dic_Period_KLineData[period];
            return null;
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

        public bool Forward()
        {
            if (IsEnd)
                return false;

            if (isTickForward)
            {
                if (currentTickData == null)                
                    currentTickData = this.dataReader.TickDataReader.GetTickData(code, currentTradingDay);

                if (currentTickData.BarPos == currentTickData.Length - 1)
                {
                    isDayEnd = true;
                }
                else
                    isDayEnd = false;
                currentTickData.BarPos++;
            }
            else
            {
                
            }
            return true;
        }

        public bool IsEnd
        {
            get { return isEnd; }
        }

        public bool IsDayEnd
        {
            get { return isDayEnd; }
        }

        public bool IsPeriodEnd(KLinePeriod klinePeriod)
        {
            return false;
        }

        //void OnTick(IRealTimeDataReader currentData);

        //void OnBar(IRealTimeDataReader currentData);

        public event DelegateOnTick OnTick;

        public event DelegateOnBar OnBar;

        #endregion
    }
}