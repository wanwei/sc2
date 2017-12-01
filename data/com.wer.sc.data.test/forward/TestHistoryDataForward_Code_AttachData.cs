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
    public class TestHistoryDataForward_Code_AttachData
    {
        string code = "RB1710";
        string code2 = "RB0000";

        private List<string> printStrs_Forward_KLine = new List<string>();

        [TestMethod]
        public void TestTickAttachTick()
        {
            int start = 20170601;
            int endDate = 20170602;
            //string code = "A0401";
            //int start = 20040106;
            //int endDate = 20040106;

            IDataForward_Code historyDataForward = ForwardDataGetter.GetHistoryDataForward_Code(code, start, endDate, true, true);
            historyDataForward.AttachOtherData(code2);
            //historyDataForward.OnBar += HistoryDataForward_OnBar;
            historyDataForward.OnTick += HistoryDataForward_OnTick;
            //historyDataForward.OnTick += KlineDataForward_OnTick;
            while (historyDataForward.Forward())
            {

            }
        }

        private void HistoryDataForward_OnTick(object sender, IForwardOnTickArgument argument)
        {
            Console.WriteLine(code + ":" + argument.TickInfo.TickBar);
            Console.WriteLine(code2 + ":" + argument.GetOtherData(code2).GetTickData());
            Console.WriteLine(code2 + ":" + argument.GetOtherData(code2).GetKLineData(KLinePeriod.KLinePeriod_1Minute));
            Console.WriteLine(code2 + ":" + argument.GetOtherData(code2).GetTimeLineData());

            ITickData tickData = argument.GetOtherData(code2).GetTickData();
            IKLineData klineData = argument.GetOtherData(code2).GetKLineData(KLinePeriod.KLinePeriod_1Minute);
            ITimeLineData timeLineData = argument.GetOtherData(code2).GetTimeLineData();
            Assert.AreEqual(tickData.Price, klineData.End);
            Assert.AreEqual(tickData.Price, timeLineData.Price);
        }

        [TestMethod]
        public void TestTickAttachKLine()
        {
            int start = 20170601;
            int endDate = 20170602;
            //string code = "A0401";
            //int start = 20040106;
            //int endDate = 20040106;

            printStrs_Forward_KLine.Clear();

            IDataForward_Code historyDataForward = ForwardDataGetter.GetHistoryDataForward_Code(code, start, endDate, false);
            historyDataForward.AttachOtherData(code2);
            historyDataForward.OnBar += HistoryDataForward_OnBar;
            historyDataForward.OnTick += HistoryDataForward_OnTick;
            //historyDataForward.OnTick += KlineDataForward_OnTick;
            while (historyDataForward.Forward())
            {

            }
            AssertUtils.AssertEqual_List("attachcode_kline", GetType(), printStrs_Forward_KLine);
        }

        private void HistoryDataForward_OnBar(object sender, IForwardOnBarArgument argument)
        {
            string code1txt = code + ":" + argument.AllFinishedBars[0];
            string code2txt = code2 + ":" + argument.GetOtherData(code2).GetKLineData(KLinePeriod.KLinePeriod_1Minute);

            printStrs_Forward_KLine.Add(code1txt);
            printStrs_Forward_KLine.Add(code2txt);
        }
    }
}