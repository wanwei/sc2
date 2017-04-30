using com.wer.sc.data.market;
using com.wer.sc.utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin
{
    /// <summary>
    /// 测试用例描述：
    /// 
    /// </summary>
    [TestClass]
    public class TestPluginMgr
    {
        [TestMethod]
        public void TestPluginScan()
        {
            IPluginMgr pluginMgr = GetPluginMgr();
            List<PluginInfo> plugins = pluginMgr.GetAllPlugins();
            Assert.AreEqual(4, plugins.Count);

            List<PluginInfo> plugins_Market = pluginMgr.GetPlugins(typeof(IPlugin_Market));
            Assert.AreEqual(2, plugins_Market.Count);

            List<PluginInfo> plugins_HistoryData = pluginMgr.GetPlugins(typeof(IPlugin_HistoryData));
            Assert.AreEqual(2, plugins_HistoryData.Count);
        }

        private static IPluginMgr GetPluginMgr()
        {
            string path = ScConfig.Instance.ScPath;
            path = path.Replace("Debug", "Mock") + "\\plugin\\";
            IPluginMgr pluginMgr = PluginMgrFactory.CreatePluginMgr(path);
            return pluginMgr;
        }

        private static void AssertMockHistoryData(PluginInfo plugin_MockHistoryData)
        {
            Assert.AreEqual("MOCK.HISTORYDATA", plugin_MockHistoryData.PluginID);
            Assert.AreEqual("模拟历史数据", plugin_MockHistoryData.PluginName);
            Assert.AreEqual("模拟历史数据，测试专用", plugin_MockHistoryData.PluginDesc);
            Assert.AreEqual("com.wer.sc.plugin.mock.historydata.Plugin_HistoryData_Mock", plugin_MockHistoryData.PluginClassType.FullName);
            Assert.AreEqual(typeof(IPlugin_HistoryData), plugin_MockHistoryData.PluginType);
        }

        private static void AssertMockHistoryDataSina(PluginInfo plugin_MockHistoryData)
        {
            Assert.AreEqual("MOCK.HISTORYDATA.SINA", plugin_MockHistoryData.PluginID);
            Assert.AreEqual("模拟历史数据，新浪数据", plugin_MockHistoryData.PluginName);
            Assert.AreEqual("模拟历史数据，模拟取新浪数据，测试专用", plugin_MockHistoryData.PluginDesc);
            Assert.AreEqual("com.wer.sc.plugin.mock.historydata.Plugin_HistoryData_Mock_Sina", plugin_MockHistoryData.PluginClassType.FullName);
            Assert.AreEqual(typeof(IPlugin_HistoryData), plugin_MockHistoryData.PluginType);
        }

        private static void AssertMockMarket(PluginInfo plugin_MockMarket)
        {
            Assert.AreEqual("MOCK.MARKET", plugin_MockMarket.PluginID);
            Assert.AreEqual("模拟市场", plugin_MockMarket.PluginName);
            Assert.AreEqual("模拟市场，测试专用", plugin_MockMarket.PluginDesc);
            Assert.AreEqual("com.wer.sc.plugin.mock.market.Plugin_Market_Mock", plugin_MockMarket.PluginClassType.FullName);
            Assert.AreEqual(typeof(IPlugin_Market), plugin_MockMarket.PluginType);
        }

        private static void AssertMockMarketWeb(PluginInfo plugin_MockMarket)
        {
            Assert.AreEqual("MOCK.MARKET.WEB", plugin_MockMarket.PluginID);
            Assert.AreEqual("模拟市场，基于网页版交易", plugin_MockMarket.PluginName);
            Assert.AreEqual("模拟市场，基于网页版交易，测试专用", plugin_MockMarket.PluginDesc);
            Assert.AreEqual("com.wer.sc.plugin.mock.market.Plugin_Market_Mock_Web", plugin_MockMarket.PluginClassType.FullName);
            Assert.AreEqual(typeof(IPlugin_Market), plugin_MockMarket.PluginType);
        }

        private static void AssertMockMarketObject(IPlugin_Market plugin_Market)
        {
            List<ConnectionInfo> connections = plugin_Market.MarketData.GetAllConnections();
            Assert.AreEqual(2, connections.Count);
            ConnectionInfo connection = connections[0];
            Assert.AreEqual("MOCKCONN1", connection.Id);
            Assert.AreEqual("MOCK连接", connection.Name);
            Assert.AreEqual("MOCK连接，测试用", connection.Description);
        }

        private static void AssertMockHistoryDataObject(IPlugin_HistoryData plugin_HistoryData)
        {
            //Assert.AreEqual(@"D:\SCTEST\MOCKDATA\", plugin_HistoryData.GetDataCenterUri());
            //Assert.AreEqual("MOCK历史数据", plugin_HistoryData.GetName());
            //Assert.AreEqual("MOCK出的历史数据，专用测试", plugin_HistoryData.GetDescription());
        }

        [TestMethod]
        public void TestGetPluginByType()
        {
            IPluginMgr pluginMgr = GetPluginMgr();
            List<PluginInfo> plugins = pluginMgr.GetPlugins(MarketType.CnFutures, typeof(IPlugin_HistoryData));
            Assert.AreEqual(1, plugins.Count);
            AssertMockHistoryData(plugins[0]);
            plugins = pluginMgr.GetPlugins(MarketType.CnStock, typeof(IPlugin_HistoryData));
            Assert.AreEqual(1, plugins.Count);
            AssertMockHistoryDataSina(plugins[0]);
            plugins = pluginMgr.GetPlugins(MarketType.CnFutures, typeof(IPlugin_Market));
            Assert.AreEqual(1, plugins.Count);
            AssertMockMarket(plugins[0]);
            plugins = pluginMgr.GetPlugins(MarketType.CnStock, typeof(IPlugin_Market));
            Assert.AreEqual(1, plugins.Count);
            AssertMockMarketWeb(plugins[0]);
        }

        [TestMethod]
        public void TestGetPluginById()
        {
            IPluginMgr pluginMgr = GetPluginMgr();
            PluginInfo pluginMarket = pluginMgr.GetPlugin("MOCK.MARKET");
            AssertMockMarket(pluginMarket);
            PluginInfo pluginMarketWeb = pluginMgr.GetPlugin("MOCK.MARKET.WEB");
            AssertMockMarketWeb(pluginMarketWeb);
            PluginInfo pluginHistoryData = pluginMgr.GetPlugin("MOCK.HISTORYDATA");
            AssertMockHistoryData(pluginHistoryData);
            PluginInfo pluginHistoryDataSina = pluginMgr.GetPlugin("MOCK.HISTORYDATA.SINA");
            AssertMockHistoryDataSina(pluginHistoryDataSina);
        }

        [TestMethod]
        public void TestCreatePluginObject()
        {
            IPluginMgr pluginMgr = GetPluginMgr();
            IPlugin_Market plugin_Market = pluginMgr.CreatePluginObject<IPlugin_Market>("MOCK.MARKET");
            AssertMockMarketObject(plugin_Market);

            IPlugin_HistoryData plugin_HistoryData = pluginMgr.CreatePluginObject<IPlugin_HistoryData>("MOCK.HISTORYDATA");
            AssertMockHistoryDataObject(plugin_HistoryData);
        }

        [TestMethod]
        public void TestGetPluginObject()
        {
            IPluginMgr pluginMgr = GetPluginMgr();
            IPlugin_Market plugin_Market = pluginMgr.GetPluginObject<IPlugin_Market>("MOCK.MARKET");
            AssertMockMarketObject(plugin_Market);

            IPlugin_HistoryData plugin_HistoryData = pluginMgr.GetPluginObject<IPlugin_HistoryData>("MOCK.HISTORYDATA");
            AssertMockHistoryDataObject(plugin_HistoryData);
        }
    }
}