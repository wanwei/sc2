using com.wer.sc.data;
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
    /// 策略绘图
    /// 该接口对应策略绘制的一张图形
    /// </summary>
    public interface IStrategyGraphic : IXmlExchange
    {
        /// <summary>
        /// 得到图形标识
        /// 可以是K线图或分时图
        /// </summary>
        IDataKey DataKey { get; }

        /// <summary>
        /// 策略绘制的标题
        /// </summary>
        IStrategyGraphicTitle Title { get; }

        /// <summary>
        /// 图形上绘制的所有图形
        /// </summary>
        IPriceShapeContainer Shapes { get; }
    }
}