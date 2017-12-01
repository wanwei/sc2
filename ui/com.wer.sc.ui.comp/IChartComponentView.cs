using com.wer.sc.comp.graphic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.ui.comp
{
    public interface IChartComponentView
    {
        /// <summary>
        /// 刷新chart
        /// </summary>
        void PaintChart();

        IGraphicData_Candle GraphicData_Candle { get; }

    }
}
