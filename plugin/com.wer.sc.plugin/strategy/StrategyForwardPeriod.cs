using com.wer.sc.data.forward;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.data;

namespace com.wer.sc.strategy
{
    public class StrategyForwardPeriod : ForwardPeriod
    {
        public StrategyForwardPeriod() : base()
        {

        }

        public StrategyForwardPeriod(bool isTickForward, KLinePeriod klineForwardPeriod) : base(isTickForward, klineForwardPeriod)
        {
        }
    }
}
