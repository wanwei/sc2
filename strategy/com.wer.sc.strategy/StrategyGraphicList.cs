using com.wer.sc.data;
using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace com.wer.sc.strategy
{
    public class StrategyGraphicList : IStrategyGraphicContainer
    {
        private List<IStrategyGraphic> graphics = new List<IStrategyGraphic>();

        private List<IDataKey> graphicKeies = new List<IDataKey>();

        private List<KLineKey> klineKeies = new List<KLineKey>();

        private List<TimeLineKey> timeLineKeies = new List<TimeLineKey>();

        private Dictionary<IDataKey, IStrategyGraphic> dic_GraphicKey_Container = new Dictionary<IDataKey, IStrategyGraphic>();

        public void AddGraphic(IStrategyGraphic graphic)
        {
            this.graphics.Add(graphic);
            IDataKey dataKey = graphic.DataKey;
            if (dataKey == null)
                return;
            dic_GraphicKey_Container.Add(dataKey, graphic);
            graphicKeies.Add(dataKey);
            if (dataKey is KLineKey)
            {
                klineKeies.Add((KLineKey)dataKey);
            }
            else if (dataKey is TimeLineKey)
            {
                timeLineKeies.Add((TimeLineKey)dataKey);
            }
        }

        public IList<IDataKey> GetGraphicKeies()
        {
            return graphicKeies;
        }

        public IList<KLineKey> GetKLineKeies()
        {
            return klineKeies;
        }

        public IList<TimeLineKey> GetTimeLineKeies()
        {
            return timeLineKeies;
        }

        public IStrategyGraphic GetGraphic(IDataKey graphicKey)
        {
            if (graphicKey == null)
                return null;
            if (dic_GraphicKey_Container.ContainsKey(graphicKey))
                return dic_GraphicKey_Container[graphicKey];
            return null;
        }

        public IList<IStrategyGraphic> GetAllGraphics()
        {
            return graphics;
        }

        public void RemoveGraphic(IStrategyGraphic graphic)
        {
            this.graphics.Remove(graphic);
            IDataKey dataKey = graphic.DataKey;
            if (dataKey == null)
                return;
            if (dataKey is KLineKey)
            {
                klineKeies.Remove((KLineKey)dataKey);
            }
            else if (dataKey is TimeLineKey)
            {
                timeLineKeies.Remove((TimeLineKey)dataKey);
            }
        }

        public void Save(XmlElement xmlElem)
        {
            for (int i = 0; i < graphics.Count; i++)
            {
                XmlElement elem = xmlElem.OwnerDocument.CreateElement("graphic");
                xmlElem.AppendChild(elem);
                graphics[i].Save(elem);
            }
        }

        public void Load(XmlElement xmlElem)
        {
            XmlNodeList nodes = xmlElem.GetElementsByTagName("graphic");
            for (int i = 0; i < nodes.Count; i++)
            {
                XmlNode node = nodes[i];
                if (node is XmlElement)
                {
                    XmlElement elem = (XmlElement)node;
                    StrategyGraphic graphic = new StrategyGraphic();
                    graphic.Load(elem);
                    this.AddGraphic(graphic);
                }
            }
        }
    }
}
