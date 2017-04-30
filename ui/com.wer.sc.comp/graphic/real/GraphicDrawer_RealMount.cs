using com.wer.sc.comp.graphic.utils;
using com.wer.sc.data;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.comp.graphic.real
{
    public class GraphicDrawer_RealMount : GraphicDrawer_Real_Abstract
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

            float mountTop = realData.Arr_Mount[startIndex];
            for (int i = startIndex + 1; i <= endIndex; i++)
            {
                if (mountTop < realData.Arr_Mount[i])
                    mountTop = realData.Arr_Mount[i];
            }

            return new PriceRectangle(startIndex, realData.Length - 1, mountTop, 0);
        }

        public override void DrawGraph(Graphics graphic)
        {
            this.BlockWidth = this.DisplayRect.Width / (DataProvider.GetRealData().Length - 1);
            ITimeLineData realData = DataProvider.GetRealData();
            DrawFrame(graphic, realData);
            DrawMount(graphic, realData);
        }

        public override void DrawGraph(Graphics graphic, Rectangle rect)
        {
            //TODO 只画区块
            this.BlockWidth = this.DisplayRect.Width / (DataProvider.GetRealData().Length - 1);
            ITimeLineData realData = DataProvider.GetRealData();
            DrawFrame(graphic, realData);
            DrawMount(graphic, realData);
        }

        private void DrawFrame(Graphics g, ITimeLineData realData)
        {
            Rectangle rectangleScale = FrameRect;
            Pen pen = this.ColorConfig.Pen_FrameLine;
            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            g.DrawRectangle(pen, rectangleScale);

            DrawVerticals(g, realData);
            DrawHorizonals(g, realData);
        }

        public override void DrawVertical(Graphics g, int index, bool dashed, double time)
        {
            base.DrawVertical(g, index, dashed, time);

            float x = PriceMapping.CalcX(index);
            float y2 = FrameRect.Y + FrameRect.Height;
            if (dashed)
            {
                string txt = Math.Round(((time - (int)time) * 100), 0) + ":00";
                g.DrawString(txt, ColorConfig.Font_CandleFrameScaleFont, ColorConfig.Brush_CandleFrameScaleBrush, x - 15, y2 + 2);
            }
        }

        private void DrawHorizonals(Graphics g, ITimeLineData realData)
        {
            Pen pen = this.ColorConfig.Pen_FrameLine;
            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;

            float distance = PriceMapping.PriceRect.PriceHeight / 5;
            for (int i = 1; i <= 4; i++)
            {
                float mountScale = (float)Math.Round(distance * i, 0);
                float y = PriceMapping.CalcY(mountScale);

                float x1 = FrameRect.X;
                float x2 = FrameRect.Right;
                g.DrawLine(pen, x1, y, x2, y);
                Brush b = ColorConfig.Brush_CandleFrameScaleBrush;
                Font f = ColorConfig.Font_CandleFrameScaleFont;
                String pstr = mountScale.ToString();
                g.DrawString(pstr, f, b, x1 - pstr.Length * 6 - 4, y - 5);
                //g.DrawString(horizonalPercent + "%", f, b, x2 + 2, y - 5);
            }
        }

        private void DrawMount(Graphics g, ITimeLineData realData)
        {
            Pen pen = ColorConfig.Pen_Line_RealMount;
            for (int i = 0; i <= DataProvider.CurrentIndex; i++)
            {
                int mount = realData.Arr_Mount[i];
                float x = PriceMapping.CalcX(i);
                float y1 = priceMapping.DrawRect.Bottom;
                float y2 = PriceMapping.CalcY(mount);
                g.DrawLine(pen, x, y1, x, y2);
            }
        }
    }
}