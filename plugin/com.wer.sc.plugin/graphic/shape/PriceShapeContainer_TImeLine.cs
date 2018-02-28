using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using com.wer.sc.data;

namespace com.wer.sc.graphic.shape
{
    public class PriceShapeContainer_TimeLine : PriceShapeContainer
    {
        private TimeLineKey timeLineKey;

        public PriceShapeContainer_TimeLine()
        {
        }

        public PriceShapeContainer_TimeLine(TimeLineKey timeLineKey)
        {
            this.timeLineKey = timeLineKey;
        }

        public override IDataKey GraphicKey
        {
            get
            {
                return timeLineKey;
            }
        }      

        public override void Save(XmlElement xmlElem)
        {
            xmlElem.SetAttribute("type", "timeline");
            base.Save(xmlElem);
            this.timeLineKey.Save(xmlElem);
        }

        public override void Load(XmlElement xmlElem)
        {
            base.Load(xmlElem);
            this.timeLineKey = new TimeLineKey();
            this.timeLineKey.Load(xmlElem);
        }
    }
}
