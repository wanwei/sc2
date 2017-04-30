using com.wer.sc.plugin;
using com.wer.sc.plugin.market;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.market
{
    /// <summary>
    /// 市场接口，用来接收数据和进行交易
    /// </summary>
    public interface IMarket
    {
        /// <summary>
        /// 得到市场数据接口
        /// </summary>
        IMarketData MarketData { get; }

        /// <summary>
        /// 得到市场交易接口
        /// </summary>
        IMarketTrader MarketTrader { get; }
    }
}