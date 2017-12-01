using com.wer.sc.data.forward;
using com.wer.sc.utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace com.wer.sc.data.account
{
    [TestClass]
    public class TestAccountSaveLoad
    {
        private XmlElement GetXmlRoot()
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement("root");
            doc.AppendChild(root);
            return root;
        }

        private bool isOpen = false;

        [TestMethod]
        public void TestAccountSave()
        {
            string code = "RB1710";
            int startDate = 20170601;
            int endDate = 20170605;
            IDataForward_Code historyDataForward = ForwardDataGetter.GetHistoryDataForward_Code(code, startDate, endDate, true);
            historyDataForward.Forward();
            IAccount account = DataCenter.Default.AccountFactory.CreateAccount(100000, historyDataForward);
            account.AccountSetting.TradeType = AccountTradeType.MARKETPRICE;
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

            account.Open(code, 3105, market.OrderSide.Buy, 10);
            account.Open(code, 3025, market.OrderSide.Buy, 10);

            for (int i = 0; i < 100; i++)
                historyDataForward.Forward();

            XmlElement root = GetXmlRoot();
            account.Save(root);

            Account account2 = (Account)DataCenter.Default.AccountFactory.CreateAccount(root);
            //Console.WriteLine(account);
            //Console.WriteLine(account2);
            Assert.AreEqual(XmlUtils.ToString(account), XmlUtils.ToString(account2));

            IDataForward_Code historyDataForward2 = account2.DataForward_Code;
            for (int i = 0; i < 100; i++)
                historyDataForward2.Forward();
            Console.WriteLine(account2);      
        }

        private void Account_OnReturnOrder(object sender, ref market.OrderInfo order)
        {
            //Console.WriteLine(order);
        }

        private void Account_OnReturnTrade(object sender, ref market.TradeInfo trade)
        {
            //Console.WriteLine(trade);
            //Console.WriteLine(((IAccount)sender).Asset);
            if (trade.OpenClose == market.OpenCloseType.Open)
                isOpen = true;
            else
                isOpen = false;
        }
    }
}
