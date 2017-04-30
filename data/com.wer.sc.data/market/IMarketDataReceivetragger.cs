using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.market
{
    /// <summary>
    /// 市场数据接收器接口
    /// </summary>
    public interface IMarketDataReceiveTragger
    {
        /// <summary>
        /// 交易日改变
        /// </summary>
        void TradingDayChanged(int currentTradingDay);

        /// <summary>
        /// tick数据接收后触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="tickData"></param>
        void TickDataReceived(object sender, ITickData tickData);

    }
}
