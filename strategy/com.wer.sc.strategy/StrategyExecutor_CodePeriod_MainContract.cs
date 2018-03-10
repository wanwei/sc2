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
    public class StrategyExecutor_CodePeriod_MainContract : StrategyExecutorAbstract, IStrategyExecutor
    {
        private object lockRunObject = new object();

        private List<StrategyExecutor_DataPackage> executors = new List<StrategyExecutor_DataPackage>();

        private StrategyExecutor_DataPackage currentExecutor;

        private StrategyArguments_CodePeriod strategyArguments;

        private List<IDataPackage_Code> dataPackages = new List<IDataPackage_Code>();

        public StrategyExecutor_CodePeriod_MainContract(IStrategyCenter strategyCenter, StrategyArguments_CodePeriod strategyArguments) : base(strategyCenter, strategyArguments)
        {
            this.strategyArguments = strategyArguments;
            this.PrepareStrategyHelper();

            IList<ICodePeriod> codePeriods = this.strategyArguments.CodePeriod.Contracts;
            this.dataPackages = new List<IDataPackage_Code>();
            for (int i = 0; i < codePeriods.Count; i++)
            {
                ICodePeriod codePeriod = codePeriods[i];
                this.dataPackages.Add(strategyCenter.BelongDataCenter.DataPackageFactory.CreateDataPackage_Code(codePeriod.Code, codePeriod.StartDate, codePeriod.EndDate));
            }
            this.BuildExecutorInfo();
            for (int i = 0; i < dataPackages.Count; i++)
            {
                IDataPackage_Code dataPackage = dataPackages[i];
                StrategyHelper executorStrategyHelper = new StrategyHelper();
                executorStrategyHelper.QueryResultManager = this.strategyHelper.QueryResultManager;
                executorStrategyHelper.Drawer = null;//TODO
                executorStrategyHelper.Trader = this.strategyHelper.Trader;

                StrategyArguments_DataPackage strategyArgument = new StrategyArguments_DataPackage(dataPackage, strategyArguments.ReferedPeriods, strategyArguments.ForwardPeriod, executorStrategyHelper);
                StrategyExecutor_DataPackage executor = new StrategyExecutor_DataPackage(strategyCenter, strategyArgument, this.strategyExecutorInfo);
                executor.OnBarFinished += Executor_OnBarFinished;
                executor.OnDayFinished += Executor_OnDayFinished;
                this.executors.Add(executor);
            }
        }

        private IStrategyHelper PrepareStrategyHelper()
        {
            //如果在参数里设定了strategyHelper，则不再更改
            if (this.strategyHelper != null)
                return this.strategyHelper;
            this.strategyHelper = GetDefaultStrategyHelper();
            return this.strategyHelper;
        }

        private void BuildExecutorInfo()
        {
            ICodePeriod codePeriod = strategyArguments.CodePeriod;
            int totalDays = 0;
            for (int i = 0; i < dataPackages.Count; i++)
            {
                totalDays += dataPackages[i].GetTradingDays().Count;
            }
            this.strategyExecutorInfo = new StrategyExecutorInfo(codePeriod, totalDays);
        }

        private void Executor_OnBarFinished(object sender, StrategyBarFinishedArguments arguments)
        {
            DealBarFinishEvent(arguments);
        }

        private void Executor_OnDayFinished(object sender, StrategyDayFinishedArguments arguments)
        {
            //this.strategyExecutorInfo.CurrentDay =
            DealDayFinishEvent(arguments);
        }

        public override IStrategy Strategy
        {
            get
            {
                return base.Strategy;
            }

            set
            {
                base.Strategy = value;
                for (int i = 0; i < executors.Count; i++)
                {
                    executors[i].Strategy = value;
                }
            }
        }

        public override IStrategyExecutorInfo StrategyExecutorInfo
        {
            get
            {
                if (currentExecutor != null)
                    return currentExecutor.StrategyExecutorInfo;
                return null;
            }
        }

        public override IStrategyHelper StrategyHelper
        {
            get
            {
                if (currentExecutor == null)
                    return null;
                return currentExecutor.StrategyHelper;
            }
        }

        public override void Cancel()
        {
            isCancel = true;
            if (currentExecutor != null)
                currentExecutor.Cancel();
        }

        public override void Run()
        {
            if (executors.Count == 0)
                return;
            if (this.state != StrategyExecutorState.NotStart)
                return;
            lock (lockRunObject)
            {
                if (this.state != StrategyExecutorState.NotStart)
                    return;

                this.BuildStrategyResult();

                StrategyExecutor_DataPackage firstExecutor = executors[0];
                DealStartEvent(new StrategyStartArguments(firstExecutor.StrategyExecutorInfo));

                for (int i = 0; i < executors.Count; i++)
                {
                    StrategyExecutor_DataPackage executor = executors[i];
                    bool continueRun = RunExecutor_DataPackage(executor);
                    if (!continueRun)
                        return;
                }

                if (IsSaveResult)
                {
                    this.PrepareStrategyResult();
                    this.SaveStrategyResult();
                }
                StrategyExecutor_DataPackage lastExecutor = executors[executors.Count - 1];
                DealFinishedEvent(new StrategyFinishedArguments(Strategy, lastExecutor.StrategyExecutorInfo, StrategyResult));
            }
        }

        private void PrepareStrategyResult()
        {
            StrategyResult_CodePeriod result_Code = new StrategyResult_CodePeriod(this.strategyArguments.CodePeriod, ForwardPeriod, ReferedPeriods, null, StrategyHelper.Trader);
            this.strategyResult.AddStrategyResult_Code(result_Code);
        }


        private bool RunExecutor_DataPackage(StrategyExecutor_DataPackage executor)
        {
            currentExecutor = executor;            
            executor.Run();
            if (isCancel)
            {
                DealCancelEvent(new StrategyCanceledArguments(StrategyExecutorInfo));
                return false;
            }

            if (this.strategyArguments.CloseOnCodeChanged)
                StrategyHelper.Trader.CloseAll();
            return true;
        }

        private void BuildStrategyResult()
        {
            this.strategyResult = new StrategyResult();
            int startDate = strategyArguments.CodePeriod.StartDate;
            int endDate = strategyArguments.CodePeriod.EndDate;
            strategyResult.Name = GetResultName(startDate, endDate);
            strategyResult.CodePeriods.Add(this.strategyArguments.CodePeriod);
            strategyResult.StartDate = startDate;
            strategyResult.EndDate = endDate;
            strategyResult.ReferedPeriods = this.ReferedPeriods;
            strategyResult.ForwardPeriod = this.ForwardPeriod;
            strategyResult.Parameters = Strategy.Parameters;
            strategyResult.StrategyQueryResultManager = strategyHelper.QueryResultManager;
        }
    }
}