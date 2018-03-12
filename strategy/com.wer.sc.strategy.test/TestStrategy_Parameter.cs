using com.wer.sc.data;
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
    public class TestStrategy_Parameter
    {
        [TestMethod]
        public void TestParamter()
        {
            IStrategy strategy = new MockStrategy_Parameter();
            strategy.Parameters.SetParameterValue("PERIOD", KLinePeriod.KLinePeriod_15Minute);

            string code = "rb1710";
            int start = 20170601;
            int end = 20170603;
            IStrategyExecutor_Single executor = StrategyTestUtils.CreateExecutor_CodePeriod(code, start, end);
            executor.Strategy = strategy;
            executor.Run();

            //List<string> results = (List<string>)strategy.GetData("RESULT");
            //AssertUtils.AssertEqual_List("StrategyParameter_15Minute", GetType(), results);
        }
    }
}
