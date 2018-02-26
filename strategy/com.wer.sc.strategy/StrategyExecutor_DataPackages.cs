using com.wer.sc.data.datapackage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace com.wer.sc.strategy
{
    public class StrategyExecutor_DataPackages : IStrategyExecutor
    {
        private IStrategy strategy;

        private List<StrategyExecutor_DataPackage> executors = new List<StrategyExecutor_DataPackage>();

        private StrategyExecutor_DataPackage currentExecutor;

        private StrategyArguments_DataPackages strategyArguments;

        private IStrategyHelper strategyHelper;

        public StrategyExecutor_DataPackages(StrategyArguments_DataPackages strategyArguments) : this(strategyArguments, new StrategyHelper(null))
        {
        }

        public StrategyExecutor_DataPackages(StrategyArguments_DataPackages strategyArguments, IStrategyHelper strategyHelper)
        {
            this.strategyArguments = strategyArguments;
            this.strategyHelper = strategyHelper;

            List<IDataPackage_Code> dataPackages = this.strategyArguments.DataPackages;
            for (int i = 0; i < dataPackages.Count; i++)
            {
                StrategyArguments_DataPackage strategyArgument = new StrategyArguments_DataPackage(dataPackages[i], strategyArguments.ReferedPeriods, strategyArguments.ForwardPeriod);
                StrategyExecutor_DataPackage executor = new StrategyExecutor_DataPackage(strategyArgument, strategyHelper);
                executor.OnBarFinished += Executor_OnBarFinished;
                executor.OnDayFinished += Executor_OnDayFinished;
                this.executors.Add(executor);
            }
        }

        private void Executor_OnBarFinished(object sender, StrategyBarFinishedArguments arguments)
        {
            if (OnBarFinished != null)
                OnBarFinished(sender, arguments);
        }

        private void Executor_OnDayFinished(object sender, StrategyDayFinishedArguments arguments)
        {
            if (OnDayFinished != null)
                OnDayFinished(sender, arguments);
        }

        public IStrategy Strategy
        {
            get
            {
                return strategy;
            }

            set
            {
                this.strategy = value;
                for (int i = 0; i < executors.Count; i++)
                {
                    executors[i].Strategy = value;
                }
            }
        }

        public IStrategyExecutorInfo StrategyExecutorInfo
        {
            get
            {
                if (currentExecutor != null)
                    return currentExecutor.StrategyExecutorInfo;
                return null;
            }
        }

        public IStrategyResult StrategyReport
        {
            get
            {
                return null;
            }
        }

        public event StrategyStart OnStart;

        public event StrategyBarFinished OnBarFinished;

        public event StrategyDayFinished OnDayFinished;

        public event StrategyFinished OnFinished;

        private bool isCancel = false;

        public void Cancel()
        {
            isCancel = true;
            if (currentExecutor != null)
                currentExecutor.Cancel();
        }

        public void Execute()
        {
            Thread thread = new Thread(new ThreadStart(Run));
            thread.Start();
        }

        public void Run()
        {
            StrategyExecutor_DataPackage firstExecutor = executors[0];
            StrategyExecutor_DataPackage lastExecutor = executors[executors.Count - 1];

            if (OnStart != null)
                OnStart(this, new StrategyStartArguments(firstExecutor.StrategyExecutorInfo));
            firstExecutor.Run();
            if (isCancel)
            {
                if (OnFinished != null)
                    OnFinished(this, new StrategyFinishedArguments(strategy, lastExecutor.StrategyExecutorInfo, StrategyReport));
                return;
            }
            if (executors.Count != 1)
            {
                int last = executors.Count - 1;
                for (int i = 1; i < last; i++)
                {
                    StrategyExecutor_DataPackage executor = executors[i];
                    executor.Run();
                    if (isCancel)
                    {
                        if (OnFinished != null)
                            OnFinished(this, new StrategyFinishedArguments(strategy, lastExecutor.StrategyExecutorInfo, StrategyReport));
                        return;
                    }
                }
                lastExecutor.Run();
            }
            if (OnFinished != null)
                OnFinished(this, new StrategyFinishedArguments(strategy, lastExecutor.StrategyExecutorInfo, StrategyReport));
        }
    }
}