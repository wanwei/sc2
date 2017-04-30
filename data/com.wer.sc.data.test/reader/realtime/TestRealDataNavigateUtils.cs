using com.wer.sc.data.utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.reader.realtime
{
    [TestClass]
    public class TestRealDataNavigateUtils
    {
        [TestMethod]
        public void TestForwardKLineDataToNextDayOpenTime()
        {
            string code = "m05";
            DataReaderFactory dataReaderFactory = ResourceLoader.GetDefaultDataReaderFactory();
            IKLineData klineData = dataReaderFactory.KLineDataReader.GetData("m05", 20120104, 20120110, KLinePeriod.KLinePeriod_1Minute);
            KLineData_RealTime realtimeKLineData = new KLineData_RealTime(klineData);
            ITickData nextDayTickData = dataReaderFactory.TickDataReader.GetTickData(code, 20120105);
            KLineBar tmpCurrentKLineBar = new KLineBar();
            //Console.WriteLine(realtimeKLineData);
            Assert.AreEqual("20120104.09,2936,2938,2933,2935,2508,0,341690", realtimeKLineData.ToString());
            RealTimeDataNavigateUtils.ForwardKLineDataToNextDayOpenTime(realtimeKLineData, 20120105, nextDayTickData, dataReaderFactory, tmpCurrentKLineBar);
            //Console.WriteLine(realtimeKLineData);
            Assert.AreEqual("20120105.085901,2928,2928,2928,2928,6,17568,318690", realtimeKLineData.ToString());
        }

        [TestMethod]
        public void TestAdjustKLineDataByTick()
        {
            DataReaderFactory fac = ResourceLoader.GetDefaultDataReaderFactory();
            IKLineData klineData = fac.KLineDataReader.GetData("m05", 20120104, 20120110, KLinePeriod.KLinePeriod_1Minute);
            KLineData_RealTime realtimeKLineData = new KLineData_RealTime(klineData);

            realtimeKLineData.BarPos = TimeIndeierUtils.IndexOfTime_KLine(klineData, 20120104.090000);
            ITickData tickData = fac.TickDataReader.GetTickData("m05", 20120104);
            tickData.BarPos = 0;

            int lastIndex = -1;
            int currentIndex = 0;
            tickData.BarPos = currentIndex;

            KLineBar klineBar = KLineBar.CopyFrom(realtimeKLineData);
            klineBar.High = klineBar.Start;
            klineBar.Low = klineBar.Start;
            klineBar.End = klineBar.Start;
            klineBar.Mount = 0;
            klineBar.Money = 0;
            klineBar.Hold = 0;
            realtimeKLineData.SetRealTimeData(klineBar);
          
            for (int i = 0; i < tickData.Length; i++)
            {
                RealTimeDataNavigateUtils.ForwardKLineDataByForwardedTick(realtimeKLineData, tickData, lastIndex, currentIndex, new KLineBar());
                Console.WriteLine(realtimeKLineData);
                lastIndex = currentIndex;
                currentIndex++;
            }
        }

        [TestMethod]
        public void TestAdjustKLineDataByKLineData()
        {
            DataReaderFactory fac = ResourceLoader.GetDefaultDataReaderFactory();
            IKLineData klineData_1Minute = fac.KLineDataReader.GetData("m05", 20120104, 20120110, KLinePeriod.KLinePeriod_1Minute);
            KLineData_RealTime realtimeKLineData_1Minute = new KLineData_RealTime(klineData_1Minute);

            IKLineData klineData_15Minute = fac.KLineDataReader.GetData("m05", 20120104, 20120110, KLinePeriod.KLinePeriod_15Minute);
            KLineData_RealTime realtimeKLineData_15Minute = new KLineData_RealTime(klineData_15Minute);


            realtimeKLineData_1Minute.BarPos = TimeIndeierUtils.IndexOfTime_KLine(klineData_1Minute, 20120104.090000);
            realtimeKLineData_15Minute.BarPos = TimeIndeierUtils.IndexOfTime_KLine(klineData_1Minute, 20120104.090000);

            int lastBarPos = realtimeKLineData_1Minute.BarPos;
            int currentBarPos = lastBarPos + 2;
            realtimeKLineData_1Minute.BarPos = currentBarPos;

            Console.WriteLine(realtimeKLineData_15Minute);
            RealTimeDataNavigateUtils.ForwardKLineDataByForwardedKLineData(realtimeKLineData_1Minute, realtimeKLineData_15Minute, lastBarPos, currentBarPos);
            Console.WriteLine(realtimeKLineData_15Minute);
        }
    }
}
