using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data
{
    /// <summary>
    /// K线时间信息类
    /// </summary>
    public interface IKLineDataTimeInfo
    {
        /// <summary>
        /// 该K线的周期
        /// </summary>
        KLinePeriod KLinePeriod { get; }

        /// <summary>
        /// 得到barPos对应的K线的开始和结束时间
        /// </summary>
        /// <param name="barPos"></param>
        /// <returns></returns>
        double[] GetKLineTime(int barPos);

        /// <summary>
        /// 得到指定位置bar的开始时间
        /// </summary>
        /// <param name="barPos"></param>
        /// <returns></returns>
        double GetKLineTimeStart(int barPos);

        /// <summary>
        /// 得到指定位置bar的结束时间
        /// </summary>
        /// <param name="barPos"></param>
        /// <returns></returns>
        double GetKLineTimeEnd(int barPos);

        ///// <summary>
        ///// 得到指定交易日的开始时的barpos
        ///// </summary>
        ///// <param name="tradingDay"></param>
        ///// <returns></returns>
        //int GetDayStartPos(int tradingDay);

        ///// <summary>
        ///// 得到指定交易日的结束时的barpos
        ///// </summary>
        ///// <param name="tradingDay"></param>
        ///// <returns></returns>
        //int GetDayEndPos(int tradingDay);

        ///// <summary>
        ///// 得到指定交易日的所有开盘时间
        ///// </summary>
        ///// <param name="tradingDay"></param>
        ///// <returns></returns>
        //IList<double[]> GetTradingTime(int tradingDay);

        ///// <summary>
        ///// 确定指定的barpos是否是一段开盘时间开始的bar
        ///// </summary>
        ///// <param name="barPos"></param>
        ///// <returns></returns>
        //bool IsPeriodStart(int barPos);

        ///// <summary>
        ///// 确定指定的barpos是否是一段开盘时间结束的bar
        ///// </summary>
        ///// <param name="barPos"></param>
        ///// <returns></returns>
        //bool IsPeriodEnd(int barPos);

        ///// <summary>
        ///// 确定指定的barpos是否是一天开盘时间开始的bar
        ///// </summary>
        ///// <param name="barPos"></param>
        ///// <returns></returns>
        //bool IsDayStart(int barPos);

        ///// <summary>
        ///// 确定指定的barpos是否是一天开盘时间结束的bar
        ///// </summary>
        ///// <param name="barPos"></param>
        ///// <returns></returns>
        //bool IsDayEnd(int barPos);        
        ///// <summary>
        ///// 
        ///// </summary>
        //IList<int> TradingTimeEndBarPoses { get; }

        ///// <summary>
        ///// 得到所有交易日的结束barPos
        ///// </summary>
        //IList<int> DayEndBarPoses { get; }
        /// <summary>
        /// 得到该K线的周期
        /// </summary>        
    }
}