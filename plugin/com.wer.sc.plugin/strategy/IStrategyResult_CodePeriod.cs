using com.wer.sc.data.codeperiod;
using com.wer.sc.data.datapackage;
using com.wer.sc.data.forward;
using com.wer.sc.graphic.shape;
using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy
{
    /// <summary>
    /// 单支股票的计算结果
    /// </summary>
    public interface IStrategyResult_CodePeriod : IXmlExchange_Multi, IDataStore
    {
        /// <summary>
        /// 单个代码周期
        /// </summary>
        ICodePeriod CodePeriod { get; }

        /// <summary>
        /// 回测前进的周期
        /// </summary>
        StrategyForwardPeriod ForwardPeriod { get; }

        /// <summary>
        /// 回测引用的周期
        /// </summary>
        StrategyReferedPeriods ReferedPeriods { get; }

        /// <summary>
        /// 策略在该代码周期内画的所有形状
        /// </summary>
        IStrategyDrawer StrategyDrawer { get; }

        /// <summary>
        /// 策略交易部分
        /// </summary>
        IStrategyTrader StrategyTrader { get; }
    }
}