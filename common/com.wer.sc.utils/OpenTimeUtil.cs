using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace com.wer.sc.data.cnfutures
{
    public class OpenTimeUtil
    {
        public OpenTimePeriod defaultOpenTime;

        public List<OpenTimeMarket> Markets = new List<OpenTimeMarket>();

        public Dictionary<String, OpenTimePeriod> dicOpenPeriod = new Dictionary<string, OpenTimePeriod>();

        public OpenTimeUtil()
        {
        }

        public void LoadFromString(string content)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(content);
            Load(doc);
        }

        public void LoadFromFile(string fileName)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(fileName);
            Load(doc);
        }

        public List<double[]> GetOpenTime(String market, String variety, int date)
        {
            for (int i = 0; i < Markets.Count; i++)
            {
                OpenTimeMarket otm = Markets[i];
                List<double[]> openTime = otm.GetOpenTime(market, variety, date);
                if (openTime != null)
                    return openTime;
            }
            return defaultOpenTime.OpenTime;
        }

        public OpenTimePeriod GetOpenPeriod(String id)
        {
            return dicOpenPeriod[id];
        }

        public void Load(XmlDocument doc)
        {
            XmlElement elem = (XmlElement)doc.DocumentElement.ChildNodes[0];
            for (int i = 0; i < elem.ChildNodes.Count; i++)
            {
                XmlNode node = elem.ChildNodes[i];
                if (node is XmlElement)
                {
                    XmlElement subNode = (XmlElement)node;
                    OpenTimePeriod period = ReadTimePeriod(subNode);
                    dicOpenPeriod.Add(period.ID, period);
                }
            }

            XmlElement elemMarket = (XmlElement)doc.GetElementsByTagName("MARKETS")[0];
            for (int i = 0; i < elemMarket.ChildNodes.Count; i++)
            {
                XmlNode node = elemMarket.ChildNodes[i];
                if (node is XmlElement)
                {
                    XmlElement subNode = (XmlElement)node;
                    OpenTimeMarket market = new OpenTimeMarket();
                    market.Config = this;
                    market.LoadConfig(subNode);
                    Markets.Add(market);
                }
            }

            String openid = elem.GetAttribute("DEFAULT");
            this.defaultOpenTime = dicOpenPeriod[openid];
        }

        private OpenTimePeriod ReadTimePeriod(XmlElement elem)
        {
            OpenTimePeriod period = new OpenTimePeriod();
            period.ID = elem.GetAttribute("ID");
            for (int i = 0; i < elem.ChildNodes.Count; i++)
            {
                XmlElement ee = (XmlElement)elem.ChildNodes[i];
                double fstart = double.Parse(ee.GetAttribute("START"));
                double fend = double.Parse(ee.GetAttribute("END"));
                double[] ff = new double[2];
                ff[0] = fstart;
                ff[1] = fend;
                period.OpenTime.Add(ff);
            }
            return period;
        }


        override
        public String ToString()
        {
            StringBuilder sb = new StringBuilder();
            String[] keies = dicOpenPeriod.Keys.ToArray();
            for (int i = 0; i < dicOpenPeriod.Count; i++)
            {
                sb.Append(dicOpenPeriod[keies[i]].ToString()).Append("\r\n");
            }

            for (int i = 0; i < Markets.Count; i++)
            {
                sb.Append(Markets[i]).Append("\r\n");
            }

            return sb.ToString();
        }
    }

    public class OpenTimeMarket
    {
        public String ID;

        public int StartTime;

        public int EndTime;

        public OpenTimePeriod Period;

        public List<OpenTimeVariety> varieties = new List<OpenTimeVariety>();

        public OpenTimeUtil Config;

        public OpenTimePeriod GetPeriod()
        {
            if (Period != null)
                return Period;
            return Config.defaultOpenTime;
        }

        public List<double[]> GetOpenTime(String market, String variety, int date)
        {
            if (!market.Equals(ID))
                return null;
            if (date < StartTime || date > EndTime)
                return null;
            for (int i = 0; i < varieties.Count; i++)
            {
                OpenTimeVariety v = varieties[i];
                if (v.IsCurrent(variety, date))
                    return v.GetOpenTime();
            }
            return GetPeriod().OpenTime;
        }

        public void LoadConfig(XmlElement elem)
        {
            this.ID = elem.GetAttribute("ID");
            String time = elem.GetAttribute("TIME");
            String[] timeArr = time.Split('-');
            string str = timeArr[0];
            if (str.Equals(""))
                StartTime = int.MinValue;
            else
                StartTime = int.Parse(str);

            str = timeArr[1];
            if (str.Equals(""))
                EndTime = int.MaxValue;
            else
                EndTime = int.Parse(str);

            string openid = elem.GetAttribute("OPENID");
            if (openid.Equals(""))
                Period = Config.defaultOpenTime;
            else
                Period = Config.GetOpenPeriod(openid);

            for (int i = 0; i < elem.ChildNodes.Count; i++)
            {
                XmlNode node = elem.ChildNodes[i];
                if (node is XmlElement)
                {
                    XmlElement ee = (XmlElement)node;
                    OpenTimeVariety variety = new OpenTimeVariety();
                    variety.BelongMarket = this;
                    variety.LoadConfig(ee);
                    varieties.Add(variety);
                }
            }
        }


        override
        public String ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(ID).Append(";");
            sb.Append(StartTime).Append("-").Append(EndTime).Append(";");
            sb.Append(Period == null ? "" : Period.ID).Append("\r\n");
            for (int i = 0; i < varieties.Count; i++)
            {
                sb.Append(varieties[i]).Append("\r\n");
            }
            return sb.ToString();
        }
    }

    public class OpenTimeVariety
    {
        public HashSet<String> set = new HashSet<string>();

        public int StartTime;

        public int EndTime;

        public OpenTimePeriod Period;

        public OpenTimeMarket BelongMarket;

        public List<double[]> GetOpenTime()
        {
            if (Period != null)
                return Period.OpenTime;
            if (BelongMarket.Period != null)
                return BelongMarket.Period.OpenTime;
            return null;
        }

        public bool IsCurrent(String variety, int date)
        {
            if (!set.Contains(variety.ToUpper()))
                return false;
            if (date < StartTime || date > EndTime)
                return false;
            return true;
        }

        public void LoadConfig(XmlElement elem)
        {
            String IDStr = elem.GetAttribute("ID");
            String[] ids = IDStr.Split(',');
            foreach (String id in ids)
                set.Add(id);
            String time = elem.GetAttribute("TIME");
            String[] timeArr = time.Split('-');
            string str = timeArr[0];
            if (str.Equals(""))
                StartTime = int.MinValue;
            else
                StartTime = int.Parse(str);

            str = timeArr[1];
            if (str.Equals(""))
                EndTime = int.MaxValue;
            else
                EndTime = int.Parse(str);

            string openid = elem.GetAttribute("OPENID");
            if (openid.Equals(""))
                Period = BelongMarket.Period;
            else
                Period = BelongMarket.Config.GetOpenPeriod(openid);
        }

        override
        public String ToString()
        {
            StringBuilder sb = new StringBuilder();
            String[] strArr = set.ToArray();
            for (int i = 0; i < strArr.Length; i++)
                sb.Append(strArr[i]).Append(",");
            sb.Append(";");
            sb.Append(StartTime).Append("-").Append(EndTime).Append(";");
            sb.Append(Period.ID);
            return sb.ToString();
        }
    }

    public class OpenTimePeriod
    {
        public String ID;

        public List<double[]> OpenTime = new List<double[]>();

        override
        public String ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(ID).Append(";");
            for (int i = 0; i < OpenTime.Count; i++)
            {
                sb.Append(OpenTime[i][0]).Append("-");
                sb.Append(OpenTime[i][1]).Append(";");
            }
            return sb.ToString();
        }
    }
}
