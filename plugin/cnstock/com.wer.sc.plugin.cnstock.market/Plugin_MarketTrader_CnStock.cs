using com.wer.sc.data.market;
using com.wer.sc.plugin;
using com.wer.sc.plugin.market;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XAPI.Callback;

namespace com.wer.sc.plugin.market.cnstock
{
    public class Plugin_MarketTrader_CnStock : Plugin_XApi_Base, IPlugin_MarketTrader
    {
        const string tradePath = @"plugin\CTP\CTP_Trade_x86.dll";

        private XApi api_Trade;

        private DelegateOnConnectionStatus onConnectionStatus;

        private DelegateOnReturnInstrument onReturnInstrument;

        public DelegateOnConnectionStatus OnConnectionStatus
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

        public Plugin_MarketTrader_CnStock()
        {
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
            api_Trade.Connect();
        }

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

        public string SendOrder(OrderInfo order)
        {
            throw new NotImplementedException();
        }

        public string CancelOrder(string orderid)
        {
            throw new NotImplementedException();
        }

        public void QueryAccount()
        {
        }

        public void QueryPosition()
        {
                       
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
                onReturnTrade = value;
            }
        }

        public DelegateOnReturnInvestorPosition OnReturnInvestorPosition
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public DelegateOnReturnAccount OnReturnAccount
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }
    }
}