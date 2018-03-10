using com.wer.sc.data;
using com.wer.sc.data.codeperiod;
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
    public class TestStrategyExecutorEvent_CodePeriod
    {
        [TestMethod]
        public void TestStrategyExecutorEvent_CodePeriod1()
        {
            string code = "RB";
            int startDate = 20170101;
            int endDate = 20180101;
            ICodePeriod codePeriod = DataCenter.Default.CodePeriodFactory.CreateCodePeriod_MainContract(code, startDate, endDate);

            StrategyReferedPeriods referedPeriods = new StrategyReferedPeriods();
            referedPeriods.UseTickData = false;
            referedPeriods.UsedKLinePeriods.Add(KLinePeriod.KLinePeriod_1Minute);
            referedPeriods.UsedKLinePeriods.Add(KLinePeriod.KLinePeriod_5Minute);
            StrategyForwardPeriod forwardPeriod = new StrategyForwardPeriod(false, KLinePeriod.KLinePeriod_1Minute);

            //StrategyArguments_DataPackage strategyCodePeriod = new StrategyArguments_DataPackage(dataPackage, referedPeriods, forwardPeriod);
            StrategyArguments_CodePeriod strategyCodePeriod = new StrategyArguments_CodePeriod(codePeriod, referedPeriods, forwardPeriod);
            IStrategyExecutor executor = StrategyCenter.Default.GetStrategyExecutorFactory().CreateExecutor_History(strategyCodePeriod);
            executor.OnBarFinished += Executor_OnBarFinished;
            executor.OnDayFinished += Executor_OnDayFinished;
            executor.OnFinished += Executor_OnFinished;

            IStrategy strategy = StrategyGetter.GetStrategy(typeof(MockStrategy_Empty));
            executor.Strategy = strategy;
            executor.Run();
        }

        private void Executor_OnFinished(object sender, StrategyFinishedArguments arguments)
        {
            Console.WriteLine("Strategy Finished");
        }

        private void Executor_OnDayFinished(object sender, StrategyDayFinishedArguments arguments)
        {
            IKLineData klineData = arguments.ExecutorInfo.CurrentKLineData;
            //Console.WriteLine(klineData.Code + "," + klineData + " Finished");
            Console.WriteLine(arguments.ExecutorInfo.CurrentDayIndex + "|" + arguments.ExecutorInfo.TotalDayCount);
        }

        private void Executor_OnBarFinished(object sender, StrategyBarFinishedArguments arguments)
        {
            //Console.WriteLine(arguments.ExecutorInfo.CurrentKLineData.GetCurrentBar() + " Finished");
        }
    }
}
