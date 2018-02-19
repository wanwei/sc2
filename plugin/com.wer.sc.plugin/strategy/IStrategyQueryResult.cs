using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy
{
    /// <summary>
    /// 策略查询结果
    /// </summary>
    public interface IStrategyQueryResult
    {
        string Name { get; }

        /// <summary>
        /// 查询结果的标题
        /// </summary>
        string[] Title { get; }

        /// <summary>
        /// 得到所有结果
        /// </summary>
        IList<IStrategyQueryResultRow> StrategyResults { get; }
    }
}