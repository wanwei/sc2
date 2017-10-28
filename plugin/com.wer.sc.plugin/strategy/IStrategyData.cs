using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy
{
    public interface IStrategyData
    {
        IStrategy Strategy { get; }

        IStrategyInfo StrategyInfo { get; }
    }
}
