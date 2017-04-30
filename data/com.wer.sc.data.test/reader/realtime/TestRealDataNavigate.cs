using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.test.reader.realtime
{
    [TestClass]
    public class TestRealDataNavigate
    {
        [TestMethod]
        public void TestCreateRealTimeDataNavigater()
        {
            DataReaderFactory fac = ResourceLoader.GetDefaultDataReaderFactory();
            IRealTimeDataNavigater dataNavigater = fac.RealTimeDataNavigaterFactory.CreateNavigater("m05", 20100105.100521);

            IKLineData klineData = dataNavigater.GetKLineData(KLinePeriod.KLinePeriod_15Minute);
            Console.WriteLine(klineData);
            klineData = dataNavigater.GetKLineData(KLinePeriod.KLinePeriod_1Minute);
            Console.WriteLine(klineData);
            klineData = dataNavigater.GetKLineData(KLinePeriod.KLinePeriod_5Minute);
            Console.WriteLine(klineData);
            klineData = dataNavigater.GetKLineData(KLinePeriod.KLinePeriod_1Hour);
            Console.WriteLine(klineData);
            klineData = dataNavigater.GetKLineData(KLinePeriod.KLinePeriod_1Day);
            Console.WriteLine(klineData);
            klineData = dataNavigater.GetKLineData(KLinePeriod.KLinePeriod_5Second);
            Console.WriteLine(klineData);

            dataNavigater.NavigateForward_Period(KLinePeriod.KLinePeriod_1Minute, 1);
            klineData = dataNavigater.GetKLineData(KLinePeriod.KLinePeriod_15Minute);
            Console.WriteLine(klineData);
            klineData = dataNavigater.GetKLineData(KLinePeriod.KLinePeriod_1Minute);
            Console.WriteLine(klineData);
            klineData = dataNavigater.GetKLineData(KLinePeriod.KLinePeriod_5Minute);
            Console.WriteLine(klineData);
            klineData = dataNavigater.GetKLineData(KLinePeriod.KLinePeriod_1Hour);
            Console.WriteLine(klineData);
            klineData = dataNavigater.GetKLineData(KLinePeriod.KLinePeriod_1Day);
            Console.WriteLine(klineData);
            klineData = dataNavigater.GetKLineData(KLinePeriod.KLinePeriod_5Second);
            Console.WriteLine(klineData);
        }

        [TestMethod]
        public void TestNavigateForward_Period()
        {
            DataReaderFactory fac = ResourceLoader.GetDefaultDataReaderFactory();
            IRealTimeDataNavigater dataNavigater = fac.RealTimeDataNavigaterFactory.CreateNavigater("m05", 20100105.100521);

            dataNavigater.NavigateForward_Period(KLinePeriod.KLinePeriod_1Minute, 1);
            IKLineData klineData = dataNavigater.GetKLineData(KLinePeriod.KLinePeriod_15Minute);
            Console.WriteLine(klineData);
            klineData = dataNavigater.GetKLineData(KLinePeriod.KLinePeriod_1Minute);
            Console.WriteLine(klineData);
            klineData = dataNavigater.GetKLineData(KLinePeriod.KLinePeriod_5Minute);
            Console.WriteLine(klineData);
            klineData = dataNavigater.GetKLineData(KLinePeriod.KLinePeriod_1Hour);
            Console.WriteLine(klineData);
            klineData = dataNavigater.GetKLineData(KLinePeriod.KLinePeriod_1Day);
            Console.WriteLine(klineData);
            klineData = dataNavigater.GetKLineData(KLinePeriod.KLinePeriod_5Second);
            Console.WriteLine(klineData);
        }
    }
}
