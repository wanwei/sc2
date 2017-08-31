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
    public interface IStrategyExecutor
    {
        /// <summary>
        /// 设置要执行的策略
        /// </summary>
        /// <param name="strategy"></param>
        void SetStrategy(IStrategy strategy);

        /// <summary>
        /// 执行策略
        /// </summary>
        void Run();

        /// <summary>
        /// 执行策略，该方法会开新的线程
        /// </summary>
        void Execute();

        /// <summary>
        /// 
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
    public delegate void StrategyExecuteFinished(IStrategy strategy);

    public class StrategyExecuteArguments
    {
        private StrategyHelper strategyHelper;

        private IStrategy strategy;

        public StrategyExecuteArguments(StrategyHelper strategyHelper,IStrategy strategy)
        {
            this.strategyHelper = strategyHelper;
            this.strategy = strategy;
        }

        public StrategyHelper StrategyHelper
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
}