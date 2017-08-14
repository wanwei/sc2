using System.Collections.Generic;
using System.Drawing;

namespace com.wer.sc.strategy.draw
{
    public class PricePolyLine 
    {
        private List<PricePoint> points = new List<PricePoint>();

        public float Width;

        public Color Color;

        public IList<PricePoint> Points
        {
            get
            {
                return points;
            }
        }

        public void AddPoint(PricePoint point)
        {
            this.points.Add(point);
        }

        public void RemovePoint(PricePoint point)
        {
            this.points.Remove(point);
        }        
    }
}