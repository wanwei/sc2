using com.wer.sc.data.datapackage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy
{
    public interface IStrategyExecutorFactory
    {
        /// <summary>
        /// 创建一个策略执行器
        /// </summary>
        /// <param name="strategyCodePeriod"></param>
        /// <returns></returns>
        IStrategyExecutor CreateExecutor_History(StrategyArguments_CodePeriod strategyCodePeriod);

        /// <summary>
        /// 创建一个策略执行器
        /// </summary>
        /// <param name="strategyCodePeriod"></param>
        /// <param name="strategyHelper"></param>
        /// <returns></returns>
        IStrategyExecutor CreateExecutor_History(StrategyArguments_CodePeriod strategyCodePeriod, IStrategyHelper strategyHelper);

        IStrategyExecutor CreateExecutor_History(StrategyArguments_DataPackage strategyDataPackage);

        IStrategyExecutor CreateExecutor_History(StrategyArguments_DataPackage strategyDataPackage, IStrategyHelper strategyHelper);

        IStrategyExecutor CreateExecutor_History(StrategyArguments_DataPackages strategyDataPackage);

        IStrategyExecutor CreateExecutor_History(StrategyArguments_DataPackages strategyDataPackage, IStrategyHelper strategyHelper);

        IStrategyExecutor_Multi CreateExecutor_Multi_History(StrategyArguments_CodePeriodPackage strategyCodePeriodPackage);

        IStrategyExecutor_Multi CreateExecutor_Multi_History(StrategyArguments_CodePeriodPackage strategyCodePeriodPackage, IStrategyHelper strategyHelper);

        IList<IStrategyExecutor> CreateExecutors_History(StrategyArguments_CodePeriodPackage strategyCodePeriodPackage);

        IList<IStrategyExecutor> CreateExecutors_History(StrategyArguments_CodePeriodPackage strategyCodePeriodPackage, IStrategyHelper strategyHelper);
    }
}
