using System.Collections.Generic;
using System.Xml;
using com.wer.sc.utils;

namespace com.wer.sc.data.account
{
    /// <summary>
    /// 交易费用
    /// </summary>
    public interface ITradeFee : IXmlExchange
    {
        /// <summary>
        /// 得到单支股票或期货的交易费用
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        ITradeFee_Code GetFee(string code);
    }
}