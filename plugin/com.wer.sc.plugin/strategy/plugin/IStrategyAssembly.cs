using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy
{
    /// <summary>
    /// 该接口表示了一个策略包，一个策略包对应dotnet的一个Assembly
    /// 策略包里面可以包含了多个策略
    /// </summary>
    public interface IStrategyAssembly
    {
        /// <summary>
        /// 得到策略包的名称
        /// </summary>
        string AssemblyName { get; }

        string Name { get; }

        string Description { get; }

        /// <summary>
        /// 得到策略包的完整路径
        /// </summary>
        string FullPath { get; }

        /// <summary>
        /// 得到策略包所有策略信息
        /// </summary>
        /// <returns></returns>
        List<IStrategyInfo> GetAllStrategies();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strategyId"></param>
        /// <returns></returns>
        IStrategyInfo GetStrategy(String strategyId);

        /// <summary>
        /// 得到所有的顶级目录
        /// 对于C#插件，目录就是命名空间
        /// 对于python插件，目录是现实中的目录
        /// </summary>
        /// <returns></returns>
        IList<string> GetRootPath();

        /// <summary>
        /// 得到所有的子命名空间，如果传入空或空字符串，则返回第一层的命名空间
        /// </summary>
        /// <param name="parentPath"></param>
        /// <returns></returns>
        IList<string> GetSubPath(string parentPath);

        /// <summary>
        /// 得到所有的子策略
        /// </summary>
        /// <param name="parentPath"></param>
        /// <returns></returns>
        IList<IStrategyInfo> GetSubStrategies(string parentPath);

        /// <summary>
        /// 创建一个新的插件对象实例
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IStrategy CreateStrategyObject(string strategyId);

        /// <summary>
        /// 创建一个新的插件对象实例
        /// </summary>
        /// <param name="strategyInfo"></param>
        /// <returns></returns>
        IStrategy CreateStrategyObject(IStrategyInfo strategyInfo);
    
        /// <summary>
        /// 根据策略名称查找策略，模糊查找
        /// </summary>
        /// <param name="strategyName"></param>
        /// <returns></returns>
        IList<IStrategyAssembly> SearchStrategyInfo(String strategyName);
    }
}
