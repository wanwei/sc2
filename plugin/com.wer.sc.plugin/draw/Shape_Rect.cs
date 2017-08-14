using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.draw
{
    public class PriceShape_Rect : PriceShape
    {
        public float PriceBottom;

        public float PriceTop;

        public float PriceLeft;

        public float PriceRight;

        private Color color;

        public bool FillRect;

        public Color Color
        {
            get
            {
                return color;
            }

            set
            {
                color = value;
            }
        }

        public PriceShapeType GetShapeType()
        {
            return PriceShapeType.Rect;
        }
    }
}