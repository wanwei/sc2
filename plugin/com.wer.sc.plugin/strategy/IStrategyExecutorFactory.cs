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
        /// <param name="arguments"></param>
        /// <returns></returns>
        IStrategyExecutor_Single CreateExecutor_History(StrategyArguments_CodePeriod arguments);

        /// <summary>
        /// 创建一个策略执行器
        /// 根据已有数据创建
        /// </summary>
        /// <param name="arguments"></param>
        /// <returns></returns>
        IStrategyExecutor_Single CreateExecutor_History(StrategyArguments_DataPackage arguments);

        /// <summary>
        /// 创建一个多重策略执行器
        /// 根据股票或期货代码，起止时间创建
        /// </summary>
        /// <param name="arguments"></param>
        /// <returns></returns>
        IStrategyExecutor_Multi CreateExecutor_Multi_History(StrategyArguments_CodePeriodList arguments);

        /// <summary>
        /// 创建多个策略执行器
        /// 根据股票或期货代码，起止时间创建
        /// </summary>
        /// <param name="arguments"></param>
        /// <returns></returns>
        IList<IStrategyExecutor_Single> CreateExecutors_History(StrategyArguments_CodePeriodList arguments);
    }
}