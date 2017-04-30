using com.wer.sc.data.market.data;
using com.wer.sc.plugin;
using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.market.impl
{
    public class MarketData : IMarketData
    {
        private IPlugin_MarketUtils plugin_MarketUtils;

        private IPlugin_MarketData currentPlugin_MarketData;

        private ConnectionInfo currentConnectionInfo;

        private List<IMarketDataReceiveTragger> marketDataReceiver = new List<IMarketDataReceiveTragger>();

        private Dictionary<string, TickData_RealTime> dic_TickData = new Dictionary<string, TickData_RealTime>();

        private int currentTradingDay;

        private MarketPluginMgr marketPluginMgr;

        public MarketData(MarketPluginMgr marketPluginMgr)
        {
            this.marketPluginMgr = marketPluginMgr;
        }

        public List<double[]> GetTradingSession(string code, int date)
        {
            if (currentPlugin_MarketData != null)
                return currentPlugin_MarketData.GetTradingSession(code, date);
            return null;
        }

        /// <summary>
        /// 连接指定服务器
        /// </summary>
        public void Connect(ConnectionInfo connectionInfo)
        {
            LogHelper.Info(GetType(), "开始连接数据服务器：" + connectionInfo.Name);
            IPlugin_Market plugin_Market = marketPluginMgr.GetMarketDataByConnection(connectionInfo);
            if (plugin_Market == null)
                throw new ApplicationException("连接" + connectionInfo.Name + "找不到对应插件");

            this.plugin_MarketUtils = plugin_Market.MarketUtils;
            this.currentPlugin_MarketData = plugin_Market.MarketData;
            if (currentPlugin_MarketData == null)
                throw new ApplicationException("连接" + connectionInfo.Name + "未实现市场数据插件");

            this.currentConnectionInfo = connectionInfo;
            this.currentPlugin_MarketData.OnConnectionStatus = OnConnectionStatus;
            this.currentPlugin_MarketData.OnReturnMarketData = OnReturnMarketData;
            this.currentPlugin_MarketData.Connect(connectionInfo);
        }

        /// <summary>
        /// 断开连接
        /// </summary>
        public void DisConnect()
        {
            if (currentPlugin_MarketData != null)
            {
                currentPlugin_MarketData.DisConnect();
            }
        }

        /// <summary>
        /// 重新连接
        /// </summary>
        public void ReConnect()
        {
            if (currentPlugin_MarketData != null)
            {
                currentPlugin_MarketData.DisConnect();
                currentPlugin_MarketData.Connect(currentConnectionInfo);
            }
        }

        /// <summary>
        /// 连接状态改变事件
        /// </summary>
        public event DelegateOnConnectionStatus ConnectionStatusChanged;

        /// <summary>
        /// 订阅指定的品种
        /// </summary>
        /// <param name="instruments"></param>
        public void Subscribe(string[] instruments)
        {
            if (this.currentPlugin_MarketData != null)
                this.currentPlugin_MarketData.Subscribe(GetRemoteInstruments(instruments));
        }

        /// <summary>
        /// 取消订阅品种
        /// </summary>
        /// <param name="instruements"></param>
        public void UnSubscribe(string[] instruements)
        {
            if (this.currentPlugin_MarketData != null)
                this.currentPlugin_MarketData.UnSubscribe(GetRemoteInstruments(instruements));
        }

        /// <summary>
        /// tick数据接收事件
        /// </summary>
        public event DelegateOnDataReceived DataReceived;

        private void OnReturnMarketData(object sender, ref ITickBar marketData)
        {
            string code = marketData.Code;
            TickData_RealTime tickData;
            if (dic_TickData.ContainsKey(code))
            {
                tickData = dic_TickData[code];
            }
            else
            {
                tickData = new TickData_RealTime(code, currentTradingDay, 200);
                dic_TickData.Add(code, tickData);
            }

            tickData.Recieve(marketData);
            for (int i = 0; i < Traggers.Count; i++)
            {
                Traggers[i].TickDataReceived(this, tickData);
            }
            DataReceived?.Invoke(this, tickData);
        }

        private void OnConnectionStatus(object sender, ConnectionStatus status, ref LoginInfo userLogin)
        {
            LogHelper.Info(GetType(), "登陆数据服务器：" + status);
            LogHelper.Info(GetType(), "用户：" + userLogin.AccountID + "|" + userLogin.InvestorName);
            if (ConnectionStatusChanged != null)
                ConnectionStatusChanged(status, status, ref userLogin);

            if (status == ConnectionStatus.Logined)
            {
                //如果交易日期改了，需要通知触发器，否则程序隔夜跑的时候不清楚生成新的tickdata
                if (this.currentTradingDay != userLogin.TradingDay)
                {
                    this.currentTradingDay = userLogin.TradingDay;
                    for (int i = 0; i < Traggers.Count; i++)
                    {
                        Traggers[i].TradingDayChanged(currentTradingDay);
                    }
                    dic_TickData.Clear();
                }
            }
        }

        public List<IMarketDataReceiveTragger> Traggers
        {
            get
            {
                return marketDataReceiver;
            }
        }

        private string[] GetRemoteInstruments(string[] instruments)
        {
            IPlugin_MarketUtils marketUtils = plugin_MarketUtils;
            if (marketUtils == null)
                return instruments;
            string[] remoteInstruments = new string[instruments.Length];
            for (int i = 0; i < instruments.Length; i++)
            {
                remoteInstruments[i] = marketUtils.TransferLocalInstrumentIdToRemote(instruments[i]);
            }
            return remoteInstruments;
        }
    }
}