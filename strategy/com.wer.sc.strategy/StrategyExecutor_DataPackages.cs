using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy
{
    public class StrategyExecutor_DataPackages 
    {
        private StrategyArguments_DataPackages strategyArguments;
        private IStrategyHelper strategyHelper;

        public StrategyExecutor_DataPackages(StrategyArguments_DataPackages strategyArguments)
        {
            this.strategyArguments = strategyArguments;
        }

        public StrategyExecutor_DataPackages(StrategyArguments_DataPackages strategyArguments, IStrategyHelper strategyHelper) : this(strategyArguments)
        {
            this.strategyHelper = strategyHelper;
        }

        public IStrategy Strategy
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public IStrategyExecutorInfo StrategyExecutorInfo
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IStrategyResult StrategyReport
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public event StrategyBarFinished BarFinished;
        public event StrategyDayFinished DayFinished;
        public event StrategyFinished ExecuteFinished;

        public void Cancel()
        {
            throw new NotImplementedException();
        }

        public void Execute()
        {
            throw new NotImplementedException();
        }

        public void Run()
        {
            throw new NotImplementedException();
        }

        public void SetStrategy(IStrategy strategy)
        {
            throw new NotImplementedException();
        }
    }
}
