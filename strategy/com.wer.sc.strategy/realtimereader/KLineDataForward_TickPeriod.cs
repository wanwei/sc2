using com.wer.sc.data;
using com.wer.sc.data.reader;
using com.wer.sc.data.realtime;
using com.wer.sc.data.transfer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy.realtimereader
{
    /// <summary>
    /// 该类用于K线数据
    /// </summary>
    public class KLineDataForward_TickPeriod : IKLineDataForward
    {
        private IDataReader dataReader;

        private string code;

        private IList<int> tradingDays;

        private Dictionary<KLinePeriod, KLineData_RealTime> dic_Period_KLineData = new Dictionary<KLinePeriod, KLineData_RealTime>();

        private TickData currentTickData;

        private int currentTradingDayIndex = 0;

        private Dictionary<KLinePeriod, KLineData_DaySplitter> dic_Period_DaySplitter = new Dictionary<KLinePeriod, KLineData_DaySplitter>();

        private KLinePeriod forwardPeriod;

        private KLineData_RealTime mainKlineData;

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="mainKLineData"></param>
        /// <param name="allKLineData"></param>
        public KLineDataForward_TickPeriod(Dictionary<KLinePeriod, KLineData_RealTime> allKLineData, IDataReader dataReader, string code, IList<int> tradingDays, KLinePeriod forwardPeriod)
        {
            this.dic_Period_KLineData = allKLineData;
            this.dataReader = dataReader;
            this.code = code;
            this.tradingDays = tradingDays;
            this.forwardPeriod = forwardPeriod;

            InitDaySplitter(dataReader, code);
            InitKLine();
        }

        private void InitDaySplitter(IDataReader dataReader, string code)
        {
            ITradingSessionReader_Instrument sessionReader = dataReader.CreateTradingSessionReader(code);
            this.dic_Period_DaySplitter = new Dictionary<KLinePeriod, KLineData_DaySplitter>();
            foreach (KLinePeriod period in dic_Period_KLineData.Keys)
            {
                KLineData_RealTime klineData = dic_Period_KLineData[period];
                KLineData_DaySplitter daySplitter = new KLineData_DaySplitter(klineData, sessionReader);
                dic_Period_DaySplitter.Add(period, daySplitter);
            }
        }

        private void InitKLine()
        {
            currentTickData = dataReader.TickDataReader.GetTickData(code, tradingDays[0]);
            foreach (KLinePeriod period in dic_Period_KLineData.Keys)
            {
                KLineData_RealTime klineData = dic_Period_KLineData[period];
                klineData.SetRealTimeData(GetKLineBar(currentTickData));
                if (period == forwardPeriod)
                    mainKlineData = klineData;
            }
        }

        private KLineBar GetKLineBar(ITickBar tickBar)
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
            return bar;
        }

        private KLineBar GetKLineBar(IKLineBar klineBar, ITickBar tickBar)
        {
            KLineBar bar = new KLineBar();
            bar.Time = tickBar.Time;
            bar.Start = klineBar.Start;
            bar.High = tickBar.Price > klineBar.High ? tickBar.Price : klineBar.High;
            bar.Low = tickBar.Price < klineBar.Low ? tickBar.Price : klineBar.Low;
            bar.End = tickBar.Price;
            bar.Money = klineBar.Money + tickBar.Mount * tickBar.Price;
            bar.Mount = klineBar.Mount + tickBar.Mount;
            bar.Hold = tickBar.Hold;
            return bar;
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
            if (OnBar != null)
            {
                bool isForwardPeriodEnd = dic_KLinePeriod_IsEnd[forwardPeriod];
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
            currentTickData = tickData;

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
            return dataReader.TickDataReader.GetTickData(code, currentTradingDay);
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

        //private void SetPeriodEnd(KLinePeriod period, bool isEnd)
        //{
        //    dic_KLinePeriod_IsEnd[period] = isEnd;            
        //}

        public ITimeLineData GetTimeLineData()
        {
            return null;
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
