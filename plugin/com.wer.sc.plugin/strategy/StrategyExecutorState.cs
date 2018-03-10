using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy
{
    /// <summary>
    /// 策略的执行状态
    /// 已经执行过的策略执行器不能够再次执行
    /// </summary>
    public enum StrategyExecutorState
    {
        /// <summary>
        /// 还没有开始执行
        /// </summary>
        NotStart = 0,

        /// <summary>
        /// 正在运行中
        /// </summary>
        Running = 1,

        /// <summary>
        /// 运行被取消
        /// </summary>
        Canceled = 2,

        /// <summary>
        /// 执行完成
        /// </summary>
        Finished = 3
    }
}