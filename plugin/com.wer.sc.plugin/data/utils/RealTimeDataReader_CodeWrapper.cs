using com.wer.sc.data.reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.utils
{
    public class RealTimeDataReader_CodeWrapper : IRealTimeDataReader_Code
    {
        private IRealTimeDataReader_Code realTimeDataReader_Code;

        public RealTimeDataReader_CodeWrapper(IRealTimeDataReader_Code realTimeDataReader_Code)
        {
            this.realTimeDataReader_Code = realTimeDataReader_Code;
            this.realTimeDataReader_Code.OnRealTimeChanged += RealTimeDataReader_Code_OnRealTimeChanged;
        }

        private void RealTimeDataReader_Code_OnRealTimeChanged(object sender, RealTimeChangedArgument argument)
        {
            if (OnRealTimeChanged != null)
                OnRealTimeChanged(this, argument);
        }

        public string Code
        {
            get
            {
                return realTimeDataReader_Code.Code;
            }
        }

        public double Time
        {
            get
            {
                return realTimeDataReader_Code.Time;
            }
        }

        public event DelegateOnRealTimeChanged OnRealTimeChanged;

        public IKLineData GetKLineData(KLinePeriod period)
        {
            return realTimeDataReader_Code.GetKLineData(period);
        }

        public ITickData GetTickData()
        {
            return realTimeDataReader_Code.GetTickData();
        }

        public ITimeLineData GetTimeLineData()
        {
            return realTimeDataReader_Code.GetTimeLineData();
        }

        public bool IsPeriodEnd(KLinePeriod period)
        {
            return realTimeDataReader_Code.IsPeriodEnd(period);
        }
    }
}
