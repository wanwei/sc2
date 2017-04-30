using com.wer.sc.data;
using com.wer.sc.data.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin.cnfutures.historydata.dataloader.tick.adjust
{
    /// <summary>
    /// Tick数据清洗
    /// 现在得到的国内期货市场的历史Tick数据是非常不准确的，不准确来源于以下：
    /// 1.时间不准，这个在早期数据中非常多。可能是接收数据的客户端时间不准确，导致时间错位，统一向前或向后错位几秒
    /// 2.在某个时间堆积了大量tick数据。可能的原因是客户端延迟导致大量的数据一次性接收
    /// 
    /// Tick数据的推送周期
    /// 上交所：5秒
    /// 深交所：3秒
    /// 中金所：0.5秒
    /// 上期所：0.5秒
    /// 大商所：平均约0.5秒
    /// 郑商所：平均约0.5秒
    /// 注: 大商所和郑商所非均匀推送
    /// 
    /// 原始的Tick数据
    /// </summary>
    public class TickDataAdjuster
    {
        private const int timesEverySecond = 4;

        public TickDataAdjuster()
        {
        }

        /// <summary>
        /// 预处理：
        /// 1.处理掉一些肯定不正确的数据，如提前3分钟开盘，然后到开盘都没有数据了。
        /// 
        /// 三种调整算法：
        /// 1.开始时间和结束时间都向前或向后移了基本相同的时间，整体迁移。
        /// 2.开始时间或者结束时间出现大量重复，分两种情况：1.如果另一头出现了时间偏差，那么整体迁移；2.如果没有，则稀释
        /// </summary>
        /// <param name="data"></param>
        /// <param name="openTime"></param>
        public void Adjust(TickData data, List<double[]> openTime)
        {
            //小于500条数据就不调整了
            if (data.Length < 500)
                return;
            //2013年以后数据就不调整了
            if (data.TradingDay > 20120000)
                return;

            //郑州20140827开始1分钟4个tick
            List<TickInfo_Period> periods = TickDataAnalysis.Analysis(data, openTime);
            if (periods[0].StartIndex == -1)
            {
                Adjust_Special(data, periods);
                return;
            }
            for (int i = 0; i < periods.Count; i++)
            {
                TickInfo_Period period = periods[i];
                Adjust(data, period);
            }
        }

        private void Adjust_Special(TickData data, List<TickInfo_Period> periods)
        {
            /**
             * 1.搜索出大量repeat的数据
             * 2.把repeat前的数据移到
             */

            return;
        }

        private int[] FindMaxRepeatIndex(TickData data)
        {
            return null;
        }

        /// <summary>
        /// 调整规则：
        /// 1.如果该开始和结束
        /// 
        /// 逻辑：
        /// 1.是否有repeat，如果没有repeat直接移动时间
        /// 2.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="period"></param>
        private void Adjust(TickData data, TickInfo_Period period)
        {
            TickPeriodAdjustInfo adjustInfo = period.adjustInfo;
            if (!adjustInfo.StartRepeat && !adjustInfo.EndRepeat)
            {
                Adjust_NoRepeat(data, period, adjustInfo);
                Console.WriteLine("NoRepeat");
            }
            else if (adjustInfo.StartRepeat && adjustInfo.EndRepeat)
            {
                Adjust_AllRepeat(data, period, adjustInfo);
                Console.WriteLine("AllRepeat");
            }                
            //有时间偏移，首先根据偏移位置移正，再处理repeat
            else if (adjustInfo.HasTimeOffset())
            {
                Adjust_HasTimeOffsetAndRepeat(data, period, adjustInfo);
                Console.WriteLine("TimeOffset");
            }
            //起始位置repeat，末尾offset，且正好合拍
            else if (adjustInfo.HasRepeatOffset())
                Adjust_HasRepeatOffset(data, period, adjustInfo);
            //该数据段没有偏移，只有repeat
            else
                Adjust_NoOffsetOnlyRepeat(data, period, adjustInfo);
        }

        private void Adjust_NoRepeat(TickData data, TickInfo_Period period, TickPeriodAdjustInfo adjustInfo)
        {
            if (adjustInfo.HasTimeOffset())
                AdjustTime(data, period.StartIndex, period.EndIndex, -adjustInfo.GetTimeOffset());
            int adjustCount = AdjustPeriodStart(data, period);
            if (adjustCount > 2)
            {
                int startIndex = adjustInfo.IsOpen ? period.StartIndex + 1 : period.StartIndex;
                int endIndex = startIndex + adjustCount - 1;
                SpreadRepeatForward(data, period, startIndex, endIndex);
            }

            adjustCount = AdjustPeriodEnd(data, period);
            if (adjustCount > 2)
            {
                int startIndex = period.EndIndex - adjustCount + 1;
                int endIndex = period.EndIndex;
                SpreadRepeatBackward(data, period, startIndex, endIndex);
            }
        }

        private void Adjust_AllRepeat(TickData data, TickInfo_Period period, TickPeriodAdjustInfo adjustInfo)
        {
            int adjustCount = AdjustPeriodStart(data, period);
            int startIndex = adjustInfo.StartRepeatIndex - adjustCount;
            int endIndex = adjustInfo.StartRepeatIndex + adjustInfo.StartRepeatTimes - 1;
            SpreadRepeatForward(data, period, startIndex, endIndex);

            adjustCount = AdjustPeriodEnd(data, period);
            startIndex = adjustInfo.EndRepeatIndex - adjustCount;
            endIndex = adjustInfo.EndRepeatIndex + adjustInfo.EndRepeatTimes - 1;
            SpreadRepeatBackward(data, period, startIndex, endIndex);
        }

        /// <summary>
        /// 既有偏移又有repeat的情况
        /// </summary>
        /// <param name="data"></param>
        /// <param name="period"></param>
        /// <param name="adjustInfo"></param>
        private void Adjust_HasTimeOffsetAndRepeat(TickData data, TickInfo_Period period, TickPeriodAdjustInfo adjustInfo)
        {
            AdjustTime(data, period.StartIndex, period.EndIndex, -adjustInfo.GetTimeOffset());
            AdjustPeriodStart(data, period);
            if (adjustInfo.StartRepeat)
            {
                int startIndex = adjustInfo.StartRepeatIndex;
                int endIndex = adjustInfo.StartRepeatIndex + adjustInfo.StartRepeatTimes - 1;
                //如果调整后向前移动的空间能够容纳下repeat，则向前填充
                if (-adjustInfo.GetTimeOffset() * 2 > adjustInfo.StartRepeatTimes)
                {
                    SpreadRepeatBackward(data, period, startIndex, endIndex);
                }
                else
                {
                    int mIndex = startIndex - adjustInfo.GetTimeOffset() * 2 - 4;
                    SpreadRepeatBackward(data, period, startIndex, mIndex);
                    SpreadRepeatForward(data, period, mIndex, endIndex);
                }
            }

            AdjustPeriodEnd(data, period);
            if (adjustInfo.EndRepeat)
            {
                int startIndex = adjustInfo.EndRepeatIndex;
                int endIndex = adjustInfo.EndRepeatIndex + adjustInfo.EndRepeatTimes - 1;
                //如果调整后向前移动的空间能够容纳下repeat，则向前填充
                if (adjustInfo.GetTimeOffset() * 2 > adjustInfo.EndRepeatTimes)
                {
                    SpreadRepeatForward(data, period, startIndex, endIndex);
                }
                else
                {
                    int mIndex = endIndex - adjustInfo.GetTimeOffset() * 2 + 4;
                    SpreadRepeatBackward(data, period, startIndex, mIndex);
                    SpreadRepeatForward(data, period, mIndex, endIndex);
                }
            }
        }

        private void Adjust_NoOffsetOnlyRepeat(TickData data, TickInfo_Period period, TickPeriodAdjustInfo adjustInfo)
        {
            //不设置偏移，直接调整
            int adjustCount = AdjustPeriodStart(data, period);
            if (adjustInfo.StartRepeat)
            {
                int startIndex = adjustInfo.StartRepeatIndex - adjustCount;
                int endIndex = adjustInfo.StartRepeatIndex + adjustInfo.StartRepeatTimes - 1;
                SpreadRepeatForward(data, period, startIndex, endIndex);
            }

            adjustCount = AdjustPeriodEnd(data, period);
            if (adjustInfo.EndRepeat)
            {
                int startIndex = adjustInfo.EndRepeatIndex - adjustInfo.EndRepeatTimes + 1;
                int endIndex = adjustInfo.EndRepeatIndex + adjustCount;
                SpreadRepeatBackward(data, period, startIndex, endIndex);
            }
        }

        /// <summary>
        /// 例子：
        /// 20071017 m05 (13:30:00一共差不多70个)
        /// 2007-10-17,13:30:00,3226,502,459820,46,3226,10,0,0,0,0,3227,183,0,0,0,0,S
        /// 2007-10-17,13:30:00,3226,26,459846,-6,3225,789,0,0,0,0,3226,18,0,0,0,0,S
        /// ...
        /// 2007-10-17,13:30:00,3222,260,463172,-58,3222,1,0,0,0,0,3225,77,0,0,0,0,S
        /// 2007-10-17,13:30:00,3223,6,463178,-2,3223,1,0,0,0,0,3224,1,0,0,0,0,B
        /// ...
        /// 2007-10-17,14:59:15,3203,48,756442,-10,3202,528,0,0,0,0,3203,43,0,0,0,0,B
        /// </summary>
        /// <param name="data"></param>
        /// <param name="period"></param>
        /// <param name="adjustInfo"></param>
        private void Adjust_HasRepeatOffset(TickData data, TickInfo_Period period, TickPeriodAdjustInfo adjustInfo)
        {
            if (adjustInfo.StartRepeat)
            {
                AdjustTime(data, period.StartIndex, period.EndIndex, -adjustInfo.EndOffset);
                SpreadRepeatBackward(data, period, adjustInfo.StartRepeatIndex, adjustInfo.StartRepeatIndex + adjustInfo.StartRepeatTimes - 1);
            }
            else
            {
                AdjustTime(data, period.StartIndex, period.EndIndex, -adjustInfo.StartOffset);
                SpreadRepeatForward(data, period, adjustInfo.EndRepeatIndex, adjustInfo.EndRepeatIndex + adjustInfo.EndRepeatTimes - 1);
            }
        }

        private void AdjustTime(TickData data, int startIndex, int endIndex, int offSecond)
        {
            for (int i = startIndex; i <= endIndex; i++)
            {
                data.arr_time[i] = TimeUtils.AddSeconds(data.arr_time[i], offSecond);
            }
        }

        private int AdjustPeriodStart(TickData data, TickInfo_Period period)
        {
            //修改开始时间
            int startIndex = period.StartIndex;
            double auctionTime = TimeUtils.AddMinutes(data.TradingDay + period.StartTime, -1);
            if (period.adjustInfo.IsOpen)
            {
                data.arr_time[period.StartIndex] = auctionTime;
                startIndex++;
            }
            double startTime = Math.Round(data.TradingDay + period.StartTime, 6); ;
            double currentTime = data.arr_time[startIndex];
            int index = startIndex;
            while (currentTime < startTime)
            {
                data.arr_time[index] = startTime;
                index++;
                currentTime = data.arr_time[index];
            }
            return index - period.StartIndex - (period.adjustInfo.IsOpen ? 1 : 0);
        }

        private int AdjustPeriodEnd(TickData data, TickInfo_Period period)
        {
            //修改结束时间
            int endIndex = period.EndIndex;
            double endTime = Math.Round(data.TradingDay + period.EndTime, 6); ;
            double currentTime = data.arr_time[endIndex];
            int index = endIndex;
            while (currentTime > endTime)
            {
                data.arr_time[index] = endTime;
                index--;
                currentTime = data.arr_time[index];
            }
            return endIndex - index;
        }

        /// <summary>
        /// 向前展开起始位置的重复数据
        /// </summary>
        /// <param name="data"></param>
        /// <param name="repeatStartIndex"></param>
        /// <param name="repeatEndIndex"></param>
        /// <param name="isPrev"></param>
        private void SpreadRepeatForward(TickData data, TickInfo_Period period, int repeatStartIndex, int repeatEndIndex)
        {
            /**
             * 找到调整结束位置
             * 算法：
             * 1.从起始位置开始查找
             * 2.一直找到(结束时间-开始时间)*2>重复的时间数+
             * 
             * 090000
             * 090000
             * 090000
             * 090000
             * 090000
             * 090002
             * 090003
             * 090003
             */
            //int timesEverySecond = 2;
            double timeRepeat = data.arr_time[repeatStartIndex];
            int repeatTimes = repeatEndIndex - repeatStartIndex + 1;
            int endIndex = -1;
            int capcity = 0;
            for (int i = repeatEndIndex + 1; i < data.Length; i++)
            {
                int currentDataCount = i - repeatEndIndex - 1 + repeatTimes;
                TimeSpan span = TimeUtils.Substract(data.arr_time[i], timeRepeat);
                int currentCapcity = timesEverySecond * (span.Minutes * 60 + span.Seconds);
                if (currentCapcity >= currentDataCount)
                {
                    endIndex = i - 1;
                    capcity = currentCapcity;
                    break;
                }
            }

            int allIndex = endIndex - repeatStartIndex + 1;
            double rate = (double)capcity / allIndex / 2;

            double endTime = Math.Round((double)data.TradingDay + period.EndTime, 6);
            double rptTime = data.arr_time[repeatStartIndex];
            for (int i = repeatStartIndex + 2; i <= endIndex; i++)
            {
                double time = TimeUtils.AddSeconds(rptTime, (int)(((double)(i - repeatStartIndex)) * rate));
                data.arr_time[i] = time < endTime ? time : endTime;
            }
        }
        

        private void SpreadRepeatBackward(TickData data, TickInfo_Period period, int repeatStartIndex, int repeatEndIndex)
        {
            /**
             * 
             * 145956
             * 145957
             * 145958
             * 145958
             * 150000
             * 150000
             * 150000
             * 150000
             * 150000
             * 
             * 该算法是处理向前扩散，比如上面例子，在一个时间上有太多的tick数据
             * 明显不正常，所以
             */
            //int timesEverySecond = 2;
            double timeRepeat = data.arr_time[repeatEndIndex];
            int repeatTimes = repeatEndIndex - repeatStartIndex + 1;
            int startIndex = -1;
            int capcity = 0;
            for (int i = repeatStartIndex - 1; i >= 0; i--)
            {
                int currentDataCount = repeatStartIndex - i - 1 + repeatTimes;
                TimeSpan span = TimeUtils.Substract(timeRepeat, data.arr_time[i]);
                int currentCapcity = timesEverySecond * (span.Minutes * 60 + span.Seconds);
                if (currentCapcity >= currentDataCount)
                {
                    startIndex = i + 1;
                    capcity = currentCapcity;
                    break;
                }
            }

            int allIndex = repeatEndIndex - startIndex + 1;
            double rate = (double)capcity / allIndex / 2;

            double startTime = Math.Round((double)data.TradingDay + period.StartTime, 6);
            double rptTime = data.arr_time[repeatStartIndex];
            for (int i = repeatEndIndex - 2; i >= startIndex; i--)
            {
                double time = TimeUtils.AddSeconds(rptTime, -(int)(((double)(repeatEndIndex - i)) * rate));
                data.arr_time[i] = time > startTime ? time : startTime;
            }
        }
    }
}
