using com.wer.sc.data.codeperiod;
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
        /// 根据股票或期货代码，起止时间创建
        /// </summary>
        /// <param name="strategyCodePeriod"></param>
        /// <returns></returns>
        IStrategyExecutor CreateExecutor_History(StrategyArguments_CodePeriod strategyCodePeriod);

        /// <summary>
        /// 创建一个策略执行器
        /// 根据已有数据创建
        /// </summary>
        /// <param name="strategyDataPackage"></param>
        /// <returns></returns>
        IStrategyExecutor CreateExecutor_History(StrategyArguments_DataPackage strategyDataPackage);

        //IStrategyExecutor CreateExecutor_History(StrategyArguments_DataPackageLink strategyDataPackage);

        /// <summary>
        /// 创建一个多重策略执行器
        /// 根据股票或期货代码，起止时间创建
        /// </summary>
        /// <param name="strategyCodePeriodList"></param>
        /// <returns></returns>
        IStrategyExecutor_Multi CreateExecutor_Multi_History(StrategyArguments_CodePeriodList strategyCodePeriodList);

        /// <summary>
        /// 创建多个策略执行器
        /// 根据股票或期货代码，起止时间创建
        /// </summary>
        /// <param name="strategyCodePeriodList"></param>
        /// <returns></returns>
        IList<IStrategyExecutor> CreateExecutors_History(StrategyArguments_CodePeriodList strategyCodePeriodList);
    }
}