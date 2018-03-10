using com.wer.sc.data;
using com.wer.sc.data.codeperiod;
using com.wer.sc.data.datapackage;
using com.wer.sc.data.forward;
using com.wer.sc.data.reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy
{
    /// <summary>
    /// 策略执行器工厂
    /// </summary>
    public class StrategyExecutorFactory : IStrategyExecutorFactory
    {
        private IStrategyCenter strategyCenter;

        public StrategyExecutorFactory(IStrategyCenter strategyCenter)
        {
            this.strategyCenter = strategyCenter;
        }

        public IStrategyExecutor CreateExecutor_History(StrategyArguments_CodePeriod strategyCodePeriod)
        {
            return new StrategyExecutor_CodePeriod(strategyCenter, strategyCodePeriod);
        }

        public IStrategyExecutor CreateExecutor_History(StrategyArguments_DataPackage strategyArguments)
        {
            return new StrategyExecutor_DataPackage(strategyCenter, strategyArguments);
        }

        public IStrategyExecutor_Multi CreateExecutor_Multi_History(StrategyArguments_CodePeriodList strategyCodePeriodPackage)
        {
            return null;
        }

        public IStrategyExecutor_Multi CreateExecutor_Multi_History(StrategyArguments_CodePeriodList strategyCodePeriodPackage, IStrategyHelper strategyHelper)
        {
            return null;
        }

        public IList<IStrategyExecutor> CreateExecutors_History(StrategyArguments_CodePeriodList strategyCodePeriodPackage)
        {
            List<IStrategyExecutor> executors = new List<IStrategyExecutor>();
            List<ICodePeriod> codePeriods = strategyCodePeriodPackage.CodePeriodPackage.CodePeriods;
            for (int i = 0; i < codePeriods.Count; i++)
            {
                StrategyArguments_CodePeriod argument = new StrategyArguments_CodePeriod(codePeriods[i], strategyCodePeriodPackage.ReferedPeriods, strategyCodePeriodPackage.ForwardPeriod);
                executors.Add(CreateExecutor_History(argument));
            }
            return executors;
        }
    }
}
