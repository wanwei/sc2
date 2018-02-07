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
    public class TestAccountImmediately
    {
        [TestMethod]
        public void TestTrade_Immediately()
        {
            string code = "RB1710";         
            IAccount account = DataCenter.Default.AccountManager.CreateAccount(100000);
            account.AccountSetting.TradeType = AccountTradeType.IMMEDIATELY;
            account.Open(code, 3099, market.OrderSide.Buy, 10);
            account.Open(code, 3095, market.OrderSide.Buy, 10);
            account.Close(code, 3102, market.OrderSide.Sell, 10);
            account.Close(code, 3102, market.OrderSide.Sell, 10);

            Assert.AreEqual(100880, account.Money);
        }

        [TestMethod]
        public void TestTrade_Immediately2()
        {
            string code = "RB1710";
            IAccount account = DataCenter.Default.AccountManager.CreateAccount(100000);
            account.Open(code, 3099, market.OrderSide.Buy, 10);
            account.Close(code, 3102, market.OrderSide.Sell, 10);
            account.Open(code, 3095, market.OrderSide.Buy, 10);            
            account.Close(code, 3102, market.OrderSide.Sell, 10);                        
            Console.WriteLine(account);
            Assert.AreEqual(100880, account.Money);
        }
    }
}
