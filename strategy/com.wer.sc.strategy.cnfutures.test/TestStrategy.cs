using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace com.wer.sc.strategy.cnfutures.test
{
    [TestClass]
    public class TestStrategy
    {
        [TestMethod]
        public void TestMethod1()
        {
            IStrategy strategy = new Strategy_CnFutures();

            string code = "";
            int startDate = 20100101;
            //int end

            //StrategyRunner_History runner = new StrategyRunner_History();
            //runner.SetStrategy(strategy);            
            //runner.Run();
        }
    }
}
