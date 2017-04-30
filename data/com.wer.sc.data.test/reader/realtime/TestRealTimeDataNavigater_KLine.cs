using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.reader.realtime
{
    [TestClass]
    public class TestRealTimeDataNavigater_KLine
    {
        [TestMethod]
        public void TestCreate()
        {
            DataReaderFactory fac = ResourceLoader.GetDefaultDataReaderFactory();

            List<KLinePeriod> periods = new List<KLinePeriod>();
            periods.Add(KLinePeriod.KLinePeriod_5Minute);
            periods.Add(KLinePeriod.KLinePeriod_15Minute);
            periods.Add(KLinePeriod.KLinePeriod_1Hour);
            periods.Add(KLinePeriod.KLinePeriod_1Day);
            RealTimeDataNavigateForward_KLine navigater = new RealTimeDataNavigateForward_KLine(fac, "m05", 20100101, 20100701, KLinePeriod.KLinePeriod_1Minute, periods);            

            IKLineData klineData_1Minute = navigater.GetKLineData(KLinePeriod.KLinePeriod_1Minute);
            Console.WriteLine(klineData_1Minute);
            IKLineData klineData_5Minute = navigater.GetKLineData(KLinePeriod.KLinePeriod_5Minute);
            Console.WriteLine(klineData_5Minute);
            IKLineData klineData_15Minute = navigater.GetKLineData(KLinePeriod.KLinePeriod_15Minute);
            Console.WriteLine(klineData_15Minute);
            IKLineData klineData_Hour = navigater.GetKLineData(KLinePeriod.KLinePeriod_1Hour);
            Console.WriteLine(klineData_Hour);
            //DataReaderFactory fac = ResourceLoader.GetDefaultDataReaderFactory();
            //RealTimeDataNavigater_KLine navigater = new RealTimeDataNavigater_KLine(fac, "m05", 20100101, 20100701);
            //IKLineData klineData = navigater.GetKLineData(KLinePeriod.KLinePeriod_1Minute);

            //Assert.AreEqual("20100104.09,3121,3135,3121,3132,5090,0,520956", klineData.ToString());
        }

        [TestMethod]
        public void TestNavigate()
        {
            DataReaderFactory fac = ResourceLoader.GetDefaultDataReaderFactory();

            List<KLinePeriod> periods = new List<KLinePeriod>();
            periods.Add(KLinePeriod.KLinePeriod_5Minute);
            periods.Add(KLinePeriod.KLinePeriod_15Minute);
            periods.Add(KLinePeriod.KLinePeriod_1Hour);
            periods.Add(KLinePeriod.KLinePeriod_1Day);
            RealTimeDataNavigateForward_KLine navigater = new RealTimeDataNavigateForward_KLine(fac, "m05", 20100101, 20100701, KLinePeriod.KLinePeriod_1Minute, periods);

            navigater.NavigateForward(1);

            IKLineData klineData_1Minute = navigater.GetKLineData(KLinePeriod.KLinePeriod_1Minute);
            Console.WriteLine(klineData_1Minute);
            IKLineData klineData_5Minute = navigater.GetKLineData(KLinePeriod.KLinePeriod_5Minute);
            Console.WriteLine(klineData_5Minute);
            IKLineData klineData_15Minute = navigater.GetKLineData(KLinePeriod.KLinePeriod_15Minute);
            Console.WriteLine(klineData_15Minute);
            IKLineData klineData_Hour = navigater.GetKLineData(KLinePeriod.KLinePeriod_1Hour);
            Console.WriteLine(klineData_Hour);
            //IKLineData klineData_1Day = navigater.GetKLineData(KLinePeriod.KLinePeriod_1Day);
            //Console.WriteLine(klineData_1Day);

        }

        [TestMethod]
        public void TestNavigate_Second()
        {
        }
    }
}
