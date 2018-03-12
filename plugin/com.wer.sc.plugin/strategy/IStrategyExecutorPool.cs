using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy
{
    /// <summary>
    /// 策略执行池
    /// 
    /// 所有的策略执行都会放到该池子里执行
    /// </summary>
    public interface IStrategyExecutorPool
    {
        /// <summary>
        /// 策略执行时的线程数量
        /// </summary>
        int ThreadCount { get; set; }

        /// <summary>
        /// 设置最大同时计算的策略数量
        /// 如果池子里要执行的策略执行器数量大于该值，那新进的策略要加入等待队列
        /// </summary>
        int MaxExecutorCount { get; set; }

        /// <summary>
        /// 是否正在运行策略
        /// </summary>
        /// <returns></returns>
        bool IsRunning();

        /// <summary>
        /// 运行
        /// </summary>
        void Execute();

        /// <summary>
        /// 停止所有正在执行的执行器，并不再继续执行
        /// </summary>
        void Stop();

        /// <summary>
        /// 将策略执行器加入执行队列
        /// </summary>
        /// <param name="strategyExecutor"></param>
        void Queue(IStrategyExecutor_Single strategyExecutor);

        /// <summary>
        /// 得到正在执行的执行器信息
        /// </summary>
        IList<IStrategyExecutor_Single> ExecutingExecutors { get; }

        /// <summary>
        /// 当策略执行池里的一个新的执行器开始执行时触发该事件
        /// </summary>
        event StrategyStart OnStrategyStart;

        /// <summary>
        /// 当策略执行池里的一个执行器执行完一天的数据后触发该事件
        /// </summary>
        event StrategyDayFinished OnStrategyDayFinished;

        /// <summary>
        /// 当策略执行池里的一个执行器执行结束时触发该事件
        /// </summary>
        event StrategyFinished OnStrategyFinished;

        /// <summary>
        /// 当策略执行池里所有策略都执行完以后触发该事件
        /// </summary>
        event PoolExecuteFinished OnPoolFinished;
    }

    /// <summary>
    /// 策略执行池完成所有策略的执行
    /// </summary>
    public delegate void PoolExecuteFinished(Object sender);
}