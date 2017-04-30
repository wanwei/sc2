using com.wer.sc.comp.graphic.info;
using com.wer.sc.comp.graphic.real;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.comp.graphic.main
{
    public interface IGraphicDataProvider_Main
    {
        IGraphicDataProvider_Candle DataProvider_Candle { get; }

        IGraphicDataProvider_Real DataProvider_Real { get; }

        IGraphicDataProvider_CurrentInfo DataProvider_Info { get; }

        IGraphicOperator_Main GetOperator();
    }
}
