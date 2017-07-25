using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.wer.sc.comp
{
    /// <summary>
    /// 图片的外边距
    /// </summary>
    public class GraphicMarginInfo
    {
        private int marginLeft;

        public int MarginLeft
        {
            get { return marginLeft; }
            set { marginLeft = value; }
        }

        private int marginTop;

        public int MarginTop
        {
            get { return marginTop; }
            set { marginTop = value; }
        }
        private int marginRight;

        public int MarginRight
        {
            get { return marginRight; }
            set { marginRight = value; }
        }
        private int marginBottom;

        public int MarginBottom
        {
            get { return marginBottom; }
            set { marginBottom = value; }
        }

        public GraphicMarginInfo()
        {
        }

        public GraphicMarginInfo(int marginLeft, int marginTop, int marginRight, int marginBottom)
        {
            this.marginLeft = marginLeft;
            this.marginTop = marginTop;
            this.marginRight = marginRight;
            this.marginBottom = marginBottom;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(MarginLeft).Append(",");
            sb.Append(MarginTop).Append(",");
            sb.Append(MarginRight).Append(",");
            sb.Append(MarginBottom);
            return sb.ToString();
        }
    }
}