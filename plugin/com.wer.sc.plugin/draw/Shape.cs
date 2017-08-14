using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.draw
{
    /// <summary>
    /// SC中画图的各种形状的基类
    /// </summary>
    public interface PriceShape
    {
        /// <summary>
        /// 获得该图形形状
        /// </summary>
        /// <returns></returns>
        PriceShapeType GetShapeType();

        /// <summary>
        /// 获得该图形的颜色
        /// </summary>
        Color Color { get; }
    }
}