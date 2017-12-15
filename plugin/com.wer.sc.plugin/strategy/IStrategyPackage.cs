using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy
{
    /// <summary>
    /// 策略包
    /// </summary>
    public interface IStrategyPackage
    {
        List<IStrategyForTrade> Strategies { get; }
    }

    public interface IStrategyForTrade
    {
        IStrategy TradeStrategy { get; set; }

        List<IStrategy> FilterStrategy { get; }
    }
}
