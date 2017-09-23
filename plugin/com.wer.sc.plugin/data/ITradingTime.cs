using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data
{
    /// <summary>
    /// 交易时间接口
    /// 得到一张合约在一天的交易时段
    /// </summary>
    public interface ITradingTime : ITextExchange
    {
        /// <summary>
        /// 得到该开盘时间的交易日
        /// </summary>
        int TradingDay { get; }

        /// <summary>
        /// 得到开盘详细时间：
        /// 如201500907.09
        /// </summary>
        double OpenTime { get; }

        /// <summary>
        /// 得到收盘详细时间：
        /// 如201500907.15
        /// </summary>
        double CloseTime { get; }

        /// <summary>
        /// 得到开盘的总阶段数
        /// 一般股票是两个阶段，上下午
        /// 期货比较复杂，没有夜盘的一般是早上两个阶段，下午一个阶段。
        /// 有夜盘的话晚上还会有一个阶段
        /// </summary>
        int PeriodCount { get; }

        /// <summary>
        /// 得到第index阶段的开盘收盘时间
        /// 返回一个保存两个时间的数组
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        double[] GetPeriodTime(int index);

        List<double[]> TradingPeriods { get; }

        /// <summary>
        /// 该期货合约的一天是否过夜
        /// 有夜盘的合约一般会过夜
        /// </summary>
        bool IsOverNight { get; }

        /// <summary>
        /// 计算当日的时间周期
        /// 该方法用来计算
        /// </summary>
        /// <param name="targetPeriod"></param>
        /// <returns></returns>
        //List<double> CalcTimeList(KLinePeriod targetPeriod);
    }
}