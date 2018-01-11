using com.wer.sc.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy.common.zigzag
{
    public interface IZigzagPoint
    {
        /// <summary>
        /// 得到该point的位置
        /// </summary>
        int BarPos
        {
            get;
        }

        /// <summary>
        /// 得到该point是否是高点
        /// </summary>
        bool IsHigh
        {
            get;
        }

        /// <summary>
        /// 得到该point的价格，如果是高点就是最高价，低点就是最低价
        /// </summary>
        float Price
        {
            get;
        }

        /// <summary>
        /// 得到K线的bar
        /// </summary>
        /// <returns></returns>
        IKLineBar GetBar();

        bool IsMergedPoint
        {
            get;
        }
    }
}