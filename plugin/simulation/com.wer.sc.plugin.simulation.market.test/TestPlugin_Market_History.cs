using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using com.wer.sc.data;

namespace com.wer.sc.plugin.market.history.test
{
    [TestClass]
    public class TestPlugin_Market_History
    {
        [TestMethod]
        public void TestMarket_History_Data_Frequency500()
        {
            IPluginMgr pluginMgr = PluginMgrFactory.DefaultPluginMgr;
            PluginInfo pluginInfo = pluginMgr.GetPlugin("MARKET.SIMULATION");
            //IPlugin_Market market = pluginMgr.CreatePluginObject<IPlugin_Market>(pluginInfo);

            //ConnectionInfo connectInfo = new ConnectionInfo();
            //connectInfo.AddValue("DataPath", @"D:\SCDATA\CNFUTURES");
            //connectInfo.AddValue("Interval", "-1");
            //connectInfo.AddValue("Time", "20100105.090000");
            //connectInfo.AddValue("TimeForward", "500");

            //market.MarketData.Subscribe(new string[] { "m05", "m09" });
            //market.MarketData.OnReturnMarketData = OnReturnMarketData;
            //market.MarketData.Connect(connectInfo);
        }

        void OnReturnMarketData(object sender, ref ITickBar marketData)
        {
            Console.WriteLine(marketData.Code + "," + marketData);
        }
    }
}
