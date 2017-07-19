using com.wer.sc.comp.graphic.utils;
using com.wer.sc.data;
using com.wer.sc.data.utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.comp.graphic.timeline
{
    public abstract class GraphicDrawer_TimeLine_Abstract : GraphicDrawer_Abstract
    {
        private IGraphicData_TimeLine dataProvider;

        public IGraphicData_TimeLine DataProvider
        {
            get
            {
                return dataProvider;
            }

            set
            {
                dataProvider = value;
            }
        }

        public abstract PriceGraphicMapping PriceMapping { get; }

        private int blockWidth = 8;

        public int BlockWidth
        {
            get
            {
                return blockWidth;
            }

            set
            {
                blockWidth = value;
            }
        }

        public abstract void DrawGraph(Graphics graphics, Rectangle rect);

        public virtual void DrawVerticals(Graphics g, ITimeLineData realData)
        {
            double lastTime = 0;
            for (int i = 0; i < realData.Length; i++)
            {
                double time = realData.Arr_Time[i];
                //整点画虚线
                if (time == Math.Round(time, 2))
                {
                    DrawVertical(g, i, true, time);
                }
                //检查是否
                if (i > 1)
                {
                    TimeSpan span = TimeUtils.Substract(time, lastTime);
                    if (span.Minutes + span.Hours * 60 > 10)
                    {
                        DrawVertical(g, i, false, time);
                    }
                }
                lastTime = time;
            }
        }

        public virtual void DrawVertical(Graphics g, int index, bool dashed, double time)
        {
            Pen pen = ColorConfig.Pen_FrameLine;
            if (dashed)
            {
                pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Custom;
                pen.DashPattern = new float[] { 2f, 2f };
            }
            else
                pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            float x = PriceMapping.CalcX(index);
            float y1 = FrameRect.Y;
            float y2 = FrameRect.Y + FrameRect.Height;
            g.DrawLine(pen, x, y1, x, y2);        
        }
    }
}
