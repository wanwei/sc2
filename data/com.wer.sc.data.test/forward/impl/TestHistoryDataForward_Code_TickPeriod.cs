using com.wer.sc.data;
using com.wer.sc.data.reader;
using com.wer.sc.data.realtime;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.forward.impl
{
    [TestClass]
    public class TestHistoryDataForward_Code_TickPeriod
    {
        [TestMethod]
        public void TestKLineDataForward_Tick()
        {
            string code = "RB1710";
            int start = 20170601;
            int endDate = 20170603;

            HistoryDataForward_Code_TickPeriod klineDataForward = GetKLineDataForward(code, start, endDate);
            //Print(klineDataForward);

            while (klineDataForward.Forward())
            {
                //Print(klineDataForward);
            }
            ITimeLineData timeLineData = klineDataForward.GetTimeLineData();
            for(int i = 0; i < timeLineData.Length; i++)
            {
                ITimeLineBar timeLineBar = timeLineData.GetBar(i);
                Console.WriteLine(timeLineBar);
            }
        }

        private static HistoryDataForward_Code_TickPeriod GetKLineDataForward(string code, int start, int endDate)
        {
            KLineData_RealTime klineData_1Minute = CommonData.GetKLineData_RealTime(code, start, endDate, KLinePeriod.KLinePeriod_1Minute);
            KLineData_RealTime klineData_5Minute = CommonData.GetKLineData_RealTime(code, start, endDate, KLinePeriod.KLinePeriod_5Minute);
            KLineData_RealTime klineData_15Minute = CommonData.GetKLineData_RealTime(code, start, endDate, KLinePeriod.KLinePeriod_15Minute);
            KLineData_RealTime klineData_1Day = CommonData.GetKLineData_RealTime(code, start, endDate, KLinePeriod.KLinePeriod_1Day);
            Dictionary<KLinePeriod, KLineData_RealTime> dic = new Dictionary<KLinePeriod, KLineData_RealTime>();

            IList<int> tradingDays = CommonData.GetDataReader().TradingDayReader.GetTradingDays(start, endDate);
            dic.Add(KLinePeriod.KLinePeriod_1Minute, klineData_1Minute);
            dic.Add(KLinePeriod.KLinePeriod_5Minute, klineData_5Minute);
            dic.Add(KLinePeriod.KLinePeriod_15Minute, klineData_15Minute);
            dic.Add(KLinePeriod.KLinePeriod_1Day, klineData_1Day);

            HistoryDataForward_Code_TickPeriod klineDataForward = new HistoryDataForward_Code_TickPeriod(CommonData.GetDataReader(), code, dic, tradingDays, KLinePeriod.KLinePeriod_1Minute);
            return klineDataForward;
        }

        private static void Print(HistoryDataForward_Code_TickPeriod klineDataForward)
        {
            Console.WriteLine("DayEnd:" + klineDataForward.IsDayEnd
                  + "|1MinuteEnd:" + klineDataForward.IsPeriodEnd(KLinePeriod.KLinePeriod_1Minute)
                  + "|5MinuteEnd:" + klineDataForward.IsPeriodEnd(KLinePeriod.KLinePeriod_5Minute)
                  + "|15MinuteEnd:" + klineDataForward.IsPeriodEnd(KLinePeriod.KLinePeriod_15Minute)
                  + "|DayEnd:" + klineDataForward.IsPeriodEnd(KLinePeriod.KLinePeriod_1Day));

            KLineData_RealTime klineData_1 = (KLineData_RealTime)klineDataForward.GetKLineData(KLinePeriod.KLinePeriod_1Minute);
            Console.WriteLine("tick:" + klineDataForward.GetTickData());
            Console.WriteLine("1minute:" + klineData_1);
            Console.WriteLine("1minute_" + klineData_1.GetCurrentRealBar());
            KLineData_RealTime klineData_1Day = (KLineData_RealTime)klineDataForward.GetKLineData(KLinePeriod.KLinePeriod_1Day);
            Console.WriteLine("1day:" + klineData_1Day);
            ITimeLineData timeLineData = klineDataForward.GetTimeLineData();
            Console.WriteLine("timeline:" + timeLineData);

            Assert.AreEqual(klineData_1.End, timeLineData.Price);
            Assert.AreEqual(klineData_1.Mount, timeLineData.Mount);
            Assert.AreEqual(klineData_1.Hold, timeLineData.Hold);

            //
            //Console.WriteLine("DayEnd:" + klineDataForward.IsDayEnd
            //      + "|1MinuteEnd:" + klineDataForward.IsPeriodEnd(KLinePeriod.KLinePeriod_1Minute)
            //      + "|5MinuteEnd:" + klineDataForward.IsPeriodEnd(KLinePeriod.KLinePeriod_5Minute)
            //      + "|15MinuteEnd:" + klineDataForward.IsPeriodEnd(KLinePeriod.KLinePeriod_15Minute)
            //      + "|DayEnd:" + klineDataForward.IsPeriodEnd(KLinePeriod.KLinePeriod_1Day));
            //Console.WriteLine("1minute:" + klineData_1);
            //KLineData_RealTime klineData_5 = (KLineData_RealTime)klineDataForward.GetKLineData(KLinePeriod.KLinePeriod_5Minute);
            //Console.WriteLine("5minute:" + klineData_5);
            //Console.WriteLine("5minute_" + klineData_5.GetCurrentRealBar());
        }

        private static void AddToList(List<string> list, HistoryDataForward_Code_KLinePeriod klineDataForward)
        {
            IKLineData klineData_1 = klineDataForward.GetKLineData(KLinePeriod.KLinePeriod_1Minute);
            list.Add("DayEnd:" + klineDataForward.IsDayEnd
                  + "|1MinuteEnd:" + klineDataForward.IsPeriodEnd(KLinePeriod.KLinePeriod_1Minute)
                  + "|5MinuteEnd:" + klineDataForward.IsPeriodEnd(KLinePeriod.KLinePeriod_5Minute)
                  + "|15MinuteEnd:" + klineDataForward.IsPeriodEnd(KLinePeriod.KLinePeriod_15Minute)
                  + "|DayEnd:" + klineDataForward.IsPeriodEnd(KLinePeriod.KLinePeriod_1Day));
            list.Add("1minute:" + klineData_1);
            KLineData_RealTime klineData_5 = (KLineData_RealTime)klineDataForward.GetKLineData(KLinePeriod.KLinePeriod_5Minute);
            list.Add("5minute:" + klineData_5);
            list.Add("5minute_" + klineData_5.GetCurrentRealBar());
        }

        [TestMethod]
        public void TestKLineDataForward_Tick_OnBar()
        {
            string code = "RB1710";
            int start = 20170601;
            int endDate = 20170603;

            HistoryDataForward_Code_TickPeriod klineDataForward = GetKLineDataForward(code, start, endDate);
            Print(klineDataForward);
            klineDataForward.OnBar += KlineDataForward_OnBar;
            while (klineDataForward.Forward())
            {
            }
        }

        private void KlineDataForward_OnBar(object sender, IKLineData klineData, int index)
        {
            Print((HistoryDataForward_Code_TickPeriod)sender);
        }
    }
}
