using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace com.wer.sc.graphic.shape
{
    public class PriceShapeFactory
    {
        public static IPriceShape CreatePriceShape(PriceShapeType type)
        {
            switch (type)
            {
                case PriceShapeType.Point:
                    return new PriceShape_Point();
                case PriceShapeType.Line:
                    return new PriceShape_Line();
                case PriceShapeType.Label:
                    return new PriceShape_Label();
                case PriceShapeType.PolyLine:
                    return new PriceShape_PolyLine();
                case PriceShapeType.Rect:
                    return new PriceShape_Rect();
            }
            return null;
        }

        public static IPriceShape CreatePriceShape(XmlElement elem)
        {
            string type = elem.GetAttribute("type");
            PriceShapeType shapeType = (PriceShapeType)EnumUtils.Parse(typeof(PriceShapeType), type);
            return CreatePriceShape(shapeType);
        }
    }
}
