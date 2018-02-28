using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace com.wer.sc.graphic
{
    public class PricePoint : IXmlExchange
    {
        public float X;

        public float Y;

        public PricePoint()
        {

        }

        public PricePoint(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }

        public virtual void Load(XmlElement xmlElem)
        {
            this.X = float.Parse(xmlElem.GetAttribute("x"));
            this.Y = float.Parse(xmlElem.GetAttribute("y"));
        }

        public virtual void Save(XmlElement xmlElem)
        {
            xmlElem.SetAttribute("x", X.ToString());
            xmlElem.SetAttribute("y", Y.ToString());
        }
    }
}
