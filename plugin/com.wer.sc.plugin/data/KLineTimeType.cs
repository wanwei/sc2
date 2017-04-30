using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data
{
    /// <summary>
    /// K线时间类型
    /// </summary>
    public enum KLineTimeType
    {
        /// <summary>
        /// 秒
        /// </summary>
        SECOND = 0,

        /// <summary>
        /// 分钟
        /// </summary>
        MINUTE = 1,

        /// <summary>
        /// 小时
        /// </summary>
        HOUR = 2,

        /// <summary>
        /// 日
        /// </summary>
        DAY = 3,

        /// <summary>
        /// 周
        /// </summary>
        WEEK = 4
    }
}
