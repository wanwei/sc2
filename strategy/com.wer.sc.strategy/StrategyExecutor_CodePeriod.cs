using com.wer.sc.data;
using com.wer.sc.data.codeperiod;
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
    /// CodePeriod执行器
    /// 
    /// 该类是代理类
    /// 
    /// </summary>
    public class StrategyExecutor_CodePeriod : IStrategyExecutor
    {
        private IStrategyExecutor strategyExecutor;

        public StrategyExecutor_CodePeriod(IStrategyCenter strategyCenter, StrategyArguments_CodePeriod strategyArguments)
        {
            InitByCodePeriod(strategyCenter, strategyArguments);
        }

        private void InitByCodePeriod(IStrategyCenter strategyCenter, StrategyArguments_CodePeriod strategyArguments)
        {
            ICodePeriod codePeriod = strategyArguments.CodePeriod;
            if (codePeriod.IsFromContracts)
            {
                this.strategyExecutor = new StrategyExecutor_CodePeriod_MainContract(strategyCenter, strategyArguments);
            }
            else
            {
                IDataPackage_Code dataPackage = strategyCenter.BelongDataCenter.DataPackageFactory.CreateDataPackage_Code(codePeriod.Code, codePeriod.StartDate, codePeriod.EndDate);
                StrategyArguments_DataPackage strategyDataPackage = new StrategyArguments_DataPackage(dataPackage, strategyArguments.ReferedPeriods, strategyArguments.ForwardPeriod);
                this.strategyExecutor = strategyCenter.GetStrategyExecutorFactory().CreateExecutor_History(strategyDataPackage);
            }

            this.strategyExecutor.OnStart += StrategyExecutor_OnStart; ;
            this.strategyExecutor.OnBarFinished += StrategyExecutor_OnBarFinished;
            this.strategyExecutor.OnDayFinished += StrategyExecutor_OnDayFinished;
            this.strategyExecutor.OnCanceled += StrategyExecutor_OnCanceled;
            this.strategyExecutor.OnFinished += StrategyExecutor_OnFinished;
        }

        private void StrategyExecutor_OnCanceled(object sender, StrategyCanceledArguments arguments)
        {
            if (OnCanceled != null)
                OnCanceled(this, arguments);
        }

        private void StrategyExecutor_OnFinished(object sender, StrategyFinishedArguments arguments)
        {
            if (OnFinished != null)
                OnFinished(this, arguments);
        }

        private void StrategyExecutor_OnDayFinished(object sender, StrategyDayFinishedArguments arguments)
        {
            if (OnDayFinished != null)
                OnDayFinished(this, arguments);
        }

        private void StrategyExecutor_OnBarFinished(object sender, StrategyBarFinishedArguments arguments)
        {
            if (OnBarFinished != null)
                OnBarFinished(this, arguments);
        }

        private void StrategyExecutor_OnStart(object sender, StrategyStartArguments arguments)
        {
            if (OnStart != null)
                OnStart(this, arguments);
        }

        public IStrategy Strategy
        {
            get { return strategyExecutor.Strategy; }
            set { strategyExecutor.Strategy = value; }
        }

        /// <summary>
        /// 执行策略
        /// 该方法会在一个新的线程里执行策略
        /// </summary>
        public void Execute()
        {
            Thread thread = new Thread(new ThreadStart(Run));
            thread.Start();
        }

        /// <summary>
        /// 执行策略
        /// </summary>
        public void Run()
        {
            this.strategyExecutor.Run();
        }

        /// <summary>
        /// 取消当前执行的策略
        /// </summary>
        public void Cancel()
        {
            this.strategyExecutor.Cancel();
        }

        /// <summary>
        /// 得到策略执行报告，策略执行完才能获得
        /// </summary>
        public IStrategyResult StrategyResult
        {
            get
            {
                return this.strategyExecutor.StrategyResult;
            }
        }

        /// <summary>
        /// 得到执行器相关信息
        /// </summary>
        public IStrategyExecutorInfo StrategyExecutorInfo
        {
            get { return this.strategyExecutor.StrategyExecutorInfo; }
        }

        public StrategyExecutorState State
        {
            get
            {
                return this.strategyExecutor.State;
            }
        }

        public StrategyReferedPeriods ReferedPeriods
        {
            get
            {
                return this.strategyExecutor.ReferedPeriods;
            }
        }

        public StrategyForwardPeriod ForwardPeriod
        {
            get
            {
                return this.strategyExecutor.ForwardPeriod;
            }
        }

        public StrategyTraderSetting TraderSetting
        {
            get
            {
                return this.strategyExecutor.TraderSetting;
            }
        }

        public IStrategyHelper StrategyHelper
        {
            get
            {
                return this.strategyExecutor.StrategyHelper;
            }
        }

        public event StrategyStart OnStart;

        public event StrategyBarFinished OnBarFinished;

        public event StrategyDayFinished OnDayFinished;

        public event StrategyCanceled OnCanceled;

        public event StrategyFinished OnFinished;
    }
}