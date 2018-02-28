using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace com.wer.sc.graphic.shape
{
    public class PriceShape_Line : IPriceShape
    {
        public PricePoint StartPoint;

        public PricePoint EndPoint;

        public float Width;

        public Color Color;

        public PriceShape_Line()
        {

        }

        public PriceShape_Line(float x1, float y1, float x2, float y2)
        {

        }

        public PriceShapeType GetShapeType()
        {
            return PriceShapeType.Line;
        }

        public void Save(XmlElement xmlElem)
        {
            xmlElem.SetAttribute("type", GetShapeType().ToString());
            if (this.StartPoint != null)
            {
                XmlElement elemStartPoint = xmlElem.OwnerDocument.CreateElement("startpoint");
                this.StartPoint.Save(elemStartPoint);
                xmlElem.AppendChild(elemStartPoint);
                this.StartPoint.Save(xmlElem);
            }
            if (this.EndPoint != null)
            {
                XmlElement elemEndPoint = xmlElem.OwnerDocument.CreateElement("endpoint");
                this.EndPoint.Save(elemEndPoint);
                xmlElem.AppendChild(elemEndPoint);
                this.EndPoint.Save(xmlElem);
            }
            if (this.Color != default(Color))
                xmlElem.SetAttribute("color", ColorTranslator.ToHtml(Color));
            if (this.Width != 0)
                xmlElem.SetAttribute("width", Width.ToString());
        }

        public void Load(XmlElement xmlElem)
        {
            XmlNodeList nodes = xmlElem.GetElementsByTagName("startpoint");
            if (nodes.Count != 0)
            {
                if (this.StartPoint == null)
                    this.StartPoint = new PricePoint();
                this.StartPoint.Load((XmlElement)nodes[0]);
            }

            nodes = xmlElem.GetElementsByTagName("endpoint");
            if (nodes.Count != 0)
            {
                if (this.EndPoint == null)
                    this.EndPoint = new PricePoint();
                this.EndPoint.Load((XmlElement)nodes[0]);
            }

            if (xmlElem.HasAttribute("width"))
                this.Width = int.Parse(xmlElem.GetAttribute("width"));
            if (xmlElem.HasAttribute("color"))
                this.Color = ColorTranslator.FromHtml(xmlElem.GetAttribute("color"));
        }
    }
}
