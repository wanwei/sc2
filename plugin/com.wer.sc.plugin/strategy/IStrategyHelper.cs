using com.wer.sc.data;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy
{
    /// <summary>
    /// 策略助手
    /// </summary>
    public interface IStrategyHelper
    {
        /// <summary>
        /// 得到策略画图器
        /// </summary>
        IStrategyDrawer Drawer { get; }

        /// <summary>
        /// 得到策略的交易器
        /// </summary>
        IStrategyTrader Trader { get; }

        /// <summary>
        /// 
        /// </summary>
        IStrategyResultManager ResultManager { get; }        
    }
}