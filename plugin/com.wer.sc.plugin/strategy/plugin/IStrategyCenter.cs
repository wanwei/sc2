using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy
{
    /// <summary>
    /// 策略中心，接口功能：
    /// 1.获得所有策略
    /// 2.创建策略执行器
    /// </summary>
    public interface IStrategyCenter
    {
        /// <summary>
        /// 策略管理器
        /// </summary>
        /// <returns></returns>
        IStrategyAssemblyMgr GetStrategyMgr();

        /// <summary>
        /// 策略回测执行器
        /// </summary>
        /// <returns></returns>
        IStrategyExecutorFactory GetStrategyExecutorFactory();
    }
}