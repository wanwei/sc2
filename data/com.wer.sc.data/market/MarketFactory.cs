using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.plugin;
using com.wer.sc.data.market.impl;

namespace com.wer.sc.data.market
{
    /// <summary>
    /// 市场工厂
    /// </summary>
    public class MarketFactory
    {
        private MarketType marketType;

        private MarketPluginMgr marketPluginMgr;

        public MarketFactory(MarketType marketType)
        {
            this.marketType = marketType;
            this.marketPluginMgr = new MarketPluginMgr(marketType);
        }

        /// <summary>
        /// 根据市场类型一个市场的数据接收和交易接口
        /// 现支持国内期货市场或股票市场
        /// </summary>
        /// <param name="marketType"></param>
        /// <returns></returns>
        public IMarket CreateMarket()
        {
            return new Market(marketPluginMgr);
        }

        /// <summary>
        /// 根据市场类型得到该市场的所有数据连接
        /// </summary>
        /// <param name="marketType"></param>
        /// <returns></returns>
        public List<ConnectionInfo> GetMarketDataConnections()
        {
            return marketPluginMgr.GetMarketDataConnections();
        }

        public ConnectionInfo GetMarketDataConnection(String id)
        {
            List<ConnectionInfo> conns = GetMarketDataConnections();
            ConnectionInfo testConn = conns[0];
            for (int i = 1; i < conns.Count; i++)
            {
                ConnectionInfo conn = conns[i];
                if (conn.Id == id)
                    testConn = conn;
            }
            return testConn;
        }

        /// <summary>
        /// 根据市场类型得到该市场的所有交易连接
        /// </summary>
        /// <param name="marketType"></param>
        /// <returns></returns>
        public List<ConnectionInfo> GetTraderConnections()
        {
            return marketPluginMgr.GetMarketTraderConnections();
        }
    }

    /// <summary>
    /// 市场插件管理器
    /// </summary>
    public class MarketPluginMgr
    {
        private MarketType marketType;

        private Dictionary<string, IPlugin_Market> dic_ConnectionId_MarketData = new Dictionary<string, IPlugin_Market>();

        private Dictionary<string, IPlugin_Market> dic_ConnectionId_MarketTrader = new Dictionary<string, IPlugin_Market>();

        private List<ConnectionInfo> marketDataConnections;

        private List<ConnectionInfo> marketTraderConnections;

        public MarketType MarketType
        {
            get
            {
                return marketType;
            }
        }

        public MarketPluginMgr(MarketType marketType)
        {
            IPluginMgr pluginMgr = PluginMgrFactory.DefaultPluginMgr;
            List<PluginInfo> plugins = pluginMgr.GetPlugins(marketType, typeof(IPlugin_Market));

            marketDataConnections = new List<ConnectionInfo>();
            marketTraderConnections = new List<ConnectionInfo>();

            for (int i = 0; i < plugins.Count; i++)
            {
                PluginInfo pluginInfo = plugins[i];
                IPlugin_Market plugin_Market = pluginMgr.GetPluginObject<IPlugin_Market>(pluginInfo);

                AddMarketDataConnections(pluginInfo.PluginID, plugin_Market, plugin_Market.MarketData.GetAllConnections());
                AddMarketTraderConnections(pluginInfo.PluginID, plugin_Market, plugin_Market.MarketTrader.GetAllConnections());
            }
        }

        private void AddMarketDataConnections(string pluginId, IPlugin_Market plugin_Market, List<ConnectionInfo> conns)
        {
            for (int i = 0; i < conns.Count; i++)
            {
                ConnectionInfo conn = conns[i];
                ConnectionInfo newConn = (ConnectionInfo)conn.Clone();
                string connID = pluginId + "." + conn.Id;
                newConn.Data["ID"] = connID;
                marketDataConnections.Add(newConn);
                dic_ConnectionId_MarketData.Add(connID, plugin_Market);
            }
        }

        private void AddMarketTraderConnections(string pluginId, IPlugin_Market plugin_Market, List<ConnectionInfo> conns)
        {
            for (int i = 0; i < conns.Count; i++)
            {
                ConnectionInfo conn = conns[i];
                ConnectionInfo newConn = (ConnectionInfo)conn.Clone();
                string connID = pluginId + "." + conn.Id;
                newConn.Data["ID"] = connID;
                marketTraderConnections.Add(newConn);
                dic_ConnectionId_MarketTrader.Add(connID, plugin_Market);
            }
        }

        public List<ConnectionInfo> GetMarketDataConnections()
        {
            return marketDataConnections;
        }

        public List<ConnectionInfo> GetMarketTraderConnections()
        {
            return marketTraderConnections;
        }

        public IPlugin_Market GetMarketDataByConnection(ConnectionInfo connection)
        {
            if (dic_ConnectionId_MarketData.ContainsKey(connection.Id))
                return dic_ConnectionId_MarketData[connection.Id];
            return null;
        }

        public IPlugin_Market GetMarketTraderByConnection(ConnectionInfo connection)
        {
            if (dic_ConnectionId_MarketTrader.ContainsKey(connection.Id))
                return dic_ConnectionId_MarketTrader[connection.Id];
            return null;
        }
    }
}