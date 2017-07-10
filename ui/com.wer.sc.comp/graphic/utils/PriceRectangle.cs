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

        public float PriceTop
        {
            get
            {
                return priceTop;
            }
        }

        public float PriceWidth
        {
            get { return PriceRight - PriceLeft; }
        }
    }
}