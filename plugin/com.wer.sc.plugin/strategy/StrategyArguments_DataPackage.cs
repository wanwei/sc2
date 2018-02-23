using com.wer.sc.data.datapackage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy
{
    public class StrategyArguments_DataPackage
    {
        private IDataPackage_Code dataPackage;

        private StrategyReferedPeriods referedPeriods;

        private StrategyForwardPeriod forwardPeriod;

        public StrategyArguments_DataPackage()
        {

        }

        public StrategyArguments_DataPackage(IDataPackage_Code dataPackage, StrategyReferedPeriods referedPeriods, StrategyForwardPeriod forwardPeriod)
        {
            this.dataPackage = dataPackage;
            this.referedPeriods = referedPeriods;
            this.forwardPeriod = forwardPeriod;
        }

        public IDataPackage_Code DataPackage
        {
            get
            {
                return dataPackage;
            }

            set
            {
                dataPackage = value;
            }
        }

        public StrategyReferedPeriods ReferedPeriods
        {
            get
            {
                return referedPeriods;
            }

            set
            {
                referedPeriods = value;
            }
        }

        public StrategyForwardPeriod ForwardPeriod
        {
            get
            {
                return forwardPeriod;
            }

            set
            {
                forwardPeriod = value;
            }
        }
    }
}