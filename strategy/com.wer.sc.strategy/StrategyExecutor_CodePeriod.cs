using com.wer.sc.data;
using com.wer.sc.data.datapackage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace com.wer.sc.strategy
{
    /// <summary>
    /// 将一个策略在多个上验证
    /// </summary>
    public class StrategyExecutor_CodePeriod : IStrategyExecutor
    {
        private IStrategy strategy;

        public IStrategy Strategy
        {
            get { return this.strategy; }
            set { this.strategy = value; }
        }

        private ICodePeriod codePeriod;

        public ICodePeriod CodePeriod
        {
            get { return codePeriod; }
        }

        private IStrategyExecutorFactory executorFactory;
        private IStrategyHelper strategyHelper;

        public StrategyExecutor_CodePeriod(IDataCenter dataCenter, StrategyArguments_CodePeriod strategyCodePeriod)
        {
            this.codePeriod = strategyCodePeriod.CodePeriod;
            this.executorFactory = StrategyCenter.Default.GetStrategyExecutorFactory();
        }

        public StrategyExecutor_CodePeriod(IDataCenter dataCenter, StrategyArguments_CodePeriod strategyCodePeriod, IStrategyHelper strategyHelper) : this(dataCenter, strategyCodePeriod)
        {
            this.strategyHelper = strategyHelper;
        }

        /// <summary>
        /// 执行策略
        /// 该方法会在一个新的线程里执行策略
        /// </summary>
        public void Execute()
        {

        }

        /// <summary>
        /// 执行策略
        /// </summary>
        public void Run()
        {

        }

        /// <summary>
        /// 取消当前执行的策略
        /// </summary>
        public void Cancel()
        {

        }


        /// <summary>
        /// 得到策略执行报告，策略执行完才能获得
        /// </summary>
        public IStrategyResult StrategyReport { get; }

        /// <summary>
        /// 得到执行器相关信息
        /// </summary>
        public IStrategyExecutorInfo StrategyExecutorInfo { get; }

        public event StrategyStart OnStart;

        /// <summary>
        /// 执行完每一个bar
        /// </summary>
        public event StrategyBarFinished OnBarFinished;

        /// <summary>
        /// 
        /// </summary>
        public event StrategyDayFinished OnDayFinished;

        /// <summary>
        /// 执行完
        /// </summary>
        public event StrategyFinished OnFinished;        
    }
}