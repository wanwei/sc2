using com.wer.sc.data;
using com.wer.sc.strategy.draw;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy
{
    /// <summary>
    /// 策略操作接口
    /// </summary>
    public interface IStrategyOperator
    {
        /// <summary>
        /// 得到策略画图接口
        /// </summary>
        IDrawOperator DrawOperator { get; }

        /// <summary>
        /// 得到策略的交易接口
        /// </summary>
        IStrategyTrader_Code Trader { get; }

        void AddStrategyResult(string code, double time, string name, string desc);
    }
}