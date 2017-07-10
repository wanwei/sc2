using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.market.impl
{
    [TestClass]
    public class TestAccountTrade
    {
        [TestMethod]
        public void TestAccountTradeSendOrder()
        {
            List<AccountFeeInfo> feeInfo = new List<AccountFeeInfo>();
            feeInfo.Add(new AccountFeeInfo("m1705", 10, 10, 3, CommissionChargeCalcType.Fix));
            AccountFee fee = new AccountFee(feeInfo);
            AccountTrade accountTrade = new AccountTrade(100000, fee);

            MockPriceGetter priceGetter = new MockPriceGetter();
            string code = "m1705";
            OrderInfo order = new OrderInfo(code, 20170601.093101, OpenCloseType.Open, 3110, 15, OrderSide.Buy);
            OrderInfo order2 = new OrderInfo(code, 20170601.093201, OpenCloseType.Open, 3120, 15, OrderSide.Buy);

            priceGetter.ChangePattern(0);
            accountTrade.SendOrder(order);
            accountTrade.SendOrder(order2);
            accountTrade.ChangeTime(20170601.093300, priceGetter);
            //accountTrade.ChangeTime(20170103.093001,)
            Console.WriteLine(accountTrade);
            
            priceGetter.ChangePattern(1);
            OrderInfo order_Sell = new OrderInfo(code, 20170601.093501, OpenCloseType.Close, 3110, 5, OrderSide.Buy);
            OrderInfo order2_Sell = new OrderInfo(code, 20170601.093505, OpenCloseType.Close, 3120, 10, OrderSide.Buy);
            accountTrade.SendOrder(order_Sell);
            accountTrade.SendOrder(order2_Sell);
            accountTrade.ChangeTime(20170601.093300, priceGetter);

            Assert.AreEqual(25, accountTrade.AccountPosition.GetAllPositions()[0].Position);
            Console.WriteLine(accountTrade);
        }

        class MockPriceGetter : IPriceGetter
        {
            private int type = 0;

            public MockPriceGetter()
            {

            }

            public double Time
            {
                get
                {
                    return 0;
                }
            }

            public event TimeChange timeChange;

            public void ChangePattern(int type)
            {
                this.type = type;
            }

            public int GetBuyMount(string code)
            {
                switch (type)
                {
                    case 0:
                        return 15;
                    case 1:
                        return 5;
                }
                return 1;
            }

            public double GetBuyPrice(string code)
            {
                switch (type)
                {
                    case 0:
                        return 3110;
                    case 1:
                        return 3120;
                }
                return 1;
            }

            public int GetSellMount(string code)
            {
                switch (type)
                {
                    case 0:
                        return 15;
                    case 1:
                        return 5;
                }
                return 1;
            }

            public double GetSellPrice(string code)
            {
                switch (type)
                {
                    case 0:
                        return 3110;
                    case 1:
                        return 3120;
                }
                return 1;
            }
        }
    }
}
