using System.Collections.Generic;

namespace com.wer.sc.strategy
{
    /// <summary>
    /// 策略包信息
    /// </summary>
    public interface IStrategyAssemblyInfo
    {
        /// <summary>
        /// 该Assembly在dotnet中的名称
        /// 如com.wer.sc.strategy.common
        /// </summary>
        string AssemblyName { get; }

        /// <summary>
        /// Assembly在strategyconfig中配置的名称
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Assembly在strategyconfig中配置的名称
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Assembly的完整路径
        /// </summary>
        string FullPath { get; }

        /// <summary>
        /// 得到策略包所有策略信息
        /// </summary>
        /// <returns></returns>
        List<IStrategyInfo> GetAllStrategies();

        /// <summary>
        /// 根据策略的类名获取策略信息
        /// </summary>
        /// <param name="strategyClsName"></param>
        /// <returns></returns>
        IStrategyInfo GetStrategyInfo(string strategyClsName);

        /// <summary>
        /// 获得所有子路径，传入空则获取第一层路径
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        IList<string> GetSubPath(string path);

        /// <summary>
        /// 获得所有子策略，传入空则获得第一层的策略
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        IList<IStrategyInfo> GetSubStrategyInfo(string path);
    }
}