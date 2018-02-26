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
        private IStrategyExecutor strategyExecutor;

        public IStrategy Strategy
        {
            get { return strategyExecutor.Strategy; }
            set { strategyExecutor.Strategy = value; }
        }

        private ICodePeriod codePeriod;

        public ICodePeriod CodePeriod
        {
            get { return codePeriod; }
        }

        private IStrategyExecutorFactory executorFactory;

        private IStrategyHelper strategyHelper;

        public StrategyExecutor_CodePeriod(IDataCenter dataCenter, StrategyArguments_CodePeriod strategyCodePeriod) : this(dataCenter, strategyCodePeriod, new StrategyHelper(null))
        {

        }

        public StrategyExecutor_CodePeriod(IDataCenter dataCenter, StrategyArguments_CodePeriod strategyCodePeriod, IStrategyHelper strategyHelper)
        {
            this.codePeriod = strategyCodePeriod.CodePeriod;
            this.executorFactory = StrategyCenter.Default.GetStrategyExecutorFactory();
            this.strategyHelper = strategyHelper;
            InitByCodePeriod(dataCenter, strategyCodePeriod, strategyHelper);
        }

        private void InitByCodePeriod(IDataCenter dataCenter, StrategyArguments_CodePeriod strategyCodePeriod, IStrategyHelper strategyHelper)
        {
            ICodePeriod codePeriod = strategyCodePeriod.CodePeriod;
            if (codePeriod.IsFromContracts)
            {
                StrategyArguments_DataPackages strategyArguments = new StrategyArguments_DataPackages();
                for(int i = 0; i < codePeriod.Contracts.Count; i++)
                {
                    ICodePeriod contractCodePeriod = codePeriod.Contracts[i];
                    IDataPackage_Code dataPackage = dataCenter.DataPackageFactory.CreateDataPackage_Code(contractCodePeriod.Code, contractCodePeriod.StartDate, contractCodePeriod.EndDate);
                    strategyArguments.DataPackages.Add(dataPackage);
                }
                strategyArguments.ReferedPeriods = strategyCodePeriod.ReferedPeriods;
                strategyArguments.ForwardPeriod = strategyCodePeriod.ForwardPeriod;
                this.strategyExecutor = this.executorFactory.CreateExecutor_History(strategyArguments);
            }
            else
            {
                IDataPackage_Code dataPackage = dataCenter.DataPackageFactory.CreateDataPackage_Code(codePeriod.Code, codePeriod.StartDate, codePeriod.EndDate);
                StrategyArguments_DataPackage strategyDataPackage = new StrategyArguments_DataPackage(dataPackage, strategyCodePeriod.ReferedPeriods, strategyCodePeriod.ForwardPeriod);
                this.strategyExecutor = this.executorFactory.CreateExecutor_History(strategyDataPackage, strategyHelper);
            }

            this.strategyExecutor.OnStart += StrategyExecutor_OnStart; ;
            this.strategyExecutor.OnBarFinished += StrategyExecutor_OnBarFinished;
            this.strategyExecutor.OnDayFinished += StrategyExecutor_OnDayFinished;
            this.strategyExecutor.OnFinished += StrategyExecutor_OnFinished;
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
        public IStrategyResult StrategyReport
        {
            get
            {
                return this.strategyExecutor.StrategyReport;
            }
        }

        /// <summary>
        /// 得到执行器相关信息
        /// </summary>
        public IStrategyExecutorInfo StrategyExecutorInfo
        {
            get { return this.strategyExecutor.StrategyExecutorInfo; }
        }

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