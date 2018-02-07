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

        public AccountStore_File(IDataCenter dataCenter, DataPathUtils pathUtils)
        {
            this.dataCenter = dataCenter;
            this.pathUtils = pathUtils;
        }

        public bool Exist(string path, string accountName)
        {
            string accountPath = pathUtils.GetAccountPath(path, accountName);
            return File.Exists(accountPath);
        }

        public IAccount Load(string path, string accountName)
        {
            string accountPath = pathUtils.GetAccountPath(path, accountName);
            if (!File.Exists(accountPath))
                return null;
            XmlDocument doc = new XmlDocument();
            doc.Load(accountPath);
            Account account = new Account();
            account.Load(doc.DocumentElement);
            return account;
        }

        public void Save(string path, string accountName, Account account)
        {
            string fullpath = pathUtils.GetAccountPath(path, accountName);
            if (File.Exists(fullpath))
                return;
            File.WriteAllText(fullpath, XmlUtils.ToString(account));
        }

        public void Save(string path, string accountName, Account account, bool overwrite)
        {
            string fullpath = pathUtils.GetAccountPath(path, accountName);
            if (overwrite || !File.Exists(fullpath))
                File.WriteAllText(fullpath, XmlUtils.ToString(account));
        }

        //public List<string> LoadAllAccountId()
        //{
        //    string path = pathUtils.GetAccountPath();
        //    DirectoryInfo dir = new DirectoryInfo(path);
        //    FileInfo[] files = dir.GetFiles(".account");
        //    List<string> accountIds = new List<string>();
        //    for (int i = 0; i < files.Length; i++)
        //    {
        //        FileInfo file = files[i];
        //        string accountId = file.Name.Substring(0, file.Name.IndexOf('.'));
        //        accountIds.Add(accountId);
        //    }
        //    return accountIds;
        //}

        /// <summary>
        /// 装载所有子目录
        /// </summary>
        /// <returns></returns>
        public IList<string> LoadSubPaths(string path)
        {
            string accountPath = pathUtils.GetAccountPath(path);
            if (!Directory.Exists(accountPath))
            {
                Directory.CreateDirectory(accountPath);
                return new string[] { };
            }
            string[] subPaths = Directory.GetDirectories(accountPath);
            for (int i = 0; i < subPaths.Length; i++)
            {
                string subPath = subPaths[i];
                subPaths[i] = path + subPath.Substring(accountPath.Length);
            }
            return subPaths;
        }

        /// <summary>
        /// 装载所有子账号名称
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public IList<string> LoadAccountNames(string path)
        {
            string accountPath = pathUtils.GetAccountPath(path);
            DirectoryInfo dir = new DirectoryInfo(accountPath);
            FileInfo[] files = dir.GetFiles("*.account");
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
        /// 删除账号名称
        /// </summary>
        /// <param name="accountName"></param>
        public void DeleteAccount(string path, string accountName)
        {
            string accountPath = pathUtils.GetAccountPath(path, accountName);
            File.Delete(accountPath);
        }

        /// <summary>
        /// 删除账号路径，该路径下所有账号都会被删除
        /// </summary>
        /// <param name="path"></param>
        public void DeleteAccountPath(string path)
        {
            string accountPath = pathUtils.GetAccountPath(path);
            Directory.Delete(accountPath, true);
        }

        public bool NewAccountPath(string path)
        {
            string accountPath = pathUtils.GetAccountPath(path);
            if (!Directory.Exists(accountPath))
            {
                Directory.CreateDirectory(accountPath);
                return true;
            }
            else
                return false;
        }
    }
}