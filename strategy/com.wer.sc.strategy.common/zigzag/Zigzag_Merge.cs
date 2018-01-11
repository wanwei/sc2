using com.wer.sc.strategy.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy.common.zigzag
{
    public class Zigzag_Merge : IDataLooper
    {
        private List<ZigzagPoint> mergedPoints = new List<ZigzagPoint>();

        private Zigzag_Simple zigzag_Simple;

        public Zigzag_Merge(Zigzag_Simple zigzag_Simple)
        {
            this.zigzag_Simple = zigzag_Simple;
        }

        public void Loop(int barPos)
        {
            this.zigzag_Simple.Loop(barPos);
            if (this.zigzag_Simple.CurrentOperator == Zigzag_Simple.OPERATOR_ADDHIGH ||
                this.zigzag_Simple.CurrentOperator == Zigzag_Simple.OPERATOR_ADDLOW)
            {
                List<ZigzagPoint> currentPoints = this.zigzag_Simple.GetPoints();
                if (currentPoints.Count < 2)
                    return;
                ZigzagPoint lastSurePoint = currentPoints[currentPoints.Count - 2];
                AddNewSurePoint(lastSurePoint);
            }
        }

        private void AddNewSurePoint(ZigzagPoint lastSurePoint)
        {
            this.mergedPoints.Add(lastSurePoint);
            if (this.mergedPoints.Count >= 4)
            {
                if (ShouldMerge())
                {
                    int firstMergedIndex = this.mergedPoints.Count - 3;
                    this.mergedPoints[firstMergedIndex + 2].IsMergedPoint = true;
                    this.mergedPoints.RemoveAt(firstMergedIndex + 1);
                    this.mergedPoints.RemoveAt(firstMergedIndex);
                }
            }
        }

        private bool ShouldMerge()
        {
            int firstIndex = this.mergedPoints.Count - 4;
            ZigzagPoint point_1 = this.mergedPoints[firstIndex];
            ZigzagPoint point_2 = this.mergedPoints[firstIndex + 1];
            ZigzagPoint point_3 = this.mergedPoints[firstIndex + 2];
            ZigzagPoint point_4 = this.mergedPoints[firstIndex + 3];

            if (IsDown(point_1, point_2, point_3, point_4) && !point_4.IsHigh)
            {
                if ((point_2.IsMergedPoint && point_2.IsHigh) || (point_3.IsMergedPoint && point_3.IsHigh))
                {
                    return false;
                }
                return true;
            }
            if (IsUp(point_1, point_2, point_3, point_4) && point_4.IsHigh)
            {
                if ((point_2.IsMergedPoint && !point_2.IsHigh) || (point_3.IsMergedPoint && !point_3.IsHigh))
                {
                    return false;
                }
                return true;
            }
            return false;
        }

        private static bool IsUp(ZigzagPoint point_1, ZigzagPoint point_2, ZigzagPoint point_3, ZigzagPoint point_4)
        {
            return point_1.Price <= point_3.Price && point_2.Price <= point_4.Price;
        }

        private static bool IsDown(ZigzagPoint point_1, ZigzagPoint point_2, ZigzagPoint point_3, ZigzagPoint point_4)
        {
            return point_1.Price >= point_3.Price && point_2.Price >= point_4.Price;
        }

        private bool ShouldMerge_Merge6()
        {
            int firstIndex = this.mergedPoints.Count - 6;
            ZigzagPoint point_1 = this.mergedPoints[firstIndex];
            ZigzagPoint point_2 = this.mergedPoints[firstIndex + 1];
            ZigzagPoint point_3 = this.mergedPoints[firstIndex + 2];
            ZigzagPoint point_4 = this.mergedPoints[firstIndex + 3];
            ZigzagPoint point_5 = this.mergedPoints[firstIndex + 4];
            ZigzagPoint point_6 = this.mergedPoints[firstIndex + 5];

            if (point_1.Price > point_3.Price && point_3.Price > point_5.Price
                && point_2.Price > point_4.Price && point_4.Price > point_6.Price)
                return true;
            if (point_1.Price < point_3.Price && point_3.Price < point_5.Price
                && point_2.Price < point_4.Price && point_4.Price < point_6.Price)
                return true;
            return false;
        }

        public List<ZigzagPoint> GetMergedPoints()
        {
            return mergedPoints;
        }
    }
}
