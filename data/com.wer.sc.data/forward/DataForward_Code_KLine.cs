using com.wer.sc.data;
using com.wer.sc.data.reader;
using com.wer.sc.data.realtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.data.datapackage;
using com.wer.sc.data.navigate;
using System.Xml;

namespace com.wer.sc.data.forward
{
    /// <summary>
    /// 该类用于K线数据
    /// </summary>
    internal class DataForward_Code_KLine : IDataForward_Code
    {
        private string[] listenedCodes = new string[1];

        private DataForForward_Code navigateData;

        private Dictionary<KLinePeriod, bool> dic_KLinePeriod_IsEnd = new Dictionary<KLinePeriod, bool>();

        private ForwardPeriod forwardPeriod;

        private IKLineData_RealTime mainKLineData;

        private List<IForwardKLineBarInfo> barFinishedInfos = new List<IForwardKLineBarInfo>();

        private IForwardOnBarArgument onBarArgument;

        private Dictionary<string, IDataForward_Code> dic_Code_DataForward = new Dictionary<string, IDataForward_Code>();

        private IDataCenter dataCenter;

        public DataForward_Code_KLine(IDataCenter dataCenter)
        {
            this.dataCenter = dataCenter;
        }

        public DataForward_Code_KLine(IDataCenter dataCenter, IDataPackage_Code dataPackage, ForwardReferedPeriods referedPeriods, ForwardPeriod forwardPeriod)
        {
            this.dataCenter = dataCenter;
            this.navigateData = new DataForForward_Code(dataPackage, referedPeriods);
            this.navigateData.TradingDay = navigateData.StartDate;
            this.forwardPeriod = forwardPeriod;
            this.mainKLineData = this.navigateData.GetKLineData(forwardPeriod.KlineForwardPeriod);
            this.listenedCodes[0] = mainKLineData.Code;
            this.onBarArgument = new ForwardOnBarArgument(this.barFinishedInfos, this);
            InitKLine();
        }

        private void InitKLine()
        {
            foreach (KLinePeriod period in this.navigateData.ReferedKLinePeriods)
            {
                IKLineData_RealTime klineData = this.navigateData.GetKLineData(period);
                //主K线最后前进
                if (klineData == mainKLineData)
                {
                    dic_KLinePeriod_IsEnd[period] = true;
                    continue;
                }
                klineData.ChangeCurrentBar(mainKLineData);
            }
        }

        public string Code
        {
            get { return navigateData.Code; }
        }

        public double Time
        {
            get
            {
                return mainKLineData.Time;
            }
        }

        public float Price
        {
            get { return mainKLineData.End; }
        }

        public double GetNextTime()
        {
            if (mainKLineData.BarPos >= mainKLineData.Length - 1)
                return -1;
            return mainKLineData.Arr_Time[mainKLineData.BarPos + 1];
        }

        public IKLineData GetKLineData()
        {
            return mainKLineData;
        }

        public IKLineData GetKLineData(KLinePeriod klinePeriod)
        {
            return navigateData.GetKLineData(klinePeriod);
        }

        public void AttachOtherData(string code)
        {
            if (dic_Code_DataForward.ContainsKey(code))
                return;
            int startDate = this.DataPackage.StartDate;
            int endDate = this.DataPackage.EndDate;
            IDataPackage_Code dataPackage_AttachCode = dataCenter.DataPackageFactory.CreateDataPackage_Code(code, startDate, endDate);
            IDataForward_Code dataForward_AttachCode = new DataForward_Code_KLine(dataCenter, dataPackage_AttachCode, navigateData.ReferedPeriods, forwardPeriod);
            this.dic_Code_DataForward.Add(code, dataForward_AttachCode);
        }

        public List<string> GetAttachedCodes()
        {
            return dic_Code_DataForward.Keys.ToList<String>();
        }

        public IRealTimeData_Code GetAttachedDataReader(string code)
        {
            if (dic_Code_DataForward.ContainsKey(code))
                return dic_Code_DataForward[code];
            return null;
        }

        #region 当前的前进信息

        private bool isEnd;

        public bool IsEnd
        {
            get { return isEnd; }
        }

        private bool isDayStart;

        /// <summary>
        /// 是否是一天的开始
        /// </summary>
        public bool IsDayStart
        {
            get
            {
                return isDayStart;
            }
        }

        private bool isDayEnd;

        public bool IsDayEnd
        {
            get
            {
                return isDayEnd;
            }
        }

        private bool isTradingTimeStart;

        /// <summary>
        /// 是否是一个交易时段的开始
        /// </summary>
        public bool IsTradingTimeStart
        {
            get
            {
                return isTradingTimeStart;
            }
        }

        private bool isTradingTimeEnd;

        public bool IsTradingTimeEnd
        {
            get
            {
                return isTradingTimeEnd;
            }
        }

        #endregion

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
                return navigateData.StartDate;
            }
        }

        public int EndDate
        {
            get
            {
                return navigateData.EndDate;
            }
        }

        public IDataPackage_Code DataPackage
        {
            get
            {
                return navigateData.DataPackage;
            }
        }

        public IList<string> ListenedCodes
        {
            get
            {
                return listenedCodes;
            }
        }

        public bool Forward()
        {
            if (isEnd)
                return false;

            double prevTime = this.Time;
            foreach (KLinePeriod period in navigateData.ReferedKLinePeriods)
            {
                IKLineData_RealTime klineData = navigateData.GetKLineData(period);
                //主K线最后前进
                if (klineData == mainKLineData)
                    continue;
                ForwardKLineData(klineData);
            }

            ForwardTimeLineData();
            mainKLineData.BarPos++;

            DealTimeInfo();

            //如果是最后一个bar，设置结束标志
            if (mainKLineData.BarPos >= mainKLineData.Length - 1)
                isEnd = true;

            foreach (KLinePeriod period in navigateData.ReferedKLinePeriods)
            {
                IKLineData_RealTime klineData = navigateData.GetKLineData(period);
                if (isEnd)
                {
                    klineData.ResetCurrentBar();
                    dic_KLinePeriod_IsEnd[period] = true;
                }
                else if (klineData.BarPos == klineData.Length - 1)
                {
                    if (isDayEnd)
                        dic_KLinePeriod_IsEnd[period] = true;
                    else
                        dic_KLinePeriod_IsEnd[period] = false;
                }
                else if (period.PeriodType >= KLineTimeType.DAY)
                {
                    if (period.Equals(KLinePeriod.KLinePeriod_1Day))
                    {
                        if (isDayEnd)
                            dic_KLinePeriod_IsEnd[period] = true;
                        else
                            dic_KLinePeriod_IsEnd[period] = false;
                    }
                }
                else
                {
                    double nextMainTime = mainKLineData.Arr_Time[mainKLineData.BarPos + 1];
                    double nextKLineTime = klineData.Arr_Time[klineData.BarPos + 1];
                    if (nextMainTime == nextKLineTime)
                        dic_KLinePeriod_IsEnd[period] = true;
                    else
                        dic_KLinePeriod_IsEnd[period] = false;
                }
            }
            foreach (string code in dic_Code_DataForward.Keys)
            {
                dic_Code_DataForward[code].Forward();
            }
            if (OnBar != null)
            {
                barFinishedInfos.Clear();
                for (int i = 0; i < navigateData.ReferedKLinePeriods.Count; i++)
                {
                    KLinePeriod period = navigateData.ReferedKLinePeriods[i];
                    if (dic_KLinePeriod_IsEnd[period])
                    {
                        IKLineData_RealTime klineData = navigateData.GetKLineData(period);
                        barFinishedInfos.Add(new ForwardOnbar_Info(klineData, klineData.BarPos));
                    }
                }
                OnBar(this, onBarArgument);
            }
            if (OnRealTimeChanged != null)
                OnRealTimeChanged(this, new RealTimeChangedArgument(prevTime, this.Time, this));


            return true;
        }

        private void DealTimeInfo()
        {
            if (mainKLineData.IsTradingPeriodStart(mainKLineData.BarPos))
                isTradingTimeStart = true;
            else
                isTradingTimeStart = false;

            if (mainKLineData.IsTradingPeriodEnd(mainKLineData.BarPos))
                isTradingTimeEnd = true;
            else
                isTradingTimeEnd = false;

            if (mainKLineData.IsDayStart(mainKLineData.BarPos))
                isDayStart = true;
            else
                isDayStart = false;

            if (mainKLineData.IsDayEnd(mainKLineData.BarPos))
                isDayEnd = true;
            else
                isDayEnd = false;
        }

        private void ForwardKLineData(IKLineData_RealTime klineData)
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

                /*
                 * 20171021 ww
                 * 处理回测时非主K线要onbar时时间不对，如下5分钟线
                 * 
                 * 1分钟:20170531.21,3102,3106,3095,3101,135598,0,3992164
                 * 1分钟:20170531.2101,3102,3103,3096,3096,58762,0,4008012
                 * 1分钟:20170531.2102,3095,3102,3094,3100,35524,0,4013054
                 * 1分钟:20170531.2103,3101,3101,3093,3094,35276,0,4023900
                 * 1分钟:20170531.2104,3094,3102,3094,3101,34660,0,4029194
                 * 5分钟:20170531.2104,3102,3106,3093,3101,299820,0,402919
                 * 
                 * 正确5分钟线
                 * 5分钟:20170531.21,3102,3106,3093,3101,299820,0,4029194
                 */
                if (klineData.Period.Equals(KLinePeriod.KLinePeriod_1Day))
                {
                    if (mainKLineData.IsDayEnd(mainKLineData.BarPos + 1))
                        klineData.ResetCurrentBar();
                }
                else if (mainKLineData.BarPos < mainKLineData.Length - 2)
                {
                    double nextMainTime2 = mainKLineData.Arr_Time[mainKLineData.BarPos + 2];
                    if (nextMainTime2 >= nextTime)
                    {
                        klineData.ResetCurrentBar();
                        return;
                    }
                }
            }
        }

        private void ForwardBar_NextPeriod(IKLineData_RealTime klineData, IKLineBar klineBar)
        {
            klineData.BarPos++;
            klineData.ChangeCurrentBar(klineBar);
        }

        private void ForwardBar_CurrentPeriod(IKLineData_RealTime klineData, IKLineBar klineBar)
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

            klineData.ChangeCurrentBar(bar);
        }

        private void ForwardTimeLineData()
        {
            if (!navigateData.UseTimeLineData)
                return;
            if (navigateData.CurrentTimeLineData == null)
                return;
            ITimeLineData_RealTime timeLineData = navigateData.CurrentTimeLineData;
            if (navigateData.CurrentTimeLineData.BarPos >= navigateData.CurrentTimeLineData.Length - 1)
            {
                int nextTradingDay = navigateData.GetNextTradingDay();
                if (nextTradingDay < 0)
                    return;
                navigateData.TradingDay = nextTradingDay;
                navigateData.CurrentTimeLineData = new TimeLineDataExtend_RealTime(DataPackage.GetTimeLineData(navigateData.TradingDay));
            }

            if (mainKLineData.BarPos >= mainKLineData.Length - 1)
                return;
            double nextTime_KLine = mainKLineData.Arr_Time[mainKLineData.BarPos + 1];
            int nextTimeLineBarPos = timeLineData.BarPos;
            double nextTime_TimeLine = timeLineData.Arr_Time[nextTimeLineBarPos];
            while (nextTime_TimeLine < nextTime_KLine && nextTimeLineBarPos < timeLineData.Length - 1)
            {
                nextTimeLineBarPos++;
                nextTime_TimeLine = timeLineData.Arr_Time[nextTimeLineBarPos];
            }
            timeLineData.BarPos = nextTimeLineBarPos;
        }

        public void NavigateTo(double time)
        {

        }

        public event DelegateOnNavigateTo OnNavigateTo;

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
            return navigateData.CurrentTimeLineData;
        }

        public ITickData GetTickData()
        {
            return null;
        }

        public bool IsPeriodEnd(KLinePeriod klinePeriod)
        {
            bool isPeriodEnd = false;
            dic_KLinePeriod_IsEnd.TryGetValue(klinePeriod, out isPeriodEnd);
            return isPeriodEnd;
        }

        public IRealTimeData_Code GetRealTimeData(string code)
        {
            if (string.Equals(Code, code, StringComparison.CurrentCultureIgnoreCase))
                return this;
            return RealTimeData_Code_Null.Instance;
        }

        public void Save(XmlElement xmlElem)
        {

        }

        public void Load(XmlElement xmlElem)
        {

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