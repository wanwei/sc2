using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.comp.graphic.utils
{
    /// <summary>
    /// 选中的信息
    /// </summary>
    public class SelectedPointInfo
    {
        private int height = -1;
        /// <summary>
        /// 如不设置，系统会自动计算
        /// </summary>
        public int Height
        {
            get { return height; }
            set { height = value; }
        }

        private int width = -1;
        /// <summary>
        /// 如不设置，系统会自动计算
        /// </summary>
        public int Width
        {
            get { return width; }
            set { width = value; }
        }

        private int gap;

        public int Gap
        {
            get { return gap; }
            set { gap = value; }
        }

        private int lineHeight;

        public int LineHeight
        {
            get { return lineHeight; }
            set { lineHeight = value; }
        }

        private Point startPoint;

        public Point StartPoint
        {
            get { return startPoint; }
            set { startPoint = value; }
        }

        private List<BlockLineInfo> lines = new List<BlockLineInfo>();

        public List<BlockLineInfo> Lines
        {
            get { return lines; }
        }

        public void DrawGraph(Graphics g, ColorConfig colorConfig)
        {
            SelectedPointInfo blockInfo = this;
            Point p = blockInfo.StartPoint;
            int blockWidth = CalcBlockInfoWidth(blockInfo);
            int blockHeight = CalcBlockInfoHeight(blockInfo);
            g.FillRectangle(new SolidBrush(Color.Black), p.X, p.Y, blockWidth, blockHeight);
            g.DrawRectangle(colorConfig.Pen_CrossHair, p.X, p.Y, blockWidth, blockHeight);

            Point linePoint = p;
            linePoint.Y += blockInfo.Gap;
            for (int i = 0; i < blockInfo.Lines.Count; i++)
            {
                BlockLineInfo lineInfo = blockInfo.Lines[i];
                g.DrawString(lineInfo.Text, lineInfo.TextFont, lineInfo.TextBrush, linePoint);
                linePoint.Y += blockInfo.LineHeight;
            }
        }

        private int CalcBlockInfoHeight(SelectedPointInfo blockInfo)
        {
            if (blockInfo.Height > 0)
                return blockInfo.Height;
            return (int)(blockInfo.Lines.Count * blockInfo.LineHeight + blockInfo.Gap);
        }

        private int CalcBlockInfoWidth(SelectedPointInfo blockInfo)
        {
            if (blockInfo.Width > 0)
                return blockInfo.Width;
            return 20;
        }
    }

    public class BlockLineInfo
    {
        private String text;

        public String Text
        {
            get { return text; }
            set { text = value; }
        }

        private Brush textBrush;

        public Brush TextBrush
        {
            get { return textBrush; }
            set { textBrush = value; }
        }

        private Font textFont;

        public Font TextFont
        {
            get { return textFont; }
            set { textFont = value; }
        }

        public BlockLineInfo()
        {
        }

        public BlockLineInfo(String text, Brush brush, Font font)
        {
            this.text = text;
            this.textBrush = brush;
            this.textFont = font;
        }
    }

}
