using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using com.wer.sc.utils;
using com.wer.sc.data.account;
using System.Xml;

namespace com.wer.sc.data.store.file
{
    public class AccountStore_File : IAccountStore
    {
        private IDataCenter dataCenter;

        private DataPathUtils pathUtils;

        public AccountStore_File(IDataCenter dataCenter, DataPathUtils path)
        {
            this.dataCenter = dataCenter;
            this.pathUtils = path;
        }

        public IAccount Load(string accountID)
        {
            string path = pathUtils.GetAccountPath(accountID);
            if (!File.Exists(path))
                return null;
            XmlDocument doc = new XmlDocument();
            doc.Load(path);
            Account account = new Account(dataCenter);
            account.Load(doc.DocumentElement);
            return account;
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
    }
}
