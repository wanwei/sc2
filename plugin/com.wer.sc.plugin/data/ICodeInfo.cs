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
        /// 得到该股票或期货所属
        /// </summary>
        string Catelog { get; }

        int Start { get; }

        int End { get; }

        string Exchange { get; }
    }
}
