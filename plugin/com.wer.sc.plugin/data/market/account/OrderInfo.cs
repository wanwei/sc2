using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.market
{
    /// <summary>
    /// 订单信息
    /// </summary>
    public class OrderInfo
    {
        public double OrderTime;

        public string Instrumentid;

        public OpenCloseType OpenClose;

        public double Price;

        public int Volume;

        public OrderSide Direction;
    }
}
