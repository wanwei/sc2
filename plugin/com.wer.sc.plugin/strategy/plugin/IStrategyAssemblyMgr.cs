using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy
{
    /// <summary>
    /// 策略包管理器
    /// 负责获取已加载的策略包，或重新加载已有的策略包
    /// </summary>
    public interface IStrategyAssemblyMgr
    {
        /// <summary>
        /// 得到所有的策略包
        /// </summary>
        /// <returns></returns>
        IList<IStrategyAssembly> GetAllStrategyAssemblies();

        /// <summary>
        /// 根据名称得到一个策略包
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <returns></returns>
        IStrategyAssembly GetStrategyAssembly(String assemblyName);

        /// <summary>
        /// 刷新所有策略包（重新装载所有策略）
        /// </summary>
        void Refresh();

        /// <summary>
        /// 刷新指定策略包
        /// </summary>
        /// <param name="strategyAssembly"></param>
        void Refresh(IStrategyAssembly strategyAssembly);
    }
}
