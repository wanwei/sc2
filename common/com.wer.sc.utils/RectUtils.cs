using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace com.wer.sc.utils
{
    public class RectUtils
    {
        public static bool isInRect(Rectangle rect, Point p)
        {
            return isInRect(rect, p.X, p.Y);
        }

        public static bool isInRect(Rectangle rect, int x, int y)
        {
            return (x >= rect.X && x <= rect.Right && x >= rect.Top && x <= rect.Bottom);
        }
    }
}
