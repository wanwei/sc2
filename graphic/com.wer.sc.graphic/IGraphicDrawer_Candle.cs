using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.graphic
{
    /// <summary>
    /// 
    /// </summary>
    public interface IGraphicDrawer_Candle : IGraphicDrawer
    {
        /// <summary>
        /// 
        /// </summary>
        IGraphicDrawer_PriceRect GraphicDrawer_Candle { get; }

        IGraphicDrawer_PriceRect GraphicDrawer_Mount { get; }
    }
}
