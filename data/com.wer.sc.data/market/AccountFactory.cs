using com.wer.sc.data.forward;
using com.wer.sc.data.market.impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.market
{
    public class AccountFactory
    {
        /// <summary>
        /// 创建一个账号
        /// </summary>
        /// <param name="money"></param>
        /// <param name="historyDataForward_Code"></param>
        /// <returns></returns>
        public static IAccount CreateAccount(string accountId, double money, IHistoryDataForward_Code historyDataForward_Code)
        {
            return new Account(accountId, money, historyDataForward_Code);
        }

        public static IAccount LoadAccount(string content)
        {
            return null;
        }
    }
}
