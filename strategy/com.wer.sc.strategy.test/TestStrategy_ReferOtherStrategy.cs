using com.wer.sc.data;
using com.wer.sc.strategy.mock;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy
{
    [TestClass]
    public class TestStrategy_ReferOtherStrategy
    {
        [TestMethod]
        public void TestReferOtherStrategy()
        {
            string code = "RB1710";
            int startDate = 20170601;
            int endDate = 20170603;

            StrategyReferedPeriods referedPeriods = new StrategyReferedPeriods();
            referedPeriods.UseTickData = false;
            referedPeriods.UsedKLinePeriods.Add(KLinePeriod.KLinePeriod_1Minute);
            referedPeriods.UsedKLinePeriods.Add(KLinePeriod.KLinePeriod_5Minute);
            StrategyForwardPeriod forwardPeriod = new StrategyForwardPeriod(false, KLinePeriod.KLinePeriod_1Minute);

            IStrategyExecutor executor = StrategyCenter.Default.GetStrategyExecutorFactory_History().CreateExecutor(code, startDate, endDate, referedPeriods, forwardPeriod, null);

            IStrategy strategy = StrategyGetter.GetStrategy(typeof(MockStrategy_ReferOtherStrategy));
            executor.SetStrategy(strategy);
            executor.Run();
        }
    }
}
