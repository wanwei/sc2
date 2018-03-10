using com.wer.sc.data;
using com.wer.sc.graphic.shape;
using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace com.wer.sc.strategy
{
    /// <summary>
    /// 策略绘图
    /// </summary>
    public class StrategyGraphic : IStrategyGraphic
    {
        private IDataKey dataKey;

        private IStrategyGraphicTitle title;

        private IPriceShapeContainer priceShapes;

        public StrategyGraphic()
        {

        }

        public StrategyGraphic(IDataKey dataKey)
        {
            this.dataKey = dataKey;
            this.title = new StrategyGraphicTitle();
            this.priceShapes = new PriceShapeContainer();
        }

        public IStrategyGraphicTitle Title
        {
            get
            {
                return title;
            }
        }

        public IDataKey DataKey
        {
            get
            {
                return dataKey;
            }
        }

        public IPriceShapeContainer Shapes
        {
            get
            {
                return priceShapes;
            }
        }

        public void Load(XmlElement xmlElem)
        {
            string type = xmlElem.GetAttribute("type");
            if ("kline".Equals(type))
            {
                dataKey = new KLineKey();
                ((KLineKey)dataKey).Load(xmlElem);
            }
            else if ("timeline".Equals(type))
            {
                dataKey = new TimeLineKey();
                ((TimeLineKey)dataKey).Load(xmlElem);
            }

            XmlNode node = xmlElem.ChildNodes[0];
            if (node is XmlElement)
            {
                XmlElement elem = (XmlElement)node;
                this.title = new StrategyGraphicTitle();
                this.title.Load(elem);
            }
            this.priceShapes = new PriceShapeContainer();
            this.priceShapes.Load(xmlElem);
        }

        public void Save(XmlElement xmlElem)
        {
            if (dataKey is KLineKey)
            {
                xmlElem.SetAttribute("type", "kline");
                ((KLineKey)dataKey).Save(xmlElem);
            }
            else if (dataKey is TimeLineKey)
            {
                xmlElem.SetAttribute("type", "timeline");
                ((TimeLineKey)dataKey).Save(xmlElem);
            }

            XmlElement elemTitle = xmlElem.OwnerDocument.CreateElement("title");
            xmlElem.AppendChild(elemTitle);
            this.Title.Save(elemTitle);
            this.priceShapes.Save(xmlElem);
        }
    }
}