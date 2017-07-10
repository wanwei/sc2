using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using com.wer.sc.data;
using com.wer.sc.data.reader;

namespace com.wer.sc.strategy.cnfutures
{
    [TestClass]
    public class TestStrategy_CnFutures
    {
        [TestMethod]
        public void TestMethod1()
        {
            IDataReader dataReader = CommonData.GetDataReader();

            string code = "RB1710";
            int start = 20170601;
            int endDate = 20170603;

            StrategyRunnerArguments args = new StrategyRunnerArguments();
            args.Code = code;
            args.StartDate = start;
            args.EndDate = endDate;
            args.IsTickForward = true;
            args.ForwardKLinePeriod = KLinePeriod.KLinePeriod_1Minute;
            StrategyRunner_History runner = new StrategyRunner_History(dataReader, args);
            

        }
    }
}
