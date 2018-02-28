using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using com.wer.sc.data;

namespace com.wer.sc.graphic.shape
{
    public class PriceShapeContainer_KLine : PriceShapeContainer
    {
        private KLineKey klineKey;

        public PriceShapeContainer_KLine()
        {
        }

        public PriceShapeContainer_KLine(KLineKey klineKey)
        {
            this.klineKey = klineKey;
        }

        public override IDataKey GraphicKey 
        {
            get
            {
                return klineKey;
            }
        }

        public override void Save(XmlElement xmlElem)
        {
            xmlElem.SetAttribute("type", "kline");
            base.Save(xmlElem);
            this.klineKey.Save(xmlElem);
        }

        public override void Load(XmlElement xmlElem)
        {
            base.Load(xmlElem);
            this.klineKey = new KLineKey();
            this.klineKey.Load(xmlElem);
        }
    }
}
