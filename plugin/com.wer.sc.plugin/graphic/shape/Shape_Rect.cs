using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace com.wer.sc.graphic.shape
{
    public class Shape_Rect : IShape
    {
        public float PriceBottom;

        public float PriceTop;

        public float PriceLeft;

        public float PriceRight;

        public Color Color;

        public bool FillRect;

        public ShapeType GetShapeType()
        {
            return ShapeType.Rect;
        }

        public void Save(XmlElement xmlElem)
        {

        }

        public void Load(XmlElement xmlElem)
        {

        }

    }
}