using com.wer.sc.data.market;
using com.wer.sc.data.market.impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.store
{
    public interface IAccountStore
    {
        /// <summary>
        /// 保存账号信息
        /// </summary>
        /// <param name="accountID"></param>
        /// <param name="account"></param>
        void Save(string accountID, Account account);

        /// <summary>
        /// 保存账号信息
        /// </summary>
        /// <param name="accountID"></param>
        /// <param name="account"></param>
        /// <param name="overwrite"></param>
        void Save(string accountID, Account account, bool overwrite);

        /// <summary>
        /// 装载K线数据
        /// </summary>
        /// <param name="accountID"></param>
        /// <returns></returns>
        Account Load(string accountID);

        /// <summary>
        /// 装载所有账号id
        /// </summary>
        /// <returns></returns>
        List<string> LoadAllAccountId();

        /// <summary>
        /// 装载所有的交易费用数据
        /// </summary>
        /// <returns></returns>
        List<AccountFeeInfo> LoadAllAccountFee();

        /// <summary>
        /// 装载所有的交易费用数据
        /// </summary>
        /// <returns></returns>
        void SaveAccountFee(List<AccountFeeInfo> accountFeeInfo);
    }
}