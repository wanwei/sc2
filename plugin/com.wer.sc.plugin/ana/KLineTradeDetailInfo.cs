using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace com.wer.sc.ana
{
    public class KLineTradeDetailInfo
    {

        public String code;

        public bool isOpen;

        public bool isMoreOrLess;

        public int mount;

        public float price;

        public double time;

        public float earn;


        KLineTradeDetailInfo()
        {
        }

        public KLineTradeDetailInfo(String code, double time, bool isOpen, bool isMoreOrLess, int mount, float price,
                float earn)
        {
            this.code = code;
            this.time = time;
            this.isOpen = isOpen;
            this.isMoreOrLess = isMoreOrLess;
            this.mount = mount;
            this.price = price;
            this.earn = earn;
        }

        public void saveToXml(XmlElement elem)
        {
            elem.SetAttribute("code", code);
            elem.SetAttribute("isOpen", isOpen + "");
            elem.SetAttribute("isMoreOrLess", isMoreOrLess + "");
            elem.SetAttribute("mount", mount.ToString());
            elem.SetAttribute("price", price.ToString());
            elem.SetAttribute("time", time.ToString());
            elem.SetAttribute("earn", Math.Round(earn, 3).ToString());
        }

        public void loadFromXml(XmlElement elem)
        {
            this.code = elem.GetAttribute("code");
            this.isOpen = bool.Parse(elem.GetAttribute("isOpen"));
            this.isMoreOrLess = bool.Parse(elem.GetAttribute("isMoreOrLess"));
            this.mount = int.Parse(elem.GetAttribute("mount"), 0);
            this.price = float.Parse(elem.GetAttribute("price"), 0);
            this.time = double.Parse(elem.GetAttribute("time"));
            this.earn = float.Parse(elem.GetAttribute("earn"), 0);
        }

        public String toString()
        {
            //return XmlUtil.saveXmlExchange(this);
            return "";
        }
    }
}
