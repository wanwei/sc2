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

        private bool isSurePoint;

        private bool isMergedPoint;

        public ZigzagPoint(IKLineData klineData, int barPos, bool isHigh)
        {
            this.klineData = klineData;
            this.barPos = barPos;
            this.isHigh = isHigh;
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

        public bool IsSurePoint
        {
            get
            {
                return isSurePoint;
            }

            set
            {
                isSurePoint = value;
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
    }
}