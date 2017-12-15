using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.utils.param;

namespace com.wer.sc.strategy
{
    /// <summary>
    /// 策略包
    /// </summary>
    public class StrategyPackage
    {
        private List<StrategyForTrade> strategies = new List<StrategyForTrade>();

        public List<StrategyForTrade> Strategies
        {
            get
            {
                return strategies;
            }
        }
    }

    public class StrategyForTrade
    {
        private IStrategy tradeStrategy;

        private List<IStrategy> filterStrategy = new List<IStrategy>();

        public IStrategy TradeStrategy
        {
            get
            {
                return tradeStrategy;
            }

            set
            {
                tradeStrategy = value;
            }
        }

        public List<IStrategy> FilterStrategy
        {
            get
            {
                return filterStrategy;
            }
        }
    }
}
