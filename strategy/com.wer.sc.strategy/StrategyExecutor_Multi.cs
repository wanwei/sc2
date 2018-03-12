
using com.wer.sc.data.codeperiod;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy
{
    public class StrategyExecutor_Multi : IStrategyExecutor_Multi
    {
        private IStrategy strategy;

        public IStrategy Strategy
        {
            get { return strategy; }
            set { this.strategy = value; }
        }

        private IStrategyCenter strategyCenter;

        private StrategyArguments_CodePeriodList arguments;

        private List<IStrategyExecutor_Single> executors = new List<IStrategyExecutor_Single>();

        private List<IStrategyExecutor_Single> executingExecutors = new List<IStrategyExecutor_Single>();

        public StrategyExecutor_Multi(IStrategyCenter strategyCenter, StrategyArguments_CodePeriodList arguments)
        {
            this.strategyCenter = strategyCenter;
            this.arguments = arguments;

            IList<ICodePeriod> codePeriods = this.arguments.CodePeriodPackage.CodePeriods;
            for (int i = 0; i < codePeriods.Count; i++)
            {
                ICodePeriod codePeriod = codePeriods[i];
                StrategyArguments_CodePeriod strategyCodePeriod = new StrategyArguments_CodePeriod(codePeriod, arguments.ReferedPeriods, arguments.ForwardPeriod);
                IStrategyExecutor_Single executor = strategyCenter.GetStrategyExecutorFactory().CreateExecutor_History(strategyCodePeriod);
                this.executors.Add(executor);
            }
        }

        /// <summary>
        /// 获得所有策略执行器
        /// </summary>
        public IList<IStrategyExecutor_Single> StrategyExecutors
        {
            get { return executors; }
        }

        /// <summary>
        /// 运行
        /// </summary>
        public void Execute()
        {
            IStrategyExecutorPool pool = this.strategyCenter.GetStrategyExecutorPool();
            for (int i = 0; i < executors.Count; i++)
            {
                IStrategyExecutor_Single executor = executors[i];
                executor.OnStart += Executor_OnStart;
                executor.OnBarFinished += Executor_OnBarFinished;
                executor.OnDayFinished += Executor_OnDayFinished;
                executor.OnCanceled += Executor_OnCanceled;
                executor.OnFinished += Executor_OnFinished;
                executor.Strategy = strategy;
                pool.Queue(executor);
            }
        }

        private object lockExecutingObj = new object();

        private void AddExecutingExecutor(IStrategyExecutor_Single executor)
        {
            lock (lockExecutingObj)
            {
                this.executingExecutors.Add(executor);
            }
        }

        private void RemoveExecutingExecutor(IStrategyExecutor_Single executor)
        {
            lock (lockExecutingObj)
            {
                this.executingExecutors.Remove(executor);
            }
        }

        private void Executor_OnStart(object sender, StrategyStartArguments arguments)
        {
            this.AddExecutingExecutor((IStrategyExecutor_Single)sender);
            if (OnStrategyStart != null)
                OnStrategyStart(sender, arguments);
        }

        private void Executor_OnBarFinished(object sender, StrategyBarFinishedArguments arguments)
        {
            if (OnStrategyBarFinished != null)
                OnStrategyBarFinished(sender, arguments);
        }

        private void Executor_OnDayFinished(object sender, StrategyDayFinishedArguments arguments)
        {
            if (OnStrategyDayFinished != null)
                OnStrategyDayFinished(sender, arguments);
        }

        private void Executor_OnCanceled(object sender, StrategyCanceledArguments arguments)
        {
            this.RemoveExecutingExecutor((IStrategyExecutor_Single)sender);
            if (OnStrategyCanceled != null)
                OnStrategyCanceled(sender, arguments);
        }

        private void Executor_OnFinished(object sender, StrategyFinishedArguments arguments)
        {
            this.RemoveExecutingExecutor((IStrategyExecutor_Single)sender);
            if (OnStrategyFinished != null)
                OnStrategyFinished(sender, arguments);
        }

        /// <summary>
        /// 停止所有正在执行的执行器，并不再继续执行
        /// </summary>
        public void Stop()
        {

        }

        /// <summary>
        /// 得到正在执行的执行器信息
        /// </summary>
        public IList<IStrategyExecutor_Single> ExecutingExecutors
        {
            get
            {
                return executingExecutors;
            }
        }

        /// <summary>
        /// 当策略执行池里的一个新的执行器开始执行时触发该事件
        /// </summary>
        public event StrategyStart OnStrategyStart;

        /// <summary>
        /// 
        /// </summary>
        public event StrategyBarFinished OnStrategyBarFinished;

        /// <summary>
        /// 当策略执行池里的一个执行器执行完一天的数据后触发该事件
        /// </summary>
        public event StrategyDayFinished OnStrategyDayFinished;

        /// <summary>
        /// 
        /// </summary>
        public event StrategyCanceled OnStrategyCanceled;

        /// <summary>
        /// 当策略执行池里的一个执行器执行结束时触发该事件
        /// </summary>
        public event StrategyFinished OnStrategyFinished;
    }
}
