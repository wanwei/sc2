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
        /// <returns></returns>
        IStrategyExecutor CreateExecutor(string code);

        IStrategyExecutor CreateExecutor(string code, int startDate, int endDate);

        IStrategyExecutor CreateExecutor(string code, int startDate, int endDate, IStrategyOperator strategyHelper);

        IStrategyExecutor CreateExecutor(string code, int startDate, int endDate, StrategyReferedPeriods referedPeriods, StrategyForwardPeriod forwardPeriod, IStrategyOperator strategyOperator);

        IStrategyExecutor CreateExecutorByDataPackage(IDataPackage_Code dataPackage);

        IStrategyExecutor CreateExecutorByDataPackage(IDataPackage_Code dataPackage, IStrategyOperator strategyHelper);

        IStrategyExecutor CreateExecutorByDataPackage(IDataPackage_Code dataPackage, StrategyReferedPeriods referedPeriods, StrategyForwardPeriod forwardPeriod, IStrategyOperator strategyOperator);

        IStrategyExecutor CreateExecutorByDataPackage(IDataPackage dataPackage);
    }
}
