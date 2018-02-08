using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data
{
    [TestClass]
    public class TestKLineDataExtend
    {
        [TestMethod]
        public void TestKLineDataExtendDayStart()
        {
            string code = "RB1710";
            int start = 20170601;
            int end = 20170603;
            IKLineData_Extend klineData = DataCenter.Default.DataReader.KLineDataReader.GetData_Extend(code, start, end, KLinePeriod.KLinePeriod_1Minute);
            int barPos = klineData.BarPos;

            for (int i = 0; i < klineData.Length; i++)
            {
                Console.WriteLine(i + ":" + klineData.GetBar(i).ToString());
            }

            Assert.IsTrue(klineData.IsDayStart());
            klineData.BarPos += 1;
            Assert.IsFalse(klineData.IsDayStart());
            Assert.IsFalse(klineData.IsDayStart(barPos + 1));

            Assert.IsFalse(klineData.IsDayEnd(12));
            Assert.IsTrue(klineData.IsDayEnd(344));
            Assert.IsTrue(klineData.IsDayStart(345));
        }
    }
}
