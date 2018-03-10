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

        public void Save(XmlElement xmlElem)
        {
            xmlElem.SetAttribute("x", x.ToString());
            if (text != null)
                xmlElem.SetAttribute("text", text.ToString());
            if (color != default(Color))
                xmlElem.SetAttribute("color", ColorTranslator.ToHtml(Color));
        }

        public void Load(XmlElement xmlElem)
        {
            this.x = int.Parse(xmlElem.GetAttribute("x"));
            this.text = xmlElem.GetAttribute("text");
            if (xmlElem.HasAttribute("color"))
                this.Color = ColorTranslator.FromHtml(xmlElem.GetAttribute("color"));
        }
    }
}
