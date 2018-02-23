using com.wer.sc.data;
using com.wer.sc.data.datapackage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy
{
    /// <summary>
    /// 策略执行器
    /// 该接口负责单支股票或期货品种在一段时间内的策略执行
    /// 
    /// 品种ID和时间可以通过CodePeriod获得
    /// </summary>
    public interface IStrategyExecutor
    {
        /// <summary>
        /// 设置和获取需要执行的策略
        /// </summary>
        /// <param name="strategy"></param>
        IStrategy Strategy { get; set; }

        /// <summary>
        /// 执行策略
        /// 该方法会在一个新的线程里执行策略
        /// </summary>
        void Execute();

        /// <summary>
        /// 执行策略
        /// </summary>
        void Run();

        /// <summary>
        /// 取消当前执行的策略
        /// </summary>
        void Cancel();

        /// <summary>
        /// 策略执行开始
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
        /// 执行完所有数据触发该事件
        /// </summary>
        event StrategyFinished OnFinished;

        /// <summary>
        /// 得到策略执行报告，策略执行完才能获得
        /// </summary>
        IStrategyResult StrategyReport { get; }

        /// <summary>
        /// 得到执行时相关信息
        /// </summary>
        IStrategyExecutorInfo StrategyExecutorInfo { get; }
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

    public class StrategyFinishedArguments : StrategyArguments
    {
        private IStrategyExecutorInfo strategyExecutorInfo;

        private IStrategyResult report;

        private IStrategy strategy;

        public StrategyFinishedArguments(IStrategy strategy, IStrategyExecutorInfo executorInfo, IStrategyResult report) : base(executorInfo)
        {
            this.strategy = strategy;
            this.strategyExecutorInfo = executorInfo;
            this.report = report;
        }

        public IStrategy Strategy
        {
            get { return this.strategy; }
        }

        public IStrategyResult Report
        {
            get
            {
                return report;
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

    public class StrategyExecuteArguments
    {
        private IStrategyHelper strategyHelper;

        private IStrategy strategy;

        public StrategyExecuteArguments(IStrategyHelper strategyHelper, IStrategy strategy)
        {
            this.strategyHelper = strategyHelper;
            this.strategy = strategy;
        }

        public IStrategyHelper StrategyHelper
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