using com.wer.sc.data.account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.store
{
    /// <summary>
    /// 账号信息存储类
    /// </summary>
    public interface IAccountStore
    {
        /// <summary>
        /// 保存账号信息，默认会覆盖掉本地的账号信息
        /// </summary>
        /// <param name="path"></param>
        /// <param name="accountName"></param>
        /// <param name="account"></param>
        void Save(string path, string accountName, Account account);

        /// <summary>
        /// 保存账号信息
        /// </summary>
        /// <param name="path"></param>
        /// <param name="accountName"></param>
        /// <param name="account"></param>
        /// <param name="overwrite"></param>
        void Save(string path, string accountName, Account account, bool overwrite);

        /// <summary>
        /// 账号是否存在
        /// </summary>
        /// <param name="path"></param>
        /// <param name="accountName"></param>
        /// <returns></returns>
        bool Exist(string path, string accountName);

        /// <summary>
        /// 装载K线数据
        /// </summary>
        /// <param name="accountName"></param>
        /// <returns></returns>
        IAccount Load(string path, string accountName);

        /// <summary>
        /// 装载所有子目录
        /// </summary>
        /// <returns></returns>
        IList<string> LoadSubPaths(string path);

        /// <summary>
        /// 装载所有子账号名称
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        IList<string> LoadAccountNames(string path);

        /// <summary>
        /// 删除账号名称
        /// </summary>
        /// <param name="accountName"></param>
        void DeleteAccount(string path, string accountName);

        /// <summary>
        /// 删除账号路径，该路径下所有账号都会被删除
        /// </summary>
        /// <param name="path"></param>
        void DeleteAccountPath(string path);

        /// <summary>
        /// 新建账号路径
        /// </summary>
        /// <param name="path"></param>
        bool NewAccountPath(string path);

        ///// <summary>
        ///// 装载所有的交易费用数据
        ///// </summary>
        ///// <returns></returns>
        //List<AccountFeeInfo> LoadAllAccountFee();

        ///// <summary>
        ///// 装载所有的交易费用数据
        ///// </summary>
        ///// <returns></returns>
        //void SaveAccountFee(List<AccountFeeInfo> accountFeeInfo);
    }
}