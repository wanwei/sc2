using com.wer.sc.plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.data.market;
using com.wer.sc.data.account;

namespace com.wer.sc.data.market
{
    public class Plugin_MarketTrader_Simu : IPlugin_MarketTrader
    {
        private IAccount account;

        public Plugin_MarketTrader_Simu()
        {

        }

        public Plugin_MarketTrader_Simu(IAccount account)
        {
            this.account = account;            
        }

        private void Account_OnReturnOrder(object sender, ref OrderInfo order)
        {
            if (this.onReturnOrder != null)            
                this.onReturnOrder(this, ref order);            
        }

        private void Account_OnReturnTrade(object sender, ref TradeInfo trade)
        {
            if (this.onReturnTrade != null)
                this.onReturnTrade(this, ref trade);
        }

        public string SendOrder(OrderInfo order)
        {
            this.account.Open(order.Instrumentid, order.Price, order.Direction, order.Volume);
            return "";
        }

        public string CancelOrder(string orderid)
        {
            this.account.CancelOrder(orderid);
            return "";
        }

        /// <summary>
        /// connectionInfo:
        /// 
        /// 新建一个账户
        /// isnew : true
        /// money : 100000
        /// id : testmock
        /// 
        /// isnew = false
        /// id:testmock
        /// 
        /// </summary>
        /// <param name="connectionInfo"></param>
        public void Connect(ConnectionInfo connectionInfo)
        {
            account = DataCenter.Default.AccountFactory.CreateAccount(100000);
            account.AccountSetting.DelayTime = 0;            

            this.account.OnReturnOrder += Account_OnReturnOrder;
            this.account.OnReturnTrade += Account_OnReturnTrade;
        }

        public void DisConnect()
        {

        }

        public List<ConnectionInfo> GetAllConnections()
        {
            return null;
        }

        public void QueryAccount()
        {

        }

        public void QueryInstruments(string[] instruments = null)
        {

        }

        public void QueryPosition()
        {

        }

        private DelegateOnConnectionStatus onConnectionStatus;

        public DelegateOnConnectionStatus OnConnectionStatus
        {
            get
            {
                return onConnectionStatus;
            }

            set
            {
                this.onConnectionStatus = value;
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

        private DelegateOnReturnInstrument onReturnInstruments;

        public DelegateOnReturnInstrument OnReturnInstruments
        {
            get
            {
                return onReturnInstruments;
            }

            set
            {
                this.onReturnInstruments = value;
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
                this.onReturnInvestorPosition = value;
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
    }
}