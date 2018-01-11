using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy
{
    /// <summary>
    /// 策略数据接口
    /// 负责获得策略及策略信息
    /// </summary>
    public interface IStrategyData
    {
        /// <summary>
        /// 获得策略
        /// 创建IStrategyData接口的时候Strategy并不会被初始化
        /// 第一次获取的时候被初始化
        /// </summary>
        IStrategy Strategy { get; }

        /// <summary>
        /// 刷新策略
        /// </summary>
        void RefreshStrategy();

        /// <summary>
        /// 获得策略信息
        /// </summary>
        IStrategyInfo StrategyInfo { get; }
    }
}
