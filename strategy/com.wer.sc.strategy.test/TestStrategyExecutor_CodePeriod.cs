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
    public class TestStrategyExecutor_CodePeriod
    {
        [TestMethod]
        public void TestExecuteStrategy_CodePeriod()
        {
            string code = "RB1710";
            int startDate = 20170601;
            int endDate = 20170603;

            StrategyReferedPeriods referedPeriods = new StrategyReferedPeriods();
            referedPeriods.UseTickData = false;
            referedPeriods.UsedKLinePeriods.Add(KLinePeriod.KLinePeriod_1Minute);
            referedPeriods.UsedKLinePeriods.Add(KLinePeriod.KLinePeriod_5Minute);
            StrategyForwardPeriod forwardPeriod = new StrategyForwardPeriod(false, KLinePeriod.KLinePeriod_1Minute);

            StrategyArguments_CodePeriod strategyDataPackage = new StrategyArguments_CodePeriod(code, startDate, endDate, referedPeriods, forwardPeriod);
            IStrategyExecutor executor = StrategyCenter.Default.GetStrategyExecutorFactory().CreateExecutor_History(strategyDataPackage);

            IStrategy strategy = StrategyGetter.GetStrategy(typeof(MockStrategy_Simple));
            executor.Strategy = strategy;
            executor.Run();
        }
    }
}
