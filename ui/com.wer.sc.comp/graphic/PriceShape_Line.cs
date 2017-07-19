using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.comp.graphic
{
    public class PriceShape_Line : PriceShape
    {
        public PriceShape_Point StartPoint;

        public PriceShape_Point EndPoint;

        public float Width;

        public Color Color;

        public PriceShapeType GetShapeType()
        {
            return PriceShapeType.Line;
        }
    }
}
