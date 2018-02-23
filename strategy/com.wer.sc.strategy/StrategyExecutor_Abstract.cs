using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy
{
    public abstract class StrategyExecutor_Abstract
    {
        public abstract StrategyExecutorInfo_CodePeriod GetStrategyExecutorInfo_CodePeriod();

        private StrategyExecutorInfo strategyExecutorInfo = null;

        public virtual StrategyExecutorInfo GetStrategyExecutorInfo()
        {
            if (strategyExecutorInfo != null)
                return strategyExecutorInfo;
            StrategyExecutorInfo_CodePeriod executorInfo = GetStrategyExecutorInfo_CodePeriod();
            
            return strategyExecutorInfo;
        }
    }
}
