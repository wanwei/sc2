using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace com.wer.sc.data.market.impl
{
    /// <summary>
    /// 账号的委托执行类
    /// </summary>
    public class AccountTodayOrder : IXmlExchange
    {
        private double available;

        //今日委托
        private List<OrderInfo> todayOrderInfos = new List<OrderInfo>();

        private Dictionary<string, OrderInfo> dic_Code_Orders = new Dictionary<string, OrderInfo>();

        //public AccountTodayOrder(double initMoney)
        //{
        //    this.available = initMoney;
        //}

        /// <summary>
        /// 得到今日的所有委托
        /// </summary>
        public List<OrderInfo> TodayOrderInfos
        {
            get
            {
                return todayOrderInfos;
            }
        }

        /// <summary>
        /// 增加新委托
        /// </summary>
        /// <param name="orderInfo"></param>
        public void SendOrder(OrderInfo orderInfo)
        {
            string id = Guid.NewGuid().ToString();
            orderInfo.OrderID = id;
            AddOrderInfo(orderInfo);
        }

        private void AddOrderInfo(OrderInfo orderInfo)
        {
            this.TodayOrderInfos.Add(orderInfo);
            this.dic_Code_Orders.Add(orderInfo.OrderID, orderInfo);
        }

        /// <summary>
        /// 计算在当前价格下成交量
        /// </summary>
        /// <param name="orderInfo"></param>
        /// <param name="price"></param>
        /// <param name="currentMount"></param>
        /// <returns></returns>
        public int CalcOrder(OrderInfo orderInfo, double price, int currentMount)
        {
            if (orderInfo.OpenClose == OpenCloseType.Open)
            {
                return DoTrade_Open(orderInfo, price, currentMount);
            }
            else
            {
                return DoTrade_Close(orderInfo, price, currentMount);
            }
        }

        private int DoTrade_Open(OrderInfo orderInfo, double price, int currentMount)
        {
            int tradeMount = 0;
            if (orderInfo.Direction == OrderSide.Buy)
            {
                if (orderInfo.Price >= price)
                {
                    int qty = orderInfo.LeavesQty;
                    tradeMount = qty <= currentMount ? qty : currentMount;
                    orderInfo.ExecType = ExecType.Trade;
                    orderInfo.CumQty += tradeMount;
                    //orderInfo.LeavesQty -= tradeMount;
                }
            }
            else
            {
                if (orderInfo.Price <= price)
                {
                    int qty = orderInfo.LeavesQty;
                    tradeMount = qty <= currentMount ? qty : currentMount;
                    orderInfo.ExecType = ExecType.Trade;
                    orderInfo.CumQty += tradeMount;
                    //orderInfo.LeavesQty -= tradeMount;
                }
            }
            return tradeMount;
        }

        private int DoTrade_Close(OrderInfo orderInfo, double price, int mount)
        {
            int tradeMount = 0;
            if (orderInfo.Direction == OrderSide.Buy)
            {
                if (orderInfo.Price >= price)
                {
                    int qty = orderInfo.LeavesQty;
                    tradeMount = qty <= mount ? qty : mount;
                    orderInfo.ExecType = ExecType.Trade;
                    orderInfo.CumQty += tradeMount;
                    //orderInfo.LeavesQty -= tradeMount;
                }
            }
            else
            {
                if (orderInfo.Price <= price)
                {
                    int qty = orderInfo.LeavesQty;
                    tradeMount = qty <= mount ? qty : mount;
                    orderInfo.ExecType = ExecType.Trade;
                    orderInfo.CumQty += tradeMount;
                    //orderInfo.LeavesQty -= tradeMount;
                }
            }
            return tradeMount;
        }

        /// <summary>
        /// 取消委托
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public int CancelOrder(string orderId)
        {
            OrderInfo value;
            bool hasOrder = dic_Code_Orders.TryGetValue(orderId, out value);
            if (hasOrder)
            {
                int tradeMount = value.LeavesQty;
                value.LeavesQty = 0;
                value.ExecType = ExecType.Cancelled;
                return tradeMount;
            }
            else
                throw new ApplicationException("委托" + orderId + "已经不存在");
        }

        public void Save(XmlElement xmlElem)
        {
            for (int i = 0; i < todayOrderInfos.Count; i++)
            {
                OrderInfo orderInfo = todayOrderInfos[i];
                XmlElement elemOrder = xmlElem.OwnerDocument.CreateElement("order");
                SaveOrder(orderInfo, elemOrder);
                xmlElem.AppendChild(elemOrder);
            }
        }

        private void SaveOrder(OrderInfo order, XmlElement xmlElem)
        {
            xmlElem.SetAttribute("id", order.OrderID);
            xmlElem.SetAttribute("time", order.OrderTime.ToString());
            xmlElem.SetAttribute("code", order.Instrumentid);
            xmlElem.SetAttribute("direction", order.Direction.ToString());
            xmlElem.SetAttribute("open", order.OpenClose.ToString());
            xmlElem.SetAttribute("ordertype", order.Type.ToString());
            xmlElem.SetAttribute("exectype", order.ExecType.ToString());
            xmlElem.SetAttribute("price", order.Price.ToString());
            xmlElem.SetAttribute("volume", order.Volume.ToString());
            xmlElem.SetAttribute("cumqty", order.CumQty.ToString());
            xmlElem.SetAttribute("leavesqty", order.LeavesQty.ToString());
        }

        public void Load(XmlElement xmlElem)
        {
            XmlNodeList nodeList = xmlElem.ChildNodes;
            for (int i = 0; i < nodeList.Count; i++)
            {
                XmlNode node = nodeList[i];
                if (node is XmlElement)
                {
                    OrderInfo orderInfo = LoadOrder((XmlElement)node);
                    this.AddOrderInfo(orderInfo);
                }
            }
        }

        public OrderInfo LoadOrder(XmlElement xmlElem)
        {
            OrderInfo order = new OrderInfo();
            order.OrderID = xmlElem.GetAttribute("id");
            order.OrderTime = double.Parse(xmlElem.GetAttribute("time"));
            order.Instrumentid = xmlElem.GetAttribute("code");
            order.Type = (OrderType)Enum.Parse(typeof(OrderType), xmlElem.GetAttribute("ordertype"));
            order.Direction = (OrderSide)Enum.Parse(typeof(OrderSide), xmlElem.GetAttribute("direction"));
            order.OpenClose = (OpenCloseType)Enum.Parse(typeof(OpenCloseType), xmlElem.GetAttribute("open"));
            order.ExecType = (ExecType)Enum.Parse(typeof(ExecType), xmlElem.GetAttribute("exectype"));
            order.Price = double.Parse(xmlElem.GetAttribute("price"));
            order.Volume = int.Parse(xmlElem.GetAttribute("volume"));
            order.CumQty = int.Parse(xmlElem.GetAttribute("cumqty"));
            order.LeavesQty = int.Parse(xmlElem.GetAttribute("leavesqty"));
            return order;
        }

        public override string ToString()
        {
            return XmlUtils.ToString(this);
        }
    }
}