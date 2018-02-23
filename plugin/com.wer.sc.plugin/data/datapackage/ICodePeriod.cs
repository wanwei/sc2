using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.datapackage
{
    /// <summary>
    /// 该类描述了一只股票或期货的一段时间
    /// 一般用做策略执行等
    /// </summary>
    public interface ICodePeriod
    {
        /// <summary>
        /// 获得或设置代码
        /// 如果是主合约，那么该属性表示品种
        /// </summary>
        string Code { get; }

        /// <summary>
        /// 获得或设置开始日期
        /// </summary>
        int StartDate { get; }

        /// <summary>
        /// 获得或设置结束日期
        /// </summary>
        int EndDate { get; }

        /// <summary>
        /// 是否是从多个合约而来的
        /// 该方式一般用于期货
        /// </summary>
        bool IsFromContracts { get; }

        /// <summary>
        /// 多个合约列表
        /// </summary>
        IList<ICodePeriod> Contracts { get; }
    }
}