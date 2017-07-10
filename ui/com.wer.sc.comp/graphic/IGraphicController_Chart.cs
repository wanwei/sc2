using com.wer.sc.comp.graphic.info;
using com.wer.sc.comp.graphic.timeline;
using com.wer.sc.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.comp.graphic
{
    /// <summary>
    /// 图表
    /// </summary>
    public interface IGraphicController_Chart:IGraphicDrawer
    {
        /// <summary>
        /// 显示蜡烛图
        /// </summary>
        /// <param name="klineData"></param>
        void ShowGraphic_Candle(IKLineData klineData);

        /// <summary>
        /// 显示分时线
        /// </summary>
        /// <param name="timeLineData"></param>
        void ShowGraphic_TimeLine(ITimeLineData timeLineData);

        /// <summary>
        /// 显示闪电线
        /// </summary>
        /// <param name="tickData"></param>
        void ShowGraphic_Tick(ITickData tickData);

        /// <summary>
        /// 得到当前显示的图形类型
        /// </summary>
        /// <returns></returns>
        GraphicType GetGraphicType();
    }

    public enum GraphicType
    {
        Candle,

        Real,

        Tick
    }
}