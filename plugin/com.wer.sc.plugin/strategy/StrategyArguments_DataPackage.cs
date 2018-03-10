using com.wer.sc.data.datapackage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy
{
    public class StrategyArguments_DataPackage : StrategyArgumentsAbstract
    {
        private IDataPackage_Code dataPackage;

        public StrategyArguments_DataPackage(IDataPackage_Code dataPackage, StrategyReferedPeriods referedPeriods, StrategyForwardPeriod forwardPeriod)
        {
            this.dataPackage = dataPackage;
            this.ReferedPeriods = referedPeriods;
            this.ForwardPeriod = forwardPeriod;
        }

        public StrategyArguments_DataPackage(IDataPackage_Code dataPackage, StrategyReferedPeriods referedPeriods, StrategyForwardPeriod forwardPeriod, IStrategyHelper strategyHelper) : this(dataPackage, referedPeriods, forwardPeriod)
        {
            this.StrategyHelper = strategyHelper;
        }

        public StrategyArguments_DataPackage(IDataPackage_Code dataPackage, StrategyReferedPeriods referedPeriods, StrategyForwardPeriod forwardPeriod, StrategyTraderSetting traderSetting) : this(dataPackage, referedPeriods, forwardPeriod)
        {
            this.TraderSetting = traderSetting;
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
    }
}