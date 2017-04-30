using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data
{
    /// <summary>
    /// 股票或期货信息接口
    /// </summary>
    public interface ICodeInfo
    {
        /// <summary>
        /// 得到该股票或期货的代码
        /// </summary>
        string Code { get; }

        /// <summary>
        /// 得到该股票或期货的名称
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 得到该股票或期货所属类别
        /// 期货返回该合约所属品种，如m1705，那返回m
        /// 股票返回其所属的行业板块
        /// </summary>
        string Catelog { get; }

        /// <summary>
        /// 该合约或股票的开始日期
        /// </summary>
        int Start { get; }

        /// <summary>
        /// 该合约或股票的结束日期
        /// 合约是到期日
        /// 股票则是退市
        /// </summary>
        int End { get; }

        /// <summary>
        /// 该合约或股票所属的交易所
        /// </summary>
        string Exchange { get; }
    }
}
