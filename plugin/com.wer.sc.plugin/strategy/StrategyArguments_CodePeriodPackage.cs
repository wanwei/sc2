using com.wer.sc.data.datapackage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy
{
    public class StrategyArguments_CodePeriodPackage
    {
        private ICodePeriodPackage codePeriodPackage;
        private StrategyReferedPeriods referedPeriods;
        private StrategyForwardPeriod forwardPeriod;

        public StrategyArguments_CodePeriodPackage(ICodePeriodPackage codePeriodPackage, StrategyReferedPeriods referedPeriods, StrategyForwardPeriod forwardPeriod)
        {
            this.codePeriodPackage = codePeriodPackage;
            this.referedPeriods = referedPeriods;
            this.forwardPeriod = forwardPeriod;
        }

        public ICodePeriodPackage CodePeriodPackage
        {
            get
            {
                return codePeriodPackage;
            }

            set
            {
                codePeriodPackage = value;
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
