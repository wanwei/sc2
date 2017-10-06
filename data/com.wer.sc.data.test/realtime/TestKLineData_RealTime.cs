using com.wer.sc.mockdata;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.realtime
{
    [TestClass]
    public class TestKLineData_RealTime
    {
        [TestMethod]
        public void TestSetRealTimeData()
        {
            IKLineData data = MockDataLoader.GetKLineData("m1405", 20131202, 20131205, KLinePeriod.KLinePeriod_1Minute);
            //IKLineData data = MockDataLoader_Long.GetKLineData("m1405", 20131202, 20131205, KLinePeriod.KLinePeriod_1Minute);
            KLineData_RealTime data_real = new KLineData_RealTime(data);            

            KLineBar chart = new KLineBar(data_real, 15);
            data.BarPos = 15;
            chart.Code = "m1405";
            chart.Time = 20131202.091455;
            chart.Start = 3745;
            chart.High = 3745;
            chart.Low = 3743;
            chart.End = 3743;
            chart.Mount = 1600;
            chart.Hold = 718011;

            data_real.SetRealTimeData(chart, 15);
            Assert.AreEqual("20131202.091455,3745,3745,3743,3743,1600,0,718011", chart.ToString());
            Assert.AreEqual(chart.ToString(), data_real.ToString());
            Assert.AreEqual(2, data_real.Height);
            //Console.WriteLine(data_real.HeightPercent);
            Assert.AreEqual(3745, data_real.BlockHigh);
            Assert.AreEqual(3743, data_real.BlockLow);
            Assert.AreEqual(2, data_real.BlockHeight);
            //Console.WriteLine(data_real.BlockHeightPercent);
            //Console.WriteLine(data_real.up);
            //Assert.AreEqual(3745, data_real.Arr_BlockHigh);

            //                list_Height.SetTmpValue(barPos, chart.Height);
            //list_HeightPercent.SetTmpValue(barPos, chart.HeightPercent);
            //list_BlockHigh.SetTmpValue(barPos, chart.BlockHigh);
            //list_BlockLow.SetTmpValue(barPos, chart.BlockLow);
            //list_BlockHeight.SetTmpValue(barPos, chart.BlockHeight);
            //list_BlockHeightPercent.SetTmpValue(barPos, chart.BlockHeightPercent);
            //float upPercent = barPos == 0 ?
            //    (float)NumberUtils.percent(chart.End, chart.Start) :
            //    (float)NumberUtils.percent(chart.End, klineData.Arr_End[barPos - 1]);
            //list_UpPercent.SetTmpValue(barPos, upPercent);

            data_real.SetRealTimeData(null, 16);
            Assert.AreEqual("20131202.0915,3354,3354,3352,3353,9202,0,1717708", data_real.ToString(15));
        }
    }
}
