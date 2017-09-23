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
    public class TestTradingSessionCache_Instrument
    {

        private CacheUtils_TradingSession GetTradingSessionCache_Instrument(string code)
        {
            return new CacheUtils_TradingSession(code, MockDataLoader.GetTradingSessions(code));

        }

        [TestMethod]
        public void TestGetTradingDay()
        {
            ITradingSessionReader_Code reader = GetTradingSessionCache_Instrument("M05");
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
            ITradingSessionReader_Code reader = GetTradingSessionCache_Instrument("m05");
            int date = reader.GetTradingDay(20140505.092000);
            Assert.AreEqual(20140505, date);

            date = reader.GetTradingDay(20140505.082000);
            Assert.AreEqual(-1, date);

            date = reader.GetRecentTradingDay(20140505.082000);
            Assert.AreEqual(20140505, date);

            date = reader.GetRecentTradingDay(20140505.192000);
            Assert.AreEqual(20140506, date);

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
            Assert.AreEqual(-1, date);

            date = reader.GetRecentTradingDay(10150718.220000);
            Assert.AreEqual(20040102, date);
        }
    }
}
