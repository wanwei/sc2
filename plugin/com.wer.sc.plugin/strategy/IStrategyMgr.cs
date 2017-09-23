using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy
{
    /// <summary>
    /// 策略管理器
    /// 策略的管理和插件管理不一样，策略的管理基于Assembly
    /// 所有策略的创建，浏览都是都是基于策略包
    /// </summary>
    public interface IStrategyMgr
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
        /// 根据策略名称查找策略，模糊查找
        /// </summary>
        /// <param name="strategyName"></param>
        /// <returns></returns>
        IList<IStrategyAssembly> SearchStrategyInfo(String strategyName);

        void Refresh();

        void Refresh(IStrategyAssembly strategyAssembly);
    }
}
