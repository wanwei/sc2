using com.wer.sc.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy.realtimereader
{
    public interface IKLineDataForward
    {
        /// <summary>
        /// 得到前进时的主K线，如果是以tick前进，则返回空
        /// </summary>
        /// <returns></returns>
        IKLineData GetKLineData();

        /// <summary>
        /// 得到制定周期的K线
        /// </summary>
        /// <param name="klinePeriod"></param>
        /// <returns></returns>
        IKLineData GetKLineData(KLinePeriod klinePeriod);

        /// <summary>
        /// 得到现在时刻的tick数据，如果是以分钟以上tick数据前进，则返回空
        /// </summary>
        /// <returns></returns>
        ITickData GetTickData();

        /// <summary>
        /// 得到当日分时线数据
        /// </summary>
        /// <returns></returns>
        ITimeLineData GetTimeLineData();

        /// <summary>
        /// 前进
        /// </summary>
        /// <returns></returns>
        bool Forward();

        /// <summary>
        /// 是否不能再前进了
        /// </summary>
        bool IsEnd { get; }

        /// <summary>
        /// 是否到一天的结束
        /// </summary>
        bool IsDayEnd { get; }

        /// <summary>
        /// 指定周期的K线是否正好走完
        /// </summary>
        /// <param name="klinePeriod"></param>
        /// <returns></returns>
        bool IsPeriodEnd(KLinePeriod klinePeriod);
    }
}
