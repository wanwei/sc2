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
    public class StrategyExecutorFactory : IStrategyExecutorFactory
    {
        private IDataCenter dataCenter;

        public StrategyExecutorFactory(IDataCenter dataCenter)
        {
            this.dataCenter = dataCenter;
        }

        public IStrategyExecutor CreateExecutor_History(StrategyArguments_DataPackage strategyArguments)
        {
            return new StrategyExecutor_DataPackage(strategyArguments);
        }

        public IStrategyExecutor CreateExecutor_History(StrategyArguments_DataPackage strategyArguments, IStrategyHelper strategyHelper)
        {
            return new StrategyExecutor_DataPackage(strategyArguments, strategyHelper);
        }

        public IStrategyExecutor CreateExecutor_History(StrategyArguments_DataPackages strategyArguments)
        {
            //return new StrategyExecutor_DataPackages(strategyArguments);
            return null;
        }

        public IStrategyExecutor CreateExecutor_History(StrategyArguments_DataPackages strategyArguments, IStrategyHelper strategyHelper)
        {
            //return new StrategyExecutor_DataPackages(strategyArguments, strategyHelper);
            return null;
        }

        public IStrategyExecutor CreateExecutor_History(StrategyArguments_CodePeriod strategyCodePeriod)
        {
            return new StrategyExecutor_CodePeriod(dataCenter, strategyCodePeriod);
        }

        public IStrategyExecutor CreateExecutor_History(StrategyArguments_CodePeriod strategyCodePeriod, IStrategyHelper strategyHelper)
        {
            return new StrategyExecutor_CodePeriod(dataCenter, strategyCodePeriod, strategyHelper);
        }

        public IStrategyExecutor CreateExecutor_History(StrategyArguments_CodePeriodPackage strategyCodePeriodPackage)
        {
            return new StrategyExecutor_CodePeriodPackage(dataCenter, strategyCodePeriodPackage);
        }

        public IStrategyExecutor CreateExecutor_History(StrategyArguments_CodePeriodPackage strategyCodePeriodPackage, IStrategyHelper strategyHelper)
        {
            return new StrategyExecutor_CodePeriodPackage(dataCenter, strategyCodePeriodPackage, strategyHelper);
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
