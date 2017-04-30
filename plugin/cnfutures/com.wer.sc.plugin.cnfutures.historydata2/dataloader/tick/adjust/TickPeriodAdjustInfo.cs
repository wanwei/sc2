using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin.cnfutures.historydata.dataloader.tick.adjust
{

    public class TickPeriodAdjustInfo
    {
        /// <summary>
        /// 是否是开盘的周期，日盘9点和夜盘9点
        /// 开盘周期会多一个集合竞价
        /// </summary>
        public bool IsOpen;

        /// <summary>
        /// 开盘数据是否错误，提前开盘1分钟以上算错误
        /// </summary>
        public bool StartErrorData;

        /// <summary>
        /// 开盘是否出现重复数据
        /// </summary>
        public bool StartRepeat;

        /// <summary>
        /// 开盘重复数据起始位置
        /// </summary>
        public int StartRepeatIndex;

        /// <summary>
        /// 开盘重复次数
        /// </summary>
        public int StartRepeatTimes;

        /// <summary>
        /// 开盘错位的时间，提前，则为负数
        /// 如 
        /// 08:59:58 为-2
        /// 09:00:08 为8
        /// </summary>
        public int StartOffset;

        /// <summary>
        /// 开盘数据是否错误，推迟收盘1分钟以上算错误
        /// </summary>
        public bool EndErrorData;

        /// <summary>
        /// 尾盘是否重复
        /// </summary>
        public bool EndRepeat;

        /// <summary>
        /// 尾盘重复的结束index （尾盘按从后向前看）
        /// </summary>
        public int EndRepeatIndex;

        /// <summary>
        /// 尾盘重复时间次数
        /// </summary>
        public int EndRepeatTimes;

        /// <summary>
        /// 尾盘结束时间的偏移量
        /// </summary>
        public int EndOffset;

        /// <summary>
        /// 差距
        /// </summary>
        /// <returns></returns>
        public int GetTimeGap()
        {
            return StartOffset - EndOffset;
        }

        /// <summary>
        /// 该时间段是否出现了时间偏移
        /// </summary>
        /// <returns></returns>
        public bool HasTimeOffset()
        {
            return StartOffset != 0 && Math.Abs(GetTimeGap()) < 10;
        }

        public bool HasRepeatOffset()
        {
            if (StartRepeat && EndRepeat)
                return false;
            //多过10次repeat才处理偏移
            //if (StartRepeatTimes < 10 || EndRepeatTimes < 10)
            //    return false;
            int startTimeGap = -(StartRepeatTimes / 2 + EndOffset);
            bool startRepeatOffset = startTimeGap >= 0 && startTimeGap < 20;
            if (startRepeatOffset)
                return true;
            int endTimeGap = StartOffset - EndRepeatTimes / 2;
            return endTimeGap >= 0 && endTimeGap < 20;
        }

        private bool HasStartRepeatOffset()
        {
            int startTimeGap = -(StartRepeatTimes / 2 + EndOffset);
            bool startRepeatOffset = startTimeGap >= 0 && startTimeGap < 20;
            if (!startRepeatOffset)
                return false;
            //如果repeat正好能填补，或者开始重复次数大于10，可以认为需要
            if (startTimeGap < 2 || StartRepeatTimes > 15)
                return true;
            double rate = EndOffset / StartRepeatTimes;
            if (rate > 2)
                return false;
            return true;
        }

        /// <summary>
        /// 返回偏移量
        /// </summary>
        /// <returns></returns>
        public int GetTimeOffset()
        {
            return StartOffset;
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

        override
        public String ToString()
        {
            return StartErrorData + "," + StartRepeat + "," + StartRepeatIndex + "," + StartRepeatTimes + "," + StartOffset
                + "," + EndErrorData + "," + EndRepeat + "," + EndRepeatIndex + "," + EndRepeatTimes + "," + EndOffset;
        }
    }
}
