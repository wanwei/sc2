using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.account
{
    public class AccountSlipTypeUtils
    {
        public static string GetName(AccountSlipType slipType)
        {
            switch (slipType)
            {
                case AccountSlipType.NOSLIP:
                    return "不滑点";
                case AccountSlipType.MINPRICE:
                    return "最小价格滑点";
                case AccountSlipType.PERCENT:
                    return "百分比价格滑点";
                case AccountSlipType.PRICE:
                    return "绝对价格滑点";
            }
            return null;
        }
    }

    /// <summary>
    /// 账户滑点方式
    /// </summary>
    public enum AccountSlipType
    {
        /// <summary>
        /// 不滑点
        /// </summary>
        NOSLIP = 0,

        /// <summary>
        /// 以最小价格变动滑点
        /// </summary>
        MINPRICE = 1,

        /// <summary>
        /// 以百分比滑点
        /// </summary>
        PERCENT = 2,

        /// <summary>
        /// 以绝对价格滑点
        /// </summary>
        PRICE = 3
    }
}
