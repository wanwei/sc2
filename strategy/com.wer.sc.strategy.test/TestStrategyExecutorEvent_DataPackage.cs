using com.wer.sc.data;
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
    public class TestStrategyExecutorEvent_DataPackage
    {
        [TestMethod]
        public void TestStrategyExecutorEvent1()
        {
            string code = "RB1710";
            int startDate = 20170601;
            int endDate = 20170603;
            ExecuteStrategy_Event(code, startDate, endDate);
        }

        [TestMethod]
        public void TestStrategyExecutorEvent2()
        {
            string code = "RB1710";
            int startDate = 20170101;
            int endDate = 20170603;
            ExecuteStrategy_Event(code, startDate, endDate);
        }

        private void ExecuteStrategy_Event(string code, int startDate, int endDate)
        {
            IDataPackage_Code dataPackage = CommonData.GetDataPackage(code, startDate, endDate);

            StrategyReferedPeriods referedPeriods = new StrategyReferedPeriods();
            referedPeriods.UseTickData = false;
            referedPeriods.UsedKLinePeriods.Add(KLinePeriod.KLinePeriod_1Minute);
            referedPeriods.UsedKLinePeriods.Add(KLinePeriod.KLinePeriod_5Minute);
            StrategyForwardPeriod forwardPeriod = new StrategyForwardPeriod(false, KLinePeriod.KLinePeriod_1Minute);

            StrategyArguments_DataPackage strategyCodePeriod = new StrategyArguments_DataPackage(dataPackage, referedPeriods, forwardPeriod);
            IStrategyExecutor_Single executor = StrategyCenter.Default.GetStrategyExecutorFactory().CreateExecutor_History(strategyCodePeriod);
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
            Console.WriteLine(arguments.ExecutorInfo.CurrentDay + " Finished");
        }

        private void Executor_OnBarFinished(object sender, StrategyBarFinishedArguments arguments)
        {
            //Console.WriteLine(arguments.ExecutorInfo.CurrentKLineData.GetCurrentBar() + " Finished");
        }
    }
}
