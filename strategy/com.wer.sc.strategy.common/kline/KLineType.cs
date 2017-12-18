using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy.common.kline
{
    public enum KLineType
    {
        /// <summary>
        /// 弃婴
        /// </summary>
        AbandonedBaby,

        /// <summary>
        /// 空头吞噬
        /// </summary>
        BearishEngulfing,

        /// <summary>
        /// 多头吞噬
        /// </summary>
        BullishEngulfing,

        /// <summary>
        /// 
        /// </summary>
        CounterAttackLines,

        /// <summary>
        /// 十字
        /// </summary>
        Doji,

        /// <summary>
        /// 锤子模式，下影线较长
        /// </summary>
        Hammer,

        /// <summary>
        /// 射击
        /// </summary>
        Shoot
    }
}