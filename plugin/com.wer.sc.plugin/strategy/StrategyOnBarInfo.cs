using com.wer.sc.data.forward;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.data;

namespace com.wer.sc.strategy
{
    public class StrategyOnBarInfo : ForwardOnbar_Info
    {
        public StrategyOnBarInfo(IKLineData klineData, int finishedBarPos) : base(klineData, finishedBarPos)
        {
        }
    }
}
