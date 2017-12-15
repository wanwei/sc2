using com.wer.sc.data.reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy
{
    public class StrategyData : IStrategyData
    {
        private IStrategyInfo strategyInfo;
        private IStrategy strategy;
        public StrategyData(IStrategyInfo strategyInfo, IStrategy strategy)
        {
            this.strategyInfo = strategyInfo;
            this.strategy = strategy;
        }

        public IStrategy Strategy
        {
            get
            {
                return strategy;
            }
            set { this.strategy = value; }
        }

        public IStrategyInfo StrategyInfo
        {
            get
            {
                return strategyInfo;
            }
        }
    }
}
