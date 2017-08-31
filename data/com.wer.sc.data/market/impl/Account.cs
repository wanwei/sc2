using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.data.market;
using com.wer.sc.data.reader;
using com.wer.sc.data.forward;
using com.wer.sc.data;
using com.wer.sc.utils;
using System.Xml;
using com.wer.sc.plugin;

namespace com.wer.sc.data.market.impl
{
    /// <summary>
    /// 针对历史数据的交易账户
    /// </summary>
    public class Account : IXmlExchange
    {
        //账号ID
        private string accountId;

        private AccountTrade accountTrade;

        private IPriceGetter priceGetter;

        /// <summary>
        /// 今日委托
        /// </summary>
        private Dictionary<string, OrderInfo> dic_ID_TodayOrderInfo;

        public Account(string accountId, double initMoney, IHistoryDataForward historyDataForward)
        {
            this.accountId = accountId;
            this.accountTrade = new AccountTrade(initMoney);
        }

        public Account(string accountId, double money, IHistoryDataForward_Code historyDataForward_Code)
        {
            this.accountId = accountId;
            this.accountTrade = new AccountTrade(money);
            this.priceGetter = new PriceGetter_HistoryDataForward_Code(historyDataForward_Code);
            this.priceGetter.timeChange += PriceGetter_timeChange;
        }

        private void PriceGetter_timeChange(IPriceGetter priceGetter)
        {
            this.accountTrade.ChangeTime(1, priceGetter);
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

        public void SendOrder(OrderInfo order)
        {
            accountTrade.SendOrder(order);
        }

        public void CancelOrder(string orderid)
        {
            accountTrade.CancelOrder(orderid);
        }

        public void Save(XmlElement xmlElem)
        {
            xmlElem.SetAttribute("accountid", this.accountId);
            this.accountTrade.Save(xmlElem);
        }

        public void Load(XmlElement xmlElem)
        {

        }

        public override string ToString()
        {
            return XmlUtils.ToString(this);
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
                onReturnAccount = value;
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
                onReturnTrade = value;
            }
        }
    }
}