using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.comp.graphic
{
    public class PriceShape_Rect : PriceShape
    {
        public float PriceBottom;

        public float PriceTop;

        public float PriceLeft;

        public float PriceRight;

        public Color Color;

        public bool FillRect;

        public PriceShapeType GetShapeType()
        {
            return PriceShapeType.Rect;
        }
    }
}