using System.Collections.Generic;
using System.Drawing;

namespace com.wer.sc.comp.graphic
{
    public class PriceShape_PolyLine : PriceShape
    {
        private List<PriceShape_Point> points = new List<PriceShape_Point>();

        public float Width;

        public Color Color;

        public IList<PriceShape_Point> Points
        {
            get
            {
                return points;
            }
        }

        public void AddPricepoint(PriceShape_Point point)
        {
            this.points.Add(point);
        }

        public void Removepoint(PriceShape_Point point)
        {
            this.points.Remove(point);
        }

        public PriceShapeType GetShapeType()
        {
            return PriceShapeType.PolyLine;
        }
    }
}