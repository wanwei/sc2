using com.wer.sc.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.comp.graphic.candle
{
    public interface GraphicOperator_Candle
    {
        void ChangeData(IKLineData klineData);

        void ChangeData(String code, int startDate, int endDate, KLinePeriod period);
    }
}
