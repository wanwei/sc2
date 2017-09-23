using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy
{
    public interface IStrategyResult
    {
        /// <summary>
        /// 得到所有结果
        /// </summary>
        IList<IStrategyResult_Single> StrategyResults { get; }
    }
}
