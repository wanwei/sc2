using com.wer.sc.data;
using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.graphic.shape
{
    /// <summary>
    /// 图形容器
    /// </summary>
    public interface IPriceShapeContainer : IXmlExchange
    {
        /// <summary>
        /// 得到图形标识，默认返回空
        /// 只有K线图和分时图会返回对应的Key
        /// </summary>
        IDataKey GraphicKey { get; }

        void AddPriceShape(IPriceShape priceShape);

        void RemovePriceShape(IPriceShape priceShape);

        IList<IPriceShape> GetAllPriceShapes();

        void Clear();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="priceRectangle"></param>
        void Paint(PriceRectangle priceRectangle);
    }
}