using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.comp.graphic.utils
{
    public class PriceRectangle
    {
        public float PriceLeft;

        public float PriceRight;

        public float PriceTop;

        public float PriceBottom;

        public PriceRectangle()
        {

        }

        public PriceRectangle(float priceLeft, float priceRight, float priceTop, float priceBottom)
        {
            this.PriceLeft = priceLeft;
            this.PriceRight = priceRight;
            this.PriceTop = priceTop;
            this.PriceBottom = priceBottom;
        }

        public float PriceHeight
        {
            get { return PriceTop - PriceBottom; }
        }
        public float PriceWidth
        {
            get { return PriceRight - PriceLeft; }
        }
    }
}
