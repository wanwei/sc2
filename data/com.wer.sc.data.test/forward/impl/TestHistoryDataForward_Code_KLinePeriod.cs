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

namespace com.wer.sc.data.forward.impl
{
    [TestClass]
    public class TestHistoryDataForward_Code_KLinePeriod
    {
        [TestMethod]
        public void TestKLineDataForward_Minute()
        {
            string code = "RB1710";
            int start = 20170601;
            int endDate = 20170603;

            HistoryDataForward_Code_KLinePeriod klineDataForward = GetDataForward(code, start, endDate);
            List<string> list = new List<string>();

            //Print(klineDataForward);
            AddToList(list, klineDataForward);

            while (klineDataForward.Forward())
            {
                //Print(klineDataForward);
                AddToList(list, klineDataForward);
            }

            AssertUtils.AssertEqual_List("forward_bigperiod", GetType(), list);
        }

        private static HistoryDataForward_Code_KLinePeriod GetDataForward(string code, int start, int endDate)
        {
            KLineData_RealTime klineData_1Minute = CommonData.GetKLineData_RealTime(code, start, endDate, KLinePeriod.KLinePeriod_1Minute);
            KLineData_RealTime klineData_5Minute = CommonData.GetKLineData_RealTime(code, start, endDate, KLinePeriod.KLinePeriod_5Minute);
            KLineData_RealTime klineData_15Minute = CommonData.GetKLineData_RealTime(code, start, endDate, KLinePeriod.KLinePeriod_15Minute);
            KLineData_RealTime klineData_1Day = CommonData.GetKLineData_RealTime(code, start, endDate, KLinePeriod.KLinePeriod_1Day);
            Dictionary<KLinePeriod, KLineData_RealTime> dic = new Dictionary<KLinePeriod, KLineData_RealTime>();
            dic.Add(KLinePeriod.KLinePeriod_1Minute, klineData_1Minute);
            dic.Add(KLinePeriod.KLinePeriod_5Minute, klineData_5Minute);
            dic.Add(KLinePeriod.KLinePeriod_15Minute, klineData_15Minute);
            dic.Add(KLinePeriod.KLinePeriod_1Day, klineData_1Day);

            HistoryDataForward_Code_KLinePeriod klineDataForward = new HistoryDataForward_Code_KLinePeriod(CommonData.GetDataReader(), code,klineData_1Minute, dic, CommonData.GetDataReader().CreateTradingSessionReader(code));
            return klineDataForward;
        }

        private static void Print(HistoryDataForward_Code_KLinePeriod klineDataForward)
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

        private List<string> list_OnBar = new List<string>();

        [TestMethod]
        public void TestKLineDataForward_OnBar()
        {
            list_OnBar.Clear();
            string code = "RB1710";
            int start = 20170601;
            int endDate = 20170603;

            HistoryDataForward_Code_KLinePeriod klineDataForward = GetDataForward(code, start, endDate);
            klineDataForward.OnBar += KlineDataForward_OnBar;
            AddToList(list_OnBar, klineDataForward);
            while (klineDataForward.Forward())
            {
            }

            AssertUtils.AssertEqual_List("forward_bigperiod", GetType(), list_OnBar);
        }

        private void KlineDataForward_OnBar(object sender, IKLineData klineData, int index)
        {
            //Print((HistoryDataForward_Code_KLinePeriod)sender);
            AddToList(list_OnBar, (HistoryDataForward_Code_KLinePeriod)sender);
        }
    }
}
