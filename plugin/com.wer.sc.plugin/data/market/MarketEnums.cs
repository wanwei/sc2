using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.market
{
   



    ///交易阶段类型
    public enum TradingPhaseType : byte
    {
        /// <summary>
        /// 开盘前
        /// </summary>
        BeforeTrading,

        /// <summary>
        /// 非交易
        /// </summary>
        NoTrading,

        /// <summary>
        /// 连续交易
        /// </summary>
        Continuous,

        /// <summary>
        /// 集合竞价报单
        /// </summary>
        AuctionOrdering,

        /// <summary>
        /// 集合竞价价格平衡
        /// </summary>
        AuctionBalance,

        /// <summary>
        /// 集合竞价撮合
        /// </summary>
        AuctionMatch,

        /// <summary>
        /// 收盘
        /// </summary>
        Closed,

        /// <summary>
        /// 停牌时段,参考于LTS
        /// </summary>
        Suspension,

        /// <summary>
        /// 熔断时段,参考于LTS
        /// </summary>
        Fuse,
    };

    public enum OpenCloseType : byte
    {
        Open,
        Close,
        CloseToday,
    };

    public enum OrderType : byte
    {
        Market,
        Stop,
        Limit,
        StopLimit,
        MarketOnClose,
        Pegged,
        TrailingStop,
        TrailingStopLimit,
    };

    public enum OrderStatus : byte
    {
        NotSent,
        PendingNew,
        New,
        Rejected,
        PartiallyFilled,
        Filled,
        PendingCancel,
        Cancelled,
        Expired,
        PendingReplace,
        Replaced,
    };

    /// <summary>
    /// 委托方向
    /// </summary>
    public enum OrderSide : byte
    {
        Buy,
        Sell,
        Unknown,
    };

    /// <summary>
    /// 持仓方向
    /// </summary>
    public enum PositionSide : byte
    {
        Long,
        Short,
    };

    public enum TimeInForce : byte
    {
        ATC,
        Day,
        GTC,
        IOC,
        OPG,
        OC,
        FOK,
        GTX,
        GTD,
        GFS,
        AUC,
    };

    public enum ExecType : byte
    {
        New,
        Stopped,
        Rejected,
        Expired,
        Trade,
        PendingCancel,
        Cancelled,
        CancelReject,
        PendingReplace,
        Replace,
        ReplaceReject,
    };
}
