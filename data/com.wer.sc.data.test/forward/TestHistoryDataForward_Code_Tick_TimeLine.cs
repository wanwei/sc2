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
    public class TestHistoryDataForward_Code_Tick_TimeLine
    {
        private List<string> printStrs_Forward_TimeLine = new List<string>();

        [TestMethod]
        public void TestForward_TimeLineData()
        {
            printStrs_Forward_TimeLine.Clear();
            string code = "RB1710";
            int start = 20170601;
            int endDate = 20170603;

            IDataForward_Code klineDataForward = ForwardDataGetter.GetHistoryDataForward_Code(code, start, endDate, true, true, new KLinePeriod[] { KLinePeriod.KLinePeriod_1Minute });
            klineDataForward.OnTick += KlineDataForward_OnTick;
            DateTime prevtime = DateTime.Now;
            while (klineDataForward.Forward())
            {

            }
            AssertUtils.AssertEqual_List("forward_tick_timeline", GetType(), printStrs_Forward_TimeLine);
            printStrs_Forward_TimeLine.Clear();
        }

        private void KlineDataForward_OnTick(object sender, IForwardOnTickArgument argument)
        {
            IDataForward_Code klineDataForward = (IDataForward_Code)sender;
            //Console.WriteLine(klineDataForward.GetTimeLineData());
            printStrs_Forward_TimeLine.Add(klineDataForward.GetTimeLineData().ToString());
        }
    }
}
