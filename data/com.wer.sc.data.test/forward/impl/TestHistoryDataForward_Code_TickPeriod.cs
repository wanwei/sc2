using com.wer.sc.data;
using com.wer.sc.data.datapackage;
using com.wer.sc.data.reader;
using com.wer.sc.data.realtime;
using com.wer.sc.mockdata;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.forward
{
    [TestClass]
    public class TestHistoryDataForward_Code_TickPeriod
    {
        private List<string> printStrs_Forward_Tick = new List<string>();
        private List<String> printStrs_Forward_TimeInfo_OnTick = new List<string>();
        private List<String> printStrs_Forward_TimeInfo_OnBar = new List<string>();

        [TestMethod]
        public void TestKLineDataForward_Tick()
        {
            printStrs_Forward_Tick.Clear();
            string code = "RB1710";
            int start = 20170601;
            int endDate = 20170603;
            //string code = "A0401";
            //int start = 20040106;
            //int endDate = 20040106;

            IHistoryDataForward_Code klineDataForward = GetKLineDataForward(code, start, endDate);
            klineDataForward.OnBar += KlineDataForward_OnBar;
            klineDataForward.OnTick += KlineDataForward_OnTick;
            while (klineDataForward.Forward())
            {

            }
            //ITimeLineData timeLineData = klineDataForward.GetTimeLineData();
            //for(int i = 0; i < timeLineData.Length; i++)
            //{
            //    ITimeLineBar timeLineBar = timeLineData.GetBar(i);
            //    Console.WriteLine(timeLineBar);
            //}
            AssertUtils.AssertEqual_List("forward_tick", GetType(), printStrs_Forward_Tick);
            printStrs_Forward_Tick.Clear();
        }

        private void KlineDataForward_OnTick(object sender, ITickData tickData, int index)
        {
            string txt = "tick:" + tickData.GetBar(index);
            printStrs_Forward_Tick.Add(txt);

            IHistoryDataForward_Code klineDataForward = (IHistoryDataForward_Code)sender;
            double price = tickData.Price;
            Assert.AreEqual(price, klineDataForward.GetKLineData(KLinePeriod.KLinePeriod_1Minute).End);
            Assert.AreEqual(price, klineDataForward.GetKLineData(KLinePeriod.KLinePeriod_5Minute).End);
            Assert.AreEqual(price, klineDataForward.GetKLineData(KLinePeriod.KLinePeriod_15Minute).End);
            Assert.AreEqual(price, klineDataForward.GetKLineData(KLinePeriod.KLinePeriod_1Day).End);
            Assert.AreEqual(price, klineDataForward.GetTimeLineData().Price);
        }

        private void KlineDataForward_OnBar(object sender, ForwardOnBarArgument argument)
        {
            ForwardOnbar_Info mainOnBarInfo = argument.MainForwardOnBar_Info;
            printStrs_Forward_Tick.Add("kline:" + mainOnBarInfo.KLineBar);
            //Console.WriteLine("kline:" + klineData.GetBar(index));
        }

        private static IHistoryDataForward_Code GetKLineDataForward(string code, int start, int endDate)
        {
            IDataPackage_Code dataPackage = DataCenter.Default.DataPackageFactory.CreateDataPackage(code, start, endDate);
            ForwardReferedPeriods referedPeriods = new ForwardReferedPeriods();
            referedPeriods.UsedKLinePeriods.Add(KLinePeriod.KLinePeriod_1Minute);
            referedPeriods.UsedKLinePeriods.Add(KLinePeriod.KLinePeriod_5Minute);
            referedPeriods.UsedKLinePeriods.Add(KLinePeriod.KLinePeriod_15Minute);
            referedPeriods.UsedKLinePeriods.Add(KLinePeriod.KLinePeriod_1Day);
            referedPeriods.UseTickData = true;

            ForwardPeriod forwardPeriod = new ForwardPeriod(true, KLinePeriod.KLinePeriod_1Minute);
            IHistoryDataForward_Code klineDataForward = DataCenter.Default.HistoryDataForwardFactory.CreateHistoryDataForward_Code(dataPackage, referedPeriods, forwardPeriod);
            //new HistoryDataForward_Code_TickPeriod(, code, periods, KLinePeriod.KLinePeriod_1Minute);
            return klineDataForward;
        }

        private static void Print(IHistoryDataForward_Code klineDataForward)
        {
            //Console.WriteLine("DayEnd:" + klineDataForward.IsDayEnd
            //      + "|1MinuteEnd:" + klineDataForward.IsPeriodEnd(KLinePeriod.KLinePeriod_1Minute)
            //      + "|5MinuteEnd:" + klineDataForward.IsPeriodEnd(KLinePeriod.KLinePeriod_5Minute)
            //      + "|15MinuteEnd:" + klineDataForward.IsPeriodEnd(KLinePeriod.KLinePeriod_15Minute)
            //      + "|DayEnd:" + klineDataForward.IsPeriodEnd(KLinePeriod.KLinePeriod_1Day));

            KLineData_RealTime klineData_1 = (KLineData_RealTime)klineDataForward.GetKLineData(KLinePeriod.KLinePeriod_1Minute);
            Console.WriteLine("tick:" + klineDataForward.GetTickData());
            Console.WriteLine("1minute:" + klineData_1);
            Console.WriteLine("1minute_" + klineData_1.GetCurrentBar_Original());
            KLineData_RealTime klineData_1Day = (KLineData_RealTime)klineDataForward.GetKLineData(KLinePeriod.KLinePeriod_1Day);
            Console.WriteLine("1day:" + klineData_1Day);
            ITimeLineData timeLineData = klineDataForward.GetTimeLineData();
            Console.WriteLine("timeline:" + timeLineData);

            Assert.AreEqual(klineData_1.End, timeLineData.Price);
            Assert.AreEqual(klineData_1.Mount, timeLineData.Mount);
            Assert.AreEqual(klineData_1.Hold, timeLineData.Hold);
        }

        [TestMethod]
        public void TestKLineDataForward_Tick_OnBar()
        {
            string code = "RB1710";
            int start = 20170601;
            int endDate = 20170603;

            printStrs_Forward_TimeInfo_OnBar.Clear();

            IHistoryDataForward_Code klineDataForward = GetKLineDataForward(code, start, endDate);
            klineDataForward.OnBar += KlineDataForward_OnBar2;
            while (klineDataForward.Forward())
            {
            }

            AssertUtils.AssertEqual_List("forward_tick_onbar", GetType(), printStrs_Forward_TimeInfo_OnBar);
            printStrs_Forward_TimeInfo_OnBar.Clear();
        }

        private void KlineDataForward_OnBar2(object sender, ForwardOnBarArgument argument)
        {
            IList<ForwardOnbar_Info> onBarInfos = argument.ForwardOnBar_Infos;
            for (int i = 0; i < onBarInfos.Count; i++)
            {
                ForwardOnbar_Info onBar_Info = onBarInfos[i];
                //Console.WriteLine(onBar_Info.KLinePeriod + ":" + onBar_Info.KLineBar.ToString());
                printStrs_Forward_TimeInfo_OnBar.Add(onBar_Info.KLinePeriod + ":" + onBar_Info.KLineBar.ToString());
            }
            //Console.WriteLine("Tick:"+((IHistoryDataForward_Code)sender).GetTickData());
            printStrs_Forward_TimeInfo_OnBar.Add("Tick:" + ((IHistoryDataForward_Code)sender).GetTickData());
            //PrintOnBar((IHistoryDataForward_Code)sender);
            //printStrs.Add("kline:" + klineData.GetBar(index));
            //Console.WriteLine("kline:" + klineData.GetBar(index));
        }

        private static void PrintOnBar(IHistoryDataForward_Code klineDataForward)
        {
            Console.WriteLine("tick:" + klineDataForward.GetTickData());
            Console.WriteLine("1minute:" + klineDataForward.GetKLineData(KLinePeriod.KLinePeriod_1Minute));
            Console.WriteLine("5minute:" + klineDataForward.GetKLineData(KLinePeriod.KLinePeriod_5Minute));
            Console.WriteLine("15minute:" + klineDataForward.GetKLineData(KLinePeriod.KLinePeriod_15Minute));
            Console.WriteLine("1day:" + klineDataForward.GetKLineData(KLinePeriod.KLinePeriod_1Day));
            ITimeLineData timeLineData = klineDataForward.GetTimeLineData();
            Console.WriteLine("timeline:" + timeLineData);
        }

        private static void AddToList(List<string> list, IHistoryDataForward_Code klineDataForward)
        {
            list.Add("DayEnd:" + klineDataForward.IsDayEnd
                  + "|1MinuteEnd:" + klineDataForward.IsPeriodEnd(KLinePeriod.KLinePeriod_1Minute)
                  + "|5MinuteEnd:" + klineDataForward.IsPeriodEnd(KLinePeriod.KLinePeriod_5Minute)
                  + "|15MinuteEnd:" + klineDataForward.IsPeriodEnd(KLinePeriod.KLinePeriod_15Minute)
                  + "|DayEnd:" + klineDataForward.IsPeriodEnd(KLinePeriod.KLinePeriod_1Day));
            IKLineData klineData_1 = klineDataForward.GetKLineData(KLinePeriod.KLinePeriod_1Minute);
            list.Add("1minute:" + klineData_1);
            KLineData_RealTime klineData_5 = (KLineData_RealTime)klineDataForward.GetKLineData(KLinePeriod.KLinePeriod_5Minute);
            list.Add("5minute:" + klineData_5);
            list.Add("5minute_" + klineData_5.GetCurrentBar_Original());
        }

        [TestMethod]
        public void TestForwardTimeInfo_OnTick()
        {
            string code = "RB1710";
            int start = 20170601;
            int endDate = 20170603;

            IHistoryDataForward_Code klineDataForward = GetKLineDataForward(code, start, endDate);
            //klineDataForward.OnBar += KlineDataForward_OnBar1; ;
            //klineDataForward.OnTick += KlineDataForward_OnTick;
            klineDataForward.OnTick += KlineDataForward_OnTick1;
            printStrs_Forward_TimeInfo_OnTick.Clear();
            while (klineDataForward.Forward())
            {

            }

            AssertUtils.AssertEqual_List("forward_tick_timeinfo", GetType(), printStrs_Forward_TimeInfo_OnTick);
            printStrs_Forward_TimeInfo_OnTick.Clear();
        }

        private void KlineDataForward_OnTick1(object sender, ITickData tickData, int index)
        {
            IHistoryDataForward_Code klineDataForward = (IHistoryDataForward_Code)sender;
            string txt = "tick:" + tickData.GetBar(index);
            printStrs_Forward_TimeInfo_OnTick.Add(txt);
            //Console.WriteLine(txt);
            txt = "tradingTimeStart:" + klineDataForward.IsTradingTimeStart
                + "|tradingTimeEnd:" + klineDataForward.IsTradingTimeEnd
                + "|dayStart:" + klineDataForward.IsDayStart
                + "|dayEnd:" + klineDataForward.IsDayEnd;
            //Console.WriteLine(txt);
            printStrs_Forward_TimeInfo_OnTick.Add(txt);
        }

        [TestMethod]
        public void TestForwardTimeInfo_OnBar()
        {
            string code = "RB1710";
            int start = 20170601;
            int endDate = 20170603;

            IHistoryDataForward_Code klineDataForward = GetKLineDataForward(code, start, endDate);
            klineDataForward.OnBar += KlineDataForward_OnBar1;
            //klineDataForward.OnTick += KlineDataForward_OnTick;
            //print_Forward_TimeInfo_OnTick.Clear();
            while (klineDataForward.Forward())
            {

            }

            //AssertUtils.AssertEqual_List("forward_tick_timeinfo", GetType(), print_Forward_TimeInfo_OnTick);
            //print_Forward_TimeInfo_OnTick.Clear();
        }

        private void KlineDataForward_OnBar1(object sender, ForwardOnBarArgument argument)
        {
            IHistoryDataForward_Code klineDataForward = (IHistoryDataForward_Code)sender;
            //Console.WriteLine("DayEnd:" + klineDataForward.IsDayEnd
            //        + "|1MinuteEnd:" + klineDataForward.IsPeriodEnd(KLinePeriod.KLinePeriod_1Minute)
            //        + "|5MinuteEnd:" + klineDataForward.IsPeriodEnd(KLinePeriod.KLinePeriod_5Minute)
            //        + "|15MinuteEnd:" + klineDataForward.IsPeriodEnd(KLinePeriod.KLinePeriod_15Minute)
            //        + "|DayEnd:" + klineDataForward.IsPeriodEnd(KLinePeriod.KLinePeriod_1Day));
            Console.WriteLine("1minute:" + klineDataForward.GetKLineData(KLinePeriod.KLinePeriod_1Minute));
            Console.WriteLine("tradingTimeStart:" + klineDataForward.IsTradingTimeStart
                + "|tradingTimeEnd:" + klineDataForward.IsTradingTimeEnd
                + "|dayStart:" + klineDataForward.IsDayStart
                + "|dayEnd:" + klineDataForward.IsDayEnd);
        }
    }
}