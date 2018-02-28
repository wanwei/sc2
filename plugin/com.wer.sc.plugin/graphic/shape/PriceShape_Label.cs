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
    public class PriceShape_Label : IPriceShape
    {
        private string text;

        private PricePoint point;

        private StringFormat stringFormat;

        private Font font;

        private Color color;

        public PricePoint Point
        {
            get
            {
                return point;
            }

            set
            {
                point = value;
            }
        }

        public string Text
        {
            get
            {
                return text;
            }

            set
            {
                text = value;
            }
        }

        public StringFormat StringFormat
        {
            get
            {
                return stringFormat;
            }

            set
            {
                stringFormat = value;
            }
        }

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

        public Font Font
        {
            get
            {
                return font;
            }

            set
            {
                font = value;
            }
        }

        public PriceShapeType GetShapeType()
        {
            return PriceShapeType.Label;
        }

        public void Save(XmlElement xmlElem)
        {
            xmlElem.SetAttribute("type", PriceShapeType.Label.ToString());
            if (this.point != null)
                this.point.Save(xmlElem);
            if (this.text != null)
                xmlElem.SetAttribute("text", this.text);
            if (this.Color != default(Color))
                xmlElem.SetAttribute("color", ColorTranslator.ToHtml(Color));
        }

        public void Load(XmlElement xmlElem)
        {
            if (this.point == null)
                this.point = new PricePoint();
            this.point.Load(xmlElem);
            if (xmlElem.HasAttribute("text"))
                this.text = xmlElem.GetAttribute("text");
            if (xmlElem.HasAttribute("color"))
                this.Color = ColorTranslator.FromHtml(xmlElem.GetAttribute("color"));
        }
    }
}