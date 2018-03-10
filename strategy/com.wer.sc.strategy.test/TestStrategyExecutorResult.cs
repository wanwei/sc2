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
    public class TestStrategyExecutorResult
    {
        [TestMethod]
        public void TestStrategyResult_DataPackage()
        {
            //ICodePeriodFactory factory = DataCenter.Default.CodePeriodFactory;
            //ICodePeriod codePeriod = factory.CreateCodePeriod("rb1801", 20170501, 20170801);

            StrategyReferedPeriods referedPeriods = new StrategyReferedPeriods();
            referedPeriods.UsedKLinePeriods.Add(KLinePeriod.KLinePeriod_15Minute);
            referedPeriods.UseTickData = true;
            StrategyForwardPeriod forwardPeriod = new StrategyForwardPeriod(true, KLinePeriod.KLinePeriod_15Minute);
            //StrategyArguments_CodePeriod strategyCodePeriod = new StrategyArguments_CodePeriod(codePeriod, referedPeriods, forwardPeriod);
            IDataPackage_Code dataPackage = DataCenter.Default.DataPackageFactory.CreateDataPackage_Code("rb1801", 20170501, 20170801);
            StrategyArguments_DataPackage strategyArguments = new StrategyArguments_DataPackage(dataPackage, referedPeriods, forwardPeriod);
            //自动保存结果
            strategyArguments.IsSaveResult = true;
            IStrategyExecutor executor = StrategyCenter.Default.GetStrategyExecutorFactory().CreateExecutor_History(strategyArguments);
            MockStrategy_Results strategy = new MockStrategy_Results();
            strategy.Name = "策略结果保存";
            executor.Strategy = strategy;
            executor.Run();

            //IStrategyTrader trader = executor.StrategyHelper.Trader;

            int day = int.Parse(DateTime.Now.ToString("yyyyMMdd"));
            IStrategyResult result = executor.StrategyResult;
            string resultName = executor.StrategyResult.Name;

            IStrategyResultStore store = StrategyCenter.Default.StrategyResultStore;
            IList<int> savedDays = store.GetAllSavedDays();
            for(int i = 0; i < savedDays.Count; i++)
            {
                Console.Write(savedDays[i] + ",");
            }
            Console.WriteLine();

            IStrategyResult result2 = store.LoadStrategyResult(day, resultName);            
            Console.WriteLine(result2);
            Assert.AreEqual(result.ToString(), result2.ToString());

            Assert.AreEqual(1, result2.StrategyResult_Codes.Count);
            Assert.AreEqual(1, result2.StrategyQueryResultManager.GetQueryResults().Count);
            Console.WriteLine(result2.StrategyResult_Codes[0]);
            Assert.AreEqual(result.StrategyResult_Codes[0].ToString(), result2.StrategyResult_Codes[0].ToString());
        }

        [TestMethod]
        public void TestStrategyResult_CodePeriod_MainContract()
        {
            string code = "RB";
            int startDate = 20170101;
            int endDate = 20180101;
            ICodePeriod codePeriod = DataCenter.Default.CodePeriodFactory.CreateCodePeriod_MainContract(code, startDate, endDate);            

            StrategyReferedPeriods referedPeriods = new StrategyReferedPeriods();
            referedPeriods.UsedKLinePeriods.Add(KLinePeriod.KLinePeriod_15Minute);
            referedPeriods.UseTickData = true;
            StrategyForwardPeriod forwardPeriod = new StrategyForwardPeriod(true, KLinePeriod.KLinePeriod_15Minute);

            StrategyArguments_CodePeriod arguments = new StrategyArguments_CodePeriod(codePeriod,referedPeriods,forwardPeriod); 
            arguments.IsSaveResult = true;
            IStrategyExecutor executor = StrategyCenter.Default.GetStrategyExecutorFactory().CreateExecutor_History(arguments);

            //IStrategy strategy = StrategyGetter.GetStrategy(typeof(MockStrategy_Simple));
            MockStrategy_Results strategy = new MockStrategy_Results();
            strategy.Name = "策略结果保存";
            executor.Strategy = strategy;
            executor.Run();
        }
    }
}