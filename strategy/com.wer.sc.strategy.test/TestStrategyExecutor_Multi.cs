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
    public class TestStrategyExecutor_Multi
    {
        private int finishedCount;

        [TestMethod]
        public void TestStrategyExecute_Multi()
        {
            finishedCount = 0;
            string[] codes = new string[] { "rb", "ma", "m", "a", "j", "jd" };
            int startDate = 20170101;
            int endDate = 20180101;
            ICodePeriodList codePeriodPackage =
                StrategyCenter.Default.BelongDataCenter.CodePeriodFactory.CreateCodePeriodList(codes, startDate, endDate, CodeChooseMethod.Maincontract);

            StrategyReferedPeriods referedPeriods = new StrategyReferedPeriods();
            referedPeriods.UseTickData = false;
            referedPeriods.UsedKLinePeriods.Add(KLinePeriod.KLinePeriod_1Minute);
            referedPeriods.UsedKLinePeriods.Add(KLinePeriod.KLinePeriod_5Minute);
            StrategyForwardPeriod forwardPeriod = new StrategyForwardPeriod(false, KLinePeriod.KLinePeriod_1Minute);

            StrategyArguments_CodePeriodList arguments = new StrategyArguments_CodePeriodList(codePeriodPackage, referedPeriods, forwardPeriod);
            IStrategyExecutor_Multi executor = StrategyCenter.Default.GetStrategyExecutorFactory().CreateExecutor_Multi_History(arguments);
            IStrategy strategy = StrategyGetter.GetStrategy(typeof(MockStrategy_Event));
            executor.Strategy = strategy;
            executor.OnStrategyDayFinished += Executor_OnStrategyDayFinished;
            executor.OnStrategyFinished += Executor_OnStrategyFinished;
            executor.Execute();

            StrategyCenter.Default.GetStrategyExecutorPool().ThreadCount = 2;
            StrategyCenter.Default.GetStrategyExecutorPool().MaxExecutorCount = 3;
            StrategyCenter.Default.GetStrategyExecutorPool().Execute();
            while (finishedCount < 6)
            {

            }
        }

        private void Executor_OnStrategyDayFinished(object sender, StrategyDayFinishedArguments arguments)
        {
            IStrategyExecutorInfo executorInfo = arguments.ExecutorInfo;
            Console.WriteLine(executorInfo.CurrentKLineData.Code + "," + executorInfo.CurrentKLineData);
        }

        private void Executor_OnStrategyFinished(object sender, StrategyFinishedArguments arguments)
        {
            IStrategyExecutorInfo executorInfo = arguments.ExecutorInfo;
            Console.WriteLine("Finished:" + executorInfo.CurrentKLineData.Code + "," + executorInfo.CurrentKLineData);
            finishedCount++;
        }
    }
}
