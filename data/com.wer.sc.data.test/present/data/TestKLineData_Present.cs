using com.wer.sc.data.receiver2;
using com.wer.sc.data.utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.test.present.data
{
    [TestClass]
    public class TestKLineData_Present
    {
        [TestMethod]
        public void TestKLineData_Dynamic_Receive()
        {
            KLinePeriod klinePeriod = KLinePeriod.KLinePeriod_1Minute;
            int lastOpenDate = 20100104;
            int openDate = 20100105;
            IKLineData klineData_History = ResourceLoader.GetDefaultDataReaderFactory().KLineDataReader.GetData("m05", 20100104, 20100104, KLinePeriod.KLinePeriod_1Minute);

            List<double[]> openTime = new List<double[]>();
            openTime.Add(new double[] { .090000, .101500 });
            openTime.Add(new double[] { .103000, .113000 });
            openTime.Add(new double[] { .133000, .150000 });

            InitKLineData(klineData_History, klinePeriod, openDate, lastOpenDate, openTime);
        }

        [TestMethod]
        public void TestKLineData_Dynamic_Receive2()
        {
            KLinePeriod klinePeriod = KLinePeriod.KLinePeriod_1Minute;
            int lastOpenDate = 20160108;
            int openDate = 20160111;
            IKLineData klineData_History = ResourceLoader.GetDefaultDataReaderFactory().KLineDataReader.GetData("m05", 20160108, 20160108, KLinePeriod.KLinePeriod_1Minute);

            List<double[]> openTime = new List<double[]>();
            openTime.Add(new double[] { .210000, .233000 });
            openTime.Add(new double[] { .090000, .101500 });
            openTime.Add(new double[] { .103000, .113000 });
            openTime.Add(new double[] { .133000, .150000 });

            InitKLineData(klineData_History, klinePeriod, openDate, lastOpenDate, openTime);
        }

        private static void InitKLineData(IKLineData klineData_History, KLinePeriod klinePeriod, int openDate, int lastOpenDate, List<double[]> openTime)
        {
            DataReaderFactory fac = ResourceLoader.GetDefaultDataReaderFactory();
            ITickData tickData = fac.TickDataReader.GetTickData("m05", openDate);

            List<double> klineTimeList = KLineTimeListUtils.GetKLineTimeList(openDate, lastOpenDate, openTime, klinePeriod);
            KLineData_Present klineData = new KLineData_Present(klineData_History, klineTimeList, klinePeriod);

            for (int i = 0; i < tickData.Length; i++)
            {
                tickData.BarPos = i;
                klineData.Receive(tickData);
                Console.WriteLine(klineData);
            }

            Console.WriteLine();
            for(int i = 0; i < klineData.Length; i++)
            {
                klineData.BarPos = i;
                Console.WriteLine(klineData);
            }
        }
    }
}
