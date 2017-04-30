using System;
using System.Collections.Generic;

namespace com.wer.sc.data
{
    /// <summary>
    /// 该接口描述了一天的Tick数据
    /// </summary>
    public interface ITickData : ITickBar
    {        
        /// <summary>
        /// 得到该Tick数据的日期
        /// </summary>
        int TradingDay { get; }

        /// <summary>
        /// 得到当前BarPos指定的Tick数据条目
        /// </summary>
        /// <returns></returns>
        ITickBar GetCurrentBar();

        /// <summary>
        /// 得到指定index的tick数据条目
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        ITickBar GetBar(int index);

        /// <summary>
        /// 设置或获取该Tick的BarPos指针
        /// </summary>
        int BarPos { get; set; }

        /// <summary>
        /// 得到该Tick数据的长度
        /// </summary>
        int Length { get; }

        #region 完整数据

        IList<double> Arr_Time { get; }

        // 交易价格
        IList<float> Arr_Price { get; }

        // 交易量
        IList<int> Arr_Mount { get; }

        // 到现在为止总成交量
        IList<int> Arr_TotalMount { get; }

        // 持仓增减
        IList<int> Arr_Add { get; }

        // 买价
        IList<float> Arr_BuyPrice { get; }

        // 买量
        IList<int> Arr_BuyMount { get; }

        // 卖价
        IList<float> Arr_SellPrice { get; }

        // 卖量
        IList<int> Arr_SellMount { get; }

        IList<int> Arr_Hold { get; }

        // 买OR卖
        IList<Boolean> Arr_IsBuy { get; }

        #endregion        

        String ToString(int index);
    }
}