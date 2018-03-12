using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.data;
using com.wer.sc.graphic;
using com.wer.sc.data.datapackage;
using System.Xml;
using com.wer.sc.utils;

namespace com.wer.sc.strategy
{
    public class StrategyDrawer : IStrategyDrawer
    {
        //private IStrategyDrawer_PriceRect drawer_Tick;

        //private IStrategyDrawer_PriceRect drawer_TimeLine;

        private Dictionary<KLinePeriod, IStrategyDrawer_PriceRect> dic_KLinePeriod_Drawer = new Dictionary<KLinePeriod, IStrategyDrawer_PriceRect>();

        private IDataPackage_Code dataPackage;

        private Dictionary<KLinePeriod, IDataKey> dic_KLinePeriod_DataKey = new Dictionary<KLinePeriod, IDataKey>();

        private Dictionary<KLinePeriod, int> dic_KLinePeriod_Start = new Dictionary<KLinePeriod, int>();

        private IStrategyDrawer_PriceRect drawer_Empty = new StrategyDrawer_PriceRect_Empty();

        public StrategyDrawer()
        {

        }

        public StrategyDrawer(IDataPackage_Code dataPackage, StrategyReferedPeriods referPeriods)
        {
            this.dataPackage = dataPackage;
            foreach (KLinePeriod period in referPeriods.UsedKLinePeriods)
            {
                IKLineData_Extend klineData = dataPackage.GetKLineData(period);
                dic_KLinePeriod_Start.Add(period, klineData.BarPos);
                dic_KLinePeriod_DataKey.Add(period, new KLineKey(dataPackage.Code, dataPackage.StartDate, dataPackage.EndDate, period));
            }
        }

        public IStrategyDrawer_PriceRect GetDrawer_KLine(KLinePeriod klinePeriod)
        {
            if (dic_KLinePeriod_Drawer.ContainsKey(klinePeriod))
                return dic_KLinePeriod_Drawer[klinePeriod];
            if (!dic_KLinePeriod_DataKey.ContainsKey(klinePeriod))
                return drawer_Empty;

            StrategyGraphic strategyGraphic = new StrategyGraphic(dic_KLinePeriod_DataKey[klinePeriod]);
            int startBarPos = dic_KLinePeriod_Start[klinePeriod];
            StrategyDrawer_PriceRect drawer = new StrategyDrawer_PriceRect(strategyGraphic, startBarPos);
            dic_KLinePeriod_Drawer.Add(klinePeriod, drawer);
            return dic_KLinePeriod_Drawer[klinePeriod];
        }

        public IStrategyDrawer_PriceRect GetDrawer_Tick(int date)
        {
            return drawer_Empty;
        }

        public IStrategyDrawer_PriceRect GetDrawer_TimeLine(int date)
        {
            return drawer_Empty;
        }

        public void Save(XmlElement xmlElem)
        {
            foreach (KLinePeriod klinePeriod in dic_KLinePeriod_Drawer.Keys)
            {
                IStrategyDrawer_PriceRect drawer = dic_KLinePeriod_Drawer[klinePeriod];
                XmlElement elemDrawer = xmlElem.OwnerDocument.CreateElement("drawer");
                klinePeriod.Save(elemDrawer);
                //elemDrawer.SetAttribute("klineperiod", klinePeriod.ToString());
                xmlElem.AppendChild(elemDrawer);
                drawer.Save(elemDrawer);
            }
        }

        public void Load(XmlElement xmlElem)
        {
            XmlNodeList nodes = xmlElem.ChildNodes;
            foreach (XmlNode node in nodes)
            {
                if (node is XmlElement)
                {
                    XmlElement elemDrawer = (XmlElement)node;
                    StrategyDrawer_PriceRect drawer = new StrategyDrawer_PriceRect();
                    drawer.Load(elemDrawer);
                    KLinePeriod klinePeriod = new KLinePeriod();
                    klinePeriod.Load(elemDrawer);
                    //KLinePeriod klinePeriod = (KLinePeriod)EnumUtils.Parse(typeof(KLinePeriod), elemDrawer.GetAttribute("klineperiod"));
                    this.dic_KLinePeriod_Drawer.Add(klinePeriod, drawer);
                }
            }
        }
    }
}