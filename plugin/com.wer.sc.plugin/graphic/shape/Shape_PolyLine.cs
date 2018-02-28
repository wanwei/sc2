using System.Collections.Generic;
using System.Drawing;
using System.Xml;

namespace com.wer.sc.graphic.shape
{
    public class Shape_PolyLine : IShape
    {
        private List<PriceShape_Point> points = new List<PriceShape_Point>();

        public float Width = 1;

        public Color Color;

        public IList<PriceShape_Point> Points
        {
            get
            {
                return points;
            }
        }

        public void AddPoint(PriceShape_Point point)
        {
            this.points.Add(point);
        }

        public void Removepoint(PriceShape_Point point)
        {
            this.points.Remove(point);
        }

        public ShapeType GetShapeType()
        {
            return ShapeType.PolyLine;
        }

        public void Save(XmlElement xmlElem)
        {

        }

        public void Load(XmlElement xmlElem)
        {

        }

    }
}