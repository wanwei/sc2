using com.wer.sc.graphic.info;
using com.wer.sc.graphic.timeline;
using com.wer.sc.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.graphic
{
    public class GraphicDataFactory
    {
        public static IGraphicData_Candle CreateGraphicData_Candle()
        {
            return new GraphicData_Candle();
        }

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

        public static IGraphicData_CurrentInfo CreateGraphicData_CurrentInfo(CurrentInfo currentInfo, ITickData tickData)
        {
            return new GraphicData_CurrentInfo(currentInfo, tickData);
        }
    }
}
