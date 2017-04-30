using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace com.wer.sc.data.datacenter
{
    /// <summary>
    /// 数据中心保存的数据类型
    /// </summary>
    public class StoreDataTypes
    {
        private const string ATTRIBUTE_TRADINGDAY = "TradingDay";
        private const string ATTRIBUTE_TRADINGSESSION = "TradingSession";
        private const string ATTRIBUTE_TICK = "Tick";

        private bool isStoredTradingDay = true;

        private bool isStoreTradingSession = false;

        private bool isStoreTick = true;

        private List<KLinePeriod> storeKLinePeriods = new List<KLinePeriod>();

        public bool IsStoredTradingDay
        {
            get
            {
                return isStoredTradingDay;
            }

            set
            {
                isStoredTradingDay = value;
            }
        }

        public bool IsStoreTradingSession
        {
            get
            {
                return isStoreTradingSession;
            }

            set
            {
                isStoreTradingSession = value;
            }
        }

        public bool IsStoreTick
        {
            get
            {
                return isStoreTick;
            }

            set
            {
                isStoreTick = value;
            }
        }

        public List<KLinePeriod> StoreKLinePeriods
        {
            get
            {
                return storeKLinePeriods;
            }
        }


        public void SaveXml(XmlElement elem)
        {
            elem.SetAttribute(ATTRIBUTE_TRADINGDAY, isStoredTradingDay.ToString());
            elem.SetAttribute(ATTRIBUTE_TRADINGSESSION, isStoreTradingSession.ToString());
            elem.SetAttribute(ATTRIBUTE_TICK, isStoreTick.ToString());
            for (int i = 0; i < storeKLinePeriods.Count; i++)
            {
                XmlElement elemKLinePeriod = elem.OwnerDocument.CreateElement("KLinePeriod");
                elem.AppendChild(elemKLinePeriod);
                KLinePeriod period = storeKLinePeriods[i];
                elemKLinePeriod.SetAttribute("type", GetKLineTimeTypeString(period.PeriodType));
                elemKLinePeriod.SetAttribute("period", period.Period.ToString());
            }
        }

        private KLineTimeType ParseKLineTimeType(string klineTimeTypeStr)
        {
            if (klineTimeTypeStr.ToLower().Equals("second"))
                return KLineTimeType.SECOND;
            if (klineTimeTypeStr.ToLower().Equals("minute"))
                return KLineTimeType.MINUTE;
            if (klineTimeTypeStr.ToLower().Equals("hour"))
                return KLineTimeType.HOUR;
            if (klineTimeTypeStr.ToLower().Equals("day"))
                return KLineTimeType.DAY;
            if (klineTimeTypeStr.ToLower().Equals("week"))
                return KLineTimeType.WEEK;
            return default(KLineTimeType);
        }

        private string GetKLineTimeTypeString(KLineTimeType klineTimeType)
        {
            switch (klineTimeType)
            {
                case KLineTimeType.SECOND:
                    return "second";
                case KLineTimeType.MINUTE:
                    return "minute";
                case KLineTimeType.HOUR:
                    return "hour";
                case KLineTimeType.DAY:
                    return "day";
                case KLineTimeType.WEEK:
                    return "week";
            }
            return "";
        }

        public void LoadXml(XmlElement elem)
        {        
            this.isStoredTradingDay = Boolean.Parse(elem.GetAttribute(ATTRIBUTE_TRADINGDAY));
            this.isStoreTradingSession = Boolean.Parse(elem.GetAttribute(ATTRIBUTE_TRADINGSESSION));
            this.isStoreTick = Boolean.Parse(elem.GetAttribute(ATTRIBUTE_TICK));
            foreach (XmlNode node in elem.ChildNodes)
            {
                if (node is XmlElement)
                {
                    XmlElement elemKLinePeriod = (XmlElement)node;
                    KLineTimeType timeType = ParseKLineTimeType(elemKLinePeriod.GetAttribute("type"));
                    KLinePeriod period = new KLinePeriod(timeType, int.Parse(elemKLinePeriod.GetAttribute("period")));
                    this.storeKLinePeriods.Add(period);
                }
            }
        }
    }
}
