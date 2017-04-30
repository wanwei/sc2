using com.wer.sc.comp.graphic.utils;
using com.wer.sc.data;
using System;
using System.Drawing;

namespace com.wer.sc.comp.graphic.real
{
    public class GraphicDrawer_RealChart : GraphicDrawer_Real_Abstract
    {
       
        private PriceGraphicMapping priceMapping = new PriceGraphicMapping();

        public override PriceGraphicMapping PriceMapping
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
            ITimeLineData realData = DataProvider.GetRealData();
            int startIndex = 0;
            int endIndex = DataProvider.CurrentIndex;

            float priceBottom = realData.Arr_Price[startIndex];
            float priceTop = realData.Arr_Price[startIndex];
            for (int i = startIndex + 1; i <= endIndex; i++)
            {
                if (priceBottom > realData.Arr_Price[i])
                    priceBottom = realData.Arr_Price[i];
                if (priceTop < realData.Arr_Price[i])
                    priceTop = realData.Arr_Price[i];
            }

            float yesterdayEnd = realData.YesterdayEnd;
            float b1 = Math.Abs(priceTop - yesterdayEnd);
            float b2 = Math.Abs(priceBottom - yesterdayEnd);
            float b = (float)Math.Round((b1 > b2 ? b1 : b2) * 1.05, 2);

            return new PriceRectangle(startIndex, realData.Length - 1, yesterdayEnd + b, yesterdayEnd - b);
        }

        public override void DrawGraph(Graphics graphic)
        {
            this.BlockWidth = this.DisplayRect.Width / (DataProvider.GetRealData().Length - 1);
            ITimeLineData realData = DataProvider.GetRealData();
            DrawFrame(graphic, realData);
            DrawReal(graphic, realData);
        }

        public override void DrawGraph(Graphics graphic, Rectangle rect)
        {
            //TODO 只画区块
            this.BlockWidth = this.DisplayRect.Width / (DataProvider.GetRealData().Length - 1);
            ITimeLineData realData = DataProvider.GetRealData();
            DrawFrame(graphic, realData);
            DrawReal(graphic, realData);
        }

        private void DrawFrame(Graphics g, ITimeLineData realData)
        {
            Rectangle rectangleScale = FrameRect;
            Pen pen = this.ColorConfig.Pen_FrameLine;
            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            g.DrawRectangle(pen, rectangleScale);

            DrawVerticals(g, realData);
            DrawHorizonal(g, realData);
        }

        private void DrawHorizonal(Graphics g, ITimeLineData realData)
        {
            Pen pen2 = this.ColorConfig.Pen_FrameLine;
            pen2.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            float percent = (PriceMapping.PriceRect.PriceHeight / realData.YesterdayEnd * 100) / 10;
            for (int i = -4; i <= 4; i++)
            {
                float horizonalPercent = (float)Math.Round(percent * i, 2);
                float price = (float)Math.Round(realData.YesterdayEnd * (1 + horizonalPercent / 100), 2);
                float y = PriceMapping.CalcY(price);

                float x1 = FrameRect.X;
                float x2 = FrameRect.Right;
                g.DrawLine(pen2, x1, y, x2, y);
                Brush b;
                if (i < 0)
                    b = ColorConfig.Brush_CandleBlockDown;
                else if (i == 0)
                    b = ColorConfig.Brush_CandleFlat;
                else
                    b = ColorConfig.Brush_CandleBlockUp;
                Font f = ColorConfig.Font_CandleFrameScaleFont;
                String pstr = price.ToString();
                g.DrawString(pstr, f, b, x1 - pstr.Length * 6 - 4, y - 5);
                g.DrawString(horizonalPercent + "%", f, b, x2 + 2, y - 5);
            }
        }

        private void DrawReal(Graphics g, ITimeLineData realData)
        {
            Pen pen = new Pen(ColorConfig.Color_White, 1);

            float lastx = PriceMapping.CalcX(0);
            float lasty = PriceMapping.CalcY(realData.Arr_Price[0]); 
            for (int i = 1; i <= DataProvider.CurrentIndex; i++)
            {
                float lastprice = realData.Arr_Price[i - 1];
                float price = realData.Arr_Price[i];
                //float lastprice = 2858f;
                //float price = 2858f;

                //float x1 = PriceMapping.CalcX(i - 1);
                //float y1 = PriceMapping.CalcY(lastprice);
                float x2 = PriceMapping.CalcX(i);
                float y2 = PriceMapping.CalcY(price);
                g.DrawLine(pen, lastx, lasty, x2, y2);

                lastx = x2;
                lasty = y2;
            }
        }
    }
}
