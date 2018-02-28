using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace com.wer.sc.data
{
    /// <summary>
    /// K线Key，一段K线的唯一关键字
    /// </summary>
    public class KLineKey : IDataKey, IXmlExchange
    {
        private string code;

        private int startDate;

        private int endDate;

        KLinePeriod kLinePeriod;

        public string Code
        {
            get
            {
                return code;
            }

            set
            {
                code = value;
            }
        }

        public int StartDate
        {
            get
            {
                return startDate;
            }

            set
            {
                startDate = value;
            }
        }

        public int EndDate
        {
            get
            {
                return endDate;
            }

            set
            {
                endDate = value;
            }
        }

        public KLinePeriod KLinePeriod
        {
            get
            {
                return kLinePeriod;
            }

            set
            {
                kLinePeriod = value;
            }
        }

        public KLineKey()
        {

        }

        public KLineKey(string code, int start, int end, KLinePeriod kLinePeriod)
        {
            this.code = code;
            this.startDate = start;
            this.endDate = end;
            this.kLinePeriod = kLinePeriod;
        }

        public override int GetHashCode()
        {
            int hashCode = 0;
            if (code != null)
                hashCode += code.GetHashCode();
            hashCode = hashCode * 10 + startDate;
            hashCode = hashCode * 10 + endDate;
            hashCode = hashCode * 10 + kLinePeriod.GetHashCode();
            return hashCode;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (!(obj is KLineKey))
                return false;
            KLineKey key = (KLineKey)obj;
            return Object.Equals(key.code, Code)
                && key.startDate == startDate
                && key.endDate == endDate
                && Object.Equals(key.kLinePeriod, kLinePeriod);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(code).Append(",")
                .Append(startDate).Append(",")
                .Append(endDate).Append(",")
                .Append(KLinePeriod);
            return sb.ToString();
        }

        public void Save(XmlElement xmlElem)
        {
            xmlElem.SetAttribute("code", code);
            xmlElem.SetAttribute("start", startDate.ToString());
            xmlElem.SetAttribute("end", endDate.ToString());
            KLinePeriod.Save(xmlElem);
        }

        public void Load(XmlElement xmlElem)
        {
            this.code = xmlElem.GetAttribute("code");
            this.startDate = int.Parse(xmlElem.GetAttribute("start"));
            this.endDate = int.Parse(xmlElem.GetAttribute("end"));
            this.kLinePeriod = new KLinePeriod();
            this.kLinePeriod.Load(xmlElem);
        }
    }
}