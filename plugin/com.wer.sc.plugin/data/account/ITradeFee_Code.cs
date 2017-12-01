using com.wer.sc.utils;
using System;

namespace com.wer.sc.data.account
{
    /// <summary>
    /// 单支股票或期货的交易费用
    /// </summary>
    public interface ITradeFee_Code : IXmlExchange, ICloneable
    {
        /// <summary>
        /// 得到或设置期货或股票代码
        /// </summary>
        string Code { get; set; }

        /// <summary>
        /// 得到或设置买入费用
        /// </summary>
        double BuyFee { get; set; }

        /// <summary>
        /// 得到或设置卖出费用
        /// </summary>
        double SellFee { get; set; }

        /// <summary>
        /// 保证金比率
        /// </summary>
        double DepositPercent { get; set; }

        /// <summary>
        /// 得到或设置每手数量
        /// </summary>
        int HandCount { get; set; }

        /// <summary>
        /// 交易费用是否按照百分比
        /// </summary>
        bool IsPercent { get; set; }

        /// <summary>
        /// 该品种最小价格变化
        /// </summary>
        double MinPriceChange { get; set; }
    }
}