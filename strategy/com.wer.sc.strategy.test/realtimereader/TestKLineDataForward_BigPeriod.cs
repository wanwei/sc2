using com.wer.sc.data;
using com.wer.sc.data.reader;
using com.wer.sc.data.realtime;
using com.wer.sc.mockdata;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy.realtimereader
{
    [TestClass]
    public class TestKLineDataForward_BigPeriod
    {
        [TestMethod]
        public void TestKLineDataForward_Minute()
        {
            string code = "RB1710";
            int start = 20170601;
            int endDate = 20170603;

            KLineData_RealTime klineData_1Minute = CommonData.GetKLineData_RealTime(code, start, endDate, KLinePeriod.KLinePeriod_1Minute);
            KLineData_RealTime klineData_5Minute = CommonData.GetKLineData_RealTime(code, start, endDate, KLinePeriod.KLinePeriod_5Minute);
            KLineData_RealTime klineData_15Minute = CommonData.GetKLineData_RealTime(code, start, endDate, KLinePeriod.KLinePeriod_15Minute);
            KLineData_RealTime klineData_1Day = CommonData.GetKLineData_RealTime(code, start, endDate, KLinePeriod.KLinePeriod_1Day);
            Dictionary<KLinePeriod, KLineData_RealTime> dic = new Dictionary<KLinePeriod, KLineData_RealTime>();
            dic.Add(KLinePeriod.KLinePeriod_1Minute, klineData_1Minute);
            dic.Add(KLinePeriod.KLinePeriod_5Minute, klineData_5Minute);
            dic.Add(KLinePeriod.KLinePeriod_15Minute, klineData_15Minute);
            dic.Add(KLinePeriod.KLinePeriod_1Day, klineData_1Day);

            KLineDataForward_BigPeriod klineDataForward = new KLineDataForward_BigPeriod(klineData_1Minute, dic, CommonData.GetDataReader().CreateTradingSessionReader(code));            
            List<string> list = new List<string>();

            //Print(klineDataForward);
            AddToList(list,klineDataForward);

            while (klineDataForward.Forward())
            {
                //Print(klineDataForward);
                AddToList(list, klineDataForward);              
            }

            AssertUtils.AssertEqual_List("forward_bigperiod", GetType(), list);
        }

        private static void Print(KLineDataForward_BigPeriod klineDataForward)
        {
            IKLineData klineData_1 = klineDataForward.GetKLineData(KLinePeriod.KLinePeriod_1Minute);
            Console.WriteLine("DayEnd:" + klineDataForward.IsDayEnd
                  + "|1MinuteEnd:" + klineDataForward.IsPeriodEnd(KLinePeriod.KLinePeriod_1Minute)
                  + "|5MinuteEnd:" + klineDataForward.IsPeriodEnd(KLinePeriod.KLinePeriod_5Minute)
                  + "|15MinuteEnd:" + klineDataForward.IsPeriodEnd(KLinePeriod.KLinePeriod_15Minute)
                  + "|DayEnd:" + klineDataForward.IsPeriodEnd(KLinePeriod.KLinePeriod_1Day));
            Console.WriteLine("1minute:" + klineData_1);
            KLineData_RealTime klineData_5 = (KLineData_RealTime)klineDataForward.GetKLineData(KLinePeriod.KLinePeriod_5Minute);
            Console.WriteLine("5minute:" + klineData_5);
            Console.WriteLine("5minute_" + klineData_5.GetCurrentRealBar());
        }

        private static void AddToList(List<string> list, KLineDataForward_BigPeriod klineDataForward)
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
    }
}
