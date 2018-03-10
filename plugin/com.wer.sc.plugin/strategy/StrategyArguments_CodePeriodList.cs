using com.wer.sc.data.codeperiod;
using com.wer.sc.data.datapackage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy
{
    public class StrategyArguments_CodePeriodList : StrategyArgumentsAbstract
    {
        private ICodePeriodList codePeriodPackage;

        public StrategyArguments_CodePeriodList(ICodePeriodList codePeriodPackage, StrategyReferedPeriods referedPeriods, StrategyForwardPeriod forwardPeriod)
        {
            this.codePeriodPackage = codePeriodPackage;
            this.ReferedPeriods = referedPeriods;
            this.ForwardPeriod = forwardPeriod;
        }

        public StrategyArguments_CodePeriodList(ICodePeriodList codePeriodPackage, StrategyReferedPeriods referedPeriods, StrategyForwardPeriod forwardPeriod, StrategyTraderSetting traderSetting) : this(codePeriodPackage, referedPeriods, forwardPeriod)
        {
            this.TraderSetting = traderSetting;
        }

        public ICodePeriodList CodePeriodPackage
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
    }
}
