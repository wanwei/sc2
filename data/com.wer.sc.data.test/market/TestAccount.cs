using com.wer.sc.data.account;
using com.wer.sc.data.forward;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.market
{
    [TestClass]
    public class TestAccount
    {
        private bool isBuy = false;

        [TestMethod]
        public void TestAccountSendOrder()
        {
            string code = "rb1710";
            int startDate = 20170601;
            int endDate = 20170601;
            IHistoryDataForward_Code historyDataForward_Code = CommonData.GetHistoryDataForward_Code(code, startDate, endDate, true);
            //IAccount account = AccountFactory.CreateAccount("test", 100000, historyDataForward_Code);
            //try
            //{
            //    while (historyDataForward_Code.Forward())
            //    {
            //        if (!isBuy)
            //        {
            //            if (historyDataForward_Code.GetTickData().Price > 3115)
            //            {
            //                OrderInfo order = new OrderInfo(code, historyDataForward_Code.GetTickData().Time, OpenCloseType.Open, 3115, 10, OrderSide.Buy);
            //                account.SendOrder(order);
            //                isBuy = true;
            //            }
            //        }
            //        else
            //        {
            //            if (historyDataForward_Code.GetTickData().Price > 3120)
            //            {
            //                OrderInfo order = new OrderInfo(code, historyDataForward_Code.GetTickData().Time, OpenCloseType.Close, 3120, 10, OrderSide.Buy);
            //                account.SendOrder(order);
            //                isBuy = false;
            //            }
            //        }
            //    }
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e.Message);
            //}

            //Console.WriteLine(account);
        }
    }
}
