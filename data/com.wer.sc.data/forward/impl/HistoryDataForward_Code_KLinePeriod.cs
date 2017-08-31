using com.wer.sc.data;
using com.wer.sc.data.reader;
using com.wer.sc.data.realtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.data.datapackage;

namespace com.wer.sc.data.forward.impl
{
    /// <summary>
    /// 该类用于K线数据
    /// </summary>
    public class HistoryDataForward_Code_KLinePeriod : IHistoryDataForward_Code
    {
        private string code;

        private KLineData_RealTime mainKLineData;

        private Dictionary<KLinePeriod, KLineData_RealTime> dic_Period_KLineData = new Dictionary<KLinePeriod, KLineData_RealTime>();

        private TimeLineData_RealTime timeLineData;

        private KLineData_DaySplitter daySplitter;

        private ForwardPeriod forwardPeriod;

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="mainKLineData"></param>
        /// <param name="allKLineData"></param>
        public HistoryDataForward_Code_KLinePeriod(string code, KLineData_RealTime mainKLineData, Dictionary<KLinePeriod, KLineData_RealTime> allKLineData)
        {
            this.code = code;
            this.mainKLineData = mainKLineData;
            this.dic_Period_KLineData = allKLineData;
            this.forwardPeriod = new ForwardPeriod(false, mainKLineData.Period);
            InitKLine();
            InitTimeLine();
        }

        public HistoryDataForward_Code_KLinePeriod(IDataReader dataReader, string code, KLineData_RealTime mainKLineData, Dictionary<KLinePeriod, KLineData_RealTime> allKLineData, ITradingSessionReader_Instrument tradingSessionReader) : this(code, mainKLineData, allKLineData)
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

        private void InitTimeLine()
        {
            // this.timeLineData = timeLineData;
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
                throw new NotImplementedException();
            }
        }

        public int EndDate
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IDataPackage DataPackage
        {
            get
            {
                return null;
            }
        }

        public bool Forward()
        {
            if (isEnd)
                return false;

            double prevTime = this.Time;
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
            if (OnRealTimeChanged != null)
                OnRealTimeChanged(this, new RealTimeChangedArgument(prevTime, this.Time, this));
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

        public event DelegateOnRealTimeChanged OnRealTimeChanged;
    }
}