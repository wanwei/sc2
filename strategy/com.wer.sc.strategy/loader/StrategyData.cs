using com.wer.sc.data.reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy.loader
{
    public class StrategyData : IStrategyData
    {
        private IStrategyInfo strategyInfo;
        private IStrategy strategy;

        public StrategyData(IStrategyInfo strategyInfo)
        {
            this.strategyInfo = strategyInfo;
        }

        public void RefreshStrategy()
        {
            this.strategy = strategyInfo.CreateStrategy();
        }

        public IStrategy Strategy
        {
            get
            {
                if (this.strategy == null)
                    this.strategy = strategyInfo.CreateStrategy();
                return strategy;
            }
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
