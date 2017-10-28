using com.wer.sc.data.forward;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.account
{
    [TestClass]
    public class TestAccount
    {
        private bool isOpen = false;

        [TestMethod]
        public void TestTrade_Tick()
        {
            string code = "RB1710";
            int startDate = 20170601;
            int endDate = 20170605;
            IDataForward_Code historyDataForward = ForwardDataGetter.GetHistoryDataForward_Code(code, startDate, endDate, true);
            Account account = new Account(100000, historyDataForward);
            account.OnReturnOrder += Account_OnReturnOrder;
            account.OnReturnTrade += Account_OnReturnTrade;
            account.Open(code, 3099, market.OrderSide.Buy, 10);
            account.Open(code, 3095, market.OrderSide.Buy, 10);
            int index = 0;

            while (index < 2000)
            {
                historyDataForward.Forward();
                index++;
                if (isOpen && historyDataForward.GetTickData().SellPrice >= 3102)
                {
                    account.Close(code, 3102, market.OrderSide.Sell, 10);
                    account.Close(code, 3105, market.OrderSide.Sell, 10);
                }
                //Console.WriteLine(historyDataForward.GetTickData());
            }
            Console.WriteLine(account);
            Assert.AreEqual(100880, account.Money);
        }

        private void Account_OnReturnOrder(object sender, ref market.OrderInfo order)
        {
            //Console.WriteLine(order);
        }

        private void Account_OnReturnTrade(object sender, ref market.TradeInfo trade)
        {
            Console.WriteLine(trade);
            if (trade.OpenClose == market.OpenCloseType.Open)
                isOpen = true;
            else
                isOpen = false;
        }

        [TestMethod]
        public void TestTrade_Minute()
        {
            string code = "RB1710";
            int startDate = 20170601;
            int endDate = 20170605;
            IDataForward_Code historyDataForward = ForwardDataGetter.GetHistoryDataForward_Code(code, startDate, endDate, false);
            Account account = new Account(100000, historyDataForward);
            account.OnReturnOrder += Account_OnReturnOrder;
            account.OnReturnTrade += Account_OnReturnTrade;
            account.Open(code, 3099, market.OrderSide.Buy, 10);
            account.Open(code, 3095, market.OrderSide.Buy, 10);
            int index = 0;

            while (index < 200)
            {
                historyDataForward.Forward();
                index++;
                if (isOpen && historyDataForward.Price >= 3102)
                {
                    account.Close(code, 3102, market.OrderSide.Sell, 10);
                    account.Close(code, 3105, market.OrderSide.Sell, 10);
                }
                //Console.WriteLine(historyDataForward.GetTickData());
            }
            Console.WriteLine(account);
            //Assert.AreEqual(100880, account.Money);
        }
    }
}
