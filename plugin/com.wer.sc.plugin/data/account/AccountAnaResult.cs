using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.account
{
    /// <summary>
    /// 账户分析结果
    /// </summary>
    public class AccountAnaResult
    {
        /// <summary>
        /// 账户开始时间
        /// </summary>
        public double StartTime;

        /// <summary>
        /// 账户结束时间
        /// </summary>
        public double EndTime;

        /// <summary>
        /// 账户初始资金
        /// </summary>
        public double InitMoney;

        /// <summary>
        /// 账户现有资金
        /// </summary>
        public double Money;

        /// <summary>
        /// 账户总资产
        /// </summary>
        public double Asset;

        /// <summary>
        /// 账户交易次数
        /// </summary>
        public int TradeCount;

        /// <summary>
        /// 账户成功委托盈利次数
        /// </summary>
        public int TradeEarnCount;

        /// <summary>
        /// 盈利交易的比例
        /// </summary>
        public double TradeEarnPercent;

        /// <summary>
        /// 最大单笔盈利
        /// </summary>
        public double MaxSingleProfit;

        /// <summary>
        /// 最大单笔亏损
        /// </summary>
        public double MaxSingleLoss;
    }
}