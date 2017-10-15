using com.wer.sc.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data
{
    /// <summary>
    /// 扩展的K线数据
    /// </summary>
    public interface IKLineData_Extend : IKLineData
    {
        /// <summary>
        /// 得到所有交易时间结束的barpos
        /// </summary>
        /// <returns></returns>
        IList<int> GetTradingTimeEndBarPoses();

        /// <summary>
        /// 得到所有交易日结束时的barpos
        /// </summary>
        /// <returns></returns>
        IList<int> GetTradingDayEndBarPoses();

        /// <summary>
        /// 是否是一个开盘周期开始
        /// </summary>
        /// <param name="barPos"></param>
        /// <returns></returns>
        bool IsTradingTimeStart(int barPos);

        /// <summary>
        /// 是否是一个开盘周期结束
        /// </summary>
        /// <param name="barPos"></param>
        /// <returns></returns>
        bool IsTradingTimeEnd(int barPos);

        /// <summary>
        /// 得到这个bar的结束时间
        /// </summary>
        /// <param name="barPos"></param>
        /// <returns></returns>
        double GetEndTime(int barPos);

        /// <summary>
        /// 是否是一天的开始
        /// </summary>
        /// <param name="barPos"></param>
        /// <returns></returns>
        bool IsDayStart(int barPos);

        /// <summary>
        /// 是否是一天结束
        /// </summary>
        /// <param name="barPos"></param>
        /// <returns></returns>
        bool IsDayEnd(int barPos);
    }
}