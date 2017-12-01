using com.wer.sc.data.forward;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.data;

namespace com.wer.sc.strategy
{
    public class StrategyOnBarInfo : ForwardOnbar_Info, IStrategyOnBarInfo
    {
        public StrategyOnBarInfo(ForwardOnbar_Info forwardOnBar) : this(forwardOnBar.KLineData, forwardOnBar.BarPos)
        {

        }

        public StrategyOnBarInfo(IKLineData_Extend klineData, int finishedBarPos) : base(klineData, finishedBarPos)
        {
        }
    }
}
