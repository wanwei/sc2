using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.comp.graphic.utils
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



        public PointF CalcPoint(PricePoint pricePoint)
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
            //if (PriceRect.PriceWidth == 0)
            //    return DrawRect.X;
            //float percentX = (float)Math.Round((priceX - PriceRect.PriceLeft) / PriceRect.PriceWidth, 2);
            //return (float)Math.Round(DrawRect.Left + DrawRect.Width * percentX, 2);
        }

        /// <summary>
        /// 该方法用来确定一个价格的y轴坐标，供子类调用
        /// </summary>
        /// <param name="price"></param>
        /// <returns></returns>
        public float CalcY(float priceY)
        {
            return drawRect.Top + (priceRect.PriceTop - priceY) * priceScaleHeight;
            //if (PriceRect.PriceHeight == 0)
            //    return DrawRect.Y;
            //float percentY = (float)Math.Round((PriceRect.PriceTop - priceY) / PriceRect.PriceHeight, 2);
            //return (float)Math.Round(DrawRect.Top + DrawRect.Height * percentY, 2);
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

            set
            {
                priceRect = value;
                RecalcScale();
            }
        }

        public Rectangle DrawRect
        {
            get
            {
                return drawRect;
            }

            set
            {
                drawRect = value;
                RecalcScale();
            }
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