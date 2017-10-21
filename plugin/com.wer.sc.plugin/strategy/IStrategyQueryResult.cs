using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy
{
    public interface IStrategyQueryResult
    {
        /// <summary>
        /// 得到所有结果
        /// </summary>
        IList<IStrategyQueryResult_Single> StrategyResults { get; }
    }
}
