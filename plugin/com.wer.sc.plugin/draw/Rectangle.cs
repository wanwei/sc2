namespace com.wer.sc.draw
{

    public class PriceRectangle
    {
        private float priceLeft;

        private float priceTop;

        private float priceRight;

        private float priceBottom;

        public PriceRectangle(float priceLeft, float priceRight, float priceTop, float priceBottom)
        {
            this.priceLeft = priceLeft;
            this.priceTop = priceTop;
            this.priceRight = priceRight;            
            this.priceBottom = priceBottom;
        }

        public float PriceLeft
        {
            get
            {
                return priceLeft;
            }
        }

        public float PriceTop
        {
            get
            {
                return priceTop;
            }
        }

        public float PriceRight
        {
            get
            {
                return priceRight;
            }
        }

        public float PriceBottom
        {
            get { return priceBottom; }
        }

        public float PriceHeight
        {
            get { return priceTop - priceBottom; }
        }

        public float PriceWidth
        {
            get { return PriceRight - PriceLeft; }
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
