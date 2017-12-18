using com.wer.sc.graphic.utils;
using com.wer.sc.data;
using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.graphic
{
    /// <summary>
    /// 画蜡烛图
    /// </summary>
    public class GraphicDrawer_CandleChart : GraphicDrawer_Candle_Abstract
    {
        private CandleFrameDrawer candleFrameDrawer;

        private CandleContentDrawer candleContentDrawer;

        private PriceGraphicMapping priceMapping;

        public PriceGraphicMapping PriceMapping
        {
            get
            {
                if (priceMapping == null)
                    priceMapping = new PriceGraphicMapping(DisplayRect, GetPriceRectangle());
                else
                    priceMapping.SetRect(DisplayRect, GetPriceRectangle());
                return priceMapping;
            }
        }

        private PriceRectangle priceRectangle;

        private PriceRectangle GetPriceRectangle()
        {
            if (priceRectangle == null)
            {
                priceRectangle = GetPriceRectangleInternal();
                return priceRectangle;
            }

            IKLineData klineData = DataProvider.GetKLineData();
            if (priceRectangle.StartIndex == GetStartIndex() && priceRectangle.EndIndex == DataProvider.EndIndex)
            {
                return priceRectangle;
            }
            priceRectangle = GetPriceRectangleInternal();
            return priceRectangle;
        }

        private int GetStartIndex()
        {
            return DataProvider.StartIndex < 0 ? 0 : DataProvider.StartIndex;
        }

        private PriceRectangle GetPriceRectangleInternal()
        {
            IKLineData data = DataProvider.GetKLineData();
            int startIndex = GetStartIndex();
            int endIndex = DataProvider.EndIndex;
            float priceBottom = data.Arr_Low[startIndex];
            float priceTop = data.Arr_High[startIndex];
            for (int i = startIndex + 1; i <= endIndex; i++)
            {
                if (priceBottom > data.Arr_Low[i])
                    priceBottom = data.Arr_Low[i];
                if (priceTop < data.Arr_High[i])
                    priceTop = data.Arr_High[i];
            }
            return new PriceRectangle(startIndex - 0.5f, endIndex + 0.5f, priceTop, priceBottom);
        }

        public GraphicDrawer_CandleChart()
        {
            this.candleFrameDrawer = new CandleFrameDrawer(ColorConfig);
            this.candleContentDrawer = new CandleContentDrawer(ColorConfig);
        }

        public override void Paint(Graphics graphic)
        {
            if (!CheckData())
            {
                return;
            }

            DataProvider.BlockMount = (int)(this.DisplayRect.Width / BlockWidth);
            if (DataProvider.BlockMount <= 0)
                return;
            //LogHelper.Info(GetType(), "K线显示数量" + DataProvider.BlockMount.ToString());
            if (priceMapping != null)
                this.PriceRect = priceMapping.PriceRect;
            DrawFrame(graphic);
            DrawCandle(graphic);
            base.Paint(graphic);
        }

        private bool CheckData()
        {

            return true;
        }

        public void DrawFrame(Graphics g)
        {
            this.candleFrameDrawer.DrawFrame(g, FrameRect, PriceMapping);
        }

        private void DrawCandle(Graphics g)
        {
            IKLineData data = DataProvider.GetKLineData();
            int startIndex = DataProvider.StartIndex;
            int endIndex = DataProvider.EndIndex;
            this.candleContentDrawer.DrawCandle(g, data, startIndex, endIndex, PriceMapping, BlockWidth, BlockPadding);
        }
    }

    class CandleFrameDrawer
    {
        private ColorConfig colorConfig;

        public CandleFrameDrawer(ColorConfig colorConfig)
        {
            this.colorConfig = colorConfig;
        }

        public void DrawFrame(Graphics g, Rectangle frameRect, PriceGraphicMapping priceMapping)
        {
            Rectangle rectangleScale = frameRect;
            g.DrawRectangle(this.colorConfig.Pen_FrameLine, rectangleScale);

            float[] prices = findScalePrices(priceMapping);

            double xLeft = rectangleScale.X;
            double xRight = rectangleScale.X + rectangleScale.Width;
            double lowY = priceMapping.CalcY(prices[0]);
            double highY = priceMapping.CalcY(prices[1]);

            drawScaleLine(g, xLeft, xRight, lowY, prices[0]);
            drawScaleLine(g, xLeft, xRight, highY, prices[1]);
        }

        private int[] ARR_SCALEPRICE = { 1, 5, 10, 20, 40, 100, 200, 500, 1000, 2000 };

        private float[] findScalePrices(PriceGraphicMapping PriceMapping)
        {
            double height = PriceMapping.PriceRect.PriceHeight;
            //最小比例是5，然后依次是10、20、40、100、500、1000、2000
            int scalePriceInterval = 0;
            for (int i = 0; i < ARR_SCALEPRICE.Length; i++)
            {
                int interval = ARR_SCALEPRICE[i];
                if (height > interval && height < 3 * interval)
                {
                    scalePriceInterval = interval;
                    break;
                }
                if (height < interval)
                {
                    int index = i - 1;
                    index = index >= 0 ? index : 0;
                    scalePriceInterval = ARR_SCALEPRICE[index];
                    break;
                }
            }
            float lowPrice = ((int)(PriceMapping.PriceRect.PriceBottom / scalePriceInterval) + 1) * scalePriceInterval;

            float[] arr = new float[2];
            arr[0] = lowPrice;
            arr[1] = lowPrice + scalePriceInterval;
            return arr;
        }

        private float[] findScalePrices2(PriceGraphicMapping PriceMapping)
        {
            double height = PriceMapping.PriceRect.PriceHeight;
            //最小比例是5，然后依次是10、20、40、100、500、1000、2000
            int scalePriceInterval = 0;
            for (int i = 0; i < ARR_SCALEPRICE.Length; i++)
            {
                int interval = ARR_SCALEPRICE[i];
                if (height / interval < 5)
                {
                    
                }
                //if (height > interval && height < 3 * interval)
                //{
                //    scalePriceInterval = interval;
                //    break;
                //}
                //if (height < interval)
                //{
                //    int index = i - 1;
                //    index = index >= 0 ? index : 0;
                //    scalePriceInterval = ARR_SCALEPRICE[index];
                //    break;
                //}
            }
            float lowPrice = ((int)(PriceMapping.PriceRect.PriceBottom / scalePriceInterval) + 1) * scalePriceInterval;

            float[] arr = new float[2];
            arr[0] = lowPrice;
            arr[1] = lowPrice + scalePriceInterval;
            return arr;
        }

        private void drawScaleLine(Graphics g, double xLeft, double xRight, double y, double price)
        {
            ColorConfig config = colorConfig;
            g.DrawLine(config.Pen_CandleFrameScaleLine, new Point((int)xLeft, (int)y), new Point((int)xRight, (int)y));

            String label = price.ToString();
            g.DrawString(label, config.Font_CandleFrameScaleFont, config.Brush_CandleFrameScaleBrush, new Point((int)xLeft - label.Length * 8 - 5, (int)y - 5));
        }
    }

    class CandleContentDrawer
    {
        private ColorConfig ColorConfig;

        public CandleContentDrawer(ColorConfig colorConfig)
        {
            this.ColorConfig = colorConfig;
        }

        public void DrawCandle(Graphics g, IKLineData data, int startIndex, int endIndex, PriceGraphicMapping priceMapping, float blockWidth, float blockPadding)
        {
            for (int i = startIndex; i <= endIndex; i++)
            {
                DrawCandle(g, new KLineBar_KLineData(data, i), i, priceMapping, blockWidth, blockPadding);
            }
            //DrawCandle(g, DataProvider.GetCurrentChart(), endIndex);
        }

        private void DrawCandle(Graphics g, IKLineBar chart, int index, PriceGraphicMapping priceMapping, float blockWidth, float blockPadding)
        {
            bool isRed = chart.End > chart.Start;
            Brush b = isRed ? this.ColorConfig.Brush_CandleBlockUp : this.ColorConfig.Brush_CandleBlockDown;
            if (chart.End.Equals(chart.Start))
                b = this.ColorConfig.Brush_CandleFlat;
            
            Pen p = new Pen(b);
            float XMiddle = priceMapping.CalcX(index);
            float YTop = priceMapping.CalcY(chart.High);
            float YBottom = priceMapping.CalcY(chart.Low);
            float YBlockTop = (float)priceMapping.CalcY(isRed ? chart.End : chart.Start);
            float YBlockBottom = (float)priceMapping.CalcY(isRed ? chart.Start : chart.End);
            //画上影线和下影线            
            g.DrawLine(p, new PointF(XMiddle, YTop), new PointF(XMiddle, YBlockTop));
            g.DrawLine(p, new PointF(XMiddle, YBottom), new PointF(XMiddle, YBlockBottom));
            
            float halfBlockWidth = (blockWidth - blockPadding) / 2;

            float XLeft = XMiddle - halfBlockWidth;
            float XRight = XMiddle + halfBlockWidth;
            //画block
            if (chart.End == chart.Start)
                g.DrawLine(p, XLeft, YBlockBottom, XRight, YBlockBottom);
            else if (isRed)
                g.DrawRectangle(p, XLeft, YBlockTop, halfBlockWidth * 2, YBlockBottom - YBlockTop);
            else
                g.FillRectangle(b, XLeft, YBlockTop, halfBlockWidth * 2, YBlockBottom - YBlockTop);
        }
    }
}