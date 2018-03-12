using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy
{
    /// <summary>
    /// 多重策略执行器
    /// 该执行器会使用线程池同时执行多个策略执行器
    /// </summary>
    public interface IStrategyExecutor_Multi : IStrategyExecutor
    {
        IStrategy Strategy { get; set; }

        /// <summary>
        /// 获得所有策略执行器
        /// </summary>
        IList<IStrategyExecutor_Single> StrategyExecutors { get; }

        /// <summary>
        /// 运行
        /// </summary>
        void Execute();

        /// <summary>
        /// 停止所有正在执行的执行器，并不再继续执行
        /// </summary>
        void Stop();

        /// <summary>
        /// 得到正在执行的执行器信息
        /// </summary>
        IList<IStrategyExecutor_Single> ExecutingExecutors { get; }

        /// <summary>
        /// 当策略执行池里的一个新的执行器开始执行时触发该事件
        /// </summary>
        event StrategyStart OnStrategyStart;


        event StrategyBarFinished OnStrategyBarFinished;

        /// <summary>
        /// 当策略执行池里的一个执行器执行完一天的数据后触发该事件
        /// </summary>
        event StrategyDayFinished OnStrategyDayFinished;

        /// <summary>
        /// 
        /// </summary>
        event StrategyCanceled OnStrategyCanceled;

        /// <summary>
        /// 当策略执行池里的一个执行器执行结束时触发该事件
        /// </summary>
        event StrategyFinished OnStrategyFinished;
    }
}