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
    public class PriceShape_Rect : IPriceShape
    {
        public float PriceBottom;

        public float PriceTop;

        public float PriceLeft;

        public float PriceRight;

        public Color Color;

        public float Width;

        public bool FillRect;

        public PriceShapeType GetShapeType()
        {
            return PriceShapeType.Rect;
        }

        public void Save(XmlElement xmlElem)
        {
            xmlElem.SetAttribute("type", PriceShapeType.Rect.ToString());
            xmlElem.SetAttribute("left", PriceLeft.ToString());
            xmlElem.SetAttribute("top", PriceTop.ToString());
            xmlElem.SetAttribute("right", PriceRight.ToString());
            xmlElem.SetAttribute("bottom", PriceBottom.ToString());
            if (this.Width != 0)
                xmlElem.SetAttribute("width", Width.ToString());
            if (this.Color != default(Color))
                xmlElem.SetAttribute("color", ColorTranslator.ToHtml(Color));
            xmlElem.SetAttribute("fillrect", FillRect.ToString());
        }

        public void Load(XmlElement xmlElem)
        {
            this.PriceLeft = float.Parse(xmlElem.GetAttribute("left"));
            this.PriceTop = float.Parse(xmlElem.GetAttribute("top"));
            this.PriceRight = float.Parse(xmlElem.GetAttribute("right"));
            this.PriceBottom = float.Parse(xmlElem.GetAttribute("bottom"));
            if (xmlElem.HasAttribute("width"))
                this.Width = int.Parse(xmlElem.GetAttribute("width"));
            if (xmlElem.HasAttribute("color"))
                this.Color = ColorTranslator.FromHtml(xmlElem.GetAttribute("color"));            
            this.FillRect = Boolean.Parse(xmlElem.GetAttribute("fillrect"));
        }
    }
}