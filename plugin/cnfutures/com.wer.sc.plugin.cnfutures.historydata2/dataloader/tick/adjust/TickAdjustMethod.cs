using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin.cnfutures.historydata.dataloader.tick.adjust
{
    /// <summary>
    /// 调整方法
    /// </summary>
    public class TickAdjustMethod
    {
        /// <summary>
        /// 该时间段是否出现了时间偏移
        /// 如果出现
        /// </summary>
        /// <returns></returns>
        public bool HasTimeOffset()
        {
            return false;
        }

        /// <summary>
        /// 返回偏移量
        /// </summary>
        /// <returns></returns>
        public double GetTimeOffset()
        {
            return -1;
        }

        /// <summary>
        /// 是否要把
        /// </summary>
        /// <returns></returns>
        public bool IsStartRepeatSpread()
        {
            return false;
        }

        public bool IsEndRepeatSpread()
        {
            return false;
        }
    }
}
