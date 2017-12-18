using com.wer.sc.graphic.utils;
using com.wer.sc.data;
using com.wer.sc.plugin;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.graphic
{
    public class GraphicDrawer_CandleMount : GraphicDrawer_Candle_Abstract
    {
        private CandleMountFrameDrawer candleMountFrameDrawer;

        private CandleMountDrawer candleMountDrawer;

        private PriceGraphicMapping priceMapping;

        public PriceGraphicMapping PriceMapping
        {
            get
            {
                if (priceMapping == null)
                    priceMapping = new PriceGraphicMapping(DisplayRect, GetPriceRectangle());
                else
                {
                    priceMapping.SetRect(DisplayRect, GetPriceRectangle());
                }
                return priceMapping;
            }
        }

        public GraphicDrawer_CandleMount()
        {
            this.candleMountDrawer = new CandleMountDrawer(ColorConfig);
            this.candleMountFrameDrawer = new CandleMountFrameDrawer(ColorConfig);
        }

        private PriceRectangle GetPriceRectangle()
        {
            IKLineData data = DataProvider.GetKLineData();
            int startIndex = DataProvider.StartIndex;
            int endIndex = DataProvider.EndIndex;
            float priceBottom = 0;
            float priceTop = data.Arr_Mount[startIndex];
            for (int i = startIndex + 1; i <= endIndex; i++)
            {
                if (priceTop < data.Arr_Mount[i])
                    priceTop = data.Arr_Mount[i];
            }
            return new PriceRectangle(startIndex - 0.5f, endIndex + 0.5f, priceTop, priceBottom);
        }

        public override void Paint(Graphics graphic)
        {
            DrawFrame(graphic);
            DrawMount(graphic);
            base.Paint(graphic);
        }

        private void DrawFrame(Graphics g)
        {
            this.candleMountFrameDrawer.DrawFrame(g, FrameRect, PriceMapping);
        }

        private void DrawMount(Graphics g)
        {
            IKLineData data = DataProvider.GetKLineData();
            int startIndex = DataProvider.StartIndex;
            int endIndex = DataProvider.EndIndex;
            if (endIndex < startIndex)
                return;
            this.candleMountDrawer.DrawMount(g, data, startIndex, endIndex, PriceMapping, BlockWidth, BlockPadding);
        }

        private void DrawMount(Graphics g, IKLineBar chart, int index)
        {
            Rectangle rectangle = DisplayRect;
            bool isRed = chart.End > chart.Start;
            Brush b = new SolidBrush(isRed ? Color.Red : Color.LightSeaGreen);
            Pen p = new Pen(b);
            double XMiddle = priceMapping.CalcX(index);
            double YTop = priceMapping.CalcY(chart.Mount);
            double YBottom = rectangle.Bottom;

            double halfBlockWidth = (BlockWidth - BlockPadding) / 2;

            float XLeft = (float)(XMiddle - halfBlockWidth);
            float XRight = (float)(XMiddle + halfBlockWidth);
            if (isRed)
                g.DrawRectangle(p, (int)XLeft, (int)YTop, (int)halfBlockWidth * 2, (int)(YBottom - YTop));
            else
                g.FillRectangle(b, (int)XLeft, (int)YTop, (int)halfBlockWidth * 2, (int)(YBottom - YTop));
        }
    }

    class CandleMountFrameDrawer
    {
        private ColorConfig colorConfig;
        public CandleMountFrameDrawer(ColorConfig colorConfig)
        {
            this.colorConfig = colorConfig;
        }

        public void DrawFrame(Graphics g, Rectangle rect, PriceGraphicMapping priceMapping)
        {
            Rectangle rectangleScale = rect;
            g.DrawRectangle(colorConfig.Pen_FrameLine, rectangleScale);
            float price = priceMapping.PriceRect.PriceTop / 2;
            int y = (int)priceMapping.CalcY(price);
            ColorConfig config = colorConfig;
            g.DrawLine(config.Pen_CandleFrameScaleLine, new Point((int)rectangleScale.X, y), new Point((int)rectangleScale.Right, (int)y));

            String label = price.ToString();
            g.DrawString(label, config.Font_CandleFrameScaleFont, config.Brush_CandleFrameScaleBrush, new Point((int)rectangleScale.X - label.Length * 8 - 5, (int)y - 5));
        }
    }

    class CandleMountDrawer
    {
        private ColorConfig colorConfig;
        public CandleMountDrawer(ColorConfig colorConfig)
        {
            this.colorConfig = colorConfig;
        }

        public void DrawMount(Graphics g, IKLineData klineData, int startIndex, int endIndex, PriceGraphicMapping priceMapping, float blockWidth, float blockPadding)
        {
            if (endIndex < startIndex)
                return;
            for (int i = startIndex; i <= endIndex; i++)
            {
                DrawMount(g, new KLineBar_KLineData(klineData, i), i, priceMapping, blockWidth, blockPadding);
            }
        }

        private void DrawMount(Graphics g, IKLineBar chart, int index, PriceGraphicMapping priceMapping, float blockWidth, float blockPadding)
        {
            Rectangle rectangle = priceMapping.DrawRect;
            bool isRed = chart.End > chart.Start;
            //Brush b = new SolidBrush(isRed ? Color.Red : Color.LightSeaGreen);
            Brush b = isRed ? this.colorConfig.Brush_CandleBlockUp : this.colorConfig.Brush_CandleBlockDown;
            Pen p = new Pen(b);
            double XMiddle = priceMapping.CalcX(index);
            double YTop = priceMapping.CalcY(chart.Mount);
            double YBottom = rectangle.Bottom;

            double halfBlockWidth = (blockWidth - blockPadding) / 2;

            float XLeft = (float)(XMiddle - halfBlockWidth);
            float XRight = (float)(XMiddle + halfBlockWidth);
            if (isRed)
                g.DrawRectangle(p, (int)XLeft, (int)YTop, (int)halfBlockWidth * 2, (int)(YBottom - YTop));
            else
                g.FillRectangle(b, (int)XLeft, (int)YTop, (int)halfBlockWidth * 2, (int)(YBottom - YTop));
        }
    }
}