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
    public class TestAccount_DelayTick
    {
        private IAccount account;

        [TestMethod]
        public void TestTrade_TickDelay()
        {
            string code = "RB1710";
            int startDate = 20170601;
            int endDate = 20170605;
            IDataForward_Code historyDataForward = ForwardDataGetter.GetHistoryDataForward_Code(code, startDate, endDate, true);
            historyDataForward.OnTick += HistoryDataForward_OnTick;
            account = DataCenter.Default.AccountFactory.CreateAccount(100000, historyDataForward);
            account.AccountSetting.TradeType = AccountTradeType.DELAYTICK;
            account.AccountSetting.AutoFilter = true;
            account.AccountSetting.DelayTick = 2;
            int index = 0;
            while (index < 100)
            {
                historyDataForward.Forward();
                index++;
            }
            Console.WriteLine(account);
            Assert.AreEqual(53395, account.Money);
            Assert.AreEqual(0, account.CurrentOrderInfo.Count);
        }

        [TestMethod]
        public void TestTrade_TickDelay2()
        {
            string code = "RB1710";
            int startDate = 20170601;
            int endDate = 20170605;
            IDataForward_Code historyDataForward = ForwardDataGetter.GetHistoryDataForward_Code(code, startDate, endDate, true);
            historyDataForward.OnTick += HistoryDataForward_OnTick;
            account = DataCenter.Default.AccountFactory.CreateAccount(100000, historyDataForward);
            account.AccountSetting.TradeType = AccountTradeType.DELAYTICK;
            account.AccountSetting.AutoFilter = true;
            account.AccountSetting.DelayTick = 4;
            int index = 0;
            while (index < 100)
            {
                historyDataForward.Forward();
                index++;
            }
            Console.WriteLine(account);
            Assert.AreEqual(53395, account.Money);
            Assert.AreEqual(1, account.CurrentOrderInfo.Count);
        }

        private void HistoryDataForward_OnTick(object sender, IForwardOnTickArgument argument)
        {
            if (argument.TickInfo.TickBar.Price >= 3105) 
                account.Open(argument.TickInfo.TickBar.Code, 3105, market.OrderSide.Sell, 10);
        }
    }
}
