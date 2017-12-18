using com.wer.sc.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy.common.kline
{

    /// <summary>
    /// 锤头K线
    ///     |||
    ///      |
    ///      |
    /// </summary>
    public class KLine_Hammer : KLineAbstract
    {
        public KLine_Hammer(IKLineData klineData, int endIndex) : base(klineData, endIndex)
        {
        }

        public override KLineType GetKLineType()
        {
            return KLineType.Hammer;
        }

        public override bool Check(IKLineData klineData, int index)
        {
            return false;
        }
    }
}