using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.data.reader;
using com.wer.sc.data.forward;
using System.Xml;

namespace com.wer.sc.data.account
{
    public class AccountFactory : IAccountFactory
    {
        private IDataCenter dataCenter;

        public AccountFactory(IDataCenter dataCenter)
        {
            this.dataCenter = dataCenter;
        }

        public IAccount CreateAccount(double money)
        {
            return new Account(money);
        }

        public IAccount CreateAccount(double money, IDataForward_Code realTimeDataReader)
        {
            return new Account(money, realTimeDataReader);
        }

        public IAccount CreateAccount(double money, IDataForward_Code realTimeDataReader, TradeFee fee)
        {
            return new Account(money, realTimeDataReader, fee);
        }

        public IAccount CreateAccount(XmlElement xmlElem)
        {
            Account account = new Account(dataCenter);
            account.Load(xmlElem);
            return account;
        }
    }
}
