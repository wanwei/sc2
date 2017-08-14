using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using com.wer.sc.data;
using com.wer.sc.data.reader;
using com.wer.sc.data.datapackage;

namespace com.wer.sc.strategy.cnfutures
{
    [TestClass]
    public class TestStrategy_CnFutures
    {
        [TestMethod]
        public void TestMethod1()
        {
            //IDataReader dataReader = CommonData.GetDataReader();
            //IDataPackage dataPackage = CommonData.g
            string code = "RB1710";
            int start = 20170601;
            int endDate = 20170603;

            IDataPackage dataPackage = CommonData.GetDataPackage(code, start, endDate);

            //StrategyRunnerArguments args = new StrategyRunnerArguments();
            //args.Code = code;
            //args.StartDate = start;
            //args.EndDate = endDate;
            //args.IsTickForward = true;
            //args.ForwardKLinePeriod = KLinePeriod.KLinePeriod_1Minute;
            StrategyReferedPeriods referedPeriods = new StrategyReferedPeriods();
            referedPeriods.UseTickData = true;
            referedPeriods.UsedKLinePeriods.Add(KLinePeriod.KLinePeriod_1Minute);
            data.forward.ForwardPeriod forwardPeriod = new data.forward.ForwardPeriod(true,KLinePeriod.KLinePeriod_1Minute);
            //StrategyExecutor_History runner = new StrategyExecutor_History(dataReader, args);
            StrategyExecutor_History runner = new StrategyExecutor_History(dataPackage, referedPeriods, forwardPeriod);
        }
    }
}
