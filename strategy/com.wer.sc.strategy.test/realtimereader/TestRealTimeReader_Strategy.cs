using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using com.wer.sc.data.reader;
using com.wer.sc.strategy;
using com.wer.sc.data;

namespace com.wer.sc.strategy.realtimereader
{
    [TestClass]
    public class TestRealTimeReader_Strategy
    {
        [TestMethod]
        public void TestMethod1()
        {
            IDataReader dataReader = CommonData.GetDataReader();
            string code = "RB1710";
            int start = 20170601;
            int endDate = 20170610;
            StrategyReferedPeriods referedPeriods = new StrategyReferedPeriods();
            referedPeriods.isReferTimeLineData = false;
            referedPeriods.UseTickData = false;
            referedPeriods.UsedKLinePeriods.Add(KLinePeriod.KLinePeriod_1Minute);
            RealTimeReader_Strategy realTimeReader = new RealTimeReader_Strategy(dataReader, code, start, endDate, referedPeriods);
            realTimeReader.SetForwardPeriod(false, KLinePeriod.KLinePeriod_1Minute);

            while (!realTimeReader.IsEnd)
            {
                realTimeReader.Forward();
                IKLineData klineData = realTimeReader.GetKLineData(KLinePeriod.KLinePeriod_1Minute);
                Console.WriteLine(klineData);
            }
        }

    }
}
