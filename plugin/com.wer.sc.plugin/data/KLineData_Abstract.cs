using com.wer.sc.data.utils;
using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data
{
    /// <summary>
    /// K线数据的实现
    /// </summary>
    public abstract class KLineData_Abstract : KLineBar_Abstract, IKLineData
    {
        private string code;
        private KLinePeriod period;

        private IList<float> arr_height;
        private IList<float> arr_HeightPercent;
        private IList<float> arr_blockhigh;
        private IList<float> arr_blocklow;
        private IList<float> arr_blockheight;
        private IList<float> arr_percentBlockHeight;
        private IList<float> arr_UpPercent;

        private int barPos;

        #region 实现IKLineBar

        public override string Code
        {
            get
            {
                return code;
            }
            set
            {
                this.code = value;
            }
        }

        public override double Time
        {
            get
            {
                return Arr_Time[barPos];
            }
            set { throw new NotImplementedException(); }
        }

        public override float Start
        {
            get
            {
                return Arr_Start[barPos];
            }
            set { throw new NotImplementedException(); }
        }

        public override float High
        {
            get
            {
                return Arr_High[BarPos];
            }
            set { throw new NotImplementedException(); }
        }

        public override float Low
        {
            get
            {
                return Arr_Low[BarPos];
            }
            set { throw new NotImplementedException(); }
        }

        public override float End
        {
            get
            {
                return Arr_End[BarPos];
            }
            set { throw new NotImplementedException(); }
        }

        public override int Mount
        {
            get
            {
                return Arr_Mount[BarPos];
            }
            set { throw new NotImplementedException(); }
        }

        public override float Money
        {
            get
            {
                return Arr_Money[BarPos];
            }
            set { throw new NotImplementedException(); }
        }

        public override int Hold
        {
            get
            {
                return Arr_Hold[BarPos];
            }
            set { throw new NotImplementedException(); }
        }

        #endregion

        #region 完整数据

        public IKLineBar GetCurrentBar()
        {
            return this;
        }

        public IKLineData GetRange(int start, int end)
        {
            IKLineData data = this;
            KLineData d1 = new KLineData(end - start + 1);
            for (int i = start; i <= end; i++)
            {
                d1.arr_time[i - start] = data.Arr_Time[i];
                d1.arr_start[i - start] = data.Arr_Start[i];
                d1.arr_high[i - start] = data.Arr_High[i];
                d1.arr_low[i - start] = data.Arr_Low[i];
                d1.arr_end[i - start] = data.Arr_End[i];
                d1.arr_mount[i - start] = data.Arr_Mount[i];
                d1.arr_money[i - start] = data.Arr_Money[i];
                d1.arr_hold[i - start] = data.Arr_Hold[i];
            }
            return d1;
        }
        /// <summary>
        /// 得到一段子K线数据，该方法和GetRange区别是生成的新k线和老K线公用K线
        /// </summary>
        /// <param name="startPos"></param>
        /// <param name="endPos"></param>
        /// <returns></returns>
        public IKLineData Sub(int startPos, int endPos)
        {
            return new KLineData_Sub(this, startPos, endPos);
        }

        public IKLineBar GetAggrKLineBar(int startIndex, int endIndex)
        {
            KLineBar bar = new KLineBar();
            bar.Time = this.Arr_Time[startIndex];
            bar.Start = this.Arr_Start[startIndex];
            bar.End = this.Arr_End[endIndex];
            bar.Hold = this.Arr_Hold[endIndex];

            float high = float.MinValue;
            float low = float.MaxValue;
            int mount = 0;
            float money = 0;
            for (int i = startIndex; i <= endIndex; i++)
            {
                float chigh = this.Arr_High[i];
                float clow = this.Arr_Low[i];
                high = high < chigh ? chigh : high;
                low = low > clow ? clow : low;
                mount += this.Arr_Mount[i];
                money += this.Arr_Money[i];
            }
            bar.High = high;
            bar.Low = low;
            bar.Mount = mount;
            bar.Money = money;
            return bar;
        }

        public virtual KLinePeriod Period
        {
            get
            {
                if (period == null)
                    period = GetPeriod(Arr_Time[0], Arr_Time[1]);
                return period;
            }
            set
            {
                this.period = value;
            }
        }

        public static KLinePeriod GetPeriod(double time1, double time2)
        {
            KLinePeriod period = new KLinePeriod();
            TimeSpan span = TimeUtils.Substract(time2, time1);
            if (span.Seconds != 0)
            {
                period.Period = span.Seconds;
                period.PeriodType = KLineTimeType.SECOND;
            }
            else if (span.Minutes != 0)
            {
                period.Period = span.Minutes;
                period.PeriodType = KLineTimeType.MINUTE;
            }
            else if (span.Hours != 0)
            {
                period.Period = span.Hours;
                period.PeriodType = KLineTimeType.HOUR;
            }
            else
            {
                period.Period = span.Days;
                period.PeriodType = KLineTimeType.DAY;
            }
            return period;
        }

        public static IKLineData Merge(IList<IKLineData> dataList)
        {
            int len = 0;
            for (int i = 0; i < dataList.Count; i++)
            {                
                len += dataList[i].Length;
            }
            KLineData data = new KLineData(len);
            int offset = 0;
            for (int i = 0; i < dataList.Count; i++)
            {
                IKLineData d1 = dataList[i];
                Copy(data, offset, d1, 0, d1.Length);
                offset += d1.Length;
            }

            return data;
        }

        private static void Copy(KLineData targetData, int targetIndex, IKLineData srcData, int srcIndex, int length)
        {
            for (int i = srcIndex; i < srcIndex + length; i++)
            {
                int currentTargetIndex = targetIndex + srcIndex + i;
                targetData.arr_time[currentTargetIndex] = srcData.Arr_Time[i];
                targetData.arr_start[currentTargetIndex] = srcData.Arr_Start[i];
                targetData.arr_high[currentTargetIndex] = srcData.Arr_High[i];
                targetData.arr_low[currentTargetIndex] = srcData.Arr_Low[i];
                targetData.arr_end[currentTargetIndex] = srcData.Arr_End[i];
                targetData.arr_mount[currentTargetIndex] = srcData.Arr_Mount[i];
                targetData.arr_money[currentTargetIndex] = srcData.Arr_Money[i];
                targetData.arr_hold[currentTargetIndex] = srcData.Arr_Hold[i];
            }
        }

        public int IndexOfTime(double time)
        {
            return TimeIndeierUtils.IndexOfTime_KLine(this, time);
        }

        public override String ToString()
        {
            return KLineToString(this);
        }

        public String ToString(int index)
        {
            return KLineToString(this, index);
        }

        public static String KLineToString(IKLineData klineData)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(klineData.Time).Append(",");
            sb.Append(klineData.Start).Append(",");
            sb.Append(klineData.High).Append(",");
            sb.Append(klineData.Low).Append(",");
            sb.Append(klineData.End).Append(",");
            sb.Append(klineData.Mount).Append(",");
            sb.Append(klineData.Money).Append(",");
            sb.Append(klineData.Hold);
            return sb.ToString();
        }

        public static String KLineToString(IKLineData klineData, int index)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(klineData.Arr_Time[index]).Append(",");
            sb.Append(klineData.Arr_Start[index]).Append(",");
            sb.Append(klineData.Arr_High[index]).Append(",");
            sb.Append(klineData.Arr_Low[index]).Append(",");
            sb.Append(klineData.Arr_End[index]).Append(",");
            sb.Append(klineData.Arr_Mount[index]).Append(",");
            sb.Append(klineData.Arr_Money[index]).Append(",");
            sb.Append(klineData.Arr_Hold[index]);
            return sb.ToString();
        }

        public IKLineBar GetBar(int index)
        {
            return new KLineBar_KLineData(this, index);
        }

        public int Length
        {
            get
            {
                return Arr_Time.Count;
            }
        }

        public int BarPos
        {
            get
            {
                return barPos;
            }

            set
            {
                barPos = value;
            }
        }

        public abstract IList<double> Arr_Time { get; }

        public abstract IList<float> Arr_Start { get; }


        public abstract IList<float> Arr_High { get; }

        public abstract IList<float> Arr_Low { get; }

        public abstract IList<float> Arr_End { get; }

        public abstract IList<int> Arr_Mount { get; }

        public abstract IList<float> Arr_Money { get; }

        public abstract IList<int> Arr_Hold { get; }

        /// <summary>
        /// 得到每个k线的振幅数组
        /// </summary>
        public virtual IList<float> Arr_Height
        {
            get
            {
                if (arr_height != null)
                    return arr_height;

                this.arr_height = new float[Length];
                for (int i = 0; i < Length; i++)
                    arr_height[i] = Arr_High[i] - Arr_Low[i];
                return arr_height;
            }
        }

        /// <summary>
        /// 得到每个k线的振幅百分比数组
        /// </summary>
        public virtual IList<float> Arr_HeightPercent
        {
            get
            {
                if (arr_HeightPercent != null)
                    return arr_HeightPercent;
                arr_HeightPercent = new float[Length];
                for (int i = 0; i < Length; i++)
                {
                    arr_HeightPercent[i] = (float)NumberUtils.percent(Math.Abs(Arr_High[i] - Arr_Low[i]), Arr_End[i]);
                }
                return arr_HeightPercent;
            }
        }

        public virtual IList<float> Arr_BlockHigh
        {
            get
            {
                if (arr_blockhigh != null)
                    return arr_blockhigh;
                arr_blockhigh = new float[Length];
                for (int i = 0; i < Length; i++)
                {
                    arr_blockhigh[i] = Arr_Start[i] > Arr_End[i] ? Arr_Start[i] : Arr_End[i];
                }

                return arr_blockhigh;
            }
        }

        public virtual IList<float> Arr_BlockLow
        {
            get
            {
                if (arr_blocklow != null)
                    return arr_blocklow;
                arr_blocklow = new float[Length];
                for (int i = 0; i < Length; i++)
                {
                    arr_blocklow[i] = Arr_Start[i] < Arr_End[i] ? Arr_Start[i] : Arr_End[i];
                }

                return arr_blocklow;
            }
        }

        public virtual IList<float> Arr_BlockHeight
        {
            get
            {
                if (arr_blockheight != null)
                    return arr_blockheight;
                arr_blockheight = new float[Length];
                for (int i = 0; i < Length; i++)
                {
                    arr_blockheight[i] = Math.Abs(Arr_Start[i] - Arr_End[i]);
                }

                return arr_blockheight;
            }
        }

        public virtual IList<float> Arr_BlockHeightPercent
        {
            get
            {
                if (arr_percentBlockHeight != null)
                    return arr_percentBlockHeight;
                arr_percentBlockHeight = new float[Length];
                for (int i = 0; i < Length; i++)
                {
                    arr_percentBlockHeight[i] = (float)NumberUtils.percent(Math.Abs(Arr_Start[i] - Arr_End[i]), Arr_End[i]);
                }
                return arr_percentBlockHeight;
            }
        }

        public virtual IList<float> Arr_UpPercent
        {
            get
            {
                if (arr_UpPercent != null)
                    return arr_UpPercent;
                arr_UpPercent = new float[Length];
                for (int i = 0; i < Length; i++)
                {
                    if (i == 0)
                        arr_UpPercent[i] = (float)NumberUtils.percent(Arr_End[i] - Arr_Start[i], Arr_Start[i]);
                    else
                        arr_UpPercent[i] = (float)NumberUtils.percent(Arr_End[i] - Arr_End[i - 1], Arr_End[i - 1]);
                }

                return arr_UpPercent;
            }
        }

        #endregion
    }
}
