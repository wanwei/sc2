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

            StrategyArguments_CodePeriod strategyCodePeriod = new StrategyArguments_CodePeriod(code, startDate, endDate, referedPeriods, forwardPeriod);
            IStrategyExecutor_Single executor = StrategyCenter.Default.GetStrategyExecutorFactory().CreateExecutor_History(strategyCodePeriod);

            IStrategy strategy = StrategyGetter.GetStrategy(typeof(MockStrategy_ReferOtherStrategy));
            executor.Strategy = strategy;
            executor.Run();
        }
    }
}
