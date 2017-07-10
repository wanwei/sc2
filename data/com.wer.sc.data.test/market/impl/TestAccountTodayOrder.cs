using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.market.impl
{
    [TestClass]
    public class TestAccountTodayOrder
    {
        [TestMethod]
        public void TestAccountTodayOrder_SendOrder()
        {
            AccountTodayOrder todayOrder = new AccountTodayOrder();

            string code = "m1705";
            OrderInfo order = new OrderInfo(code, 20170601.093101, OpenCloseType.Open, 3110, 15, OrderSide.Buy);
            OrderInfo order2 = new OrderInfo(code, 20170601.093201, OpenCloseType.Open, 3120, 15, OrderSide.Buy);

            todayOrder.SendOrder(order);
            todayOrder.SendOrder(order2);

            int tradeMount = todayOrder.CalcOrder(order, 3115, 10);
            Assert.AreEqual(0, tradeMount);

            tradeMount = todayOrder.CalcOrder(order2, 3115, 10);
            Assert.AreEqual(10, tradeMount);

            todayOrder.CancelOrder(order.OrderID);
            Console.WriteLine(todayOrder);
        }
    }
}
