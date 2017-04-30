using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data
{
    /// <summary>
    /// Tick数据点，表示一条Tick数据
    /// </summary>
    public interface ITickBar
    {
        /// <summary>
        /// 得到该Tick对应的股票或期货代码
        /// </summary>
        string Code { get; }

        /// <summary>
        /// 得到该Tick发生的时间
        /// </summary>
        double Time { get; }

        /// <summary>
        /// 得到该Tick的交易价格
        /// </summary>
        float Price { get; }

        /// <summary>
        /// 得到该Tick的交易量
        /// </summary>
        int Mount { get; }

        /// <summary>
        /// 得到今日从开盘到现在为止的总成交量
        /// </summary>
        int TotalMount { get; }

        /// <summary>
        /// 得到该Tick后持仓增加或是减少的量
        /// </summary>
        int Add { get; }

        /// <summary>
        /// 得到该Tick的买一价格
        /// </summary>
        float BuyPrice { get; }

        /// <summary>
        /// 得到该Tick的买一量
        /// </summary>
        int BuyMount { get; }

        /// <summary>
        /// 得到该Tick的卖一价格
        /// </summary>
        float SellPrice { get; }

        /// <summary>
        /// 得到该Tick的卖一量
        /// </summary>
        int SellMount { get; }

        /// <summary>
        /// 得到该Tick时的持仓
        /// </summary>
        int Hold { get; }

        /// <summary>
        /// 得到该Tick是买OR卖
        /// </summary>
        Boolean IsBuy { get; }
    }
}
