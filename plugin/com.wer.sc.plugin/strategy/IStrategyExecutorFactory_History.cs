using com.wer.sc.data.datapackage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy
{
    /// <summary>
    /// 历史数据回测执行器创建工厂
    /// </summary>
    public interface IStrategyExecutorFactory_History
    {
        /// <summary>
        /// 创建策略执行器
        /// </summary>
        /// <param name="code"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="referedPeriods"></param>
        /// <param name="forwardPeriod"></param>
        /// <returns></returns>
        IStrategyExecutor CreateExecutor(string code, int startDate, int endDate, StrategyReferedPeriods referedPeriods, StrategyForwardPeriod forwardPeriod);

        IStrategyExecutor CreateExecutor(string code, int startDate, int endDate, StrategyReferedPeriods referedPeriods, StrategyForwardPeriod forwardPeriod, IStrategyHelper strategyOperator);

        /// <summary>
        /// 创建一个策略执行器
        /// </summary>
        /// <param name="dataPackage"></param>
        /// <param name="referedPeriods"></param>
        /// <param name="forwardPeriod"></param>
        /// <returns></returns>
        IStrategyExecutor CreateExecutorByDataPackage(IDataPackage_Code dataPackage, StrategyReferedPeriods referedPeriods, StrategyForwardPeriod forwardPeriod);

        IStrategyExecutor CreateExecutorByDataPackage(IDataPackage_Code dataPackage, StrategyReferedPeriods referedPeriods, StrategyForwardPeriod forwardPeriod, IStrategyHelper strategyOperator);
    }
}