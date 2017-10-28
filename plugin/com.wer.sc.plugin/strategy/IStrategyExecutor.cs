using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy
{
    /// <summary>
    /// 策略执行器接口
    /// 该接口负责策略的执行，一个策略执行器实例只能执行一个策略
    /// </summary>
    public interface IStrategyExecutor
    {
        /// <summary>
        /// 设置希望要执行的策略
        /// </summary>
        /// <param name="strategy"></param>
        void SetStrategy(IStrategy strategy);

        /// <summary>
        /// 执行策略
        /// 该方法会在一个新的线程里执行策略
        /// </summary>
        void Execute();

        /// <summary>
        /// 执行策略 TODO 有execute就可以了，该方法不需要
        /// </summary>
        void Run();

        /// <summary>
        /// 取消当前执行的策略
        /// </summary>
        void Cancel();

        /// <summary>
        /// 执行完每一个bar
        /// </summary>
        event StrategyExecuteBarFinished BarFinished;

        /// <summary>
        /// 
        /// </summary>
        event StrategyExecuteDayFinished DayFinished;

        /// <summary>
        /// 执行完
        /// </summary>
        event StrategyExecuteFinished ExecuteFinished;

        /// <summary>
        /// 得到策略执行报告，策略执行完才能获得
        /// </summary>
        IStrategyReport StrategyReport { get; }

        /// <summary>
        /// 得到执行器相关信息
        /// </summary>
        IStrategyExecutorInfo StrategyExecutorInfo { get; }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="strategy"></param>
    public delegate void StrategyExecuteBarFinished(IStrategy strategy);


    public delegate void StrategyExecuteDayFinished(IStrategy strategy);

    /// <summary>
    /// 整个执行完毕
    /// </summary>
    /// <param name="strategy"></param>
    public delegate void StrategyExecuteFinished(IStrategy strategy, StrategyExecuteFinishedArguments arg);

    public class StrategyExecuteArguments
    {
        private IStrategyOperator strategyHelper;

        private IStrategy strategy;

        public StrategyExecuteArguments(IStrategyOperator strategyHelper, IStrategy strategy)
        {
            this.strategyHelper = strategyHelper;
            this.strategy = strategy;
        }

        public IStrategyOperator StrategyHelper
        {
            get
            {
                return strategyHelper;
            }
        }

        public IStrategy Strategy
        {
            get
            {
                return strategy;
            }
        }
    }

    public class StrategyExecuteFinishedArguments
    {
        private IStrategyReport report;

        public StrategyExecuteFinishedArguments(IStrategyReport report)
        {
            this.report = report;
        }

        public IStrategyReport Report
        {
            get
            {
                return report;
            }            
        }
    }
}