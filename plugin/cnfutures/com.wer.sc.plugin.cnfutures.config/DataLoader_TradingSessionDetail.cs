using com.wer.sc.data.reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using com.wer.sc.data;
using com.wer.sc.plugin.historydata;

namespace com.wer.sc.plugin.cnfutures.config
{
    /// <summary>
    /// 交易时间的原始数据加载
    /// 该装载器可以得到
    /// </summary>
    public class DataLoader_TradingSessionDetail : ITradingTimeReader
    {
        private TradingSessionDetail_Item holidayItem;

        private HashSet<int> set_Holiday = new HashSet<int>();

        public HashSet<int> Set_Holiday
        {
            get { return set_Holiday; }
        }

        public TradingSessionDetail_Item defaultOpenTime;

        public List<TradingSession_Market> Markets = new List<TradingSession_Market>();

        public Dictionary<String, TradingSessionDetail_Item> dicOpenPeriod = new Dictionary<string, TradingSessionDetail_Item>();

        private DataLoader_Variety dataLoader_Variety;

        public DataLoader_TradingSessionDetail(string pluginPath, DataLoader_Variety dataLoader_Variety)
        {
            PathUtils pathUtils = new PathUtils(pluginPath);
            XmlDocument doc = new XmlDocument();
            doc.Load(pathUtils.TradingSessionDetailPath);
            this.dataLoader_Variety = dataLoader_Variety;
            Load(doc);
        }

        public List<double[]> GetTradingTime(String code, int date)
        {
            return GetTradingSessionDetail(GetCodeMarket(code), GetVariety(code), date);
        }

        private String GetCodeMarket(String code)
        {
            string variety = GetVariety(code);
            VarietyInfo v = dataLoader_Variety.GetVariety(variety);
            if (v == null)
                return null;
            return v.Exchange;
        }

        private String GetVariety(String code)
        {
            return new CodeIdParser(code).VarietyId;
        }

        public List<double[]> GetTradingSessionDetail(String market, String variety, int date)
        {
            if (set_Holiday.Contains(date))
            {
                return holidayItem.OpenTime;
            }

            for (int i = 0; i < Markets.Count; i++)
            {
                TradingSession_Market otm = Markets[i];
                List<double[]> openTime = otm.GetOpenTime(market, variety, date);
                if (openTime != null)
                    return openTime;
            }
            return defaultOpenTime.OpenTime;
        }

        public TradingSessionDetail_Item GetOpenPeriod(String id)
        {
            return dicOpenPeriod[id];
        }

        public void Load(XmlDocument doc)
        {
            ReadOpenTime(doc);
            ReadHoliday(doc);
            ReadMarket(doc);
        }

        private void ReadHoliday(XmlDocument doc)
        {
            XmlElement elemHoliday = (XmlElement)doc.GetElementsByTagName("HOLIDAY")[0];
            string openTimeId = elemHoliday.GetAttribute("OPENID");
            XmlNodeList nodes = elemHoliday.ChildNodes;
            for (int i = 0; i < nodes.Count; i++)
            {
                XmlNode node = nodes[i];
                this.set_Holiday.Add(int.Parse(((XmlElement)node).GetAttribute("TIME")));
            }
            this.holidayItem = dicOpenPeriod[openTimeId];
        }

        private void ReadOpenTime(XmlDocument doc)
        {
            XmlElement elem = (XmlElement)doc.DocumentElement.ChildNodes[0];
            for (int i = 0; i < elem.ChildNodes.Count; i++)
            {
                XmlNode node = elem.ChildNodes[i];
                if (node is XmlElement)
                {
                    XmlElement subNode = (XmlElement)node;
                    TradingSessionDetail_Item period = ReadTimePeriod(subNode);
                    dicOpenPeriod.Add(period.ID, period);
                }
            }
            String openid = elem.GetAttribute("DEFAULT");
            this.defaultOpenTime = dicOpenPeriod[openid];
        }

        private void ReadMarket(XmlDocument doc)
        {
            XmlElement elemMarket = (XmlElement)doc.GetElementsByTagName("MARKETS")[0];
            for (int i = 0; i < elemMarket.ChildNodes.Count; i++)
            {
                XmlNode node = elemMarket.ChildNodes[i];
                if (node is XmlElement)
                {
                    XmlElement subNode = (XmlElement)node;
                    TradingSession_Market market = new TradingSession_Market();
                    market.dataLoader = this;
                    market.LoadConfig(subNode);
                    Markets.Add(market);
                }
            }
        }

        private TradingSessionDetail_Item ReadTimePeriod(XmlElement elem)
        {
            TradingSessionDetail_Item period = new TradingSessionDetail_Item();
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

    public class TradingSession_Market
    {
        public String ID;

        public int StartTime;

        public int EndTime;

        public TradingSessionDetail_Item Period;

        public List<TradingSession_Variety> varieties = new List<TradingSession_Variety>();

        public DataLoader_TradingSessionDetail dataLoader;

        public TradingSessionDetail_Item GetPeriod()
        {
            if (Period != null)
                return Period;
            return dataLoader.defaultOpenTime;
        }

        public List<double[]> GetOpenTime(String market, String variety, int date)
        {
            if (!market.Equals(ID))
                return null;
            if (date < StartTime || date > EndTime)
                return null;
            for (int i = 0; i < varieties.Count; i++)
            {
                TradingSession_Variety v = varieties[i];
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
                Period = dataLoader.defaultOpenTime;
            else
                Period = dataLoader.GetOpenPeriod(openid);

            for (int i = 0; i < elem.ChildNodes.Count; i++)
            {
                XmlNode node = elem.ChildNodes[i];
                if (node is XmlElement)
                {
                    XmlElement ee = (XmlElement)node;
                    TradingSession_Variety variety = new TradingSession_Variety();
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

    public class TradingSession_Variety
    {
        public HashSet<String> set = new HashSet<string>();

        public int StartTime;

        public int EndTime;

        public TradingSessionDetail_Item Period;

        public TradingSession_Market BelongMarket;

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
                Period = BelongMarket.dataLoader.GetOpenPeriod(openid);
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

    public class TradingSessionDetail_Item
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
