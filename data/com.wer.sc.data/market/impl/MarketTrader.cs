using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.plugin;
using com.wer.sc.utils;

namespace com.wer.sc.data.market.impl
{
    public class MarketTrader : IMarketTrader
    {
        private MarketPluginMgr marketPluginMgr;

        private IPlugin_MarketTrader currentPlugin_MarketTrader;

        private ConnectionInfo currentConnectionInfo;

        public MarketTrader(MarketPluginMgr marketPluginMgr)
        {
            this.marketPluginMgr = marketPluginMgr;
        }

        /// <summary>
        /// 连接指定服务器
        /// </summary>
        public void Connect(ConnectionInfo connectionInfo)
        {
            LogHelper.Info(GetType(), "开始连接服务器：" + connectionInfo.Name);
            IPlugin_Market plugin_Market = marketPluginMgr.GetMarketTraderByConnection(connectionInfo);
            if (plugin_Market == null)
                throw new ApplicationException("连接" + connectionInfo.Name + "找不到对应插件");

            this.currentPlugin_MarketTrader = plugin_Market.MarketTrader;
            if (currentPlugin_MarketTrader == null)
                throw new ApplicationException("连接" + connectionInfo.Name + "未实现市场交易插件");

            this.currentConnectionInfo = connectionInfo;
            this.currentPlugin_MarketTrader.OnConnectionStatus = OnConnectionStatus;
            this.currentPlugin_MarketTrader.OnReturnInstruments = OnReturnInstrument;
            this.currentPlugin_MarketTrader.OnReturnAccount = OnReturnAccount;
            this.currentPlugin_MarketTrader.OnReturnInvestorPosition = OnReturnInvestorPosition;
            this.currentPlugin_MarketTrader.OnReturnOrder = OnReturnOrder;
            this.currentPlugin_MarketTrader.OnReturnTrade = OnReturnTrade;

            this.currentPlugin_MarketTrader.Connect(currentConnectionInfo);
        }

        private void OnReturnTrade(object sender, ref TradeInfo trade)
        {
            if (TradeReturned != null)
                TradeReturned(this, ref trade);
        }

        private void OnReturnOrder(object sender, ref OrderInfo order)
        {
            if (OrderReturned != null)
                OrderReturned(this, ref order);
        }

        private void OnReturnInvestorPosition(object sender, ref PositionInfo trade)
        {
            if (InvestorPositionReturned != null)
                InvestorPositionReturned(this, ref trade);
        }

        private void OnReturnAccount(object sender, ref AccountInfo account)
        {
            if (AccountReturned != null)
                AccountReturned(this, ref account);
        }

        public void OnReturnInstrument(object sender, ref List<InstrumentInfo> instruments)
        {
            if (InstrumentsReturned != null)
                InstrumentsReturned(sender, ref instruments);
        }

        private void OnConnectionStatus(object sender, ConnectionStatus status, ref LoginInfo userLogin)
        {
            LogHelper.Info(GetType(), "登陆交易服务器：" + status);
            LogHelper.Info(GetType(), "用户：" + userLogin.AccountID + "|" + userLogin.InvestorName);
            if (ConnectionStatusChanged != null)
                ConnectionStatusChanged(status, status, ref userLogin);
        }

        /// <summary>
        /// 断开连接
        /// </summary>
        public void DisConnect()
        {
            if (currentPlugin_MarketTrader != null)
                currentPlugin_MarketTrader.DisConnect();
        }

        /// <summary>
        /// 重新连接
        /// </summary>
        public void ReConnect()
        {
            if (currentPlugin_MarketTrader != null)
            {
                currentPlugin_MarketTrader.DisConnect();
                currentPlugin_MarketTrader.Connect(currentConnectionInfo);
            }
        }

        /// <summary>
        /// 连接状态改变事件
        /// </summary>
        public event DelegateOnConnectionStatus ConnectionStatusChanged;

        /// <summary>
        /// 查询品种信息
        /// </summary>
        /// <param name="instruments"></param>
        public void QueryInstruments(string[] instruments = null)
        {
            this.currentPlugin_MarketTrader.QueryInstruments(instruments);
        }

        /// <summary>
        /// 返回合约信息
        /// </summary>
        public event DelegateOnReturnInstrument InstrumentsReturned;

        /// <summary>
        /// 下单
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public string SendOrder(OrderInfo order)
        {
            return this.currentPlugin_MarketTrader.SendOrder(order);
        }

        /// <summary>
        /// 撤单
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        public string CancelOrder(string orderid)
        {
            return this.currentPlugin_MarketTrader.CancelOrder(orderid);
        }

        /// <summary>
        /// 设置或获取交易委托回调
        /// </summary>
        public event DelegateOnReturnOrder OrderReturned;

        /// <summary>
        /// 设置或获取成交回调
        /// </summary>
        public event DelegateOnReturnTrade TradeReturned;

        /// <summary>
        /// 查询仓位信息
        /// </summary>
        public void QueryPosition()
        {
            this.currentPlugin_MarketTrader.QueryPosition();
        }

        /// <summary>
        /// 设置或获取持仓信息回调
        /// </summary>
        public event DelegateOnReturnInvestorPosition InvestorPositionReturned;

        /// <summary>
        /// 查询账户信息
        /// </summary>
        public void QueryAccount()
        {
            this.currentPlugin_MarketTrader.QueryAccount();
        }

        /// <summary>
        /// 设置或获取账号信息回调
        /// </summary>
        public event DelegateOnReturnAccount AccountReturned;
    }
}