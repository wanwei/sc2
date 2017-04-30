using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data
{
    /// <summary>
    /// 分时线数据的抽象类
    /// </summary>
    public abstract class TimeLineData_Abstract : ITimeLineData
    {
        private String code;

        private int barPos;

        private float yesterdayEnd;


        public String Code
        {
            get { return code; }
            set { code = value; }
        }

        public int BarPos
        {
            get { return barPos; }
            set { barPos = value; }
        }

        public void SetBarPosByTime(double time)
        {
            int index = IndexOfTime(time);
            this.BarPos = index;
        }

        public float YesterdayEnd
        {
            get
            {
                return yesterdayEnd;
            }
            set
            {
                yesterdayEnd = value;
            }
        }

        public int Date
        {
            get { return (int)Arr_Time[Length - 1]; }
        }

        public double Time
        {
            get
            {
                return Arr_Time[BarPos];
            }
        }

        public float Price
        {
            get
            {
                return Arr_Price[BarPos];
            }
        }

        public float UpRange
        {
            get
            {
                return Arr_UpRange[BarPos];
            }
        }

        public float UpPercent
        {
            get
            {
                return Arr_UpPercent[BarPos];
            }
        }

        public int Mount
        {
            get
            {
                return Arr_Mount[BarPos];
            }
        }

        public int Hold
        {
            get
            {
                return Arr_Hold[BarPos];
            }
        }

        public ITimeLineBar GetCurrentBar()
        {
            return this;
        }

        public ITimeLineBar GetBar(int index)
        {
            return new TimeLineBar_TimeLineData(this, index);
        }

        #region 完整数据信息

        public int IndexOfTime(double time)
        {
            double t = Math.Round(time, 4);
            return this.Arr_Time.IndexOf(t);
        }

        public int Length { get { return Arr_Time.Count; } }

        public abstract IList<double> Arr_Time { get; }

        public abstract IList<float> Arr_Price { get; }

        public abstract IList<int> Arr_Mount { get; }

        public abstract IList<int> Arr_Hold { get; }

        public abstract IList<float> Arr_UpPercent { get; }

        public abstract IList<float> Arr_UpRange { get; }

        #endregion

        public override String ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Time).Append(",");
            sb.Append(Price).Append(",");
            sb.Append(UpRange).Append(",");
            sb.Append(UpPercent).Append(",");
            sb.Append(Mount).Append(",");
            sb.Append(Hold);
            return sb.ToString();
        }

        public String ToString(int index)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Arr_Time).Append(",");
            sb.Append(Arr_Price).Append(",");
            sb.Append(Arr_UpRange).Append(",");
            sb.Append(Arr_UpPercent).Append(",");
            sb.Append(Arr_Mount).Append(",");
            sb.Append(Arr_Hold);
            return sb.ToString();
        }
    }
}