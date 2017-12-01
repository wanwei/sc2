using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.data.reader;

namespace com.wer.sc.data.forward
{
    public class ForwardOnBarArgument : IForwardOnBarArgument
    {
        private IList<IForwardKLineBarInfo> klineData_BarFinished;

        private Dictionary<KLinePeriod, IForwardKLineBarInfo> dic_Period_Bar = new Dictionary<KLinePeriod, IForwardKLineBarInfo>();

        private IDataForward_Code dataForward_Code;

        public IDataForward_Code DataForward_Code
        {
            get { return dataForward_Code; }
        }

        public ForwardOnBarArgument(IList<IForwardKLineBarInfo> finishedBarInfos, IDataForward_Code dataForward_Code)
        {
            this.klineData_BarFinished = finishedBarInfos;
            this.dataForward_Code = dataForward_Code;
        }

        public string Code
        {
            get { return dataForward_Code.Code; }
        }

        public double Time
        {
            get { return dataForward_Code.Time; }
        }

        public IList<IForwardKLineBarInfo> AllFinishedBars
        {
            get
            {
                return klineData_BarFinished;
            }
        }

        public IForwardKLineBarInfo MainBar
        {
            get { return klineData_BarFinished[0]; }
        }

        public IRealTimeDataReader_Code CurrentData
        {
            get
            {
                return dataForward_Code;
            }
        }

        public IRealTimeDataReader_Code GetOtherData(string code)
        {
            return dataForward_Code.GetAttachedDataReader(code);
        }
    }
}
