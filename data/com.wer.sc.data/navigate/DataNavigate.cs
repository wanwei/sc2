using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.data.datapackage;
using com.wer.sc.data.reader;

namespace com.wer.sc.data.navigate
{
    public class DataNavigate : IDataNavigate
    {
        private IDataNavigate_Code currentNavigate_Code;        

        private IDataNavigateFactory fac;

        public DataNavigate(IDataNavigateFactory fac, IDataNavigate_Code currentNavigate_Code)
        {
            this.currentNavigate_Code = currentNavigate_Code;
            this.fac = fac;
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
                return currentNavigate_Code.Time;
            }
        }

        public event DelegateOnNavigateTo OnNavigateTo;

        public event DelegateOnRealTimeChanged OnRealTimeChanged;

        public bool Backward(KLinePeriod forwardPeriod)
        {
            return currentNavigate_Code.Backward(forwardPeriod);
        }

        public void Change(string code)
        {
            this.currentNavigate_Code = fac.CreateDataNavigate_Code(code, this.Time);
        }

        public void Change(string code, double time)
        {
            this.currentNavigate_Code = fac.CreateDataNavigate_Code(code, time);
        }

        public bool Forward(KLinePeriod forwardPeriod)
        {
            return this.currentNavigate_Code.Forward(forwardPeriod);
        }

        public IKLineData GetKLineData(KLinePeriod period)
        {
            return this.currentNavigate_Code.GetKLineData(period);
        }

        public ITickData GetTickData()
        {
            return this.currentNavigate_Code.GetTickData();
        }

        public ITimeLineData GetTimeLineData()
        {
            return this.currentNavigate_Code.GetTimeLineData();
        }

        public bool IsPeriodEnd(KLinePeriod period)
        {
            return this.currentNavigate_Code.IsPeriodEnd(period);
        }

        public bool NavigateTo(double time)
        {
            return this.currentNavigate_Code.NavigateTo(time);
        }

        public void Play()
        {

        }

        public void Pause()
        {

        }
    }
}