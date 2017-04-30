using com.wer.sc.comp.graphic.utils;
using com.wer.sc.data;
using com.wer.sc.plugin;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.comp.graphic
{
    public class GraphicDrawer_CandleMount : GraphicDrawer_Candle_Abstract
    {
        private PriceGraphicMapping priceMapping = new PriceGraphicMapping();

        public PriceGraphicMapping PriceMapping
        {
            get
            {
                priceMapping.DrawRect = DisplayRect;
                priceMapping.PriceRect = GetPriceRectangle();
                return priceMapping;
            }
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

        public override void DrawGraph(Graphics graphic)
        {
            DrawFrame(graphic);
            DrawMount(graphic);
        }

        private void DrawFrame(Graphics g)
        {
            Rectangle rectangleScale = FrameRect;
            g.DrawRectangle(ColorConfig.Pen_FrameLine, rectangleScale);
            float price = PriceMapping.PriceRect.PriceTop / 2;
            int y = (int)priceMapping.CalcY(price);
            ColorConfig config = ColorConfig;
            g.DrawLine(config.Pen_CandleFrameScaleLine, new Point((int)rectangleScale.X, y), new Point((int)rectangleScale.Right, (int)y));

            String label = price.ToString();
            g.DrawString(label, config.Font_CandleFrameScaleFont, config.Brush_CandleFrameScaleBrush, new Point((int)rectangleScale.X - label.Length * 8 - 5, (int)y - 5));
        }

        private void DrawMount(Graphics g)
        {
            IKLineData data = DataProvider.GetKLineData();
            int startIndex = DataProvider.StartIndex;
            int endIndex = DataProvider.EndIndex;
            for (int i = startIndex; i < endIndex; i++)
            {
                DrawMount(g, new KLineBar_KLineData(data, i), i);
            }
            DrawMount(g, DataProvider.GetCurrentChart(), endIndex);
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
}
