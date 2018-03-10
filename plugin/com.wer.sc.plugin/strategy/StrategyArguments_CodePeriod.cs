using com.wer.sc.data.codeperiod;
using com.wer.sc.data.datapackage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy
{
    public class StrategyArguments_CodePeriod : StrategyArgumentsAbstract
    {
        private bool closeOnCodeChanged;

        private ICodePeriod codePeriod;

        public StrategyArguments_CodePeriod(string code, int startDate, int endDate, StrategyReferedPeriods referedPeriods, StrategyForwardPeriod forwardPeriod) : this(new CodePeriod(code, startDate, endDate), referedPeriods, forwardPeriod)
        {
        }

        public StrategyArguments_CodePeriod(ICodePeriod codePeriod, StrategyReferedPeriods referedPeriods, StrategyForwardPeriod forwardPeriod)
        {
            this.codePeriod = codePeriod;
            this.ReferedPeriods = referedPeriods;
            this.ForwardPeriod = forwardPeriod;
        }

        public StrategyArguments_CodePeriod(ICodePeriod codePeriod, StrategyReferedPeriods referedPeriods, StrategyForwardPeriod forwardPeriod, StrategyTraderSetting traderSetting) : this(codePeriod, referedPeriods, forwardPeriod)
        {
            this.TraderSetting = traderSetting;
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

        public bool CloseOnCodeChanged
        {
            get
            {
                return closeOnCodeChanged;
            }

            set
            {
                closeOnCodeChanged = value;
            }
        }
    }
}