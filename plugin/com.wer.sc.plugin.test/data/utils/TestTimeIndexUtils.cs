using com.wer.sc.mockdata;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace com.wer.sc.data.utils
{
    [TestClass]
    public class TestTimeIndexUtils
    {
        [TestMethod]
        public void TestIndexOfKLine()
        {
            IKLineData klineData = MockDataLoader.GetKLineData("m1505", 20141215, 20150116, KLinePeriod.KLinePeriod_1Minute);
            int index = TimeIndeierUtils.IndexOfTime_KLine(klineData, 20141225.090100);
            Assert.AreEqual(1801, index);

            index = TimeIndeierUtils.IndexOfTime_KLine(klineData, 20141225.090059);
            Assert.AreEqual(1800, index);

            index = TimeIndeierUtils.IndexOfTime_KLine(klineData, 20141225.090059, false);
            Assert.AreEqual(1801, index);
        }

        [TestMethod]
        public void TestIndexOfTick()
        {
            ITickData tickData = MockDataLoader.GetTickData("m1505", 20150121);
            int index = TimeIndeierUtils.IndexOfTime_Tick(tickData, 20150120.210116);
            Assert.AreEqual(147, index);

            tickData = MockDataLoader.GetTickData("m0805", 20070920);
            index = TimeIndeierUtils.IndexOfTime_Tick(tickData, 20070919.093027, true, 0);
            for(int i = 0; i < tickData.Length; i++)
            {
                double time = tickData.Arr_Time[i];
                index = TimeIndeierUtils.IndexOfTime_Tick(tickData, time);
                Assert.AreEqual(time, tickData.Arr_Time[index]);
            }
        }

        [TestMethod]
        public void TestIndexOfTick_Repeat()
        {
            ITickData tickData = MockDataLoader.GetTickData("m0805", 20070919);

            int index = TimeIndeierUtils.IndexOfTime_Tick(tickData, 20070919.092414, true, 0);
            Assert.AreEqual(2416, index);
            index = TimeIndeierUtils.IndexOfTime_Tick(tickData, 20070919.092414, true, 1);
            Assert.AreEqual(2417, index);
            index = TimeIndeierUtils.IndexOfTime_Tick(tickData, 20070919.092414, true, 2);
            Assert.AreEqual(2418, index);
            index = TimeIndeierUtils.IndexOfTime_Tick(tickData, 20070919.092414, true, 3);
            Assert.AreEqual(2419, index);

            //index = TimeIndeierUtils.IndexOfTime_Tick(tickData, 20070919.092414, false, 0);
            //Assert.AreEqual(2416, index);
            //index = TimeIndeierUtils.IndexOfTime_Tick(tickData, 20070919.092414, false, 1);
            //Assert.AreEqual(2417, index);
            //index = TimeIndeierUtils.IndexOfTime_Tick(tickData, 20070919.092414, false, 2);
            //Assert.AreEqual(2418, index);
            //index = TimeIndeierUtils.IndexOfTime_Tick(tickData, 20070919.092414, false, 3);
            //Assert.AreEqual(2419, index);
            //Console.WriteLine(index);
            //Console.WriteLine(tickData.Arr_Time[index]);
            //Console.WriteLine(tickData.GetBar(index));

        }
    }
}
