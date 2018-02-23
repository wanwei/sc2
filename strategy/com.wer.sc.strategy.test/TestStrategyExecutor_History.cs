using com.wer.sc.data;
using com.wer.sc.data.datapackage;
using com.wer.sc.data.utils;
using com.wer.sc.mockdata;
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
    public class TestStrategyExecutor_History
    {
        [TestMethod]
        public void TestRunStrategy()
        {
            string code = "rb1710";
            int start = 20170601;
            int end = 20170603;
            IStrategyExecutor executor = StrategyTestUtils.CreateExecutor_CodePeriod(code, start, end);
            MockStrategy strategy = new MockStrategy();
            executor.Strategy = strategy;
            //executor.Execute();
            executor.Run();
            //AssertUtils.PrintLineList(strategy.PrintData);
            AssertUtils.AssertEqual_List("executorhistory", GetType(), strategy.PrintData);
        }

        [TestMethod]
        public void TestExecuteStrategy()
        {
            string code = "rb1710";
            int start = 20170601;
            int end = 20170603;
            IStrategyExecutor executor = StrategyTestUtils.CreateExecutor_CodePeriod(code, start, end);
            MockStrategy strategy = new MockStrategy();
            executor.Strategy = strategy;
            executor.OnFinished += Executor_OnFinished;
            executor.Execute();
            //AssertUtils.AssertEqual_List("executorhistory", GetType(), strategy.PrintData);
        }

        private void Executor_OnFinished(object sender, StrategyFinishedArguments arguments)
        {
            AssertUtils.AssertEqual_List("executorhistory", GetType(), ((MockStrategy)arguments.Strategy).PrintData);
        }        

        //public static IStrategyExecutor GetExecutor(string code, int start, int end)
        //{
        //    IStrategyExecutorFactory_History executorFactory = StrategyCenter.Default.GetStrategyExecutorFactory_History();
        //    List<KLinePeriod> usedKLinePeriods = new List<KLinePeriod>();
        //    usedKLinePeriods.Add(KLinePeriod.KLinePeriod_1Minute);
        //    usedKLinePeriods.Add(KLinePeriod.KLinePeriod_5Minute);
        //    usedKLinePeriods.Add(KLinePeriod.KLinePeriod_15Minute);
        //    usedKLinePeriods.Add(KLinePeriod.KLinePeriod_1Hour);
        //    usedKLinePeriods.Add(KLinePeriod.KLinePeriod_1Day);
        //    StrategyReferedPeriods referedPeriods = new StrategyReferedPeriods(usedKLinePeriods, true, true);
        //    StrategyForwardPeriod forwardPeriod = new StrategyForwardPeriod(true, KLinePeriod.KLinePeriod_1Minute);
        //    IStrategyExecutor executor = executorFactory.CreateExecutor(code, start, end, referedPeriods, forwardPeriod);
        //    return executor;
        //}

        [TestMethod]
        public void TestExecuteStrategy_MA()
        {
            string code = "rb1710";
            int start = 20170601;
            int end = 20170603;
            IStrategyExecutor executor = StrategyTestUtils.CreateExecutor_CodePeriod(code, start, end);
            MockStrategy_Ma strategy = new MockStrategy_Ma();
            executor.Strategy = strategy;
            executor.Run();

            List<float> floats = strategy.MAList;
            IDataPackage_Code dataPackage = DataCenter.Default.DataPackageFactory.CreateDataPackage_Code(code, start, end);
            IKLineData_Extend klineData = dataPackage.GetKLineData(KLinePeriod.KLinePeriod_1Minute);
            List<float> floats2 = KLineDataUtils.MaList(klineData, klineData.BarPos, klineData.Length - 1, 5);
            for(int i = 0; i < floats.Count; i++)
            {
                Console.WriteLine(floats[i] + "," + floats2[i]);
            }
            AssertUtils.AssertEqual_List(floats2, floats);
        }

        private double GetMa(IKLineData klineData, int index, int len)
        {
            double d = 0;

            return d / len;
        }
    }
}
