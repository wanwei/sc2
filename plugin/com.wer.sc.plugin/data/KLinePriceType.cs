using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data
{
    public enum KLinePriceType
    {
        /// <summary>
        /// 开盘价
        /// </summary>
        Start = 0,

        /// <summary>
        /// 最高价
        /// </summary>
        High = 1,

        Low = 2,

        End = 3,

        BlockHigh = 4,

        BlockLow = 5
    }
}
