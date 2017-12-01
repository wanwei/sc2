using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data
{
    /// <summary>
    /// 分时线数据点的抽象类
    /// </summary>
    public abstract class TimeLineBar_Abstract : ITimeLineBar
    {
        public abstract string Code { get; set; }

        public abstract double Time { get; set; }

        public abstract float Price { get; set; }
    
        public abstract int Mount { get; set; }

        public abstract int Hold { get; set; }

        public abstract float UpPercent { get; set; }

        public abstract float UpRange { get; set; }

        public override string ToString()
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
    }
}
