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
    public interface IStrategyOperator
    {
        /// <summary>
        /// 得到
        /// </summary>
        IDrawHelper DrawHelper { get; }

        IStrategyTrader_Code Trader { get; }

        /// <summary>
        /// 添加策略执行结果
        /// </summary>
        /// <param name="strategyResult"></param>
        //void AddStrategyResult(IStrategyResult_Single strategyResult);

        void AddStrategyResult(string code, double time, string name, string desc);
    }
}