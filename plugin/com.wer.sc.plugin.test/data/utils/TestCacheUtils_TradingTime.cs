using com.wer.sc.data.reader;
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
    public class TestCacheUtils_TradingTime
    {

        private CacheUtils_TradingTime GetTradingSessionCache_Instrument(string code)
        {
            return new CacheUtils_TradingTime(code, MockDataLoader.GetTradingTimeList(code));
        }

        [TestMethod]
        public void TestGetTradingDay()
        {
            ITradingTimeReader_Code reader = GetTradingSessionCache_Instrument("M1305");
            int day = reader.GetTradingDay(20130105.093000);
            Assert.AreEqual(-1, day);
            day = reader.GetTradingDay(20130108.090000);
            Assert.AreEqual(20130108, day);
            day = reader.GetTradingDay(20130108.085900);
            Assert.AreEqual(-1, day);
        }

        [TestMethod]
        public void TestTradingSession_GetTradingDay()
        {
            ITradingTimeReader_Code reader = GetTradingSessionCache_Instrument("m1405");
            int date = reader.GetTradingDay(20140505.092000);
            Assert.AreEqual(20140505, date);

            date = reader.GetTradingDay(20140505.082000);
            Assert.AreEqual(-1, date);

            date = reader.GetRecentTradingDay(20140505.082000);
            Assert.AreEqual(20140430, date);

            date = reader.GetRecentTradingDay(20140505.192000);
            Assert.AreEqual(20140505, date);

            reader = GetTradingSessionCache_Instrument("m1605");
            date = reader.GetTradingDay(20150717.220000);
            Assert.AreEqual(20150720, date);

            date = reader.GetTradingDay(20150718.220000);
            Assert.AreEqual(20150720, date);

            date = reader.GetRecentTradingDay(20150816.100000, true);
            Assert.AreEqual(20150817, date);

            date = reader.GetRecentTradingDay(20150816.100000, false);
            Assert.AreEqual(20150817, date);

            date = reader.GetRecentTradingDay(20150816.100000, false);
            Assert.AreEqual(20150817, date);

            date = reader.GetRecentTradingDay(20150814.160000, true);
            Assert.AreEqual(20150817, date);

            date = reader.GetRecentTradingDay(20150814.160000, false);
            Assert.AreEqual(20150814, date);

            date = reader.GetRecentTradingDay(30150718.220000);
            Assert.AreEqual(20160520, date);

            reader = GetTradingSessionCache_Instrument("m1705");
            date = reader.GetRecentTradingDay(20170130.10, false);
            Assert.AreEqual(20170126, date);
            date = reader.GetRecentTradingDay(20170130.10);
            Assert.AreEqual(20170126, date);

            reader = GetTradingSessionCache_Instrument("m0405");
            date = reader.GetRecentTradingDay(10150718.220000);
            Assert.AreEqual(-1, date);

            reader = GetTradingSessionCache_Instrument("RB1805");
            date = reader.GetRecentTradingDay(20171221.2101);
            Assert.AreEqual(20171222, date);
        }

        public void TestTradingTime_GetRecentTime()
        {
            ITradingTimeReader_Code reader = GetTradingSessionCache_Instrument("RB1801");
            double time = reader.GetRecentTradingTime(20170930.0900, false);
            Assert.AreEqual(20170929.15, time);
            time = reader.GetRecentTradingTime(20170929.1501, false);
            Assert.AreEqual(20170929.15, time);
            time = reader.GetRecentTradingTime(20170929.1401, false);
            Assert.AreEqual(20170929.1401, time);
            time = reader.GetRecentTradingTime(20170929.1201, false);
            Assert.AreEqual(20170929.1130, time);
            time = reader.GetRecentTradingTime(20170929.0801, false);
            Assert.AreEqual(20170928.2300, time);
        }
    }
}
