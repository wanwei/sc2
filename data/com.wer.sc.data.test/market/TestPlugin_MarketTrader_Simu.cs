using com.wer.sc.data.account;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.market
{
    [TestClass]
    public class TestPlugin_MarketTrader_Simu
    {
        public void Test()
        {
            IAccountFactory fac = DataCenter.Default.AccountFactory;
            IAccount account = fac.CreateAccount(100000);
            account.AccountSetting.TradeType = AccountTradeType.IMMEDIATELY;
            Plugin_MarketTrader_Simu trader = new Plugin_MarketTrader_Simu(account);
            
            OrderInfo order = new OrderInfo();
            order.Instrumentid = "rb1805";
            order.OpenClose = OpenCloseType.Open;
            order.Volume = 20;
            order.Price = 2880;
            trader.SendOrder(order);
        }
    }
}
