using com.wer.sc.plugin;
using com.wer.sc.plugin.market;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XAPI.Callback;
using XAPI;
using System.IO;
using System.Reflection;
using com.wer.sc.data.market;

namespace com.wer.sc.plugin.cnfutures.market
{
    public class Plugin_MarketTrader_CnFutures : Plugin_XApi_Base, IPlugin_MarketTrader
    {
        const string tradePath = @"plugin\cnfutures\ctp\CTP_Trade_x86.dll";

        private XApi api_Trade;

        public Plugin_MarketTrader_CnFutures()
        {
            //string dllPath = Assembly.GetExecutingAssembly().Location;
            //FileInfo f = new FileInfo(dllPath);
            //string path = f.DirectoryName + tradePath;
            api_Trade = new XApi(tradePath);
        }

        /// <summary>
        /// 连接市场服务器
        /// </summary>
        public void Connect(ConnectionInfo connectionInfo)
        {
            api_Trade.Server.BrokerID = connectionInfo.GetValue("BrokerId");
            api_Trade.Server.Address = connectionInfo.GetValue("TradeServer");
            api_Trade.User.UserID = connectionInfo.GetValue("UserID");
            api_Trade.User.Password = connectionInfo.GetValue("Passwd");

            api_Trade.OnConnectionStatus = XApi_OnConnectionStatus;
            api_Trade.OnRspQryInstrument = XApi_OnRspQryInstrument;

            api_Trade.OnRspQryTradingAccount = XApi_OnRspQryTradingAccount;
            api_Trade.OnRspQryInvestorPosition = XApi_OnRspQryInvestorPosition;
            api_Trade.OnRspQrySettlementInfo = Xapi_OnRspQrySettlementInfo;
            api_Trade.OnRtnOrder = XApi_OnRtnOrder;
            api_Trade.OnRtnError = XApi_OnRtnError;
            api_Trade.OnRtnTrade = XApi_OnRtnTrade;
            api_Trade.Connect();
        }

        #region XApi Delegate

        private void XApi_OnConnectionStatus(object sender, XAPI.ConnectionStatus status, ref XAPI.RspUserLoginField userLogin, int size1)
        {
            if (onConnectionStatus == null)
                return;

            LoginInfo loginInfo = StructTransfer.TransferUserLogin(userLogin);
            onConnectionStatus(sender, EnumTransfer.TransferConnectionStatus(status), ref loginInfo);
        }

        private List<InstrumentInfo> instruments = new List<InstrumentInfo>();

        private void XApi_OnRspQryInstrument(object sender, ref XAPI.InstrumentField instrument, int size1, bool bIsLast)
        {
            if (onReturnInstrument == null)
                return;

            //TODO 这里连续查询两次会有问题，暂不处理
            instruments.Add(StructTransfer.TransferInstrumentInfo(instrument));
            if (bIsLast)// || instruments.Count == size1)
            {
                onReturnInstrument(sender, ref instruments);
                instruments.Clear();
            }
        }


        private void XApi_OnRspQryTradingAccount(object sender, ref AccountField account, int size1, bool bIsLast)
        {
            if (onReturnAccount != null)
            {
                AccountInfo accountInfo = StructTransfer.TransferAccountInfo(account);
                onReturnAccount(this, ref accountInfo);
            }
        }


        private void Xapi_OnRspQrySettlementInfo(object sender, ref SettlementInfoClass settlementInfo, int size1, bool bIsLast)
        {

        }

        private void XApi_OnRtnOrder(object sender, ref OrderField order)
        {
            if (onReturnOrder != null)
            {
                OrderInfo orderInfo = StructTransfer.TransferOrderInfo(order);
                onReturnOrder(this, ref orderInfo);
            }
        }

        private void XApi_OnRtnError(object sender, ref ErrorField error)
        {
            //TODO
        }

        private void XApi_OnRtnTrade(object sender, ref TradeField trade)
        {
            if (onReturnTrade != null)
            {
                TradeInfo tradeInfo = StructTransfer.TransferTradeInfo(trade);
                onReturnTrade(this, ref tradeInfo);
            }
        }

        private void XApi_OnRspQryInvestorPosition(object sender, ref PositionField position, int size1, bool bIsLast)
        {
            if (onReturnInvestorPosition != null)
            {
                PositionInfo positionInfo = StructTransfer.TransferPositionInfo(position);
                onReturnInvestorPosition(this, ref positionInfo);
            }
        }

        #endregion

        #region 委托属性

        private sc.data.market.DelegateOnConnectionStatus onConnectionStatus;

        public sc.data.market.DelegateOnConnectionStatus OnConnectionStatus
        {
            get
            {
                return onConnectionStatus;
            }

            set
            {
                onConnectionStatus = value;
            }
        }

        private DelegateOnReturnInstrument onReturnInstrument;

        public DelegateOnReturnInstrument OnReturnInstrument
        {
            get
            {
                return onReturnInstrument;
            }

            set
            {
                onReturnInstrument = value;
            }
        }

        private DelegateOnReturnAccount onReturnAccount;

        public DelegateOnReturnAccount OnReturnAccount
        {
            get
            {
                return onReturnAccount;
            }

            set
            {
                this.onReturnAccount = value;
            }
        }

        public DelegateOnReturnInstrument OnReturnInstruments
        {
            get
            {
                return onReturnInstrument;
            }

            set
            {
                this.onReturnInstrument = value;
            }
        }

        private DelegateOnReturnOrder onReturnOrder;

        public DelegateOnReturnOrder OnReturnOrder
        {
            get
            {
                return onReturnOrder;
            }

            set
            {
                this.onReturnOrder = value;
            }
        }

        private DelegateOnReturnTrade onReturnTrade;

        public DelegateOnReturnTrade OnReturnTrade
        {
            get
            {
                return onReturnTrade;
            }

            set
            {
                this.onReturnTrade = value;
            }
        }

        private DelegateOnReturnInvestorPosition onReturnInvestorPosition;

        public DelegateOnReturnInvestorPosition OnReturnInvestorPosition
        {
            get
            {
                return onReturnInvestorPosition;
            }

            set
            {
                onReturnInvestorPosition = value;
            }
        }
        #endregion

        /// <summary>
        /// 断开市场服务器
        /// </summary>
        public void DisConnect()
        {
            api_Trade.Disconnect();
        }

        public void QueryInstruments(string[] instruments)
        {
            XAPI.ReqQueryField field = new XAPI.ReqQueryField();
            api_Trade.ReqQuery(XAPI.QueryType.ReqQryInstrument, ref field);
        }

        public string SendOrder(OrderInfo orderInfo)
        {
            OrderField order = new OrderField();
            order.InstrumentID = orderInfo.Instrumentid;
            order.OpenClose = XAPI.OpenCloseType.Open;
            order.HedgeFlag = HedgeFlagType.Speculation;
            order.Price = orderInfo.Price;
            order.Qty = orderInfo.Volume;
            order.Type = XAPI.OrderType.Limit;
            order.Side = XAPI.OrderSide.Buy;
            return api_Trade.SendOrder(ref order);
        }

        public string CancelOrder(string orderId)
        {
            return api_Trade.CancelOrder(orderId);
        }

        public void QueryAccount()
        {

        }

        /// <summary>
        /// 查询仓位信息
        /// </summary>
        public void QueryPosition()
        {

        }
    }
}