using com.wer.sc.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data
{
    /// <summary>
    /// 增强版的K线数据
    /// 该数据会附带一些交易时间信息
    /// 比如：
    /// 1.能够得到整条K线上所有开盘和收盘周期以及在K线上对应的位置
    /// 2.现在还有多长时间到该周期结束
    /// 3.现在是否是该时间段的结束周期
    /// ...
    /// </summary>
    public interface IKLineData_Extend : IKLineData
    {
        #region Day

        /// <summary>
        /// 得到当前barPos对应的交易日
        /// </summary>
        int TradingDay { get; }

        /// <summary>
        /// 得到该K线的所有交易日
        /// </summary>
        /// <returns></returns>
        IList<int> GetAllTradingDays();

        /// <summary>
        /// 得到指定交易日在K线上开始位置的BarPos
        /// </summary>
        /// <param name="tradingDay"></param>
        /// <returns></returns>
        int GetDayStartBarPosByTradingDay(int tradingDay);

        /// <summary>
        /// 得到指定交易日在K线上结束位置的BarPos
        /// </summary>
        /// <param name="tradingDay"></param>
        /// <returns></returns>
        int GetDayEndBarPosByTradingDay(int tradingDay);

        /// <summary>
        /// 得到当前交易日的所有barCount
        /// </summary>
        /// <returns></returns>
        int GetDayBarCount();

        /// <summary>
        /// 获得当前barpos所在交易日的第一个K线的BarPos
        /// </summary>
        /// <returns></returns>
        int GetDayStartBarPos();

        /// <summary>
        /// 获得指定barpos所在交易日的第一个K线的BarPos
        /// </summary>
        /// <param name="barPos"></param>
        /// <returns></returns>
        int GetDayStartBarPos(int barPos);

        /// <summary>
        /// 获得当前barpos所在交易日的最后一根K线的BarPos
        /// </summary>
        /// <returns></returns>
        int GetDayEndBarPos();

        /// <summary>
        /// 获得指定barpos所在交易日的最后一根K线的BarPos
        /// </summary>
        /// <param name="barPos"></param>
        /// <returns></returns>
        int GetDayEndBarPos(int barPos);

        /// <summary>
        /// 得到当前BarPos所在交易日的交易时间
        /// </summary>
        /// <returns></returns>
        ITradingTime GetTradingTime();

        /// <summary>
        /// 得到指定BarPos所在交易日的交易时间
        /// </summary>
        /// <param name="barPos"></param>
        /// <returns></returns>
        ITradingTime GetTradingTime(int barPos);

        /// <summary>
        /// 当前BarPos是否是一天的开始
        /// </summary>
        /// <returns></returns>
        bool IsDayStart();

        /// <summary>
        /// 指定的barPos是否是一天的开始
        /// </summary>
        /// <param name="barPos"></param>
        /// <returns></returns>
        bool IsDayStart(int barPos);

        /// <summary>
        /// 当前的
        /// </summary>
        /// <returns></returns>
        bool IsDayEnd();

        /// <summary>
        /// 是否是一天结束
        /// </summary>
        /// <param name="barPos"></param>
        /// <returns></returns>
        bool IsDayEnd(int barPos);

        /// <summary>
        /// 得到所有交易日结束的barpos
        /// </summary>
        /// <returns></returns>
        IList<int> GetAllTradingDayEndBarPoses();

        #endregion

        #region TradingPeriod

        /// <summary>
        /// 得到当前BarPos所在的交易周期
        /// </summary>
        double[] GetTradingPeriods();

        /// <summary>
        /// 得到指定BarPos所在的交易周期
        /// </summary>
        double[] GetTradingPeriods(int barPos);

        /// <summary>
        /// 得到当前交易周期的bar的数量
        /// </summary>
        /// <returns></returns>
        int GetTradingPeriodsBarCount();

        /// <summary>
        /// 得到当前交易周期的bar的数量
        /// </summary>
        /// <returns></returns>
        int GetTradingPeriodsBarCount(int barPos);

        /// <summary>
        /// 获得当前barpos所在交易时段的第一个K线的BarPos
        /// </summary>
        /// <returns></returns>
        int GetTradingPeriodsStartBarPos();

        /// <summary>
        /// 获得指定barpos所在交易时段的第一个K线的BarPos
        /// </summary>
        /// <param name="barPos"></param>
        /// <returns></returns>
        int GetTradingPeriodsStartBarPos(int barPos);

        /// <summary>
        /// 获得当前barpos所在交易时段的最后一根K线的BarPos
        /// </summary>
        /// <returns></returns>
        int GetTradingPeriodsEndBarPos();

        /// <summary>
        /// 获得指定barpos所在交易时段的最后一根K线的BarPos
        /// </summary>
        /// <param name="barPos"></param>
        /// <returns></returns>
        int GetTradingPeriodsEndBarPos(int barPos);

        /// <summary>
        /// 得到当前BarPos在交易时段的Index
        /// </summary>
        /// <returns></returns>
        int GetIndexInTradingPeriods();

        /// <summary>
        /// 得到指定BarPos在交易时段的Index
        /// </summary>
        /// <param name="barPos"></param>
        /// <returns></returns>
        int GetIndexInTradingPeriods(int barPos);

        /// <summary>
        /// 得到当前barPos所在交易时段是交易日的第几个交易时段
        /// </summary>
        /// <returns></returns>
        int GetTradingPeriodsIndexInTradingDay();

        /// <summary>
        /// 得到指定barPos所在交易时段是交易日的第几个交易时段
        /// </summary>
        /// <returns></returns>
        int GetTradingPeriodsIndexInTradingDay(int barPos);

        /// <summary>
        /// 得到当前BarPos是否是一个开盘周期的开始
        /// </summary>
        /// <param name="barPos"></param>
        /// <returns></returns>
        bool IsTradingPeriodStart();

        /// <summary>
        /// 得到指定barpos是否是一个开盘周期的开始
        /// </summary>
        /// <param name="barPos"></param>
        /// <returns></returns>
        bool IsTradingPeriodStart(int barPos);

        /// <summary>
        /// 得到当前BarPos是否是一个开盘周期结束
        /// </summary>
        /// <param name="barPos"></param>
        /// <returns></returns>
        bool IsTradingPeriodEnd();

        /// <summary>
        /// 得到指定BarPos是否是一个开盘周期结束
        /// </summary>
        /// <param name="barPos"></param>
        /// <returns></returns>
        bool IsTradingPeriodEnd(int barPos);

        /// <summary>
        /// 得到所有交易时间结束的barpos
        /// </summary>
        /// <returns></returns>
        IList<int> GetAllTradingTimeEndBarPoses();

        #endregion

        #region KLinePeriod

        /// <summary>
        /// 得到距离这个bar结束的时间
        /// </summary>
        /// <returns></returns>
        TimeSpan GetTimeToKLinePeriodEnd();

        /// <summary>
        /// 得到这个bar的结束时间
        /// </summary>
        /// <param name="barPos"></param>
        /// <returns></returns>
        double GetKLinePeriodEndTime(int barPos);

        #endregion
    }
}