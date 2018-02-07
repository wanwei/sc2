using com.wer.sc.mockdata;
using com.wer.sc.utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin.cnfutures.config
{
    [TestClass]
    public class TestDataLoader_TradingSessionDetail
    {
        [TestMethod]
        public void TestGetTradingSessionDetail()
        {
            string pluginPath = ScConfig.Instance.ScPath;
            DataLoader_Variety dataLoader_Instrument = new DataLoader_Variety(pluginPath);
            DataLoader_TradingSessionDetail dataLoader = new DataLoader_TradingSessionDetail(pluginPath, dataLoader_Instrument);

            List<double[]> tradingSessionDetail = dataLoader.GetTradingTime("m1005", 20100104);
            AssertUtils.AssertEqual_List<double[]>("tradingsessiondetail_normal", GetType(), tradingSessionDetail);

            tradingSessionDetail = dataLoader.GetTradingTime("rb1005", 20100106);
            AssertUtils.AssertEqual_List<double[]>("tradingsessiondetail_sqearly", GetType(), tradingSessionDetail);

            tradingSessionDetail = dataLoader.GetTradingTime(20100104);
            AssertUtils.PrintLineList(tradingSessionDetail);
        }
    }
}
