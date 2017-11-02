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
        private IList<IForwardOnbar_Info> klineData_BarFinished;

        private Dictionary<KLinePeriod, IForwardOnbar_Info> dic_Period_Bar = new Dictionary<KLinePeriod, IForwardOnbar_Info>();

        private IDataForward_Code dataForward_Code;

        public ForwardOnBarArgument(IList<IForwardOnbar_Info> barFinishedInfo, IDataForward_Code dataForward_Code)
        {
            this.klineData_BarFinished = barFinishedInfo;
            this.dataForward_Code = dataForward_Code;
        }

        public IList<IForwardOnbar_Info> ForwardOnBar_Infos
        {
            get
            {
                return klineData_BarFinished;
            }
        }

        public IForwardOnbar_Info MainForwardOnBar_Info
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
