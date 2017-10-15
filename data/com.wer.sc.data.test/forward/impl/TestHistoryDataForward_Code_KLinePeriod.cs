using com.wer.sc.data;
using com.wer.sc.data.reader;
using com.wer.sc.data.realtime;
using com.wer.sc.mockdata;
using com.wer.sc.strategy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.forward
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

            IHistoryDataForward_Code klineDataForward = GetDataForward(code, start, endDate);
            //List<string> list = new List<string>();

            klineDataForward.OnBar += KlineDataForward_OnBar;
            //AddToList(list, klineDataForward);
            Print(klineDataForward);
            while (klineDataForward.Forward())
            {
            }

            //AssertUtils.AssertEqual_List("forward_bigperiod", GetType(), list);
        }

        private static IHistoryDataForward_Code GetDataForward(string code, int start, int endDate)
        {
            ForwardReferedPeriods referedPeriods = new ForwardReferedPeriods();
            referedPeriods.UseTickData = false;
            referedPeriods.UsedKLinePeriods.Add(KLinePeriod.KLinePeriod_1Minute);
            referedPeriods.UsedKLinePeriods.Add(KLinePeriod.KLinePeriod_5Minute);
            referedPeriods.UsedKLinePeriods.Add(KLinePeriod.KLinePeriod_15Minute);
            referedPeriods.UsedKLinePeriods.Add(KLinePeriod.KLinePeriod_1Day);
            ForwardPeriod forwardPeriod = new ForwardPeriod(false, KLinePeriod.KLinePeriod_1Minute);
            return DataCenter.Default.HistoryDataForwardFactory.CreateHistoryDataForward_Code(code, start, endDate, referedPeriods, forwardPeriod);
        }

        private static void Print(IHistoryDataForward_Code klineDataForward)
        {
            Console.WriteLine("DayEnd:" + klineDataForward.IsDayEnd
                  + "|1MinuteEnd:" + klineDataForward.IsPeriodEnd(KLinePeriod.KLinePeriod_1Minute)
                  + "|5MinuteEnd:" + klineDataForward.IsPeriodEnd(KLinePeriod.KLinePeriod_5Minute)
                  + "|15MinuteEnd:" + klineDataForward.IsPeriodEnd(KLinePeriod.KLinePeriod_15Minute)
                  + "|DayEnd:" + klineDataForward.IsPeriodEnd(KLinePeriod.KLinePeriod_1Day));
            Console.WriteLine("1minute:" + klineDataForward.GetKLineData(KLinePeriod.KLinePeriod_1Minute));
            Console.WriteLine("5minute:" + klineDataForward.GetKLineData(KLinePeriod.KLinePeriod_5Minute));
            Console.WriteLine("15minute:" + klineDataForward.GetKLineData(KLinePeriod.KLinePeriod_15Minute));
            Console.WriteLine("1day:" + klineDataForward.GetKLineData(KLinePeriod.KLinePeriod_1Day));
            Console.WriteLine("timeline:" + klineDataForward.GetTimeLineData());

            double price = klineDataForward.GetKLineData(KLinePeriod.KLinePeriod_1Minute).End;
            Assert.AreEqual(price, klineDataForward.GetKLineData(KLinePeriod.KLinePeriod_5Minute).End);
            Assert.AreEqual(price, klineDataForward.GetKLineData(KLinePeriod.KLinePeriod_15Minute).End);
            Assert.AreEqual(price, klineDataForward.GetKLineData(KLinePeriod.KLinePeriod_1Day).End);
            Assert.AreEqual(price, klineDataForward.GetTimeLineData().Price);
        }

        private static void AddToList(List<string> list, IHistoryDataForward_Code klineDataForward)
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
            list.Add("5minute_" + klineData_5.GetCurrentBar_Original());
        }

        private List<string> list_OnBar = new List<string>();

        [TestMethod]
        public void TestKLineDataForward_OnBar()
        {
            list_OnBar.Clear();
            string code = "RB1710";
            int start = 20170601;
            int endDate = 20170603;

            IHistoryDataForward_Code klineDataForward = GetDataForward(code, start, endDate);
            klineDataForward.OnBar += KlineDataForward_OnBar;
            list_OnBar.Add(KLinePeriod.KLinePeriod_1Minute + ":" + klineDataForward.GetKLineData(KLinePeriod.KLinePeriod_1Minute));
            //Console.WriteLine(KLinePeriod.KLinePeriod_1Minute + ":" + klineDataForward.GetKLineData(KLinePeriod.KLinePeriod_1Minute));
            while (klineDataForward.Forward())
            {
            }

            AssertUtils.AssertEqual_List("forward_kline", GetType(), list_OnBar);
        }

        private void KlineDataForward_OnBar(object sender, ForwardOnBarArgument argument)
        {
            for (int i = 0; i < argument.ForwardOnBar_Infos.Count; i++)
            {
                ForwardOnbar_Info info = argument.ForwardOnBar_Infos[i];
                //Console.WriteLine(info.KLinePeriod + ":" + info.KLineBar);
                list_OnBar.Add(info.KLinePeriod + ":" + info.KLineBar);
            }
            //Print((IHistoryDataForward_Code)sender);
            //AddToList(list_OnBar, (IHistoryDataForward_Code)sender);
        }
    }
}
