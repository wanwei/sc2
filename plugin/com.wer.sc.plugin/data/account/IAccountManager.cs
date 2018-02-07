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
    public interface IAccountManager
    {
        IAccount CreateAccount(double money);

        IAccount CreateAccount(double money, IRealTimeDataReader realTimeDataReader);

        IAccount CreateAccount(double money, IRealTimeDataReader realTimeDataReader, ITradeFee fee);

        IAccount CreateAccount(XmlElement xmlElem);

        /// <summary>
        /// 保存账号信息，默认会覆盖掉本地的账号信息
        /// </summary>
        /// <param name="path"></param>
        /// <param name="accountName"></param>
        /// <param name="account"></param>
        void Save(string path, string accountName, IAccount account);

        /// <summary>
        /// 新的账号路径，创建成功返回true
        /// </summary>
        /// <param name="path"></param>
        bool NewPath(string path);

        /// <summary>
        /// 保存账号信息
        /// </summary>
        /// <param name="path"></param>
        /// <param name="accountName"></param>
        /// <param name="account"></param>
        /// <param name="overwrite"></param>
        void Save(string path, string accountName, IAccount account, bool overwrite);

        /// <summary>
        /// 是否存在账号
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
    }
}
