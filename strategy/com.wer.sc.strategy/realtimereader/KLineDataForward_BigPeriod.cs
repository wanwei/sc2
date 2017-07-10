using com.wer.sc.data;
using com.wer.sc.data.reader;
using com.wer.sc.data.realtime;
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
    public class KLineDataForward_BigPeriod : IKLineDataForward
    {
        private KLinePeriod forwardPeriod;

        private KLineData_RealTime mainKLineData;

        private Dictionary<KLinePeriod, KLineData_RealTime> dic_Period_KLineData = new Dictionary<KLinePeriod, KLineData_RealTime>();

        private KLineData_DaySplitter daySplitter;

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="mainKLineData"></param>
        /// <param name="allKLineData"></param>
        public KLineDataForward_BigPeriod(KLineData_RealTime mainKLineData, Dictionary<KLinePeriod, KLineData_RealTime> allKLineData)
        {
            this.mainKLineData = mainKLineData;
            this.dic_Period_KLineData = allKLineData;
            this.forwardPeriod = mainKLineData.Period;
            InitKLine();
        }

        public KLineDataForward_BigPeriod(KLineData_RealTime mainKLineData, Dictionary<KLinePeriod, KLineData_RealTime> allKLineData, ITradingSessionReader_Instrument tradingSessionReader) : this(mainKLineData, allKLineData)
        {
            this.daySplitter = new KLineData_DaySplitter(mainKLineData, tradingSessionReader);
            this.daySplitter.NextDay();
            this.daySplitter.NextDay();
        }

        private void InitKLine()
        {
            foreach (KLinePeriod period in dic_Period_KLineData.Keys)
            {
                KLineData_RealTime klineData = dic_Period_KLineData[period];
                //主K线最后前进
                if (klineData == mainKLineData)
                    continue;
                klineData.SetRealTimeData(mainKLineData);
            }
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

            foreach (KLinePeriod period in dic_Period_KLineData.Keys)
            {
                KLineData_RealTime klineData = dic_Period_KLineData[period];
                //主K线最后前进
                if (klineData == mainKLineData)
                    continue;
                ForwardKLineData(klineData);
            }
            mainKLineData.BarPos++;

            //如果是最后一个bar，设置结束标志
            if (mainKLineData.BarPos >= mainKLineData.Length - 1)
                isEnd = true;

            foreach (KLinePeriod period in dic_Period_KLineData.Keys)
            {
                KLineData_RealTime klineData = dic_Period_KLineData[period];
                if (isEnd)
                {
                    dic_KLinePeriod_IsEnd[period] = true;
                }
                else if (period.PeriodType >= KLineTimeType.DAY)
                {
                    if (daySplitter == null)
                        continue;
                    if (klineData.BarPos + 1 >= klineData.Length)
                    {
                        dic_KLinePeriod_IsEnd[period] = false;
                        continue;
                    }

                    if (daySplitter.CurrentDay == klineData.Arr_Time[klineData.BarPos + 1] && mainKLineData.BarPos + 1 == daySplitter.CurrentDayKLineIndex)
                        dic_KLinePeriod_IsEnd[period] = true;
                    else
                        dic_KLinePeriod_IsEnd[period] = false;
                }
                else
                {
                    if (klineData.BarPos + 1 >= klineData.Length)
                    {
                        dic_KLinePeriod_IsEnd[period] = false;
                        continue;
                    }
                    double nextMainTime = mainKLineData.Arr_Time[mainKLineData.BarPos + 1];
                    double nextKLineTime = klineData.Arr_Time[klineData.BarPos + 1];
                    if (nextMainTime == nextKLineTime)
                        dic_KLinePeriod_IsEnd[period] = true;
                    else
                        dic_KLinePeriod_IsEnd[period] = false;
                }
            }

            if (daySplitter != null)
            {
                if (mainKLineData.BarPos + 1 == daySplitter.CurrentDayKLineIndex)
                {
                    isDayEnd = true;
                    daySplitter.NextDay();
                }
                else
                {
                    isDayEnd = isEnd;
                }
            }
            if (OnBar != null)
                OnBar(this, mainKLineData, mainKLineData.BarPos);
            return true;
        }

        private void ForwardKLineData(KLineData_RealTime klineData)
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

        private void ForwardBar_NextPeriod(KLineData_RealTime klineData, IKLineBar klineBar)
        {
            klineData.BarPos++;
            klineData.SetRealTimeData(klineBar);
        }

        private void ForwardBar_CurrentPeriod(KLineData_RealTime klineData, IKLineBar klineBar)
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

            klineData.SetRealTimeData(bar);
        }

        public ITimeLineData GetTimeLineData()
        {
            return null;
        }

        public ITickData GetTickData()
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