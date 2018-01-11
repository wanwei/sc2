using com.wer.sc.data;
using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy.common.zigzag
{
    public class Zigzag_Simple : IDataLooper
    {
        public const int OPERATOR_NONE = 0;

        public const int OPERATOR_ADDHIGH = 1;

        public const int OPERATOR_ADDLOW = 2;

        public const int OPERATOR_REPLACEHIGH = 3;

        public const int OPERATOR_REPLACELOW = 4;

        //转折起始长度
        private int turnLength;

        //确认高低点位置的长度
        private int highLowLength;

        private IKLineData klineData;

        private List<ZigzagPoint> currentPoints = new List<ZigzagPoint>();

        private int currentOperator = OPERATOR_NONE;

        public int CurrentOperator
        {
            get { return currentOperator; }
        }

        public Zigzag_Simple(IKLineData klineData) : this(klineData, 2, 5)
        {
        }

        public Zigzag_Simple(IKLineData klineData, int turnLength, int highLowLength)
        {
            this.klineData = klineData;
            this.turnLength = turnLength;
            this.highLowLength = highLowLength;
        }

        private const int LASTTYPE_UNKNOWN = 0;
        private const int LASTTYPE_LOW = -1;
        private const int LASTTYPE_HIGH = 1;

        public void Loop(int barPos)
        {
            this.currentOperator = OPERATOR_NONE;
            if (barPos < highLowLength)
                return;
            IList<float> arr_HighPrice = klineData.Arr_High;
            IList<float> arr_LowPrice = klineData.Arr_Low;

            int lastPointType = GetLastPointType(barPos);

            bool hasFindLowPoint = IsPreviousBarTheLowestBar(arr_LowPrice, barPos, turnLength, highLowLength);
            bool hasFindHighPoint = IsPreviousBarTheHighestBar(arr_HighPrice, barPos, turnLength, highLowLength);
            if (!hasFindLowPoint && !hasFindHighPoint)
            {
                return;
            }
            //    if (lastPointType == LASTTYPE_HIGH)
            //    {
            //        ZigzagPoint point = currentPoints.Last();
            //        if (point.Price < arr_HighPrice[barPos])
            //        {
            //            ReplaceHighPoint(arr_HighPrice, barPos);
            //        }                    
            //    }
            //    else if (lastPointType == LASTTYPE_LOW)
            //    {
            //        ZigzagPoint point = currentPoints.Last();
            //        if (point.Price > arr_LowPrice[barPos])
            //        {
            //            ReplaceLowPoint(arr_LowPrice, barPos);
            //        }                    
            //    }
            //    return;
            //}

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
            this.currentPoints.Add(point);
            this.currentOperator = OPERATOR_ADDLOW;
        }

        private void ReplaceLowPoint(IList<float> arr_LowPrice, int barPos)
        {
            float currentLowPrice = arr_LowPrice[barPos];
            float recentLowPrice = currentPoints.Last().GetBar().Low;
            if (currentLowPrice > recentLowPrice)
                return;

            currentPoints.RemoveAt(currentPoints.Count - 1);
            currentPoints.Add(new ZigzagPoint(klineData, barPos, false));
            this.currentOperator = OPERATOR_REPLACELOW;
        }

        private void AddHighPoint(IKLineData klineData, int barPos)
        {
            ZigzagPoint point = new ZigzagPoint(klineData, barPos, true);
            //this.highPoints.Add(point);
            this.currentPoints.Add(point);
            this.currentOperator = OPERATOR_ADDHIGH;
        }

        private void ReplaceHighPoint(IList<float> arr_HighPrice, int barPos)
        {
            float currentHighPrice = arr_HighPrice[barPos];
            float recentHighPrice = currentPoints.Last().GetBar().High;
            if (currentHighPrice < recentHighPrice)
                return;

            currentPoints.RemoveAt(currentPoints.Count - 1);
            currentPoints.Add(new ZigzagPoint(klineData, barPos, true));
            this.currentOperator = OPERATOR_REPLACEHIGH;
        }

        public List<ZigzagPoint> GetPoints()
        {
            return currentPoints;
        }
    }
}
