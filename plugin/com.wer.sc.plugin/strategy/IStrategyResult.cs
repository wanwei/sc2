using com.wer.sc.data.forward;
using com.wer.sc.utils.param;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy
{
    /// <summary>
    /// 策略执行结果
    /// </summary>
    public interface IStrategyResult
    {
        string Code { get; }

        int StartDate { get; }

        int EndDate { get; }

        ForwardPeriod ForwardPeriod { get; }

        IParameters Parameters { get; }

        IStrategyQueryResultManager StrategyResult { get; }

        IStrategyTrader StrategyTrader { get; }
    }
}
