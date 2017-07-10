using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.market
{
    /// <summary>
    /// 交易委托信息类
    /// </summary>
    public class OrderInfo
    {
        public OrderInfo()
        {

        }

        public OrderInfo(string code, double orderTime, OpenCloseType openClose, double price, int qty, OrderSide direction) : this(code, orderTime, openClose, price, qty, direction, OrderType.Market)
        {
        }

        public OrderInfo(string code, double orderTime, OpenCloseType openClose, double price, int qty, OrderSide direction, OrderType orderType)
        {
            this.Instrumentid = code;
            this.OrderTime = orderTime;
            this.OpenClose = openClose;
            this.Price = price;
            this.Volume = qty;
            this.Direction = direction;
            this.Type = orderType;
        }

        /// <summary>
        /// 委托ID，由服务器端生成传回客户端
        /// </summary>
        public string OrderID;

        /// <summary>
        /// 账户ID
        /// </summary>
        public string AccountId;

        /// <summary>
        /// 委托时间
        /// </summary>
        public double OrderTime;

        /// <summary>
        /// 合约代码
        /// </summary>
        public string Instrumentid;

        /// <summary>
        /// 开仓还是平仓
        /// </summary>
        public OpenCloseType OpenClose;

        /// <summary>
        /// 委托价格
        /// </summary>
        public double Price;

        private int volume;

        /// <summary>
        /// 委托数量
        /// </summary>
        public int Volume
        {
            get { return volume; }
            set
            {
                this.volume = value;
                this.leavesQty = volume;
            }
        }

        /// <summary>
        /// 委托方向
        /// </summary>
        public OrderSide Direction;

        /// <summary>
        /// 委托类型，如限价单，追价单等
        /// </summary>
        public OrderType Type;

        /// <summary>
        /// 订单的被执行状态，是新开订单还是已经被
        /// </summary>
        public ExecType ExecType;

        private int leavesQty;

        /// <summary>
        /// 还剩下多少没成交
        /// </summary>
        public int LeavesQty
        {
            get { return leavesQty; }
            set
            {
                this.leavesQty = value;
            }
        }

        private int cumQty;

        /// <summary>
        /// 累计成交数量
        /// </summary>
        public int CumQty
        {
            get { return cumQty; }
            set
            {
                this.cumQty = value;
                this.leavesQty = this.volume - cumQty;
            }
        }

        /// <summary>
        /// 平均成交价格
        /// </summary>
        public double AvgPx;
    }
}