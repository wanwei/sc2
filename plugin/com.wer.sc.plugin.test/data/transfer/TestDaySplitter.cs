using com.wer.sc.data.utils;
using com.wer.sc.mockdata;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.transfer
{
    [TestClass]
    public class TestDaySplitter
    {
        [TestMethod]
        public void TestSplit_Normal()
        {
            AssertDaySplitter("m05", 20131201, 20131231, KLinePeriod.KLinePeriod_1Minute, "DaySplit_M05_20131201_20131231");
        }

        private void AssertDaySplitter(string code, int start, int end, KLinePeriod period, string fileName)
        {
            CacheUtils_TradingSession cache = GetTradingSessionCache(code);

            IKLineData klineData = MockDataLoader.GetKLineData(code, start, end, period);
            List<SplitterResult> results = DaySplitter.Split(klineData, cache);

            AssertUtils.AssertEqual_List<SplitterResult>(fileName, GetType(), results);
            //AssertUtils.PrintKLineData(klineData);
            //AssertUtils.PrintLineList(results);
        }

        private static CacheUtils_TradingSession GetTradingSessionCache(string code)
        {
            List<TradingSession> tradingSessions = MockDataLoader.GetTradingSessions(code);
            CacheUtils_TradingSession cache = new CacheUtils_TradingSession(code, tradingSessions);
            return cache;
        }

        [TestMethod]
        public void TestSplit_Night()
        {
            AssertDaySplitter("m05", 20150625, 20150715, KLinePeriod.KLinePeriod_1Minute, "DaySplit_M05_20150625_20150715");
        }

        [TestMethod]
        public void TestSplit_OverNightWeekend()
        {
            AssertDaySplitter("m05", 20141229, 20150115, KLinePeriod.KLinePeriod_1Minute, "DaySplit_M05_20141229_20150115");
        }

        [TestMethod]
        public void TestSplit_EndInNight()
        {
            IKLineData klineData = MockDataLoader.GetKLineData("m05", 20150105, 20150106, KLinePeriod.KLinePeriod_1Minute);
            IKLineData subData = klineData.Sub(0, 570);
            List<SplitterResult> results = DaySplitter.Split(subData, GetTradingSessionCache("m05"));            
            Assert.AreEqual("20150105,0", results[0].ToString());
            Assert.AreEqual("20150106,225", results[1].ToString());
        }
    }

    //class MockTimeGetter : TimeGetter
    //{
    //    private IKLineData klineData;
    //    public MockTimeGetter(string code, int start, int end, List<double[]> openTime)
    //    {
    //        this.klineData = DataTestUtils.GetKLineData(code, start, end, new KLinePeriod(KLinePeriod.TYPE_MINUTE, 1), openTime);
    //    }

    //    public MockTimeGetter(IKLineData klineData)
    //    {
    //        this.klineData = klineData;
    //    }

    //    public int Count
    //    {
    //        get
    //        {
    //            return klineData.Length;
    //        }
    //    }

    //    public double GetTime(int index)
    //    {
    //        return klineData.Arr_Time[index];
    //    }
    //}
}
