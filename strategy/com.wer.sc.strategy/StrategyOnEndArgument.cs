using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.data.reader;
using com.wer.sc.data.forward;

namespace com.wer.sc.strategy
{
    public class StrategyOnEndArgument : IStrategyOnEndArgument
    {
        private IDataForward_Code dataForward;

        public StrategyOnEndArgument(IDataForward_Code dataForward)
        {
            this.dataForward = dataForward;
        }

        public IRealTimeData_Code CurrentData
        {
            get
            {
                return dataForward;
            }
        }

        public IRealTimeData_Code GetOtherData(string code)
        {
            return dataForward.GetAttachedDataReader(code);
        }
    }
}
