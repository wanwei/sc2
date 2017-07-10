using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace com.wer.sc.data.market.impl
{
    public class AccountTrade : IXmlExchange
    {
        private AccountTodayOrder accountOrder;

        private AccountPosition accountPosition;

        private object lockObj = new object();

        public double Money
        {
            get
            {
                return accountPosition.Money;
            }
        }

        public AccountTodayOrder AccountOrder
        {
            get
            {
                return accountOrder;
            }            
        }

        public AccountPosition AccountPosition
        {
            get
            {
                return accountPosition;
            }
        }

        public AccountTrade(double money)
        {
            this.accountOrder = new AccountTodayOrder();
            this.accountPosition = new AccountPosition(money);
        }

        public AccountTrade(double money, AccountFee accountFee)
        {
            this.accountOrder = new AccountTodayOrder();
            this.accountPosition = new AccountPosition(money, accountFee);
        }

        public void SendOrder(OrderInfo orderInfo)
        {
            /*
           * 执行顺序：
           * 1.首先检查order能不能执行
           * 2.执行委托
           */
            lock (lockObj)
            {
                this.accountPosition.CheckCanSendOrder(orderInfo);
                this.accountOrder.SendOrder(orderInfo);
            }
        }

        public void ChangeDate(int date)
        {
            lock (lockObj)
            {

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="priceGetter"></param>
        public void ChangeTime(double time, IPriceGetter priceGetter)
        {
            lock (lockObj)
            {
                List<OrderInfo> orders = accountOrder.TodayOrderInfos;
                for (int i = 0; i < orders.Count; i++)
                {
                    OrderInfo order = orders[i];
                    string code = order.Instrumentid;
                    int mount = accountOrder.CalcOrder(order, priceGetter.GetBuyPrice(code), priceGetter.GetBuyMount(code));
                    if (mount > 0)
                        accountPosition.SendPosition(order, mount);
                }
            }
        }

        /// <summary>
        /// 取消订单
        /// </summary>
        /// <param name="orderId"></param>
        public void CancelOrder(string orderId)
        {
            lock (lockObj)
            {
                this.accountOrder.CancelOrder(orderId);
            }
        }

        public void Save(XmlElement xmlElem)
        {
            XmlElement xmlElemPosition = xmlElem.OwnerDocument.CreateElement("positions");
            accountPosition.Save(xmlElemPosition);
            xmlElem.AppendChild(xmlElemPosition);
            XmlElement xmlElemOrder = xmlElem.OwnerDocument.CreateElement("orders");
            accountOrder.Save(xmlElemOrder);
            xmlElem.AppendChild(xmlElemOrder);
        }

        public void Load(XmlElement xmlElem)
        {
            this.accountPosition.Load((XmlElement)xmlElem.GetElementsByTagName("positions")[0]);
            this.accountOrder.Load((XmlElement)xmlElem.GetElementsByTagName("orders")[0]);
        }

        public override string ToString()
        {
            return XmlUtils.ToString(this);
        }
    }
}