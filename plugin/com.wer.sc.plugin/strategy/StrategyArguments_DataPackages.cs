using com.wer.sc.data.datapackage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy
{
    public class StrategyArguments_DataPackages
    {
        private List<IDataPackage_Code> dataPackages = new List<IDataPackage_Code>();
        private StrategyReferedPeriods referedPeriods;
        private StrategyForwardPeriod forwardPeriod;

        public List<IDataPackage_Code> DataPackages
        {
            get
            {
                return dataPackages;
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
