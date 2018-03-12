using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace com.wer.sc.strategy
{
    public class StrategyGraphicTitle : IStrategyGraphicTitle
    {
        private int x;

        public int X
        {
            get { return x; }
            set { this.x = value; }
        }

        private string text;

        public string Text
        {
            get { return text; }
            set { this.text = value; }
        }

        private Color color;

        public Color Color
        {
            get { return this.color; }
            set { this.color = value; }
        }

        private Font font;

        public Font Font
        {
            get { return this.font; }
            set { this.font = value; }
        }

        public void Save(XmlElement xmlElem)
        {
            if (x != 0)
                xmlElem.SetAttribute("x", x.ToString());
            if (text != null)
                xmlElem.SetAttribute("text", text.ToString());
            if (color != default(Color))
                xmlElem.SetAttribute("color", ColorTranslator.ToHtml(Color));
            if (font != null)
            {
                xmlElem.SetAttribute("familyname", font.FontFamily.Name);
                xmlElem.SetAttribute("emsize", font.Size.ToString());
                xmlElem.SetAttribute("style", font.Style.ToString());
            }
        }

        public void Load(XmlElement xmlElem)
        {
            if (xmlElem.HasAttribute("x"))
                this.x = int.Parse(xmlElem.GetAttribute("x"));
            if (xmlElem.HasAttribute("text"))
                this.text = xmlElem.GetAttribute("text");
            if (xmlElem.HasAttribute("color"))
                this.Color = ColorTranslator.FromHtml(xmlElem.GetAttribute("color"));
            if (xmlElem.HasAttribute("familyname"))
            {
                string familyName = xmlElem.GetAttribute("familyname");
                float emsize = float.Parse(xmlElem.GetAttribute("emsize"));
                FontStyle fontStyle = (FontStyle)EnumUtils.Parse(typeof(FontStyle), xmlElem.GetAttribute("style"));
                this.font = new Font(familyName, emsize, fontStyle);
            }
        }
    }
}
