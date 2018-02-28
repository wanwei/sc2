using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using com.wer.sc.data;

namespace com.wer.sc.graphic.shape
{
    public class PriceShapeContainer : IPriceShapeContainer
    {
        private List<IPriceShape> priceShapes = new List<IPriceShape>();  

        public void AddPriceShape(IPriceShape priceShape)
        {
            priceShapes.Add(priceShape);
        }

        public void RemovePriceShape(IPriceShape priceShape)
        {
            priceShapes.Remove(priceShape);
        }

        public IList<IPriceShape> GetAllPriceShapes()
        {
            return priceShapes;
        }

        public void Paint(PriceRectangle priceRectangle)
        {

        }

        public virtual void Save(XmlElement xmlElem)
        {
            for (int i = 0; i < priceShapes.Count; i++)
            {
                XmlElement elemShape = xmlElem.OwnerDocument.CreateElement("shape");
                priceShapes[i].Save(elemShape);
                xmlElem.AppendChild(elemShape);
            }
        }

        public virtual void Load(XmlElement xmlElem)
        {
            XmlNodeList nodes = xmlElem.GetElementsByTagName("shape");
            for (int i = 0; i < nodes.Count; i++)
            {
                XmlNode node = nodes[i];
                if (node is XmlElement)
                {
                    XmlElement elem = (XmlElement)node;
                    IPriceShape priceShape = PriceShapeFactory.CreatePriceShape(elem);
                    priceShape.Load(elem);
                    this.priceShapes.Add(priceShape);
                }
            }
        }

        public virtual IDataKey GraphicKey { get; }
    }
}