using com.wer.sc.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy
{
    public class StrategyTestUtils
    {
        public static IStrategyExecutor GetExecutor(string code, int start, int end)
        {
            IStrategyExecutorFactory_History executorFactory = StrategyCenter.Default.GetStrategyExecutorFactory_History();
            List<KLinePeriod> usedKLinePeriods = new List<KLinePeriod>();
            usedKLinePeriods.Add(KLinePeriod.KLinePeriod_1Minute);
            usedKLinePeriods.Add(KLinePeriod.KLinePeriod_5Minute);
            usedKLinePeriods.Add(KLinePeriod.KLinePeriod_15Minute);
            usedKLinePeriods.Add(KLinePeriod.KLinePeriod_1Hour);
            usedKLinePeriods.Add(KLinePeriod.KLinePeriod_1Day);
            StrategyReferedPeriods referedPeriods = new StrategyReferedPeriods(usedKLinePeriods, true, true);
            StrategyForwardPeriod forwardPeriod = new StrategyForwardPeriod(true, KLinePeriod.KLinePeriod_1Minute);
            IStrategyExecutor executor = executorFactory.CreateExecutor(code, start, end, referedPeriods, forwardPeriod);
            return executor;
        }
    }
}
