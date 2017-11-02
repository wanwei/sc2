using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace com.wer.sc.data.forward
{
    /// <summary>
    /// 数据模拟前进时应用到的数据类型
    /// </summary>
    public class ForwardReferedPeriods : IXmlExchange
    {
        private bool useTickData = false;

        private bool useTimeLineData = false;

        private List<KLinePeriod> usedKLinePeriods = new List<KLinePeriod>();

        public ForwardReferedPeriods()
        {
        }

        public ForwardReferedPeriods(IList<KLinePeriod> usedKLinePeriods, bool useTick, bool useTimeLine)
        {
            this.UsedKLinePeriods.AddRange(usedKLinePeriods);
            this.useTickData = useTick;
            this.useTimeLineData = useTimeLine;
            this.usedKLinePeriods.Sort();
        }

        public bool UseTickData
        {
            get
            {
                return useTickData;
            }

            set
            {
                useTickData = value;
            }
        }

        public bool UseTimeLineData
        {
            get
            {
                return useTimeLineData;
            }

            set
            {
                useTimeLineData = value;
            }
        }

        public List<KLinePeriod> UsedKLinePeriods
        {
            get
            {
                return usedKLinePeriods;
            }

            set
            {
                usedKLinePeriods = value;
            }
        }

        public KLinePeriod GetMinPeriod()
        {
            if (UsedKLinePeriods == null)
                return null;
            return UsedKLinePeriods.Min();
        }

        public override bool Equals(object obj)
        {
            if (!(obj is ForwardReferedPeriods))
                return false;
            ForwardReferedPeriods referedPeriods = (ForwardReferedPeriods)obj;
            if (this.useTickData != referedPeriods.useTickData || this.useTimeLineData != referedPeriods.useTimeLineData)
                return false;
            if (this.usedKLinePeriods.Count != referedPeriods.usedKLinePeriods.Count)
                return false;
            this.usedKLinePeriods.Sort();
            referedPeriods.usedKLinePeriods.Sort();
            for (int i = 0; i < this.usedKLinePeriods.Count; i++)
            {
                if (!this.usedKLinePeriods[i].Equals(referedPeriods.usedKLinePeriods[i]))
                    return false;
            }
            return true;
        }

        public void Save(XmlElement xmlElem)
        {
            xmlElem.SetAttribute("useTickData", useTickData.ToString());
            xmlElem.SetAttribute("useTimeLineData", useTimeLineData.ToString());
            foreach (KLinePeriod period in usedKLinePeriods)
            {
                XmlElement elemPeriod = xmlElem.OwnerDocument.CreateElement("period");
                xmlElem.AppendChild(elemPeriod);
                period.Save(elemPeriod);
            }
        }

        public void Load(XmlElement xmlElem)
        {
            this.useTickData = bool.Parse(xmlElem.GetAttribute("useTickData"));
            this.useTimeLineData = bool.Parse(xmlElem.GetAttribute("useTimeLineData"));
            XmlNodeList nodes = xmlElem.GetElementsByTagName("period");
            foreach (XmlNode node in nodes)
            {
                XmlElement elem = (XmlElement)node;
                KLinePeriod period = new KLinePeriod();
                period.Load(elem);
                this.usedKLinePeriods.Add(period);
            }
        }
    }
}
