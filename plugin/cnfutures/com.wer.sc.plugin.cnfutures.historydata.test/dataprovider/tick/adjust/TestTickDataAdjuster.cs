using com.wer.sc.data;
using com.wer.sc.mockdata;
//using com.wer.sc.plugin.cnfutures.historydata.dataprovider.biaopuyonghua.adjust;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin.cnfutures.historydata.dataloader.tick.adjust
{
    [TestClass]
    public class TestTickDataAdjuster
    {
        [TestMethod]
        public void TestTickDataClean_M05_20040106()
        {
            //ITickData tickData = GetAdjustTickData("m05", 20040106);
            //AssertUtils.AssertEqual_TickData("TickDataAdjust_M05_20040106", GetType(), tickData);
        }

        //[TestMethod]
        //public void TestTickDataClean_M05_20140106()
        //{
        //    ITickData tickData = GetAdjustTickData("m05", 20140106);
        //    AssertUtils.AssertEqual_TickData("TickDataAdjust_M05_20140106", GetType(), tickData);
        //}

        //private ITickData GetAdjustTickData(string code, int date)
        //{
        //    TickData tickData = (TickData)MockDataLoader.GetTickData(code, date);
        //    List<double[]> openTime = MockDataLoader.GetTradingTime(code, date);
        //    TickDataAdjuster clean = new TickDataAdjuster();
        //    clean.Adjust(tickData, openTime);
        //    return tickData;
        //}
    }
}
