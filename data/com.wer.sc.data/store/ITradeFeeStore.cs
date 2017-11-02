using com.wer.sc.data.account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.store
{
    /// <summary>
    /// 交易费用保存
    /// </summary>
    public interface ITradeFeeStore
    {
        /// <summary>
        /// 装载所有的费用信息
        /// </summary>
        /// <returns></returns>
        List<string> LoadAllNames();

        /// <summary>
        /// 保存交易费用信息
        /// </summary>
        /// <param name="name"></param>
        /// <param name="tradeFee"></param>
        void Save(string name, TradeFee tradeFee);

        /// <summary>
        /// 装载交易费用
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        TradeFee Load(string name);
    }
}