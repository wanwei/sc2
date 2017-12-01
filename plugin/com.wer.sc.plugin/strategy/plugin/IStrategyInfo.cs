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
        /// 得到插件所在的Assembly
        /// </summary>
        IStrategyAssembly StrategyAssembly { get; }

        /// <summary>
        /// 是否是错误的策略
        /// </summary>
        bool IsError { get; }

        /// <summary>
        /// 策略的错误信息
        /// </summary>
        string ErrorInfo { get; }

        /// <summary>
        /// 
        /// </summary>
        Type StrategyClassType { get; }

        /// <summary>
        /// 插件ID
        /// </summary>
        string StrategyID { get; }

        /// <summary>
        /// 插件名称
        /// </summary>
        string StrategyName { get; }

        string StrategyDesc { get; }

        string StrategyPath { get; }

        /// <summary>
        /// 得到策略的缺省参数
        /// </summary>
        IParameters Parameters { get; }

        IStrategyData CreateStrategy();
    }
}
