using com.wer.sc.comp.graphic.timeline;
using com.wer.sc.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.comp.graphic
{
    public class GraphicDataFactory
    {
        public static IGraphicData_Candle CreateGraphicData_Candle(IKLineData klineData, int startIndex, int endIndex)
        {
            return new GraphicData_Candle(klineData, startIndex, endIndex);
        }

        public static IGraphicData_TimeLine CreateGraphicData_TimeLine(ITimeLineData timeLineData)
        {
            return new GraphicData_TimeLine(timeLineData);
        }

        public static IGraphicData_Tick CreateGraphicData_Tick(IKLineData klineData, int startIndex, int endIndex)
        {
            return null;
        }

    }
}
