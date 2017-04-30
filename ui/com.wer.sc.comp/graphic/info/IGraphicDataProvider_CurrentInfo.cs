using com.wer.sc.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.comp.graphic.info
{
    public interface IGraphicDataProvider_CurrentInfo
    {
        CurrentInfo GetCurrentInfo();

        ITickData GetCurrentTickData();

        int CurrentTickIndex { get; }

        IGraphicOperator_CurrentInfo GetOperator();
    }
}
