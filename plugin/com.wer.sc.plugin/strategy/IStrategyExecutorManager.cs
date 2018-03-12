using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy
{
    /// <summary>
    /// 策略执行管理器
    /// 
    /// 该接口统一管理服务器上所有正在运行的策略，
    /// 包括本次程序打开后已经运行完的策略
    /// </summary>
    public interface IStrategyExecutorManager
    {
        /// <summary>
        /// 得到正在执行的策略执行器
        /// </summary>
        /// <returns></returns>
        IList<IStrategyExecutor_Single> GetExecutingExecutor();
    }
}