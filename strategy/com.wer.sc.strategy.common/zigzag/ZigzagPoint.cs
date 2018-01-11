using com.wer.sc.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy.common.zigzag
{
    public class ZigzagPoint
    {
        private IKLineData klineData;

        private int barPos;

        private bool isHigh;

        private bool isMergedPoint;

        //private List<ZigzagPoint> mergedPoints;

        public ZigzagPoint(IKLineData klineData, int barPos, bool isHigh)
        {
            this.klineData = klineData;
            this.barPos = barPos;
            this.isHigh = isHigh;
        }

        public float Price
        {
            get
            {
                return isHigh ? GetBar().High : GetBar().Low;
            }
        }

        public IKLineBar GetBar()
        {
            return klineData.GetBar(barPos);
        }

        public int BarPos
        {
            get { return barPos; }
        }

        /// <summary>
        /// 得到或设置是否是高点
        /// </summary>
        public bool IsHigh
        {
            get
            {
                return isHigh;
            }
        }

        public bool IsMergedPoint
        {
            get
            {
                return isMergedPoint;
            }

            set
            {
                isMergedPoint = value;
            }
        }

        //public void AddMergePoint(ZigzagPoint point)
        //{
        //    if (this.mergedPoints == null)
        //        this.mergedPoints = new List<ZigzagPoint>();
        //    this.mergedPoints.Add(point);
        //}

        //public List<ZigzagPoint> MergedPoints
        //{
        //    get { return mergedPoints; }
        //}

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(isHigh ? "high" : "low").Append(",");
            sb.Append(barPos).Append(",");
            sb.Append(Price).Append(",");
            sb.Append(GetBar().Time);
            return sb.ToString();
        }
    }
}