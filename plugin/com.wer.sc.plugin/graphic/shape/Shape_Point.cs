using System.Drawing;
using System.Xml;

namespace com.wer.sc.graphic.shape
{
    public class Shape_Point : IShape
    {
        public float X;

        public float Y;

        public float Width;

        public Color Color;

        public Shape_Point()
        {

        }

        public Shape_Point(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }

        public Shape_Point(float x, float y, float width, Color color)
        {
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Color = color;
        }

        public ShapeType GetShapeType()
        {
            return ShapeType.Point;
        }

        public void Save(XmlElement xmlElem)
        {

        }

        public void Load(XmlElement xmlElem)
        {

        }

    }
}