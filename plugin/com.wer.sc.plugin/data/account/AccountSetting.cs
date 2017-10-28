using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.account
{
    /// <summary>
    /// 账号设置
    /// </summary>
    public class AccountSetting
    {
        /// <summary>
        /// 成交方式：立即成交，不管买卖价格
        /// </summary>
        public const int TRADETYPE_IMMEDIATELY = 0;

        /// <summary>
        /// 延迟成交
        /// </summary>
        public const int TRADETYPE_DELAY = 1;

        /// <summary>
        /// 滑点成交，预计成交会比下单价贵的跳数
        /// </summary>
        public const int TRADETYPE_SLIP = 2;

        private int tradeType;

        private int delaySecond = 0;

        /// <summary>
        /// 自动过滤掉
        /// </summary>
        private bool autoFilter;
    }
}