using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.market
{
    /// <summary>
    /// 交易账号接口
    /// </summary>
    public interface IAccount
    {
        /// <summary>
        /// 下委托
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        void SendOrder(OrderInfo order);


        void CancelOrder(string orderid);
    }
}
