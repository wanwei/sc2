using com.wer.sc.utils;
using System;
using System.Drawing;
using System.Xml;

namespace com.wer.sc.graphic.shape
{
    public class PriceShape_Point : PricePoint, IPriceShape
    {
        public float Width = 0;

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

        public override void Save(XmlElement xmlElem)
        {
            xmlElem.SetAttribute("type", PriceShapeType.Point.ToString());
            base.Save(xmlElem);
            if (this.Width != 0)
                xmlElem.SetAttribute("width", Width.ToString());
            if (this.Color != default(Color))
                xmlElem.SetAttribute("color", ColorTranslator.ToHtml(Color));
        }

        public override void Load(XmlElement xmlElem)
        {
            base.Load(xmlElem);
            if (xmlElem.HasAttribute("width"))
                this.Width = int.Parse(xmlElem.GetAttribute("width"));
            if (xmlElem.HasAttribute("color"))
                this.Color = ColorTranslator.FromHtml(xmlElem.GetAttribute("color"));
        }
    }
}