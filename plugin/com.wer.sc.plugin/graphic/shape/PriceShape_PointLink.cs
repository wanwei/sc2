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
    public class PriceShape_PointLink : IPriceShape
    {
        private float startX;

        private IList<float> yList;

        public float Width = 0;

        public Color Color;

        public PriceShape_PointLink()
        {

        }

        public PriceShape_PointLink(float startX, IList<float> yList)
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
            return PriceShapeType.PointLink;
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

        public void Save(XmlElement xmlElem)
        {
            xmlElem.SetAttribute("type", PriceShapeType.PointLink.ToString());
            xmlElem.SetAttribute("startx", startX.ToString());
            if (YList != null)
                xmlElem.SetAttribute("ylist", StringUtils.JoinArr(this.YList));
            if (this.Color != default(Color))
                xmlElem.SetAttribute("color", ColorTranslator.ToHtml(Color));
            if (this.Width != 0)
                xmlElem.SetAttribute("width", Width.ToString());

        }
    }
}
