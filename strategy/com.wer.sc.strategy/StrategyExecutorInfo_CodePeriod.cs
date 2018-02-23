using com.wer.sc.data.datapackage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.data;

namespace com.wer.sc.strategy
{
    /// <summary>
    /// 
    /// </summary>
    public class StrategyExecutorInfo_CodePeriod : IStrategyExecutorInfo
    {
        private IList<int> allTradingDays;

        private int currentTradingDay;

        private int currentTradingDayIndex;

        private IStrategyExecutor executor;

        private ICodePeriod codePeriod;

        public StrategyExecutorInfo_CodePeriod(IStrategyExecutor executor, ICodePeriod codePeriod)
        {
            this.executor = executor;
            this.codePeriod = codePeriod;
        }

        /// <summary>
        /// 
        /// </summary>
        public ICodePeriod CodePeriod
        {
            get
            {
                return this.codePeriod;
            }
        }
        public IStrategy Strategy
        {
            get
            {
                return executor.Strategy;
            }
        }

        /// <summary>
        /// 总共天数
        /// </summary>
        public int TotalDayCount { get; }

        /// <summary>
        /// 当前执行的日期
        /// </summary>
        public int CurrentDay { get; }

        /// <summary>
        /// 当前执行的日期索引号
        /// </summary>
        public int CurrentDayIndex { get; }

        /// <summary>
        /// 执行是否结束
        /// </summary>
        public bool IsFinished { get; }

        public IKLineData CurrentKLineData
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// 策略执行器引用的周期
        /// </summary>
        /// <returns></returns>
        public StrategyReferedPeriods GetReferedPeriods()
        {
            return null;
        }

        /// <summary>
        /// 策略执行器的前进周期
        /// </summary>
        /// <returns></returns>
        public StrategyForwardPeriod GetForwardPeriod()
        {
            return null;
        }
    }
}
