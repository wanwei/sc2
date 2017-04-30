using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.comp.graphic
{
    public class ColorConfig
    {
        private Color color_TextUp = ColorUtils.GetColor("#FF3C39");

        public Color Color_TextUp
        {
            get { return color_TextUp; }
            set { color_TextUp = value; }
        }

        private Color color_TextDown = ColorUtils.GetColor("#00DC00");

        public Color Color_TextDown
        {
            get { return color_TextDown; }
            set { color_TextDown = value; }
        }

        private Color color_BlockUp = ColorUtils.GetColor("#FF3C3C");

        public Color Color_BlockUp
        {
            get { return color_BlockUp; }
            set { color_BlockUp = value; }
        }

        private Color color_BlockDown = ColorUtils.GetColor("#00F0F0");

        public Color Color_BlockDown
        {
            get { return color_BlockDown; }
            set { color_BlockDown = value; }
        }

        private Color color_White = ColorUtils.GetColor("#E7E7E7");

        public Color Color_White
        {
            get { return color_White; }
            set { color_White = value; }
        }

        private Color color_StockInfo = ColorUtils.GetColor("#DCDC0A");

        public Color Color_StockInfo
        {
            get { return color_StockInfo; }
        }

        /// <summary>
        /// 十字线使用的笔
        /// </summary>
        public Pen Pen_CrossHair
        {
            get { return new Pen(color_White); }
        }

        private Pen pen_FrameLine = new Pen(ColorUtils.GetColor("#8A0000"));

        /// <summary>
        /// 边框使用的笔
        /// </summary>
        public Pen Pen_FrameLine
        {
            get { return pen_FrameLine; }
        }

        private Pen pen_Line_RealMount = new Pen(ColorUtils.GetColor("#CCCC00"));
        public Pen Pen_Line_RealMount
        {
            get { return pen_Line_RealMount; }
        }

        private Pen pen_FrameDashLine = new Pen(ColorUtils.GetColor("#8A0000"));

        /// <summary>
        /// 边框使用的笔
        /// </summary>
        public Pen Pen_FrameDashLine
        {
            get
            {
                pen_FrameDashLine.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                return pen_FrameDashLine;
            }
        }

        private Pen pen_CandleFrameScaleLine = GetPenFrameScaleLine();

        private static Pen GetPenFrameScaleLine()
        {
            Pen pen1 = new Pen(Color.Red);
            pen1.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            return pen1;
        }

        public Pen Pen_CandleFrameScaleLine
        {
            get { return pen_CandleFrameScaleLine; }
            set { pen_CandleFrameScaleLine = value; }
        }

        //private Font font_CandleFrameScaleFont = new Font("宋体", 10, FontStyle.Bold);
        private Font font_CandleFrameScaleFont = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

        public Font Font_CandleFrameScaleFont
        {
            get { return font_CandleFrameScaleFont; }
            set { font_CandleFrameScaleFont = value; }
        }

        private Brush brush_CandleFrameScaleBrush = new SolidBrush(Color.Gray);

        public Brush Brush_CandleFrameScaleBrush
        {
            get { return brush_CandleFrameScaleBrush; }
            set { brush_CandleFrameScaleBrush = value; }
        }

        public Brush Brush_CandleBlockUp
        {
            get { return new SolidBrush(Color_BlockUp); }
        }

        public Brush Brush_CandleBlockDown
        {
            get { return new SolidBrush(color_BlockDown); }
        }

        public Brush Brush_CandleFlat
        {
            get { return new SolidBrush(color_White); }
        }

        private Color color_RealFrame = Color.FromArgb(240, 128, 0, 0);

        public Color Color_RealFrame
        {
            get { return color_RealFrame; }
            set { color_RealFrame = value; }
        }

        public Pen Pen_RealFrame
        {
            get
            {
                Pen p = new Pen(color_RealFrame);
                return p;
            }
        }

        public static ColorConfig instance = new ColorConfig();
    }
}
