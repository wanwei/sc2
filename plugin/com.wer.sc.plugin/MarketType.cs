using com.wer.sc.utils.attr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin
{
    /// <summary>
    /// 市场类别
    /// </summary>
    public enum MarketType : byte
    {
        /// <summary>
        /// 国内期货市场
        /// </summary>
        [Remark("中国期货市场")]
        CnFutures = 0,

        /// <summary>
        /// 国内股票市场
        /// </summary>
        [Remark("中国股票市场")]
        CnStock = 1
    }
}