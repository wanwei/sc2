using com.wer.sc.data.receiver2;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.test.present
{
    [TestClass]
    public class TestRecentKLineDataLoader
    {
        [TestMethod]
        public void TestLoadRecentKLineData()
        {
            DataReaderFactory dataReaderFactory = ResourceLoader.GetDefaultDataReaderFactory();
            RecentKLineDataLoader loader = new RecentKLineDataLoader(dataReaderFactory);
            IKLineData klineData = loader.GetRecentKLineData("m05", 20100105, KLinePeriod.KLinePeriod_1Minute);
            for (int i = 0; i < klineData.Length; i++)
            {
                klineData.BarPos = i;
                Console.WriteLine(klineData);
            }
        }
    }
}
