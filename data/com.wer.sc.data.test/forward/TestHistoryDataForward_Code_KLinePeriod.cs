using com.wer.sc.data;
using com.wer.sc.data.navigate;
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
        private Dictionary<KLinePeriod, List<string>> dic_Period_Content = new Dictionary<KLinePeriod, List<string>>();

        private void AddContent(KLinePeriod period, string content)
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

        [TestMethod]
        public void TestKLineDataForward_Minute()
        {
            string code = "RB1710";
            int start = 20170101;
            int endDate = 20170803;

            IDataForward_Code klineDataForward = GetDataForward(code, start, endDate);
            //List<string> list = new List<string>();
            dic_Period_Content.Clear();
            klineDataForward.OnBar += KlineDataForward_OnBar;
            //AddToList(list, klineDataForward);
            //Print(klineDataForward);            
            //Console.WriteLine(KLinePeriod.KLinePeriod_1Minute + ":" + klineDataForward.GetKLineData(KLinePeriod.KLinePeriod_1Minute));
            AddContent(KLinePeriod.KLinePeriod_1Minute, klineDataForward.GetKLineData(KLinePeriod.KLinePeriod_1Minute).ToString());
            while (klineDataForward.Forward())
            {
            }

            AssertKLineDataInDic(code, start, endDate);
            //AssertUtils.AssertEqual_List("forward_bigperiod", GetType(), list);
        }

        private void AssertKLineDataInDic(string code, int start, int endDate)
        {
            foreach (KLinePeriod period in dic_Period_Content.Keys)
            {
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

        private static IDataForward_Code GetDataForward(string code, int start, int endDate)
        {
            ForwardReferedPeriods referedPeriods = new ForwardReferedPeriods();
            referedPeriods.UseTickData = false;
            referedPeriods.UseTimeLineData = true;
            referedPeriods.UsedKLinePeriods.Add(KLinePeriod.KLinePeriod_1Minute);
            referedPeriods.UsedKLinePeriods.Add(KLinePeriod.KLinePeriod_5Minute);
            referedPeriods.UsedKLinePeriods.Add(KLinePeriod.KLinePeriod_15Minute);
            referedPeriods.UsedKLinePeriods.Add(KLinePeriod.KLinePeriod_1Day);
            ForwardPeriod forwardPeriod = new ForwardPeriod(false, KLinePeriod.KLinePeriod_1Minute);
            return DataCenter.Default.HistoryDataForwardFactory.CreateDataForward_Code(code, start, endDate, referedPeriods, forwardPeriod);
        }

        private static void Print(IDataForward_Code klineDataForward)
        {
            //Console.WriteLine("DayEnd:" + klineDataForward.IsDayEnd
            //      + "|1MinuteEnd:" + klineDataForward.IsPeriodEnd(KLinePeriod.KLinePeriod_1Minute)
            //      + "|5MinuteEnd:" + klineDataForward.IsPeriodEnd(KLinePeriod.KLinePeriod_5Minute)
            //      + "|15MinuteEnd:" + klineDataForward.IsPeriodEnd(KLinePeriod.KLinePeriod_15Minute)
            //      + "|DayEnd:" + klineDataForward.IsPeriodEnd(KLinePeriod.KLinePeriod_1Day));
            //Console.WriteLine("1minute:" + klineDataForward.GetKLineData(KLinePeriod.KLinePeriod_1Minute));
            //Console.WriteLine("5minute:" + klineDataForward.GetKLineData(KLinePeriod.KLinePeriod_5Minute));
            //Console.WriteLine("15minute:" + klineDataForward.GetKLineData(KLinePeriod.KLinePeriod_15Minute));
            //Console.WriteLine("1day:" + klineDataForward.GetKLineData(KLinePeriod.KLinePeriod_1Day));
            //Console.WriteLine("timeline:" + klineDataForward.GetTimeLineData());

            double price = klineDataForward.GetKLineData(KLinePeriod.KLinePeriod_1Minute).End;
            Assert.AreEqual(price, klineDataForward.GetKLineData(KLinePeriod.KLinePeriod_5Minute).End);
            Assert.AreEqual(price, klineDataForward.GetKLineData(KLinePeriod.KLinePeriod_15Minute).End);
            Assert.AreEqual(price, klineDataForward.GetKLineData(KLinePeriod.KLinePeriod_1Day).End);
            Assert.AreEqual(price, klineDataForward.GetTimeLineData().Price);
        }

        private static void AddToList(List<string> list, IDataForward_Code klineDataForward)
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

            IDataForward_Code klineDataForward = GetDataForward(code, start, endDate);
            klineDataForward.OnBar += KlineDataForward_OnBar;
            list_OnBar.Add(KLinePeriod.KLinePeriod_1Minute + ":" + klineDataForward.GetKLineData(KLinePeriod.KLinePeriod_1Minute));
            Console.WriteLine(KLinePeriod.KLinePeriod_1Minute + ":" + klineDataForward.GetKLineData(KLinePeriod.KLinePeriod_1Minute));
            while (klineDataForward.Forward())
            {
            }

            AssertUtils.AssertEqual_List("forward_kline", GetType(), list_OnBar);
        }

        private void KlineDataForward_OnBar(object sender, IForwardOnBarArgument argument)
        {
            for (int i = 0; i < argument.AllFinishedBars.Count; i++)
            {
                IForwardKLineBarInfo info = argument.AllFinishedBars[i];
                Assert.AreEqual(info.KLineBar.End, info.KLineData.End);
                Console.WriteLine(info.KLinePeriod + ":" + info.KLineBar);                
                list_OnBar.Add(info.KLinePeriod + ":" + info.KLineBar);
                AddContent(info.KLinePeriod, info.KLineBar.ToString());
            }
            //Print((IDataForward_Code)sender);
            //AddToList(list_OnBar, (IHistoryDataForward_Code)sender);
        }
    }
}
