using com.wer.sc.graphic.shape;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.graphic
{
    public interface IGraphicDrawer_PriceRect : IGraphicDrawer
    {
        /// <summary>
        /// 设置价格块
        /// </summary>
        PriceRectangle PriceRect { get; set; }

        /// <summary>
        /// 绘制形状
        /// </summary>
        /// <param name="priceShape"></param>
        void DrawPriceShape(IPriceShape priceShape);

        /// <summary>
        /// 得到所有形状
        /// </summary>
        /// <returns></returns>
        List<IPriceShape> GetAllShapes();

        /// <summary>
        /// 删除形状
        /// </summary>
        /// <param name="shape"></param>
        void RemoveShape(IPriceShape shape);

        /// <summary>
        /// 清除所有图形
        /// </summary>
        void ClearPriceShapes();
    }
}