using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.account
{
    [TestClass]
    public class TestAccount_Slip
    {
        [TestMethod]
        public void TestTrade_SlipPrice()
        {
            string code = "RB1710";
            IAccount account = DataCenter.Default.AccountFactory.CreateAccount(100000);
            account.AccountSetting.TradeType = AccountTradeType.IMMEDIATELY;
            account.AccountSetting.SlipPrice = 1;
            account.Open(code, 3099, market.OrderSide.Buy, 10);
            account.Open(code, 3095, market.OrderSide.Buy, 10);
            account.Close(code, 3102, market.OrderSide.Sell, 10);
            account.Close(code, 3102, market.OrderSide.Sell, 10);

            //Console.WriteLine(account);
            //Console.WriteLine(account.Money);
            //Assert.AreEqual(100880, account.Money);
            //Assert.AreEqual(100510, account.Money);
            Assert.AreEqual(100680, account.Money);
        }
    }
}
