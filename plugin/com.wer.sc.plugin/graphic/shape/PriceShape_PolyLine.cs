using com.wer.sc.utils;
using System.Collections.Generic;
using System.Drawing;
using System.Xml;

namespace com.wer.sc.graphic.shape
{
    public class PriceShape_PolyLine : IPriceShape
    {
        private List<PricePoint> points = new List<PricePoint>();

        public float Width = 1;

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

        public void Removepoint(PricePoint point)
        {
            this.points.Remove(point);
        }

        public PriceShapeType GetShapeType()
        {
            return PriceShapeType.PolyLine;
        }

        public void Save(XmlElement xmlElem)
        {
            xmlElem.SetAttribute("type", PriceShapeType.PolyLine.ToString());
            for (int i = 0; i < points.Count; i++)
            {
                XmlElement elemPoint = xmlElem.OwnerDocument.CreateElement("point");
                xmlElem.AppendChild(elemPoint);
                points[i].Save(elemPoint);
            }
            if (this.Color != default(Color))
                xmlElem.SetAttribute("color", ColorTranslator.ToHtml(Color));
            if (this.Width != 0)
                xmlElem.SetAttribute("width", Width.ToString());
        }

        public void Load(XmlElement xmlElem)
        {
            XmlNodeList nodes = xmlElem.GetElementsByTagName("point");
            for (int i = 0; i < nodes.Count; i++)
            {
                XmlNode node = nodes[i];
                if (node is XmlElement)
                {
                    XmlElement nodeElem = (XmlElement)node;
                    PricePoint point = new PricePoint();
                    point.Load(nodeElem);
                    this.points.Add(point);
                }
            }
            if (xmlElem.HasAttribute("width"))
                this.Width = int.Parse(xmlElem.GetAttribute("width"));
            if (xmlElem.HasAttribute("color"))
                this.Color = ColorTranslator.FromHtml(xmlElem.GetAttribute("color"));
        }
    }
}