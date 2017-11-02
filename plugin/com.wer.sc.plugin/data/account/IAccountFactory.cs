using com.wer.sc.data.forward;
using com.wer.sc.data.reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace com.wer.sc.data.account
{
    /// <summary>
    /// 账号工厂
    /// </summary>
    public interface IAccountFactory
    {
        IAccount CreateAccount(double money);

        IAccount CreateAccount(double money, IDataForward_Code realTimeDataReader);

        IAccount CreateAccount(double money, IDataForward_Code realTimeDataReader, TradeFee fee);

        IAccount CreateAccount(XmlElement xmlElem);
    }
}
