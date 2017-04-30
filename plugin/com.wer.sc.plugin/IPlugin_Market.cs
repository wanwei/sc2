using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin
{
    /// <summary>
    /// 市场插件
    /// </summary>
    public interface IPlugin_Market
    {
        /// <summary>
        /// 提供市场数据接收
        /// </summary>
        IPlugin_MarketData MarketData { get; }

        /// <summary>
        /// 提供市场交易接口
        /// </summary>
        IPlugin_MarketTrader MarketTrader { get; }

        /// <summary>
        /// 提供市场的工具方法
        /// </summary>
        IPlugin_MarketUtils MarketUtils { get; }
    }
}