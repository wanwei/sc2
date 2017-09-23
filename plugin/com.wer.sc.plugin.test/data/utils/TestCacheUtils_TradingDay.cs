using com.wer.sc.mockdata;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.utils
{
    [TestClass]
    public class TestTradingDayCache
    {
        private static CacheUtils_TradingDay tradingDayCache;

        private static CacheUtils_TradingDay GetTradingDayCache()
        {
            if (tradingDayCache != null)
                return tradingDayCache;

            String[] lines = TestCaseManager.LoadTestCaseFile(typeof(TestTradingDayCache), "OpenDate_Cache").Split('\r');
            List <int> openDates = new List<int>(lines.Length);
            for (int i = 0; i < lines.Length; i++)
            {
                openDates.Add(int.Parse(lines[i].Trim()));
            }
            tradingDayCache = new CacheUtils_TradingDay(openDates);
            return tradingDayCache;
        }

        [TestMethod]
        public void TestTradingDayCache_IsOpen()
        {
            CacheUtils_TradingDay cache = GetTradingDayCache();
            Assert.IsFalse(cache.IsTrade(19900101));
            Assert.IsTrue(cache.IsTrade(20100201));
            Assert.IsFalse(cache.IsTrade(20100206));
            Assert.IsFalse(cache.IsTrade(20170101));
        }

        [TestMethod]
        public void TestTradingDayCache_GetAllOpenDates()
        {
            CacheUtils_TradingDay cache = GetTradingDayCache();
            
            String[] lines = TestCaseManager.LoadTestCaseFile(typeof(TestTradingDayCache), "OpenDate_Cache").Split('\r');
            List<int> openDates = cache.GetAllTradingDays();
            for (int i = 0; i < lines.Length; i++)
                Assert.AreEqual(int.Parse(lines[i]), openDates[i]);
            Assert.AreEqual(lines.Length, openDates.Count);
        }

        [TestMethod]
        public void TestTradingDayCache_FirstLastOpenDate()
        {
            CacheUtils_TradingDay cache = GetTradingDayCache();
            Assert.AreEqual(20040102, cache.FirstTradingDay);
            Assert.AreEqual(20160429, cache.LastTradingDay);
        }

        [TestMethod]
        public void TestTradingDayCache_GetOpenDates()
        {
            CacheUtils_TradingDay cache = GetTradingDayCache();
            List<int> allOpenDates = cache.GetAllTradingDays();
            IList<int> openDates = cache.GetTradingDays(20110101, 20110205);
            for (int i = 0; i < openDates.Count; i++)
            {
                Assert.AreEqual(allOpenDates[1701 + i], openDates[i]);
            }

            Assert.AreEqual(0, cache.GetTradingDays(20110101, 20101209).Count);

            Assert.AreEqual(cache.GetAllTradingDays().Count, cache.GetTradingDays(-1, 20170101).Count);
        }

        [TestMethod]
        public void TestTradingDayCache_GetOpenDateCount()
        {
            CacheUtils_TradingDay cache = GetTradingDayCache();
            int cnt = cache.GetTradingDayCount(20100101, 20100201);
            Assert.AreEqual(21, cnt);
        }

        [TestMethod]
        public void TestTradingDayCache_GetOpenDate_GetOpenDateIndex()
        {
            CacheUtils_TradingDay cache = GetTradingDayCache();
            Assert.AreEqual(-1, cache.GetTradingDayIndex(20100101));
            Assert.AreEqual(1460, cache.GetTradingDayIndex(20100105));

            Assert.AreEqual(20040609, cache.GetTradingDay(100));

            Assert.AreEqual(-1, cache.GetTradingDay(20000101));
            Assert.AreEqual(-1, cache.GetTradingDay(20170101));
        }

        [TestMethod]
        public void TestTradingDayCache_GetNextOpenDate_GetPrevOpenDate()
        {
            CacheUtils_TradingDay cache = GetTradingDayCache();
            Assert.AreEqual(20100202, cache.GetNextTradingDay(20100201));
            Assert.AreEqual(20100208, cache.GetNextTradingDay(20100205));
            Assert.AreEqual(20100208, cache.GetNextTradingDay(20100206));
            Assert.AreEqual(20100210, cache.GetNextTradingDay(20100206, 3));
            Assert.AreEqual(20040102, cache.GetNextTradingDay(20000101));
            Assert.AreEqual(-1, cache.GetNextTradingDay(30000101, 3));

            Assert.AreEqual(20100204, cache.GetPrevTradingDay(20100205));
            Assert.AreEqual(20100202, cache.GetPrevTradingDay(20100205, 3));
            Assert.AreEqual(20100205, cache.GetPrevTradingDay(20100207));
            Assert.AreEqual(20100203, cache.GetPrevTradingDay(20100207, 3));
            Assert.AreEqual(-1, cache.GetPrevTradingDay(20000101));
            Assert.AreEqual(20160429, cache.GetPrevTradingDay(30000101));
        }
    }
}