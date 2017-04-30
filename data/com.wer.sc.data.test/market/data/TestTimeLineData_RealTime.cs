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
    public class TestTimeLineData_RealTime
    {
        [TestMethod]
        public void TestTimeLineDataReceive()
        {
            string code = "m05";
            int date = 20100111;
            TradingDayCache cache = new TradingDayCache(MockDataLoader.GetAllTradingDays());
            int lastOpenDate = cache.GetPrevTradingDay(date);

            IKLineData lastKlineData = MockDataLoader.GetKLineData(code, lastOpenDate, lastOpenDate, KLinePeriod.KLinePeriod_1Day);
            float lastEnd = lastKlineData.End;
            List<double> timeList = KLineTimeListUtils.GetKLineTimeList(date, lastOpenDate, MockDataLoader.GetTradingTime(code, date), KLinePeriod.KLinePeriod_1Minute);
            TimeLineData_RealTime timeLineData = new TimeLineData_RealTime(lastEnd, timeList);

            ITickData tickData = MockDataLoader.GetTickData(code, date);
            for (int i = 0; i < tickData.Length; i++)
            {
                tickData.BarPos = i;
                timeLineData.Receive(tickData);
                //Console.WriteLine(timeLineData);
                Assert.AreEqual(tickData.Price, timeLineData.Price);
            }

            AssertUtils.AssertEqual_TimeLineData("TimeLineData_RealTime", GetType(), timeLineData);
            //AssertUtils.PrintTimeLineData(timeLineData);
        }
    }
}
