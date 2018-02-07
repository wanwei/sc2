using com.wer.sc.data;
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
    public class StrategyExecutorFactory : IStrategyExecutorFactory_History
    {
        private IDataCenter dataCenter;

        public StrategyExecutorFactory(IDataCenter dataCenter)
        {
            this.dataCenter = dataCenter;
        }

        public IStrategyExecutor CreateExecutor(string code, int startDate, int endDate, StrategyReferedPeriods referedPeriods, StrategyForwardPeriod forwardPeriod)
        {
            return CreateExecutor(code, startDate, endDate, referedPeriods, forwardPeriod, null);
        }

        public IStrategyExecutor CreateExecutor(string code, int startDate, int endDate, StrategyReferedPeriods referedPeriods, StrategyForwardPeriod forwardPeriod, IStrategyHelper strategyOperator)
        {
            IDataPackage_Code dataPackage_Code = dataCenter.DataPackageFactory.CreateDataPackage_Code(code, startDate, endDate, 200, 0);
            return CreateExecutorByDataPackage(dataPackage_Code, referedPeriods, forwardPeriod);
        }

        public IStrategyExecutor CreateExecutorByDataPackage(IDataPackage_Code dataPackage, StrategyReferedPeriods referedPeriods, StrategyForwardPeriod forwardPeriod)
        {
            return new StrategyExecutor_History(dataPackage, referedPeriods, forwardPeriod);
        }

        public IStrategyExecutor CreateExecutorByDataPackage(IDataPackage_Code dataPackage, StrategyReferedPeriods referedPeriods, StrategyForwardPeriod forwardPeriod, IStrategyHelper strategyOperator)
        {
            return new StrategyExecutor_History(dataPackage, referedPeriods, forwardPeriod, strategyOperator);
        }
    }

    /// <summary>
    /// 策略的执行参数
    /// </summary>
    //public interface StrategyRunnerArgument
    //{
    //    /// <summary>
    //    /// 获得
    //    /// </summary>
    //    /// <returns></returns>
    //    StrategyReferedPeriods GetReferedPeriods();

    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    /// <returns></returns>
    //    ForwardPeriod GetForwardPeriod();

    //    event DelegateOnStrategyRedraw Redraw;
    //}

    //public delegate void DelegateOnStrategyRedraw(object sender, StrategyHelper strategyHelper);
}
