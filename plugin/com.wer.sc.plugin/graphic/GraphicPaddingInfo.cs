using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.wer.sc.graphic
{
    /// <summary>
    /// 绘图时选定区域的内边距
    /// </summary>
    public class GraphicPaddingInfo
    {
        private int paddingLeft;

        public int PaddingLeft
        {
            get { return paddingLeft; }
            set { paddingLeft = value; }
        }

        private int paddingTop;

        public int PaddingTop
        {
            get { return paddingTop; }
            set { paddingTop = value; }
        }

        private int paddingRight;

        public int PaddingRight
        {
            get { return paddingRight; }
            set { paddingRight = value; }
        }

        private int paddingBottom;

        public int PaddingBottom
        {
            get { return paddingBottom; }
            set { paddingBottom = value; }
        }

        public GraphicPaddingInfo()
        {
        }

        public GraphicPaddingInfo(int paddingLeft, int paddingTop, int paddingRight, int paddingBottom)
        {
            this.paddingLeft = paddingLeft;
            this.paddingTop = paddingTop;
            this.paddingRight = paddingRight;
            this.paddingBottom = paddingBottom;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(PaddingLeft).Append(",");
            sb.Append(PaddingTop).Append(",");
            sb.Append(PaddingRight).Append(",");
            sb.Append(PaddingBottom);
            return sb.ToString();
        }
    }
}