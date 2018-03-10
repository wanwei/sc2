using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy
{
    /// <summary>
    /// 策略执行器事件接口
    /// </summary>
    public interface IStrategyExecutor_Event
    {
        /// <summary>
        /// 策略开始执行事件
        /// </summary>
        event StrategyStart OnStart;

        /// <summary>
        /// 每执行完一个主周期的bar触发该事件
        /// </summary>
        event StrategyBarFinished OnBarFinished;

        /// <summary>
        /// 每执行完一天的所有bar触发该事件
        /// </summary>
        event StrategyDayFinished OnDayFinished;

        /// <summary>
        /// 策略被取消后触发
        /// </summary>
        event StrategyCanceled OnCanceled;

        /// <summary>
        /// 执行完所有数据触发该事件
        /// </summary>
        event StrategyFinished OnFinished;
    }

    /// <summary>
    /// 策略开始执行
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="executorInfo"></param>
    public delegate void StrategyStart(Object sender, StrategyStartArguments arguments);

    /// <summary>
    /// 策略执行时bar
    /// </summary>
    /// <param name="strategy"></param>
    public delegate void StrategyBarFinished(Object sender, StrategyBarFinishedArguments arguments);

    /// <summary>
    /// 当天的策略执行完毕
    /// </summary>
    /// <param name="strategy"></param>
    /// <param name="args"></param>
    public delegate void StrategyDayFinished(Object sender, StrategyDayFinishedArguments arguments);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="arguments"></param>
    public delegate void StrategyCanceled(Object sender, StrategyCanceledArguments arguments);

    /// <summary>
    /// 策略执行完毕
    /// </summary>
    /// <param name="strategy"></param>
    public delegate void StrategyFinished(Object sender, StrategyFinishedArguments arguments);

    public abstract class StrategyArguments
    {
        private IStrategyExecutorInfo executorInfo;

        public StrategyArguments(IStrategyExecutorInfo executorInfo)
        {
            this.executorInfo = executorInfo;
        }

        public IStrategyExecutorInfo ExecutorInfo
        {
            get
            {
                return executorInfo;
            }
        }
    }

    public class StrategyStartArguments : StrategyArguments
    {
        public StrategyStartArguments(IStrategyExecutorInfo executorInfo) : base(executorInfo)
        {
        }
    }

    public class StrategyBarFinishedArguments : StrategyArguments
    {
        public StrategyBarFinishedArguments(IStrategyExecutorInfo executorInfo) : base(executorInfo)
        {
        }
    }

    public class StrategyDayFinishedArguments : StrategyArguments
    {
        public StrategyDayFinishedArguments(IStrategyExecutorInfo executorInfo) : base(executorInfo)
        {
        }
    }

    public class StrategyCanceledArguments : StrategyArguments
    {
        public StrategyCanceledArguments(IStrategyExecutorInfo executorInfo) : base(executorInfo)
        {
        }
    }

    public class StrategyFinishedArguments : StrategyArguments
    {
        private IStrategyExecutorInfo strategyExecutorInfo;

        private IStrategyResult result;

        private IStrategy strategy;

        public StrategyFinishedArguments(IStrategy strategy, IStrategyExecutorInfo executorInfo, IStrategyResult report) : base(executorInfo)
        {
            this.strategy = strategy;
            this.strategyExecutorInfo = executorInfo;
            this.result = report;
        }

        public IStrategy Strategy
        {
            get { return this.strategy; }
        }

        public IStrategyResult Result
        {
            get
            {
                return result;
            }
        }

        public IStrategyExecutorInfo StrategyExecutorInfo
        {
            get
            {
                return strategyExecutorInfo;
            }
        }
    }
}
