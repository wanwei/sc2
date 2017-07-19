using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data
{
    /// <summary>
    /// K线柱子的抽象实现
    /// 该抽象类实现了该K线柱子一些数据的计算，如中间价等
    /// </summary>
    public abstract class KLineBar_Abstract : IKLineBar
    {
        public abstract string Code { get; set; }

        public abstract double Time { get; set; }

        public abstract float Start { get; set; }

        public abstract float High { get; set; }

        public abstract float Low { get; set; }

        public abstract float End { get; set; }

        public abstract int Mount { get; set; }

        public abstract float Money { get; set; }

        public abstract int Hold { get; set; }

        public float Height
        {
            get
            {
                return High - Low;
            }
        }

        public float TopShadow
        {
            get
            {
                return High - BlockHigh;
            }
        }

        public float BottomShadow
        {
            get
            {
                return BlockLow - Low;
            }
        }

        /**
         * 得到当日中间价
         * @return
         */
        public float Middle
        {
            get
            {
                return (High + Low) / 2;
            }
        }

        /**
         * 得到开盘收盘的中间价
         * @return
         */
        public float BlockMiddle
        {
            get
            {
                return (BlockHigh + BlockLow) / 2;
            }
        }

        /**
         * 得到开盘收盘价格低的那个
         * @return
         */
        public float BlockLow
        {
            get
            {
                return Start < End ? Start : End;
            }
        }

        /**
         * 得到开盘收盘价格高的那个
         * @return
         */
        public float BlockHigh
        {
            get
            {
                return Start > End ? Start : End;
            }
        }

        /**
         * 得到开盘收盘价格差
         * @return
         */
        public float BlockHeight
        {
            get
            {
                return BlockHigh - BlockLow;
            }
        }

        public float HeightPercent
        {
            get
            {
                return (float)NumberUtils.percent(Height, End);
            }
        }
        public float BlockHeightPercent
        {
            get
            {
                return (float)NumberUtils.percent(BlockHeight, End);
            }
        }

        public bool isRed()
        {
            return End >= Start;
        }

        public void Copy2KLineData(KLineData klineData, int currentKLineIndex)
        {
            klineData.arr_time[currentKLineIndex] = this.Time;
            klineData.arr_start[currentKLineIndex] = this.Start;
            klineData.arr_high[currentKLineIndex] = this.High;
            klineData.arr_low[currentKLineIndex] = this.Low;
            klineData.arr_end[currentKLineIndex] = this.End;
            klineData.arr_mount[currentKLineIndex] = this.Mount;
            klineData.arr_money[currentKLineIndex] = this.Money;
            klineData.arr_hold[currentKLineIndex] = this.Hold;
        }

        public override String ToString()
        {
            StringBuilder sb = new StringBuilder();
            //sb.Append(Code).Append(",");
            sb.Append(Time).Append(",");
            sb.Append(Start).Append(",");
            sb.Append(High).Append(",");
            sb.Append(Low).Append(",");
            sb.Append(End).Append(",");
            sb.Append(Mount).Append(",");
            sb.Append(Money).Append(",");
            sb.Append(Hold);
            return sb.ToString();
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (GetType() != obj.GetType())
                return false;
            KLineBar_Abstract klineBar = (KLineBar_Abstract)obj;
            return this.Time == klineBar.Time && this.Start == klineBar.Time && this.High == klineBar.High && this.Low == klineBar.Low 
                && this.End == klineBar.End && this.Mount == klineBar.Mount && this.Money == klineBar.Money && this.Hold == klineBar.Hold;
        }
    }
}
