using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.market
{
    /// <summary>
    /// 成交信息
    /// </summary>
    public class TradeInfo
    {
        /// <summary>
        /// 成交ID，由服务器端生成
        /// </summary>
        public string TradeID;

        /// <summary>
        /// 交易账号ID
        /// </summary>
        public String AccountID;

        /// <summary>
        /// 交易合约ID
        /// </summary>
        public string InstrumentID;

        /// <summary>
        /// 交易时间
        /// </summary>
        public double Time;

        /// <summary>
        /// 交易方向
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

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            //sb.Append(TradeID).Append(",");
            sb.Append(InstrumentID).Append(",");
            sb.Append(Time).Append(",");
            sb.Append(Side).Append(",");
            sb.Append(Qty).Append(",");
            sb.Append(Price).Append(",");
            sb.Append(OpenClose);
            return sb.ToString();
        }
    }
}