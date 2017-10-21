using com.wer.sc.data.forward;
using com.wer.sc.utils.param;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy
{
    public interface IStrategyReport
    {
        string Code { get; }

        int StartDate { get; }

        int EndDate { get; }

        ForwardPeriod ForwardPeriod { get; }

        IParameters Parameters { get; }

        IStrategyQueryResult StrategyResult { get; }

        IStrategyTrader StrategyTrader { get; }
    }
}
