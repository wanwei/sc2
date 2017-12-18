using com.wer.sc.data;
using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy.common.zigzag
{
    /// <summary>
    /// zigzag算法生成
    /// </summary>
    public class Zigzag
    {
        //转折起始长度
        private int turnLength;

        //确认高低点位置的长度
        private int highLowLength;

        private IKLineData klineData;

        //private List<ZigzagPoint> highPoints = new List<ZigzagPoint>();

        //private List<ZigzagPoint> lowPoints = new List<ZigzagPoint>();

        private List<ZigzagPoint> currentPoints = new List<ZigzagPoint>();

        private List<ZigzagPoint> mergedPoints = new List<ZigzagPoint>();

        public Zigzag(IKLineData klineData) : this(klineData, 2, 5)
        {
        }

        public Zigzag(IKLineData klineData, int turnLength, int highLowLength)
        {
            this.klineData = klineData;
            this.turnLength = turnLength;
            this.highLowLength = highLowLength;
        }

        #region Loop

        private const int LASTTYPE_UNKNOWN = 0;
        private const int LASTTYPE_LOW = -1;
        private const int LASTTYPE_HIGH = 1;

        public void Loop(IKLineData klineData, int barPos)
        {
            if (barPos < highLowLength)
                return;
            IList<float> arr_HighPrice = klineData.Arr_High;
            IList<float> arr_LowPrice = klineData.Arr_Low;

            bool hasFindLowPoint = IsPreviousBarTheLowestBar(arr_LowPrice, barPos, turnLength, highLowLength);
            bool hasFindHighPoint = IsPreviousBarTheHighestBar(arr_HighPrice, barPos, turnLength, highLowLength);
            if (!hasFindLowPoint && !hasFindHighPoint)
                return;
            int lastPointType = GetLastPointType(barPos);

            int turnPointIndex = barPos - turnLength;
            if (turnPointIndex < 0)
                return;
            //找到第一个高低点，此时不知道第一个是高点还是低点
            if (lastPointType == LASTTYPE_UNKNOWN)
            {
                if (hasFindHighPoint)
                    AddHighPoint(klineData, turnPointIndex);
                else
                    AddLowPoint(klineData, turnPointIndex);
            }
            //上一个是高点，找低点
            else if (lastPointType == LASTTYPE_HIGH)
            {
                if (hasFindHighPoint)
                {
                    ReplaceHighPoint(arr_HighPrice, turnPointIndex);
                }
                else if (hasFindLowPoint)
                {
                    AddLowPoint(klineData, turnPointIndex);
                    CalcMergePoint();
                }
            }
            //上一个是低点，找高点
            else
            {
                if (hasFindLowPoint)
                {
                    ReplaceLowPoint(arr_LowPrice, turnPointIndex);
                }
                else if (hasFindHighPoint)
                {
                    AddHighPoint(klineData, turnPointIndex);
                    CalcMergePoint();
                }
            }
        }

        private bool IsPreviousBarTheLowestBar(IList<float> prices, int currentIndex, int previousLength, int checkLength)
        {
            float previousPrice = MathUtils.GetPreviousData(prices, currentIndex, previousLength);
            float recentLow = MathUtils.Lowest(prices, currentIndex, checkLength);
            return previousPrice == recentLow;
        }

        private bool IsPreviousBarTheHighestBar(IList<float> prices, int currentIndex, int previousLength, int checkLength)
        {
            float previousPrice = MathUtils.GetPreviousData(prices, currentIndex, previousLength);
            float recentHigh = MathUtils.Highest(prices, currentIndex, checkLength);
            return previousPrice == recentHigh;
        }

        private int GetLastPointType(int barPos)
        {
            if (currentPoints.Count == 0)
                return LASTTYPE_UNKNOWN;
            ZigzagPoint point = currentPoints.Last();
            if (point.IsHigh)
                return LASTTYPE_HIGH;
            return LASTTYPE_LOW;
        }

        private void AddLowPoint(IKLineData klineData, int barPos)
        {
            ZigzagPoint point = new ZigzagPoint(klineData, barPos, false);
            //this.lowPoints.Add(point);
            this.currentPoints.Add(point);
        }

        private void ReplaceLowPoint(IList<float> arr_LowPrice, int barPos)
        {
            float currentLowPrice = arr_LowPrice[barPos];
            float recentLowPrice = currentPoints.Last().GetBar().Low;
            if (currentLowPrice > recentLowPrice)
                return;

            currentPoints.RemoveAt(currentPoints.Count - 1);
            currentPoints.Add(new ZigzagPoint(klineData, barPos, false));
        }

        private void AddHighPoint(IKLineData klineData, int barPos)
        {
            ZigzagPoint point = new ZigzagPoint(klineData, barPos, true);
            //this.highPoints.Add(point);
            this.currentPoints.Add(point);
        }

        private void ReplaceHighPoint(IList<float> arr_HighPrice, int barPos)
        {
            float currentHighPrice = arr_HighPrice[barPos];
            float recentHighPrice = currentPoints.Last().GetBar().Low;
            if (currentHighPrice < recentHighPrice)
                return;

            currentPoints.RemoveAt(currentPoints.Count - 1);
            currentPoints.Add(new ZigzagPoint(klineData, barPos, true));
        }

        private void CalcMergePoint()
        {
            if (currentPoints.Count < 2)
                return;
            ZigzagPoint lastSurePoint = currentPoints[currentPoints.Count - 2];
            //mergedPoints.Add(lastPoint);
        }

        #endregion

        public List<ZigzagPoint> GetPoints()
        {
            return currentPoints;
        }

        public List<ZigzagPoint> GetMergedPoints()
        {
            return mergedPoints;
        }
    }
}