using com.wer.sc.data;
using com.wer.sc.data.datapackage;
using com.wer.sc.strategy.mock;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace com.wer.sc.strategy
{
    [TestClass]
    public class TestStrategyExecutorPool
    {
        private PoolDetector poolDetector = new PoolDetector();
        private bool isFinished = false;
        [TestMethod]
        public void TestExecuteStrategyExecutorPool()
        {
            try
            {
                StrategyExecutorPool pool = (StrategyExecutorPool)StrategyCenter.Default.GetStrategyExecutorPool();
                pool.ThreadCount = 5;
                pool.MaxExecutorCount = 10;

                pool.OnStrategyStart += Pool_OnStart;
                pool.OnStrategyDayFinished += Pool_OnDayFinished;
                pool.OnStrategyFinished += Pool_OnFinished;

                List<string> codes = new List<string>();
                codes.Add("rb");
                codes.Add("m");
                codes.Add("ma");
                codes.Add("i");
                codes.Add("j");
                codes.Add("jd");
                codes.Add("jm");
                codes.Add("p");
                codes.Add("pp");
                StrategyArguments_CodePeriodPackage strategyCodePeriodPackage = GetStrategyPackage(codes, 20170101, 20170601);

                IList<IStrategyExecutor> executors = StrategyCenter.Default.GetStrategyExecutorFactory().CreateExecutors_History(strategyCodePeriodPackage);
                for (int i = 0; i < executors.Count; i++)
                {
                    IStrategyExecutor executor = executors[i];
                    executor.Strategy = new MockStrategy_Empty();
                    pool.Queue(executor);
                }

                //pool.Run();
                long startTick = DateTime.Now.Ticks;
                pool.OnPoolFinished += Pool_OnPoolFinished;
                pool.Execute();
                //Thread.Sleep(20000);

                while (!isFinished)
                {
                }
                long endTick = DateTime.Now.Ticks;
                Console.WriteLine(endTick - startTick);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        private void Pool_OnPoolFinished(object sender)
        {
            isFinished = true;
        }

        private void Pool_OnFinished(object sender, StrategyFinishedArguments arguments)
        {
            poolDetector.EndExecutor(arguments.ExecutorInfo);
        }

        private void Pool_OnDayFinished(object sender, StrategyDayFinishedArguments arguments)
        {
            poolDetector.DayEndExecutor(arguments.ExecutorInfo);
        }

        private void Pool_OnStart(object sender, StrategyStartArguments arguments)
        {
            poolDetector.StartExecutor(arguments.ExecutorInfo);
        }

        private static StrategyArguments_CodePeriodPackage GetStrategyPackage(List<string> codes, int start, int end)
        {
            StrategyReferedPeriods referedPeriods = new StrategyReferedPeriods();
            referedPeriods.UseTickData = false;
            referedPeriods.UsedKLinePeriods.Add(KLinePeriod.KLinePeriod_1Minute);
            referedPeriods.UsedKLinePeriods.Add(KLinePeriod.KLinePeriod_5Minute);
            StrategyForwardPeriod forwardPeriod = new StrategyForwardPeriod(false, KLinePeriod.KLinePeriod_1Minute);

            ICodePeriodFactory codePeriodFactory = DataCenter.Default.CodePackageFactory;
            ICodePeriodPackage codePeriodPackage = codePeriodFactory.CreateCodePeriodPackage(codes, start, end, CodeChooseMethod.Maincontract);
            StrategyArguments_CodePeriodPackage strategyCodePeriodPackage = new StrategyArguments_CodePeriodPackage(codePeriodPackage, referedPeriods, forwardPeriod);
            return strategyCodePeriodPackage;
        }
    }

    public class PoolDetector
    {
        private List<IStrategyExecutorInfo> runningExecutor = new List<IStrategyExecutorInfo>();

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < runningExecutor.Count; i++)
            {
                if (i != 0)
                    sb.Append("\r\n");
                sb.Append(runningExecutor[i]);
            }
            //Console.WriteLine("\r\n");
            return sb.ToString();
        }

        public void StartExecutor(IStrategyExecutorInfo executorInfo)
        {
            this.runningExecutor.Add(executorInfo);
            Console.WriteLine("Start:" + executorInfo);
        }

        public void DayEndExecutor(IStrategyExecutorInfo executorInfo)
        {
            Console.WriteLine("DayEnd:" + executorInfo);
        }

        public void EndExecutor(IStrategyExecutorInfo executorInfo)
        {
            this.runningExecutor.Remove(executorInfo);
            Console.WriteLine("End:" + executorInfo);
        }
    }
}