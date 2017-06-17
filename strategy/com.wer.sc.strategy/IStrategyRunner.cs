using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy
{
    /// <summary>
    /// 策略执行器
    /// </summary>
    public interface IStrategyRunner
    {
        /// <summary>
        /// 设置要执行的策略
        /// </summary>
        /// <param name="strategy"></param>
        void SetStrategy(IStrategy strategy);

        void Run();
    }
}