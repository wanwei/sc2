using com.wer.sc.data.market;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.account
{
    public class AccountOrders
    {
        private Account account;

        //所有的委托
        private List<OrderInfo> historyOrders = new List<OrderInfo>();

        //还没执行完成的委托
        private List<OrderInfo> waitingOrders = new List<OrderInfo>();

        private Dictionary<string, double> dic_OrderID_LockMoney = new Dictionary<string, double>();

        public AccountOrders(Account account)
        {
            this.account = account;
        }

        /// <summary>
        /// 开仓委托
        /// </summary>
        /// <param name="code"></param>
        /// <param name="price"></param>
        /// <param name="orderSide"></param>
        /// <param name="mount"></param>
        /// <returns></returns>
        public OrderInfo Open(string code, double price, OrderSide orderSide, int mount)
        {
            if (mount <= 0)
                return null;
            double buymoney = CalcTradeMoney(code, price, mount, OpenCloseType.Open);
            if (buymoney > account.Money)
                return null;
            OrderInfo orderInfo = new OrderInfo(code, this.account.Time, OpenCloseType.Open, price, mount, orderSide, OrderType.Market);
            orderInfo.OrderID = Guid.NewGuid().ToString();
            this.waitingOrders.Add(orderInfo);
            this.historyOrders.Add(orderInfo);
            this.account.money -= buymoney;
            this.dic_OrderID_LockMoney.Add(orderInfo.OrderID, buymoney);
            return orderInfo;
        }

        private double CalcTradeMoney(String code, double price, int hand, OpenCloseType openCloseType)
        {
            TradeFee_Code tradeFee = account.GetTradeFee_Code(code);
            //交易费用
            double handFee = tradeFee.BuyFee;
            return hand * (price * tradeFee.HandCount * (tradeFee.DepositPercent / 100) + handFee);
        }

        //计算该价格下最大可买数量
        private int GetMaxCanBuy(String code, double price, double percent)
        {
            TradeFee_Code tradeFee = account.GetTradeFee_Code(code);
            return (int)(this.account.Money * percent / 100 / (price * tradeFee.HandCount * (tradeFee.DepositPercent / 100) + tradeFee.BuyFee));
        }

        /// <summary>
        /// 平仓委托
        /// </summary>
        /// <param name="code"></param>
        /// <param name="price"></param>
        /// <param name="orderSide"></param>
        /// <param name="mount"></param>
        /// <returns></returns>
        public OrderInfo Close(string code, double price, OrderSide orderSide, int mount)
        {
            if (mount <= 0)
                return null;
            //int position = account.AccountPosition.GetPosition(code, orderSide);
            //if (position < mount)
            //    return null;
            //OrderInfo orderInfo = new OrderInfo(code, this.account.Time, OpenCloseType.Open, price, mount, orderSide, OrderType.Market);
            //orderInfo.OrderID = Guid.NewGuid().ToString();
            //this.waitingOrders.Add(orderInfo);
            //return orderInfo;
            return null;
        }

        public TradeInfo DoTrade(OrderInfo order, double price, int mount)
        {
            return null;
        }

        public void DoTradingDayChange()
        {

        }

        /// <summary>
        /// 取消委托
        /// </summary>
        /// <param name="orderId"></param>
        public void CancelOrder(string orderId)
        {
            if (this.dic_OrderID_LockMoney.ContainsKey(orderId))
            {

            }
        }
    }
}