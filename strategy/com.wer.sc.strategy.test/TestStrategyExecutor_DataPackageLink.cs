using com.wer.sc.data;
using com.wer.sc.data.codeperiod;
using com.wer.sc.data.datapackage;
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
    public class TestStrategyExecutor_DataPackageLink
    {
        [TestMethod]
        public void TestExecutor_DataPackageLink()
        {
            string code = "RB";
            int startDate = 20170101;
            int endDate = 20180101;
            ICodePeriod codePeriod = DataCenter.Default.CodePeriodFactory.CreateCodePeriod_MainContract(code, startDate, endDate);

            StrategyReferedPeriods referedPeriods = new StrategyReferedPeriods();
            referedPeriods.UsedKLinePeriods.Add(KLinePeriod.KLinePeriod_15Minute);
            referedPeriods.UseTickData = true;
            StrategyForwardPeriod forwardPeriod = new StrategyForwardPeriod(true, KLinePeriod.KLinePeriod_15Minute);

            StrategyArguments_CodePeriod arguments = new StrategyArguments_CodePeriod(codePeriod, referedPeriods, forwardPeriod);     
            IStrategyExecutor executor = StrategyCenter.Default.GetStrategyExecutorFactory().CreateExecutor_History(arguments);

            IStrategy strategy = StrategyGetter.GetStrategy(typeof(MockStrategy_Simple));
            executor.Strategy = strategy;
            executor.Run();
        }
    }
}
