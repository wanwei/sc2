using com.wer.sc.mockdata;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.transfer
{
    [TestClass]
    public class TestDataTransfer_KLine2TimeLine
    {
        [TestMethod]
        public void TestTransferTimeLineData()
        {
            IKLineData klineData = MockDataLoader.GetKLineData("m1505", 20150107, 20150107, KLinePeriod.KLinePeriod_1Minute);
            ITimeLineData timeLineData = DataTransfer_KLine2TimeLine.ConvertTimeLineData(klineData, 2849);
            AssertUtils.AssertEqual_TimeLineData("KLine2TimeLine_M05_20150107", GetType(), timeLineData);
        }
    }
}