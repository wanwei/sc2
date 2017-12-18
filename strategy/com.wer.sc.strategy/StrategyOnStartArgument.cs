using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.data;
using com.wer.sc.data.forward;
using com.wer.sc.data.reader;

namespace com.wer.sc.strategy
{
    /// <summary>
    /// 
    /// </summary>
    public class StrategyOnStartArgument : IStrategyOnStartArgument
    {
        private IDataForward_Code dataForward;

        private StrategyReferedPeriods referedPeriods;

        private StrategyForwardPeriod forwardPeriod;

        public StrategyOnStartArgument(IDataForward_Code dataForward, StrategyReferedPeriods referedPeriods, StrategyForwardPeriod forwardPeriod)
        {
            this.dataForward = dataForward;
            this.referedPeriods = referedPeriods;
            this.forwardPeriod = forwardPeriod;
        }

        public StrategyReferedPeriods ReferedPeriods
        {
            get
            {
                return referedPeriods;
            }
        }

        public StrategyForwardPeriod ForwardPeriod
        {
            get
            {
                return forwardPeriod;
            }
        }

        public IRealTimeDataReader_Code CurrentData
        {
            get
            {
                return dataForward;
            }
        }

        public IRealTimeDataReader_Code GetOtherData(string code)
        {
            return dataForward.GetAttachedDataReader(code);
        }
    }
}
