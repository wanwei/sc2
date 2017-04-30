using com.wer.sc.data.reader.cache;
using com.wer.sc.data.utils;
using com.wer.sc.mockdata;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.market.data
{
    [TestClass]
    public class TestKLineData_RealTime
    {
        [TestMethod]
        public void TestKLineDataReceive()
        {
            string code = "m05";
            int date = 20100111;
            TradingDayCache cache = new TradingDayCache(MockDataLoader.GetAllTradingDays());
            int lastOpenDate = cache.GetPrevTradingDay(date);
            IKLineData historyKLineData = MockDataLoader.GetKLineData(code, 20100101, lastOpenDate, KLinePeriod.KLinePeriod_1Minute);

            List<double[]> openTime = MockDataLoader.GetTradingTime(code, date);
            List<double> timeList = KLineTimeListUtils.GetKLineTimeList(date, lastOpenDate, openTime, KLinePeriod.KLinePeriod_1Minute);

            KLineData_RealTime klineData_Real = new KLineData_RealTime(historyKLineData, timeList, KLinePeriod.KLinePeriod_1Minute);

            ITickData tickData = MockDataLoader.GetTickData(code, date);
            for(int i = 0; i < tickData.Length; i++)
            {
                tickData.BarPos = i;
                klineData_Real.Receive(tickData);
                //Console.WriteLine(tickData);
                //Console.WriteLine(klineData_Real);
                Assert.AreEqual(tickData.Price, klineData_Real.End);
            }

            AssertUtils.AssertEqual_KLineData("KLineData_RealTime", GetType(), klineData_Real);
        }
    }
}
