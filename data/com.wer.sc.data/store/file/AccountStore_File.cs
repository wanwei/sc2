using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using com.wer.sc.utils;
using com.wer.sc.data.market;
using com.wer.sc.data.market.impl;

namespace com.wer.sc.data.store.file
{
    public class AccountStore_File : IAccountStore
    {
        private DataPathUtils pathUtils;

        private AccountFeeInfoStore_File feeInfoStore;

        public AccountStore_File(DataPathUtils path)
        {
            this.pathUtils = path;
            this.feeInfoStore = new AccountFeeInfoStore_File(path.GetAccountPath_Fee());
        }

        public Account Load(string accountID)
        {
            string path = pathUtils.GetAccountPath(accountID);
            if (!File.Exists(path))
                return null;
            string content = File.ReadAllText(path);
            //return Account.LoadAccount(content);
            return null;
        }

        public void Save(string accountID, Account account)
        {
            string path = pathUtils.GetAccountPath(accountID);
            if (File.Exists(path))
                return;
            File.WriteAllText(path, XmlUtils.ToString(account));
        }

        public void Save(string accountID, Account account, bool overwrite)
        {
            string path = pathUtils.GetAccountPath(accountID);
            if (overwrite || !File.Exists(path))
                File.WriteAllText(path, XmlUtils.ToString(account));
        }

        public List<string> LoadAllAccountId()
        {
            string path = pathUtils.GetAccountPath();
            DirectoryInfo dir = new DirectoryInfo(path);
            FileInfo[] files = dir.GetFiles(".account");
            List<string> accountIds = new List<string>();
            for (int i = 0; i < files.Length; i++)
            {
                FileInfo file = files[i];
                string accountId = file.Name.Substring(0, file.Name.IndexOf('.'));
                accountIds.Add(accountId);
            }
            return accountIds;
        }

        /// <summary>
        /// 装载所有的交易费用数据
        /// </summary>
        /// <returns></returns>
        public List<AccountFeeInfo> LoadAllAccountFee()
        {
            return feeInfoStore.LoadAllAccountFee();
        }


        public void SaveAccountFee(List<AccountFeeInfo> accountFeeInfo)
        {
            feeInfoStore.SaveAccountFee(accountFeeInfo);
        }
    }
}
