using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy
{
    public interface IStrategyExecutorFactory_Current
    {
        IStrategyExecutor CreateExecutor(string code, StrategyReferedPeriods referedPeriods, StrategyForwardPeriod forwardPeriod);

        IStrategyExecutor CreateExecutor(string code, StrategyReferedPeriods referedPeriods, StrategyForwardPeriod forwardPeriod, IStrategyOperator strategyOperator);
    }
}
