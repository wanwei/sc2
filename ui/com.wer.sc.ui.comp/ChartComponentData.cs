using com.wer.sc.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.ui.comp
{
    public class ChartComponentData
    {
        //当前绘制图形的Code
        private string code;

        //当前绘制图形的时间
        private double time;

        //当前绘制的图形类型
        private ChartType chartType = ChartType.KLine;

        //K线周期
        private KLinePeriod klinePeriod;

        private int showKLineIndex;

        public ChartComponentData(string code, double time, KLinePeriod klinePeriod, int showKLineIndex)
        {
            this.code = code;
            this.time = time;
            this.klinePeriod = klinePeriod;
            this.showKLineIndex = showKLineIndex;
        }

        public ChartComponentData(ChartComponentData compData)
        {
            this.code = compData.code;
            this.time = compData.time;
            this.chartType = compData.chartType;
            this.klinePeriod = compData.klinePeriod;
            this.showKLineIndex = compData.showKLineIndex;
        }

        public string Code
        {
            get
            {
                return code;
            }
        }

        public ChartType ChartType
        {
            get
            {
                return chartType;
            }
        }

        public double Time
        {
            get
            {
                return time;
            }
        }

        public KLinePeriod KlinePeriod
        {
            get
            {
                return klinePeriod;
            }
        }

        public int ShowKLineIndex
        {
            get { return showKLineIndex; }
        }

        public bool Change(string code, double time, KLinePeriod period)
        {
            bool isChanged = Change(code);
            if (this.time != time)
            {
                this.time = time;
                isChanged = true;
            }
            this.klinePeriod = period;
            return isChanged;
        }

        /// <summary>
        /// 修改图中显示的品种
        /// </summary>
        /// <param name="code"></param>
        public bool Change(string code)
        {
            if (this.code.Equals(code))
                return false;
            this.code = code;
            return true;
        }

        /// <summary>
        /// 修改当前时间
        /// </summary>
        /// <param name="time"></param>
        public bool Change(double time)
        {
            if (this.time == time)
                return false;
            this.time = time;
            return true;
        }

        /// <summary>
        /// 修改图中显示的品种和当前时间
        /// </summary>
        /// <param name="code"></param>
        /// <param name="time"></param>
        public bool Change(String code, double time)
        {
            bool isChanged = Change(code);
            if (Change(time))
                isChanged = true;
            return isChanged;
        }

        public bool Change(KLinePeriod klinePeriod)
        {
            if (this.klinePeriod.Equals(klinePeriod))
                return false;
            this.klinePeriod = klinePeriod;
            return true;
        }

        public bool Change(ChartType chartType)
        {
            if (this.chartType == chartType)
                return false;
            this.chartType = chartType;
            return true;
        }

        public bool ChangeKLineIndex(int showKlineIndex)
        {
            if (this.showKLineIndex == showKlineIndex)
                return false;
            this.showKLineIndex = showKlineIndex;
            return true;
        }

        public bool CheckData()
        {
            if (time == 0)
                return false;
            if (code == null)
                return false;
            if (klinePeriod == null)
                return false;
            return true;
        }

        public Object Clone()
        {
            ChartComponentData compData = new ChartComponentData(this.code, this.time, this.klinePeriod, this.showKLineIndex);
            compData.chartType = this.chartType;
            return compData;
        }

        public void CopyFrom(ChartComponentData compData)
        {
            this.code = compData.code;
            this.time = compData.time;
            this.klinePeriod = compData.klinePeriod;
            this.chartType = compData.chartType;
            this.showKLineIndex = compData.showKLineIndex;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(code).Append(",");
            sb.Append(time).Append(",");
            sb.Append(chartType).Append(",");
            sb.Append(klinePeriod).Append(",");
            sb.Append(showKLineIndex);
            return sb.ToString();
        }

        public override bool Equals(object obj)
        {
            if (!(obj is ChartComponentData))
                return false;
            ChartComponentData compData = (ChartComponentData)obj;
            return Object.Equals(this.code, compData.code) && this.time == compData.time
                && this.chartType == compData.chartType && this.klinePeriod == compData.klinePeriod
                && this.showKLineIndex == compData.showKLineIndex;
        }

        public override int GetHashCode()
        {
            int hash = (int)time;
            hash += hash * 10 + code == null ? 0 : code.GetHashCode();
            hash += hash * 10 + chartType.GetHashCode();
            hash += hash * 10 + klinePeriod.GetHashCode();
            hash += hash * 10 + showKLineIndex;
            return hash;
        }
    }

    public enum ChartType
    {
        KLine = 0,

        TimeLine = 1,

        Tick = 2
    }
}
