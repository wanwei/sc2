using com.wer.sc.data;
using com.wer.sc.data.forward;
using com.wer.sc.data.navigate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy
{
    public class StrategyReferedPeriods : ForwardReferedPeriods
    {
        public StrategyReferedPeriods() : base()
        {
        }

        public StrategyReferedPeriods(IList<KLinePeriod> usedKLinePeriods, bool useTick, bool useTimeLine) : base(usedKLinePeriods, useTick, useTimeLine)
        {
        }
    }
}
