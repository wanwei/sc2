using com.wer.sc.strategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.data.reader;

namespace com.wer.sc.strategy
{
    public abstract class StrategyAbstract : IStrategy
    {
        private StrategyHelper strategyHelper;

        public abstract StrategyReferedPeriods GetStrategyPeriods();

        public abstract void OnBar(IRealTimeDataReader currentData);

        public abstract void OnTick(IRealTimeDataReader currentData);

        public abstract void StrategyEnd();

        public abstract void StrategyStart();

        public StrategyHelper StrategyHelper
        {
            get { return strategyHelper; }
            set { strategyHelper = value; }
        }
    }
}
