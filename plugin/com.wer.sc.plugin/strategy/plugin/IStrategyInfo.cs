using com.wer.sc.utils.param;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy
{
    /// <summary>
    /// 策略信息接口
    /// </summary>
    public interface IStrategyInfo
    {
        /// <summary>
        /// 获得策略类名
        /// </summary>
        string ClassName { get; }

        /// <summary>
        /// 策略名称，strategyconfig里配置的name
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 策略描述，strategyconfig里配置的desc
        /// </summary>
        string Description { get; }

        /// <summary>
        /// 是否是错误的策略
        /// </summary>
        bool IsError { get; }

        /// <summary>
        /// 策略的错误信息
        /// </summary>
        string ErrorInfo { get; }

        string StrategyPath { get; }

        /// <summary>
        /// 得到策略的缺省参数，在strategyconfig里配置的参数
        /// </summary>
        IParameters Parameters { get; }

        /// <summary>
        /// 获得实现策略的type，如果IsError为true，则返回空
        /// </summary>
        Type StrategyClassType { get; }

        /// <summary>
        /// 得到插件所在的Assembly
        /// </summary>
        IStrategyAssembly StrategyAssembly { get; }

        /// <summary>
        /// 创建策略数据
        /// </summary>
        /// <returns></returns>
        IStrategyData CreateStrategyData();

        /// <summary>
        /// 创建策略
        /// </summary>
        /// <returns></returns>
        IStrategy CreateStrategy();
    }
}
