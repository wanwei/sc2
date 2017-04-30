using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace com.wer.sc.data.utils
{
    [TestClass]
    public class TestTimeUtils
    {
        [TestMethod]
        public void TestTimeUtils_Convert()
        {
            double d = 20140101;
            DateTime dt = TimeUtils.ConvertToDateTime(d);
            Assert.AreEqual(d, TimeUtils.ConvertToDoubleTime(dt));

            d = 20140101.1;
            dt = TimeUtils.ConvertToDateTime(d);
            Assert.AreEqual(d, TimeUtils.ConvertToDoubleTime(dt));

            d = 20140101.09;
            dt = TimeUtils.ConvertToDateTime(d);
            Assert.AreEqual(d, TimeUtils.ConvertToDoubleTime(dt));

            d = 20140101.092;
            dt = TimeUtils.ConvertToDateTime(d);
            Assert.AreEqual(d, TimeUtils.ConvertToDoubleTime(dt));

            d = 20140101.0925;
            dt = TimeUtils.ConvertToDateTime(d);
            Assert.AreEqual(d, TimeUtils.ConvertToDoubleTime(dt));

            d = 20140101.09251;
            dt = TimeUtils.ConvertToDateTime(d);
            Assert.AreEqual(d, TimeUtils.ConvertToDoubleTime(dt));

            d = 20140101.092501;
            dt = TimeUtils.ConvertToDateTime(d);
            Assert.AreEqual(d, TimeUtils.ConvertToDoubleTime(dt));
        }

        [TestMethod]
        public void TestDateTime()
        {
            DateTime dt = Convert.ToDateTime("2014-09-12 09:30:05");
            Console.WriteLine(string.Format("{0:yyyyMMddHHmmss}", dt));

            Double d = Double.Parse(string.Format("{0:yyyyMMdd.HHmmss}", dt));
            Console.WriteLine(d);
        }

        [TestMethod]
        public void TestTimeUtils_Add()
        {
            Assert.AreEqual(20140912.10, TimeUtils.AddSeconds(20140912.095955, 5));
        }
    }
}