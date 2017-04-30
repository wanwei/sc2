using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.market
{
    /// <summary>
    /// 持仓信息
    /// </summary>
    public struct PositionInfo
    {
        public string InstrumentName;

        public string Symbol;

        public string InstrumentID;

        public string ExchangeID;

        public string ClientID;

        public string AccountID;

        public PositionSide Side;
        /// <summary>
        /// 日期
        /// </summary>
        public int Date;
        /// <summary>
        /// 持仓成本
        /// </summary>
        public double PositionCost;

        /// <summary>
        /// 总持仓
        /// </summary>
        public double Position;
        /// <summary>
        /// 今日持仓
        /// </summary>
        public double TodayPosition;
        /// <summary>
        /// 历史持仓
        /// </summary>
        public double HistoryPosition;
        /// <summary>
        /// 历史冻结持仓
        /// </summary>
        public double HistoryFrozen;

        /// <summary>
        /// 今日买卖持仓
        /// </summary>
        public double TodayBSPosition;
        /// <summary>
        /// 今日买卖持仓冻结
        /// </summary>
        public double TodayBSFrozen;
        /// <summary>
        /// 今日申赎持仓
        /// </summary>
        public double TodayPRPosition;
        /// <summary>
        /// 今日申赎持仓冻结
        /// </summary>
        public double TodayPRFrozen;
    }
}
