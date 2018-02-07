using com.wer.sc.data.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data
{
    /// <summary>
    /// K线时间信息
    /// 包括每一个bar的起止时间
    /// 每日的起止bar
    /// </summary>
    public class KLineDataTimeInfo : IKLineDataTimeInfo
    {
        private KLinePeriod klinePeriod;

        private IList<double[]> klineTimeInfo;        

        public KLineDataTimeInfo(IList<double[]> klineTimeInfo, KLinePeriod klinePeriod)
        {
            this.klineTimeInfo = klineTimeInfo;
            this.klinePeriod = klinePeriod;            
        }

        /// <summary>
        /// 得到指定位置bar的开始时间
        /// </summary>
        /// <param name="barPos"></param>
        /// <returns></returns>
        public double GetKLineTimeStart(int barPos)
        {
            return this.klineTimeInfo[barPos][0];
        }

        /// <summary>
        /// 得到指定位置bar的结束时间
        /// </summary>
        /// <param name="barPos"></param>
        /// <returns></returns>
        public double GetKLineTimeEnd(int barPos)
        {
            return this.klineTimeInfo[barPos][1];
        }

        /// <summary>
        /// 得到barPos对应的K线的
        /// </summary>
        /// <param name="barPos"></param>
        /// <returns></returns>
        public double[] GetKLineTime(int barPos)
        {
            return klineTimeInfo[barPos];
        }

        public KLinePeriod KLinePeriod
        {
            get { return klinePeriod; }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("KLinePeriod:").Append(klinePeriod);
            for (int i = 0; i < klineTimeInfo.Count; i++)
            {
                sb.Append("\r\n");
                double[] timeInfo = klineTimeInfo[i];
                sb.Append(timeInfo[0]).Append("-").Append(timeInfo[1]).Append(",");
            }
            return sb.ToString();
        }
    }
}