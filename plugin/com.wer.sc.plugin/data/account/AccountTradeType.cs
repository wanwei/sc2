using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.account
{
    public class AccountTradeTypeUtils
    {
        public static string GetName(AccountTradeType tradeType)
        {
            switch (tradeType)
            {
                case AccountTradeType.IMMEDIATELY:
                    return "立即成交";
                case AccountTradeType.MARKETPRICE:
                    return "市价成交";
                case AccountTradeType.DELAYTIME:
                    return "延时成交";
                case AccountTradeType.DELAYTICK:
                    return "延迟tick成交";
            }
            return null;
        }
    }

    public enum AccountTradeType
    {
        /// <summary>
        /// 以用户下单的买价或卖价立即成交
        /// </summary>
        IMMEDIATELY = 0,

        /// <summary>
        /// 以当前市价成交
        /// </summary>
        MARKETPRICE = 1,

        /// <summary>
        /// 以当前市价延迟一定时间成交
        /// </summary>
        DELAYTIME = 2,

        /// <summary>
        /// 以当前市价延迟一定数量tick成交
        /// </summary>
        DELAYTICK = 3
    }
}
