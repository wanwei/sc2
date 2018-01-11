using com.wer.sc.data;
using com.wer.sc.mockdata;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data
{
    [TestClass]
    public class TestKLineTimeInfo
    {
        [TestMethod]
        public void TestGetKLineTimeInfo_Day()
        {
            string code = "rb1805";
            int start = 20171015;
            int end = 20171020;
            IKLineData klineData = DataCenter.Default.DataReader.KLineDataReader.GetData(code, start, end, KLinePeriod.KLinePeriod_1Minute);            
            IList<ITradingTime> tradingTimes = DataCenter.Default.DataReader.CreateTradingTimeReader(code).GetTradingTime(start, end);
            KLineTimeInfo klineTimeInfo = new KLineTimeInfo(klineData, tradingTimes);

            AssertUtils.PrintList(klineTimeInfo.TradingDays);
            Console.WriteLine();
            //for(int i = 0; i < klineTimeInfo.TradingDays.Count; i++)
            //{
            //    int tradingDay = klineTimeInfo.TradingDays[i];
            //    // klineTimeInfo.GetKLineTimeInfo_Day(tradingDay);
            //}
            Console.WriteLine(klineTimeInfo);

            KLineTimeInfo_TradingPeriods periods = klineTimeInfo.GetTradingPeriodsByBarPos(350);
            Assert.AreEqual("TradingPeriods:0,345,464", periods.ToString());
            //Console.WriteLine(periods);
        }
    }
}
