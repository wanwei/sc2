using com.wer.sc.data.reader;
using com.wer.sc.data.reader.realtime;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.test.reader.realtime
{
    [TestClass]
    public class TestRealDataReader
    {
        [TestMethod]
        public void TestRealDataReader_GetData()
        {
            DataReaderFactory fac = ResourceLoader.GetDefaultDataReaderFactory();
            IRealTimeDataReader reader = new RealTimeDataReader(fac, "m05", 20100104.093002);
            ITickData tickData = reader.GetTickData();
            Console.WriteLine(tickData);
            Assert.AreEqual("20100104.093002,3127,2,44492,0,3127,25,3128,173,0", tickData.ToString());
            IKLineData klineData = reader.GetKLineData(KLinePeriod.KLinePeriod_15Minute);
            Console.WriteLine(klineData);
            Assert.AreEqual("20100104.093,3127,3127,3127,3127,40,0,523048", klineData.ToString());

            ITimeLineData timeLineData = reader.GetTimeLineData();
            Console.WriteLine(timeLineData);
            Assert.AreEqual("20100104.093,3127,11,0.35,40,524676", timeLineData.ToString());
        }
    }
}
