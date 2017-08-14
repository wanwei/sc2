using System.Drawing;

namespace com.wer.sc.draw
{
    public class PriceShape_Point : PriceShape
    {
        public double X;

        public double Y;

        public float Width;

        private Color color;

        public Color Color
        {
            get
            {
                return color;
            }

            set
            {
                color = value;
            }
        }

        public PriceShapeType GetShapeType()
        {
            return PriceShapeType.Point;
        }
    }
}