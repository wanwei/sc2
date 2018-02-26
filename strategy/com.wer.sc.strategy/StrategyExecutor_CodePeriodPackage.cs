using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.data;
using com.wer.sc.data.datapackage;

namespace com.wer.sc.strategy
{
    public class StrategyExecutor_CodePeriodPackage 
    {
        private IDataCenter dataCenter;
        private StrategyArguments_CodePeriodPackage strategyCodePeriodPackage;
        private IStrategyHelper strategyHelper;

        public StrategyExecutor_CodePeriodPackage(IDataCenter dataCenter, StrategyArguments_CodePeriodPackage strategyCodePeriodPackage)
        {
            this.dataCenter = dataCenter;
            this.strategyCodePeriodPackage = strategyCodePeriodPackage;
        }

        public StrategyExecutor_CodePeriodPackage(IDataCenter dataCenter, StrategyArguments_CodePeriodPackage strategyCodePeriodPackage, IStrategyHelper strategyHelper) : this(dataCenter, strategyCodePeriodPackage)
        {
            this.strategyHelper = strategyHelper;
        }

        /// <summary>
        /// 设置和获取需要执行的策略
        /// </summary>
        /// <param name="strategy"></param>
        public IStrategy Strategy { get; set; }

        public ICodePeriod CodePeriod
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// 设置希望要执行的策略包
        /// </summary>
        /// <param name="strategyPackage"></param>
        //void SetStrategyPackage(IStrategyPackage strategyPackage);

        /// <summary>
        /// 执行策略
        /// 该方法会在一个新的线程里执行策略
        /// </summary>
        public void Execute()
        {

        }

        /// <summary>
        /// 执行策略
        /// </summary>
        public void Run()
        {

        }

        /// <summary>
        /// 取消当前执行的策略
        /// </summary>
        public void Cancel()
        {

        }

        /// <summary>
        /// 执行完每一个bar
        /// </summary>
        public event StrategyBarFinished OnBarFinished;

        /// <summary>
        /// 
        /// </summary>
        public event StrategyDayFinished OnDayFinished;

        /// <summary>
        /// 执行完
        /// </summary>
        public event StrategyFinished OnFinished;
        public event StrategyStart OnStart;

        /// <summary>
        /// 得到策略执行报告，策略执行完才能获得
        /// </summary>
        public IStrategyResult StrategyReport { get; }

        /// <summary>
        /// 得到执行器相关信息
        /// </summary>
        public IStrategyExecutorInfo StrategyExecutorInfo { get; }
    }
}
