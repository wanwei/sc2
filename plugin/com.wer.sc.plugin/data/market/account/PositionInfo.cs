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
    public class PositionInfo
    {
        public PositionInfo()
        {

        }

        public PositionInfo(string code, PositionSide positionSide, int position, double positionCost)
        {
            this.InstrumentID = code;
            this.Side = positionSide;
            this.Position = position;
            this.PositionCost = positionCost;
        }

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
        public int Position;

        /// <summary>
        /// 今日持仓
        /// </summary>
        public int TodayPosition;

        /// <summary>
        /// 历史持仓
        /// </summary>
        public int HistoryPosition;

        /// <summary>
        /// 历史冻结持仓
        /// </summary>
        public int HistoryFrozen;

        /// <summary>
        /// 今日买卖持仓
        /// </summary>
        public int TodayBSPosition;
        /// <summary>
        /// 今日买卖持仓冻结
        /// </summary>
        public int TodayBSFrozen;
        /// <summary>
        /// 今日申赎持仓
        /// </summary>
        public int TodayPRPosition;
        /// <summary>
        /// 今日申赎持仓冻结
        /// </summary>
        public int TodayPRFrozen;
    }
}
