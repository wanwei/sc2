using System.Text;

namespace com.wer.sc.comp.graphic
{
    public class PriceRectangle
    {
        private float priceBottom;
        private float priceTop;
        private float priceLeft;
        private float priceRight;

        public PriceRectangle(float priceLeft, float priceRight, float priceTop, float priceBottom)
        {
            this.priceLeft = priceLeft;
            this.priceRight = priceRight;
            this.priceTop = priceTop;
            this.priceBottom = priceBottom;
        }

        public float PriceBottom
        {
            get { return priceBottom; }
        }

        public float PriceTop
        {
            get
            {
                return priceTop;
            }
        }

        public float PriceHeight
        {
            get { return priceTop - priceBottom; }
        }

        public float PriceLeft
        {
            get
            {
                return priceLeft;
            }
        }

        public float PriceRight
        {
            get
            {
                return priceRight;
            }
        }

        public float PriceWidth
        {
            get { return PriceRight - PriceLeft; }
        }

        public int StartIndex
        {
            get
            {
                int start = (int)this.PriceLeft;
                start = (start == this.PriceLeft) ? start : start + 1;
                return start;
            }
        }

        public int EndIndex
        {
            get
            {
                int end = (int)this.PriceRight;
                end = (end == this.PriceRight) ? end : end - 1;
                return end;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(PriceLeft).Append(",");
            sb.Append(PriceTop).Append(",");
            sb.Append(PriceWidth).Append(",");
            sb.Append(PriceHeight);
            return sb.ToString();
        }

        public override bool Equals(object obj)
        {
            if (!(obj is PriceRectangle))
                return false;
            PriceRectangle rect = (PriceRectangle)obj;
            return this.priceLeft == rect.priceLeft && this.priceRight == rect.priceRight
                && this.priceTop == rect.priceTop && this.priceBottom == rect.priceBottom;
        }

        public override int GetHashCode()
        {
            return (int)(priceLeft * 1000 + priceRight * 100 + priceTop * 10 + priceBottom);
        }
    }
}
