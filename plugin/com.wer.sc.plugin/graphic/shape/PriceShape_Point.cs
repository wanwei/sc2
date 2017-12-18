using System.Drawing;

namespace com.wer.sc.graphic.shape
{
    public class PriceShape_Point : PriceShape
    {
        public float X;

        public float Y;

        public float Width;

        public Color Color;

        public PriceShape_Point()
        {

        }

        public PriceShape_Point(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }

        public PriceShape_Point(float x, float y, float width, Color color)
        {
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Color = color;
        }

        public PriceShapeType GetShapeType()
        {
            return PriceShapeType.Point;
        }
    }
}