using com.wer.sc.data.datapackage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy
{
    /// <summary>
    /// 策略执行信息
    /// </summary>
    public interface IStrategyExecutorInfo_Multi
    {
        /// <summary>
        /// 线程总数
        /// </summary>
        int ThreadCount { get; }

        /// <summary>
        /// 得到所有的执行器信息
        /// </summary>
        IList<IStrategyExecutorInfo_Multi> AllCodePeriod { get; }

        /// <summary>
        /// 得到已经结束的执行器信息
        /// </summary>
        IList<IStrategyExecutorInfo_Multi> FinishedExecutor { get; }

        /// <summary>
        /// 得到正在执行的执行器信息
        /// </summary>
        IList<IStrategyExecutorInfo_Multi> ExecutingCodePeriod { get; }

        /// <summary>
        /// 获得指定的执行器
        /// </summary>
        /// <param name="codePeriod"></param>
        /// <returns></returns>
        IStrategyExecutorInfo_Multi GetExecutorInfo_CodePeriod(ICodePeriod codePeriod);

        /// <summary>
        /// CodePeriod执行结束时触发该事件
        /// </summary>
        event StrategyExecuteFinished ExecuteFinished;
    }
}