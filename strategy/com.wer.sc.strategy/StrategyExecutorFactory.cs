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
    public class StrategyExecutorFactory
    {
        public static IStrategyExecutor CreateHistoryExecutor(IDataPackage_Code dataPackage, ForwardReferedPeriods referedPeriods, ForwardPeriod forwardPeriod, StrategyOperator strategyHelper)
        {
            return new StrategyExecutor_History(dataPackage, referedPeriods, forwardPeriod, strategyHelper);
        }

        //public static IStrategyExecutor CreateStrategyRunner(IDataPackage dataPackage, StrategyRunnerArgument arg)
        //{
        //    return new StrategyExecutor_History(dataPackage, arg);
        //}
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
