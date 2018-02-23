using com.wer.sc.data.datapackage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy
{
    public class StrategyArguments_CodePeriod
    {
        private ICodePeriod codePeriod;
        private StrategyReferedPeriods referedPeriods;
        private StrategyForwardPeriod forwardPeriod;

        public StrategyArguments_CodePeriod(string code, int startDate, int endDate, StrategyReferedPeriods referedPeriods, StrategyForwardPeriod forwardPeriod)
        {
            this.codePeriod = new CodePeriod(code, startDate, endDate);
            this.referedPeriods = referedPeriods;
            this.forwardPeriod = forwardPeriod;
        }

        public StrategyArguments_CodePeriod(ICodePeriod codePeriod, StrategyReferedPeriods referedPeriods, StrategyForwardPeriod forwardPeriod)
        {
            this.codePeriod = codePeriod;
            this.referedPeriods = referedPeriods;
            this.forwardPeriod = forwardPeriod;
        }

        public ICodePeriod CodePeriod
        {
            get
            {
                return codePeriod;
            }

            set
            {
                codePeriod = value;
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