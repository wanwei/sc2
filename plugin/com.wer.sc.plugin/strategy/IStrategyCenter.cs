using com.wer.sc.data;
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
        IDataCenter BelongDataCenter { get; }

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

        /// <summary>
        /// 得到策略执行管理器
        /// </summary>
        IStrategyExecutorManager StrategyExecutorManager { get; }

        /// <summary>
        /// 得到默认的策略执行池
        /// </summary>
        /// <returns></returns>
        IStrategyExecutorPool GetStrategyExecutorPool();

        /// <summary>
        /// 得到策略结果集保存器
        /// </summary>
        /// <returns></returns>
        IStrategyResultStore StrategyResultStore { get; }
    }
}