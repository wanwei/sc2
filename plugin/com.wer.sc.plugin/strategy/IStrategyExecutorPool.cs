using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy
{
    /// <summary>
    /// 策略执行器池子
    /// </summary>
    public interface IStrategyExecutorPool
    {
        /// <summary>
        /// 策略执行时的线程数量
        /// </summary>
        int ThreadCount { get; set; }

        /// <summary>
        /// 运行
        /// </summary>
        void Run();

        /// <summary>
        /// 停止所有正在执行的执行器，并不再继续执行
        /// </summary>
        void Stop();

        /// <summary>
        /// 将策略执行器加入队列
        /// </summary>
        /// <param name="strategyExecutor"></param>
        void Queue(IStrategyExecutor strategyExecutor);

        /// <summary>
        /// 得到所有执行器
        /// </summary>
        IList<IStrategyExecutor> AllExecutor { get; }

        /// <summary>
        /// 得到已经结束的执行器信息
        /// </summary>
        IList<IStrategyExecutor> FinishedExecutor { get; }

        /// <summary>
        /// 得到正在执行的执行器信息
        /// </summary>
        IList<IStrategyExecutor> ExecutingExecutor { get; }

        /// <summary>
        /// 当一个新的执行器开始执行时触发该事件
        /// </summary>
        event StrategyStart OnStart;

        /// <summary>
        /// 当一个执行器执行结束时触发该事件
        /// </summary>
        event StrategyFinished OnFinished;
    }
}