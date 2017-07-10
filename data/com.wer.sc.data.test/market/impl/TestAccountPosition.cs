using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.market.impl
{
    [TestClass]
    public class TestAccountPosition
    {
        [TestMethod]
        public void TestSendPosition()
        {
            AccountPosition accountPosition = new AccountPosition(100000);
            Assert.AreEqual(100000, accountPosition.Money);
            Trade(accountPosition);
            Assert.AreEqual(100000, accountPosition.Money);

            List<AccountFeeInfo> feeInfo = new List<AccountFeeInfo>();
            feeInfo.Add(new AccountFeeInfo("m1705", 10, 10, 3, CommissionChargeCalcType.Fix));
            AccountFee fee = new AccountFee(feeInfo);
            accountPosition = new AccountPosition(100000, fee);
            Trade(accountPosition);
            Assert.AreEqual(99910, accountPosition.Money);
        }

        private static void Trade(AccountPosition accountPosition)
        {
            string code = "m1705";
            OrderInfo order = new OrderInfo(code, 20170601.093101, OpenCloseType.Open, 3110, 15, OrderSide.Buy);
            OrderInfo order2 = new OrderInfo(code, 20170601.093201, OpenCloseType.Open, 3120, 15, OrderSide.Buy);

            accountPosition.SendPosition(order, 5);
            accountPosition.SendPosition(order2, 10);
            Console.WriteLine(accountPosition);

            OrderInfo order_Sell = new OrderInfo(code, 20170601.093101, OpenCloseType.Close, 3110, 5, OrderSide.Buy);
            OrderInfo order2_Sell = new OrderInfo(code, 20170601.093201, OpenCloseType.Close, 3120, 10, OrderSide.Buy);
            accountPosition.SendPosition(order_Sell, 5);
            accountPosition.SendPosition(order2_Sell, 10);
            Console.WriteLine(accountPosition);
        }

        [TestMethod]
        public void TestCheckPosition()
        {

        }
    }
}
