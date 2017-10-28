using com.wer.sc.strategy;
using com.wer.sc.strategy.draw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.ui.comp
{
    /// <summary>
    /// 组件策略
    /// </summary>
    public interface ICompChartStrategy
    {
        IDrawOperator DrawHelper { get; }

        /// <summary>
        /// 绑定一个策略
        /// </summary>
        /// <param name="strategy"></param>
        void BindStrategy(IStrategy strategy);

        /// <summary>
        /// 得到策略
        /// </summary>
        IStrategy Strategy { get; }

    }
}
