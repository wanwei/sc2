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
    public class DataNavigate_Code : IDataNavigate_Code
    {
        private string[] codes = new string[1];

        private DataForNavigate_Code dataForNavigate;

        internal DataNavigate_Code(DataForNavigate_Code dataForNavigate)
        {
            this.dataForNavigate = dataForNavigate;
            codes[0] = dataForNavigate.Code;
        }

        public DataNavigate_Code(IDataPackage_Code dataPackage, double time)
        {
            this.dataForNavigate = new DataForNavigate_Code(dataPackage, time);
            codes[0] = dataForNavigate.Code;
            this.NavigateTo(time);
        }

        public string Code
        {
            get
            {
                return dataForNavigate.Code;
            }
        }

        public IDataPackage_Code DataPackage
        {
            get
            {
                return dataForNavigate.DataPackage;
            }
        }

        public double Time
        {
            get
            {
                if (isTickForward)
                    return dataForward.Time;
                return dataForNavigate.Time;
            }
        }

        public float Price
        {
            get
            {
                if (isTickForward)
                    return dataForward.Price;
                return dataForNavigate.Price;
            }
        }

        public IList<string> ListenedCodes
        {
            get
            {
                return this.codes;
            }
        }

        public event DelegateOnNavigateTo OnNavigateTo;
        public event DelegateOnRealTimeChanged OnRealTimeChanged;

        public bool Backward(KLinePeriod forwardPeriod)
        {
            IKLineData klineData = GetKLineData(forwardPeriod);
            int prevBarPos = klineData.BarPos - 1;
            if (prevBarPos < 0)
                return false;
            double time = klineData.Arr_Time[prevBarPos];
            NavigateTo(time);
            return true;
        }

        public bool Forward(KLinePeriod forwardPeriod)
        {
            IKLineData klineData = GetKLineData(forwardPeriod);
            int nextBarPos = klineData.BarPos + 1;
            if (nextBarPos >= klineData.Length)
            {
                ITickData tickData = GetTickData();
                if (tickData.BarPos == tickData.Length - 1)
                    return false;
                double endtime = tickData.Arr_Time[tickData.Length - 1];
                NavigateTo(endtime);
                return true;
            }
            double time = klineData.Arr_Time[nextBarPos];
            NavigateTo(time);
            return true;
        }

        public bool Forward(ForwardPeriod forwardPeriod)
        {
            if (!forwardPeriod.IsTickForward)
                return Forward(forwardPeriod.KlineForwardPeriod);
            IDataForward_Code dataForward = GetDataForward();
            isTickForward = true;
            return dataForward.Forward();
        }

        public IKLineData GetKLineData(KLinePeriod period)
        {
            if (isTickForward)
                return dataForward.GetKLineData(period);
            return dataForNavigate.GetKLineData(period);
        }

        public ITickData GetTickData()
        {
            if (isTickForward)
                return dataForward.GetTickData();
            return dataForNavigate.GetTickData();
        }

        public ITimeLineData GetTimeLineData()
        {
            if (isTickForward)
                return dataForward.GetTimeLineData();
            return dataForNavigate.GetTimeLineData();
        }

        public bool IsPeriodEnd(KLinePeriod period)
        {
            return false;
        }

        public bool NavigateTo(double time)
        {
            this.isTickForward = false;
            double prevTime = time;
            bool canNav = this.dataForNavigate.NavigateTo(time);
            if (OnRealTimeChanged != null)
                OnRealTimeChanged(this, new RealTimeChangedArgument(prevTime, this.Time, this));
            if (OnNavigateTo != null)
                OnNavigateTo(this, new DataNavigateEventArgs(Code, Code, prevTime, time));
            return canNav;
        }

        public IRealTimeData_Code GetRealTimeData(string code)
        {
            if (!code.Equals(this.Code))
                return null;
            return this;
        }

        //现在是否是基于tick前进
        private bool isTickForward = false;

        private IDataForward_Code dataForward;

        private IDataForward_Code GetDataForward()
        {
            if (isTickForward)
            {
                return dataForward;
            }
            if (this.dataForward != null)
            {
                this.dataForward.NavigateTo(Time);
                return this.dataForward;
            }
            ForwardPeriod forwardPeriod = new ForwardPeriod(true, KLinePeriod.KLinePeriod_1Minute);
            ForwardReferedPeriods referedPeriods = new ForwardReferedPeriods();
            referedPeriods.UseTickData = true;
            referedPeriods.UsedKLinePeriods.Add(KLinePeriod.KLinePeriod_1Minute);
            referedPeriods.UsedKLinePeriods.Add(KLinePeriod.KLinePeriod_5Minute);
            referedPeriods.UsedKLinePeriods.Add(KLinePeriod.KLinePeriod_15Minute);
            referedPeriods.UsedKLinePeriods.Add(KLinePeriod.KLinePeriod_1Hour);
            referedPeriods.UsedKLinePeriods.Add(KLinePeriod.KLinePeriod_1Day);
            this.dataForward = DataCenter.Default.HistoryDataForwardFactory.CreateDataForward_Code(this.DataPackage, referedPeriods, forwardPeriod);
            this.dataForward.Forward();
            this.dataForward.NavigateTo(this.Time);
            this.dataForward.OnRealTimeChanged += DataForward_OnRealTimeChanged;
            return this.dataForward;
        }

        private void DataForward_OnRealTimeChanged(object sender, RealTimeChangedArgument argument)
        {
            if (OnRealTimeChanged != null)
                OnRealTimeChanged(this, new RealTimeChangedArgument(argument.PrevTime, this.Time, this));
            if (OnNavigateTo != null)
                OnNavigateTo(this, new DataNavigateEventArgs(Code, Code, argument.PrevTime, argument.Time));
        }
    }
}