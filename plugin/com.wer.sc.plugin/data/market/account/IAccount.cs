using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.market.account
{
    /// <summary>
    /// 账号接口
    /// </summary>
    public interface IAccount
    {        
        /// <summary>
        /// 设置或获取
        /// </summary>
        IAccountDataProvider AccountDataProvider { get; set; }

        /// <summary>
        /// 下单，返回订单id
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        string SendOrder(OrderInfo order);

        /// <summary>
        /// 撤单
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        string CancelOrder(string orderid);

        void CloseBuy(string code, double price, int count);

        void CloseSell(string code, double price, int count);

        /// <summary>
        /// 得到
        /// </summary>
        /// <returns></returns>
        List<OrderInfo> GetOrderInfos();

        List<TradeInfo> GetTradeInfos();
    }
}