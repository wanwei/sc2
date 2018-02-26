using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy
{
    /// <summary>
    /// 策略执行器
    /// </summary>
    public interface IStrategyExecutor_Multi
    {
        IList<IStrategyExecutor> StrategyExecutors { get; }
    }
}
