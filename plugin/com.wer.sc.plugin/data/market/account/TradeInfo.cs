using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.market
{
    public class TradeInfo
    {
        public string TradeID;

        public string InstrumentName;

        public string InstrumentID;

        public string AccountID;
        /// <summary>
        /// 交易时间
        /// </summary>
        public double Time;
        /// <summary>
        /// 方向
        /// </summary>
        public OrderSide Side;
        /// <summary>
        /// 成交量
        /// </summary>
        public double Qty;
        /// <summary>
        /// 成交价格
        /// </summary>
        public double Price;

        /// <summary>
        /// 开仓还是平仓
        /// </summary>
        public OpenCloseType OpenClose;
    }
}
