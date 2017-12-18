using com.wer.sc.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.graphic
{
    public class GraphicDrawerFactory
    {
        public IGraphicDrawer_PriceRect CreateGraphicDrawer_PriceRect(IGraphicData graphicData)
        {
            if (graphicData is IGraphicData_Candle)
            {
                //return new GraphicDrawer_Candle()
            }
            return null;
        }
    }
}
