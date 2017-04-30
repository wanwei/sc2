using com.wer.sc.data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data
{
    [TestClass]
    public class TestKLinePeriod
    {
        [TestMethod]
        public void TestCompareTo()
        {
            KLinePeriod period_5Second = KLinePeriod.KLinePeriod_5Second;
            KLinePeriod period_1Minute = KLinePeriod.KLinePeriod_1Minute;
            KLinePeriod period_15Minute = KLinePeriod.KLinePeriod_15Minute;
            KLinePeriod period_1Hour = KLinePeriod.KLinePeriod_1Hour;
            KLinePeriod period_1Day = KLinePeriod.KLinePeriod_1Day;

            Assert.AreEqual(0, period_1Minute.CompareTo(KLinePeriod.KLinePeriod_1Minute));
            Assert.AreEqual(1, period_1Minute.CompareTo(period_5Second));
            Assert.AreEqual(1, period_15Minute.CompareTo(period_1Minute));            
            Assert.AreEqual(1, period_1Hour.CompareTo(period_1Minute));
            Assert.AreEqual(1, period_1Day.CompareTo(period_1Hour));
        }
    }
}
