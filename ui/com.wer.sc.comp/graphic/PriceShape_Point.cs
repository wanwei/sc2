using System.Drawing;

namespace com.wer.sc.comp.graphic
{
    public class PriceShape_Point : PriceShape
    {
        public float X;

        public float Y;

        public float Width;

        public Color Color;

        public PriceShapeType GetShapeType()
        {
            return PriceShapeType.Point;
        }
    }
}