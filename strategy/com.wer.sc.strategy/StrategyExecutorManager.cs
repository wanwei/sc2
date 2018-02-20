using com.wer.sc.data.datapackage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy
{
    /// <summary>
    /// 策略运行管理器
    /// </summary>
    public class StrategyExecutorManager
    {
        public StrategyExecutorManager()
        {

        }

    }

    public abstract class StrategyExecuteInfo_Abstract
    {
        private StrategyExecutor_History executor;

        public StrategyExecuteInfo_Abstract(StrategyExecutor_History executor)
        {
            this.executor = executor;
            this.executor.DayFinished += Executor_DayFinished;
        }

        private void Executor_DayFinished(IStrategy strategy)
        {
            
        }

        /// <summary>
        /// 策略
        /// </summary>
        public IStrategy Strategy;

        /// <summary>
        /// 策略引用的周期
        /// </summary>
        public StrategyReferedPeriods ReferedPeriods;

        /// <summary>
        /// 策略的前进周期
        /// </summary>
        public StrategyForwardPeriod ForwardPeriod;

        /// <summary>
        /// 总共的日子
        /// </summary>
        public int TotalDays;

        public int CurrentDay;


    }

    /// <summary>
    /// 策略执行信息
    /// </summary>
    public class StrategyExecuteInfo_Normal
    {
        //public CodePackageInfo_Code CodePackageInfo;
    }
}