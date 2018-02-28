using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using com.wer.sc.data;

namespace com.wer.sc.graphic.shape
{
    public class PriceShapeContainerManager : IPriceShapeContainerManager
    {
        private List<IPriceShapeContainer> containers = new List<IPriceShapeContainer>();

        private List<IDataKey> graphicKeies = new List<IDataKey>();

        private List<KLineKey> klineKeies = new List<KLineKey>();

        private List<TimeLineKey> timeLineKeies = new List<TimeLineKey>();

        private Dictionary<IDataKey, IPriceShapeContainer> dic_GraphicKey_Container = new Dictionary<IDataKey, IPriceShapeContainer>();

        public void AddContainer(IPriceShapeContainer priceShapeContainer)
        {
            this.containers.Add(priceShapeContainer);
            IDataKey dataKey = priceShapeContainer.GraphicKey;
            if (dataKey == null)
                return;
            dic_GraphicKey_Container.Add(dataKey, priceShapeContainer);
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

        public IPriceShapeContainer GetShapeContainer(IDataKey graphicKey)
        {
            if (graphicKey == null)
                return null;
            if (dic_GraphicKey_Container.ContainsKey(graphicKey))
                return dic_GraphicKey_Container[graphicKey];
            return null;
        }

        public IList<IPriceShapeContainer> GetAllPriceShapeContainer()
        {
            return containers;
        }

        public void RemoveContainer(IPriceShapeContainer priceShapeContainer)
        {
            this.containers.Remove(priceShapeContainer);
            IDataKey dataKey = priceShapeContainer.GraphicKey;
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
            for (int i = 0; i < containers.Count; i++)
            {
                XmlElement elem = xmlElem.OwnerDocument.CreateElement("shapecontainer");
                xmlElem.AppendChild(elem);
                containers[i].Save(elem);
            }
        }

        public void Load(XmlElement xmlElem)
        {
            XmlNodeList nodes = xmlElem.GetElementsByTagName("shapecontainer");
            for (int i = 0; i < nodes.Count; i++)
            {
                XmlNode node = nodes[i];
                if (node is XmlElement)
                {
                    XmlElement elem = (XmlElement)node;
                    string type = elem.GetAttribute("type");
                    if ("kline".Equals(type))
                    {
                        PriceShapeContainer_KLine container = new PriceShapeContainer_KLine();
                        container.Load(elem);
                        this.AddContainer(container);
                    }
                    else if ("timeline".Equals(type))
                    {
                        PriceShapeContainer_TimeLine container = new PriceShapeContainer_TimeLine();
                        container.Load(elem);
                        this.AddContainer(container);
                    }
                }
            }
        }
    }
}