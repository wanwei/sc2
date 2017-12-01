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

            IDataForward_Code klineDataForward = GetKLineDataForward(code, start, endDate);
            klineDataForward.OnBar += KlineDataForward_OnBar;
            klineDataForward.OnTick += KlineDataForward_OnTick;
            DateTime prevtime = DateTime.Now;
            while (klineDataForward.Forward())
            {

            }

            DateTime time = DateTime.Now;
            TimeSpan span = time.Subtract(prevtime);
            Console.WriteLine(span.Minutes * 60 * 1000 + span.Seconds * 1000 + span.Milliseconds);
            //ITimeLineData timeLineData = klineDataForward.GetTimeLineData();
            //for(int i = 0; i < timeLineData.Length; i++)
            //{
            //    ITimeLineBar timeLineBar = timeLineData.GetBar(i);
            //    Console.WriteLine(timeLineBar);
            //}
            AssertUtils.AssertEqual_List("forward_tick", GetType(), printStrs_Forward_Tick);
            printStrs_Forward_Tick.Clear();
        }

        private void KlineDataForward_OnTick(object sender, IForwardOnTickArgument argument)
        {
            string txt = "tick:" + argument.TickInfo.TickBar;
            printStrs_Forward_Tick.Add(txt);
            Console.WriteLine(txt);

            IDataForward_Code klineDataForward = (IDataForward_Code)sender;
            double price = argument.TickInfo.TickData.Price;
            Assert.AreEqual(price, klineDataForward.GetKLineData(KLinePeriod.KLinePeriod_1Minute).End);
            Assert.AreEqual(price, klineDataForward.GetKLineData(KLinePeriod.KLinePeriod_5Minute).End);
            Assert.AreEqual(price, klineDataForward.GetKLineData(KLinePeriod.KLinePeriod_15Minute).End);
            Assert.AreEqual(price, klineDataForward.GetKLineData(KLinePeriod.KLinePeriod_1Day).End);
            Assert.AreEqual(price, klineDataForward.GetTimeLineData().Price);
        }

        private void KlineDataForward_OnBar(object sender, IForwardOnBarArgument argument)
        {
            IForwardKLineBarInfo mainOnBarInfo = argument.MainBar;
            printStrs_Forward_Tick.Add("kline:" + mainOnBarInfo.KLineBar);
            Console.WriteLine("kline:" + mainOnBarInfo.KLineBar);
        }

        private static IDataForward_Code GetKLineDataForward(string code, int start, int endDate)
        {
            IDataPackage_Code dataPackage = DataCenter.Default.DataPackageFactory.CreateDataPackage_Code(code, start, endDate);
            ForwardReferedPeriods referedPeriods = new ForwardReferedPeriods();
            referedPeriods.UsedKLinePeriods.Add(KLinePeriod.KLinePeriod_1Minute);
            referedPeriods.UsedKLinePeriods.Add(KLinePeriod.KLinePeriod_5Minute);
            referedPeriods.UsedKLinePeriods.Add(KLinePeriod.KLinePeriod_15Minute);
            referedPeriods.UsedKLinePeriods.Add(KLinePeriod.KLinePeriod_1Day);
            referedPeriods.UseTickData = true;
            referedPeriods.UseTimeLineData = true;

            ForwardPeriod forwardPeriod = new ForwardPeriod(true, KLinePeriod.KLinePeriod_1Minute);
            IDataForward_Code klineDataForward = DataCenter.Default.HistoryDataForwardFactory.CreateDataForward_Code(dataPackage, referedPeriods, forwardPeriod);
            //new HistoryDataForward_Code_TickPeriod(, code, periods, KLinePeriod.KLinePeriod_1Minute);
            return klineDataForward;
        }

        private static void Print(IDataForward_Code klineDataForward)
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

            IDataForward_Code klineDataForward = GetKLineDataForward(code, start, endDate);
            klineDataForward.OnBar += KlineDataForward_OnBar2;
            while (klineDataForward.Forward())
            {
            }

            AssertUtils.AssertEqual_List("forward_tick_onbar", GetType(), printStrs_Forward_TimeInfo_OnBar);
            printStrs_Forward_TimeInfo_OnBar.Clear();
        }

        private void KlineDataForward_OnBar2(object sender, IForwardOnBarArgument argument)
        {
            Console.WriteLine("Tick:" + ((IDataForward_Code)sender).GetTickData());
            printStrs_Forward_TimeInfo_OnBar.Add("Tick:" + ((IDataForward_Code)sender).GetTickData());

            IList<IForwardKLineBarInfo> onBarInfos = argument.AllFinishedBars;
            for (int i = 0; i < onBarInfos.Count; i++)
            {
                IForwardKLineBarInfo onBar_Info = onBarInfos[i];
                //Assert.AreEqual(onBar_Info.KLineBar.End, onBar_Info.KlineData.End);
                Console.WriteLine(onBar_Info.KLinePeriod + ":" + onBar_Info.KLineBar.ToString());
                printStrs_Forward_TimeInfo_OnBar.Add(onBar_Info.KLinePeriod + ":" + onBar_Info.KLineBar.ToString());
            }
            //PrintOnBar((IHistoryDataForward_Code)sender);
            //printStrs.Add("kline:" + klineData.GetBar(index));
            //Console.WriteLine("kline:" + klineData.GetBar(index));
        }

        private static void PrintOnBar(IDataForward_Code klineDataForward)
        {
            Console.WriteLine("tick:" + klineDataForward.GetTickData());
            Console.WriteLine("1minute:" + klineDataForward.GetKLineData(KLinePeriod.KLinePeriod_1Minute));
            Console.WriteLine("5minute:" + klineDataForward.GetKLineData(KLinePeriod.KLinePeriod_5Minute));
            Console.WriteLine("15minute:" + klineDataForward.GetKLineData(KLinePeriod.KLinePeriod_15Minute));
            Console.WriteLine("1day:" + klineDataForward.GetKLineData(KLinePeriod.KLinePeriod_1Day));
            ITimeLineData timeLineData = klineDataForward.GetTimeLineData();
            Console.WriteLine("timeline:" + timeLineData);
        }

        private static void AddToList(List<string> list, IDataForward_Code klineDataForward)
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

            IDataForward_Code klineDataForward = GetKLineDataForward(code, start, endDate);
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

        private void KlineDataForward_OnTick1(object sender, IForwardOnTickArgument argument)
        {
            IDataForward_Code klineDataForward = (IDataForward_Code)sender;
            string txt = "tick:" + argument.TickInfo.TickBar;
            printStrs_Forward_TimeInfo_OnTick.Add(txt);
            Console.WriteLine(txt);
            txt = "tradingTimeStart:" + klineDataForward.IsTradingTimeStart
                + "|tradingTimeEnd:" + klineDataForward.IsTradingTimeEnd
                + "|dayStart:" + klineDataForward.IsDayStart
                + "|dayEnd:" + klineDataForward.IsDayEnd;
            Console.WriteLine(txt);
            printStrs_Forward_TimeInfo_OnTick.Add(txt);
        }

        [TestMethod]
        public void TestForwardTimeInfo_OnBar()
        {
            string code = "RB1710";
            int start = 20170601;
            int endDate = 20170603;

            IDataForward_Code klineDataForward = GetKLineDataForward(code, start, endDate);
            klineDataForward.OnBar += KlineDataForward_OnBar1;
            //klineDataForward.OnTick += KlineDataForward_OnTick;
            //print_Forward_TimeInfo_OnTick.Clear();
            while (klineDataForward.Forward())
            {

            }

            //AssertUtils.AssertEqual_List("forward_tick_timeinfo", GetType(), print_Forward_TimeInfo_OnTick);
            //print_Forward_TimeInfo_OnTick.Clear();
        }

        private void KlineDataForward_OnBar1(object sender, IForwardOnBarArgument argument)
        {
            IDataForward_Code klineDataForward = (IDataForward_Code)sender;
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


        //[TestMethod]
        public void TestKLineDataForward_Tick_Long()
        {
            printStrs_Forward_Tick.Clear();
            //string code = "RB1710";
            //int start = 20170401;
            //int endDate = 20170803;
            string code = "RB0000";
            int start = 20150401;
            int endDate = 20170803;

            IDataForward_Code klineDataForward = GetKLineDataForward(code, start, endDate);
            klineDataForward.OnBar += KlineDataForward_OnBar_Long;
            klineDataForward.OnTick += KlineDataForward_OnTick_Long;
            while (klineDataForward.Forward())
            {

            }
        }

        private void KlineDataForward_OnTick_Long(object sender, IForwardOnTickArgument argument)
        {

        }

        private void KlineDataForward_OnBar_Long(object sender, IForwardOnBarArgument argument)
        {

        }

        private Dictionary<KLinePeriod, List<string>> dic_Period_Content = new Dictionary<KLinePeriod, List<string>>();

        private void AddContent_KLine(KLinePeriod period, string content)
        {
            if (dic_Period_Content.ContainsKey(period))
                dic_Period_Content[period].Add(content);
            else
            {
                List<string> strs = new List<string>();
                strs.Add(content);
                dic_Period_Content.Add(period, strs);
            }
        }

        private Dictionary<int, List<string>> dic_Date_TickData = new Dictionary<int, List<string>>();

        private void AddContent_Tick(int date, string content)
        {
            if (dic_Date_TickData.ContainsKey(date))
                dic_Date_TickData[date].Add(content);
            else
            {
                List<string> strs = new List<string>();
                strs.Add(content);
                dic_Date_TickData.Add(date, strs);
            }
        }

        [TestMethod]
        public void TestCompareWithReader()
        {
            string code = "RB0000";
            int start = 20170601;
            int endDate = 20170605;

            IDataForward_Code klineDataForward = GetKLineDataForward(code, start, endDate);
            klineDataForward.Forward();

            AddContent_Tick(start, klineDataForward.GetTickData().ToString());
            klineDataForward.OnBar += KlineDataForward_OnBar_CompareWithReader;
            klineDataForward.OnTick += KlineDataForward_OnTick_CompareWithReader;
            while (klineDataForward.Forward())
            {

            }

            AssertKLineDataInDic(code, start, endDate);
            AssertTickDataInDic(code);
        }

        private void AssertKLineDataInDic(string code, int start, int endDate)
        {
            foreach (KLinePeriod period in dic_Period_Content.Keys)
            {
                //if (period.Equals(KLinePeriod.KLinePeriod_1Day))
                //    continue;
                List<string> contents = dic_Period_Content[period];
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < contents.Count; i++)
                {
                    if (i != 0)
                        sb.Append("\r\n");
                    sb.Append(contents[i]);
                }
                IKLineData klineData = DataCenter.Default.DataReader.KLineDataReader.GetData(code, start, endDate, period);
                AssertUtils.AssertEqual_KLineData(sb.ToString(), klineData);
            }
        }

        private void AssertTickDataInDic(string code)
        {
            foreach (int date in dic_Date_TickData.Keys)
            {
                //if (period.Equals(KLinePeriod.KLinePeriod_1Day))
                //    continue;
                List<string> contents = dic_Date_TickData[date];
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < contents.Count; i++)
                {
                    if (i != 0)
                        sb.Append("\r\n");
                    sb.Append(contents[i]);
                }
                ITickData tickData = DataCenter.Default.DataReader.TickDataReader.GetTickData(code, date);

                AssertUtils.AssertEqual_TickData(sb.ToString(), tickData);
            }
        }

        private void KlineDataForward_OnTick_CompareWithReader(object sender, IForwardOnTickArgument argument)
        {
            AddContent_Tick(argument.TickInfo.TickData.TradingDay, argument.TickInfo.TickBar.ToString());
        }

        private void KlineDataForward_OnBar_CompareWithReader(object sender, IForwardOnBarArgument argument)
        {
            for (int i = 0; i < argument.AllFinishedBars.Count; i++)
            {
                IForwardKLineBarInfo info = argument.AllFinishedBars[i];
                Console.WriteLine(info.KLinePeriod + ":" + info.KLineBar);
                AddContent_KLine(info.KLinePeriod, info.KLineBar.ToString());
                //list_OnBar.Add(info.KLinePeriod + ":" + info.KLineBar);
            }
        }
    }
}