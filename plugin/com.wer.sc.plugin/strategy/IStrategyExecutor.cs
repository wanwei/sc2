using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy
{
    /// <summary>
    /// 策略执行器
    /// 该接口负责单支股票或期货品种在一段时间内的策略执行
    /// 
    /// 一个策略执行器创建后只能执行一次，一旦执行后就不能被再次执行
    /// </summary>
    public interface IStrategyExecutor : IStrategyExecutor_Event
    {
        /// <summary>
        /// 设置和获取需要执行的策略
        /// </summary>
        /// <param name="strategy"></param>
        IStrategy Strategy { get; set; }

        /// <summary>
        /// 执行策略
        /// 该方法会在一个新的线程里执行策略
        /// </summary>
        void Execute();

        /// <summary>
        /// 执行策略
        /// </summary>
        void Run();

        /// <summary>
        /// 取消当前执行的策略
        /// </summary>
        void Cancel();

        /// <summary>
        /// 策略执行器的运行状态
        /// 还未开始，正在执行，被取消，执行完毕
        /// </summary>
        StrategyExecutorState State { get; }

        /// <summary>
        /// 该策略执行器引用的数据周期
        /// </summary>
        StrategyReferedPeriods ReferedPeriods { get; }

        /// <summary>
        /// 该策略的前进周期
        /// </summary>
        StrategyForwardPeriod ForwardPeriod { get; }

        /// <summary>
        /// 策略执行时的交易设定
        /// </summary>
        StrategyTraderSetting TraderSetting { get; }

        /// <summary>
        /// 策略执行帮助接口
        /// </summary>
        IStrategyHelper StrategyHelper { get; }

        /// <summary>
        /// 得到策略执行结果，策略执行结束后生成
        /// </summary>
        IStrategyResult StrategyResult { get; }

        /// <summary>
        /// 得到执行时相关信息
        /// </summary>
        IStrategyExecutorInfo StrategyExecutorInfo { get; }
    }   
}