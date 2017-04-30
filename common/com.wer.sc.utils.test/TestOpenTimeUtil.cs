using com.wer.sc.data.cnfutures;
using com.wer.sc.utils.test.Properties;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.utils.test
{
    [TestClass]
    public class TestOpenTimeUtil
    {
        [TestMethod]
        public void TestGetOpenTime()
        {
            OpenTimeUtil openTimeUtil = new OpenTimeUtil();
            openTimeUtil.LoadFromString(Resources.opentime);
            List<double[]> opentime = openTimeUtil.GetOpenTime("DL", "m", 20150105);
            //PrintOpenTime(opentime);
            Assert.AreEqual("0.09-0.1015", GetOpenPeriodString(opentime[0]));
            Assert.AreEqual("0.103-0.113", GetOpenPeriodString(opentime[1]));
            Assert.AreEqual("0.133-0.15", GetOpenPeriodString(opentime[2]));

            opentime = openTimeUtil.GetOpenTime("DL", "m", 20150115);
            //PrintOpenTime(opentime);
            Assert.AreEqual("0.21-0.023", GetOpenPeriodString(opentime[0]));
            Assert.AreEqual("0.09-0.1015", GetOpenPeriodString(opentime[1]));
            Assert.AreEqual("0.103-0.113", GetOpenPeriodString(opentime[2]));
            Assert.AreEqual("0.133-0.15", GetOpenPeriodString(opentime[3]));
        }

        private void PrintOpenTime(List<double[]> openTime)
        {
            for (int i = 0; i < openTime.Count; i++)
            {
                double[] openPeriod = openTime[i];
                Console.WriteLine(openPeriod[0] + "-" + openPeriod[1]);
            }
        }

        private string GetOpenPeriodString(double[] openPeriod)
        {
            return openPeriod[0] + "-" + openPeriod[1];
        }
    }
}