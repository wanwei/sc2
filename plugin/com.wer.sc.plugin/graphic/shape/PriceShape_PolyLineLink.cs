using com.wer.sc.utils;
using System.Collections.Generic;
using System.Drawing;
using System.Xml;

namespace com.wer.sc.graphic.shape
{
    public class PriceShape_PolyLineLink : IPriceShape
    {
        private float startX;

        private IList<float> yList;

        public float Width = 1;

        public Color Color;
        public PriceShape_PolyLineLink()
        {

        }

        public PriceShape_PolyLineLink(float startX, IList<float> yList)
        {
            this.startX = startX;
            this.yList = yList;
        }
        public float StartX
        {
            get { return startX; }
        }

        public IList<float> YList
        {
            get { return yList; }
        }

        public PriceShapeType GetShapeType()
        {
            return PriceShapeType.PolyLineLink;
        }

        public void Save(XmlElement xmlElem)
        {
            xmlElem.SetAttribute("type", PriceShapeType.PolyLineLink.ToString());
            xmlElem.SetAttribute("startx", startX.ToString());
            if (YList != null)
                xmlElem.SetAttribute("ylist", StringUtils.JoinArr(this.YList));
            if (this.Color != default(Color))
                xmlElem.SetAttribute("color", ColorTranslator.ToHtml(Color));
            if (this.Width != 0)
                xmlElem.SetAttribute("width", Width.ToString());
        }

        public void Load(XmlElement xmlElem)
        {
            this.startX = float.Parse(xmlElem.GetAttribute("startx"));
            if (xmlElem.HasAttribute("ylist"))
            {
                string[] arr_YList = xmlElem.GetAttribute("ylist").Split(',');
                List<float> fylist = new List<float>();
                for (int i = 0; i < arr_YList.Length; i++)
                    fylist.Add(float.Parse(arr_YList[i]));
                this.yList = fylist;
            }
            if (xmlElem.HasAttribute("width"))
                this.Width = int.Parse(xmlElem.GetAttribute("width"));
            if (xmlElem.HasAttribute("color"))
                this.Color = ColorTranslator.FromHtml(xmlElem.GetAttribute("color"));
        }
    }
}