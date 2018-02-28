using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace com.wer.sc.data
{
    public class TimeLineKey : IDataKey, IXmlExchange
    {
        private string code;

        private int date;

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

        public int Date
        {
            get
            {
                return date;
            }

            set
            {
                date = value;
            }
        }

        public TimeLineKey()
        {

        }

        public TimeLineKey(string code,int date)
        {
            this.code = code;
            this.date = date;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (!(obj is TimeLineKey))
                return false;
            TimeLineKey key = (TimeLineKey)obj;
            return Object.Equals(key.code, code)
                && Object.Equals(key.date, date);
        }

        public override int GetHashCode()
        {
            int hashCode = date;
            if (code != null)
                hashCode = hashCode * 10 + date;
            return hashCode;
        }

        public void Load(XmlElement xmlElem)
        {
            this.code = xmlElem.GetAttribute("code");
            this.date = int.Parse(xmlElem.GetAttribute("date"));
        }

        public void Save(XmlElement xmlElem)
        {
            xmlElem.SetAttribute("code", code);
            xmlElem.SetAttribute("date", date.ToString());
        }

        public override string ToString()
        {
            return code + "," + date;
        }
    }
}