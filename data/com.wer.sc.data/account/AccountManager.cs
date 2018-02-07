using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.data.reader;
using com.wer.sc.data.forward;
using System.Xml;
using com.wer.sc.data.store;

namespace com.wer.sc.data.account
{
    public class AccountManager : IAccountManager
    {
        private IDataCenter dataCenter;

        private IAccountStore accountStore;

        public AccountManager(IDataCenter dataCenter, IAccountStore accountStore)
        {
            this.dataCenter = dataCenter;
            this.accountStore = accountStore;
        }

        public IAccount CreateAccount(double money)
        {
            return new Account(money);
        }

        public IAccount CreateAccount(double money, IRealTimeDataReader realTimeDataReader)
        {
            return new Account(money, realTimeDataReader);
        }

        public IAccount CreateAccount(double money, IRealTimeDataReader realTimeDataReader, ITradeFee fee)
        {
            return new Account(money, realTimeDataReader, fee);
        }

        public IAccount CreateAccount(XmlElement xmlElem)
        {
            Account account = new Account();
            account.Load(xmlElem);
            return account;
        }

        /// <summary>
        /// 保存账号信息，默认会覆盖掉本地的账号信息
        /// </summary>
        /// <param name="path"></param>
        /// <param name="accountName"></param>
        /// <param name="account"></param>
        public void Save(string path, string accountName, IAccount account)
        {
            accountStore.Save(path, accountName, (Account)account);
        }

        /// <summary>
        /// 保存账号信息
        /// </summary>
        /// <param name="path"></param>
        /// <param name="accountName"></param>
        /// <param name="account"></param>
        /// <param name="overwrite"></param>
        public void Save(string path, string accountName, IAccount account, bool overwrite)
        {
            accountStore.Save(path, accountName, (Account)account, overwrite);
        }

        public bool NewPath(string path)
        {
            return accountStore.NewAccountPath(path);
        }

        public bool Exist(string path, string accountName)
        {
            return accountStore.Exist(path, accountName);
        }

        /// <summary>
        /// 装载K线数据
        /// </summary>
        /// <param name="accountName"></param>
        /// <returns></returns>
        public IAccount Load(string path, string accountName)
        {
            return accountStore.Load(path, accountName);
        }

        /// <summary>
        /// 装载所有子目录
        /// </summary>
        /// <returns></returns>
        public IList<string> LoadSubPaths(string path)
        {
            return accountStore.LoadSubPaths(path);
        }

        /// <summary>
        /// 装载所有子账号名称
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public IList<string> LoadAccountNames(string path)
        {
            return this.accountStore.LoadAccountNames(path);
        }

        /// <summary>
        /// 删除账号名称
        /// </summary>
        /// <param name="accountName"></param>
        public void DeleteAccount(string path, string accountName)
        {
            accountStore.DeleteAccount(path, accountName);
        }

        /// <summary>
        /// 删除账号路径，该路径下所有账号都会被删除
        /// </summary>
        /// <param name="path"></param>
        public void DeleteAccountPath(string path)
        {
            accountStore.DeleteAccountPath(path);
        }
    }
}
