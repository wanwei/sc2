using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.comp.graphic
{
    /// <summary>
    /// 图像边界
    /// </summary>
    public class PriceGraphicMapping
    {
        private PriceRectangle priceRect;

        private Rectangle drawRect;

        private float priceScaleWidth;

        private float priceScaleHeight;

        public PriceGraphicMapping(Rectangle drawRect, PriceRectangle priceRect)
        {
            SetRect(drawRect, priceRect);
        }

        public PointF CalcPoint(PriceShape_Point pricePoint)
        {
            return new PointF(CalcX(pricePoint.X), CalcY(pricePoint.Y));
        }

        /// <summary>
        /// 得到index所在的位置
        /// 如果是柱状图得到块的中间位置
        /// 如果是线图获得连接线的点
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public float CalcX(float priceX)
        {
            return drawRect.Left + (priceX - priceRect.PriceLeft) * priceScaleWidth;
        }

        /// <summary>
        /// 该方法用来确定一个价格的y轴坐标，供子类调用
        /// </summary>
        /// <param name="price"></param>
        /// <returns></returns>
        public float CalcY(float priceY)
        {
            return drawRect.Top + (priceRect.PriceTop - priceY) * priceScaleHeight;
        }

        public float PriceWidth
        {
            get
            {
                return DrawRect.Width / PriceRect.PriceWidth;
            }
        }

        public float PriceHeight
        {
            get
            {
                return DrawRect.Height / PriceRect.PriceHeight;
            }
        }

        public PriceRectangle PriceRect
        {
            get
            {
                return priceRect;
            }
        }

        public Rectangle DrawRect
        {
            get
            {
                return drawRect;
            }
        }

        public void SetRect(Rectangle drawRect, PriceRectangle priceRect)
        {
            bool isDrawRectChange = this.drawRect != drawRect;
            if (isDrawRectChange)
                this.drawRect = drawRect;
            bool isPriceRectChange = this.priceRect != priceRect;
            if (isPriceRectChange)
                this.priceRect = priceRect;

            if (isDrawRectChange || isPriceRectChange)
                RecalcScale();
        }

        private void RecalcScale()
        {
            if (priceRect == null)
                return;
            this.priceScaleWidth = drawRect.Width / priceRect.PriceWidth;
            this.priceScaleHeight = drawRect.Height / priceRect.PriceHeight;
        }

        public float PriceScaleWidth
        {
            get
            {
                return priceScaleWidth;
            }
        }

        public float PriceScaleHeight
        {
            get
            {
                return priceScaleHeight;
            }
        }

        public float CalcPriceX(float x)
        {
            float distance = x - DrawRect.X;
            return distance / PriceWidth + PriceRect.PriceLeft;
        }

        public float CalcPriceY(float y)
        {
            float distance = y - DrawRect.Y;
            return distance / PriceHeight + PriceRect.PriceTop;
        }
    }
}