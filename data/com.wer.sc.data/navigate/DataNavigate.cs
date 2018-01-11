using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.data.datapackage;
using com.wer.sc.data.reader;
using com.wer.sc.data.forward;

namespace com.wer.sc.data.navigate
{
    public class DataNavigate : IDataNavigate
    {
        public const int OPERATOR_NAVIGATE = 0;

        public const int OPERATOR_PLAY = 1;

        private IRealTimeData_Code currentRealTimeDataReader;

        private IDataForward_Code dataForward_Code;

        private bool isPlaying;

        private int beforeDays;

        private int afterDays;

        private int lastOperator;

        private IDataNavigate_Code currentNavigate_Code;

        private IDataNavigateFactory dataNavigateFactory;

        private IDataCenter dataCenter;

        private IDataReader dataReader;

        private CodeInfo prevCodeInfo;

        private CodeInfo codeInfo;

        public DataNavigate(IDataCenter dataCenter, string code, double time, int beforeDays, int afterDays)
        {
            this.dataCenter = dataCenter;
            this.dataNavigateFactory = dataCenter.DataNavigateFactory;
            this.dataReader = dataCenter.DataReader;
            this.beforeDays = beforeDays;
            this.afterDays = afterDays;
            this.Change(code, time);
        }

        public string Code
        {
            get
            {
                return currentNavigate_Code.Code;
            }
        }

        public IDataPackage_Code DataPackage
        {
            get
            {
                return currentNavigate_Code.DataPackage;
            }
        }

        public double Time
        {
            get
            {
                if (LastOperator == OPERATOR_NAVIGATE)
                    return currentNavigate_Code.Time;
                return dataForward_Code.Time;
            }
        }

        public float Price
        {
            get
            {
                if (LastOperator == OPERATOR_NAVIGATE)
                    return currentNavigate_Code.Price;
                return dataForward_Code.Price;
            }
        }

        public int LastOperator
        {
            get
            {
                return lastOperator;
            }

            set
            {
                lastOperator = value;
                if (lastOperator == OPERATOR_NAVIGATE)
                    this.currentRealTimeDataReader = currentNavigate_Code;
                else
                    this.currentRealTimeDataReader = dataForward_Code;
            }
        }

        public event DelegateOnNavigateTo OnNavigateTo;

        public event DelegateOnRealTimeChanged OnRealTimeChanged;

        public void Change(string code)
        {
            this.Change(code, Time);
        }

        public void Change(string code, double time)
        {
            double prevTime = time;
            this.prevCodeInfo = codeInfo;
            this.codeInfo = dataReader.CodeReader.GetCodeInfo(code);

            if (this.currentNavigate_Code != null)
            {
                this.currentNavigate_Code.OnNavigateTo -= CurrentNavigate_Code_OnNavigateTo;
                this.currentNavigate_Code.OnRealTimeChanged -= CurrentNavigate_Code_OnRealTimeChanged;
                this.currentNavigate_Code = null;
            }
            this.currentNavigate_Code = dataNavigateFactory.CreateDataNavigate_Code(code, time, beforeDays, afterDays);
            this.currentNavigate_Code.OnNavigateTo += CurrentNavigate_Code_OnNavigateTo;
            this.currentNavigate_Code.OnRealTimeChanged += CurrentNavigate_Code_OnRealTimeChanged;
            this.LastOperator = OPERATOR_NAVIGATE;

            if (this.OnNavigateTo != null)           
                this.OnNavigateTo(this, new DataNavigateEventArgs(prevCodeInfo.Code, code, prevTime, time));            
            if (this.OnRealTimeChanged != null)
                this.OnRealTimeChanged(this, new RealTimeChangedArgument(prevTime, time, this));            
        }

        private void CurrentNavigate_Code_OnRealTimeChanged(object sender, RealTimeChangedArgument argument)
        {
            if (this.OnRealTimeChanged != null)
            {
                this.OnRealTimeChanged(sender, argument);
            }
        }

        private void CurrentNavigate_Code_OnNavigateTo(object sender, DataNavigateEventArgs e)
        {
            if (this.OnNavigateTo != null)
            {
                this.OnNavigateTo(sender, e);
            }
        }

        public bool Backward(KLinePeriod forwardPeriod)
        {
            bool canBackWard = currentNavigate_Code.Backward(forwardPeriod);
            if (!canBackWard)
            {
                int startDate = this.DataPackage.StartDate;
                if (startDate > codeInfo.Start)
                {
                    currentNavigate_Code = dataNavigateFactory.CreateDataNavigate_Code(Code, Time);
                    this.LastOperator = OPERATOR_NAVIGATE;
                    return currentNavigate_Code.Backward(forwardPeriod);
                }
            }
            this.LastOperator = OPERATOR_NAVIGATE;
            return canBackWard;
        }

        public bool Forward(ForwardPeriod forwardPeriod)
        {
            bool canForward = this.currentNavigate_Code.Forward(forwardPeriod);
            if (!canForward)
            {
                int endDate = this.DataPackage.EndDate;
                int codeEndDate = codeInfo.End;
                if (codeEndDate == 0)
                    codeEndDate = this.dataReader.TradingDayReader.FirstTradingDay;
                if (endDate < codeEndDate)
                {
                    currentNavigate_Code = dataNavigateFactory.CreateDataNavigate_Code(Code, Time);
                    this.LastOperator = OPERATOR_NAVIGATE;
                    return currentNavigate_Code.Forward(forwardPeriod);
                }
            }
            this.LastOperator = OPERATOR_NAVIGATE;
            return canForward;
        }

        public bool Forward(KLinePeriod forwardPeriod)
        {
            bool canForward = this.currentNavigate_Code.Forward(forwardPeriod);
            if (!canForward)
            {
                int endDate = this.DataPackage.EndDate;
                int codeEndDate = codeInfo.End;
                if (codeEndDate == 0)
                    codeEndDate = this.dataReader.TradingDayReader.FirstTradingDay;
                if (endDate < codeEndDate)
                {
                    currentNavigate_Code = dataNavigateFactory.CreateDataNavigate_Code(Code, Time);
                    this.LastOperator = OPERATOR_NAVIGATE;
                    return currentNavigate_Code.Forward(forwardPeriod);
                }
            }
            this.LastOperator = OPERATOR_NAVIGATE;
            return canForward;
        }

        public IKLineData GetKLineData(KLinePeriod period)
        {
            return this.currentRealTimeDataReader.GetKLineData(period);
        }

        public ITickData GetTickData()
        {
            return this.currentRealTimeDataReader.GetTickData();
        }

        public ITimeLineData GetTimeLineData()
        {
            return this.currentRealTimeDataReader.GetTimeLineData();
        }

        public bool IsPeriodEnd(KLinePeriod period)
        {
            return this.currentRealTimeDataReader.IsPeriodEnd(period);
        }

        public bool NavigateTo(double time)
        {
            if (Time == time)
                return true;
            bool canNav = this.currentNavigate_Code.NavigateTo(time);
            if (!canNav)
            {
                int tradingDay = this.dataReader.CreateTradingTimeReader(Code).GetTradingDay(time);
                if (!IsInTradingDay(codeInfo, tradingDay))
                    return false;
                this.currentNavigate_Code = dataNavigateFactory.CreateDataNavigate_Code(Code, time);
            }
            this.LastOperator = OPERATOR_NAVIGATE;
            return canNav;
        }

        private bool IsInTradingDay(CodeInfo codeInfo, int tradingDay)
        {
            if (codeInfo.Start != 0 && tradingDay < codeInfo.Start)
                return false;
            if (codeInfo.End != 0 && tradingDay > codeInfo.End)
                return false;
            return true;
        }

        public bool IsPlaying
        {
            get { return isPlaying; }
        }

        public IList<string> ListenedCodes
        {
            get
            {
                return new string[] { Code};
            }
        }

        public void Play()
        {
            this.isPlaying = true;
            if (dataForward_Code == null || this.LastOperator == OPERATOR_NAVIGATE)
            {
                if (this.dataForward_Code != null)
                    this.dataForward_Code.OnRealTimeChanged -= DataForward_Code_OnRealTimeChanged;
                ForwardReferedPeriods referedPeriods = new ForwardReferedPeriods(new KLinePeriod[] { KLinePeriod.KLinePeriod_1Minute, KLinePeriod.KLinePeriod_5Minute,
                    KLinePeriod.KLinePeriod_15Minute, KLinePeriod.KLinePeriod_1Hour, KLinePeriod.KLinePeriod_1Day }, true, true);
                ForwardPeriod forwardPeriod = new ForwardPeriod(true, KLinePeriod.KLinePeriod_1Minute);
                this.dataForward_Code = dataCenter.HistoryDataForwardFactory.CreateDataForward_Code(this.DataPackage, referedPeriods, forwardPeriod);
                this.dataForward_Code.OnRealTimeChanged += DataForward_Code_OnRealTimeChanged;
                this.dataForward_Code.NavigateTo(Time);
            }

            this.LastOperator = OPERATOR_PLAY;
            this.dataForward_Code.Play();
        }

        private void DataForward_Code_OnRealTimeChanged(object sender, RealTimeChangedArgument argument)
        {
            if (this.OnRealTimeChanged != null)
            {
                double prevTime = argument.PrevTime;
                double time = argument.Time;
                this.OnRealTimeChanged(this, new RealTimeChangedArgument(prevTime, time, this));
            }
            if (this.OnNavigateTo != null)
            {
                string prevCode = Code;
                string code = Code;
                double prevTime = argument.PrevTime;
                double time = argument.Time;
                this.OnNavigateTo(this, new DataNavigateEventArgs(prevCode, code, prevTime, time));
            }
        }

        public void Pause()
        {
            if (this.lastOperator == OPERATOR_NAVIGATE)
                return;
            this.dataForward_Code.Pause();
            this.isPlaying = false;
        }

        public void ChangeByDataPackage(IDataPackage_Code dataPackage, double time)
        {
            throw new NotImplementedException();
        }

        public IRealTimeData_Code GetRealTimeData(string code)
        {
            return this;
        }
    }
}