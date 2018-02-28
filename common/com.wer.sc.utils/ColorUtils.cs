using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace com.wer.sc.utils
{
    public class ColorUtils
    {
        public static string ToString(Color color)
        {
            return ColorTranslator.ToHtml(color);
        }

        public static System.Drawing.Color GetColor(string colorString)
        {
            System.Drawing.Color color;
            if (colorString[0] == '#' && colorString.Length < 8)
            {
                string s = colorString.Substring(1);
                while (s.Length != 6)
                {
                    s = string.Concat("0", s);
                }
                int red = Convert.ToInt32(s.Substring(0, 2), 16);
                int green = Convert.ToInt32(s.Substring(2, 2), 16);
                int blue = Convert.ToInt32(s.Substring(4, 2), 16);
                color = System.Drawing.Color.FromArgb(red, green, blue);
            }
            else
            {
                color = System.Drawing.Color.FromName(colorString);
            }
            return color;
        }
    }
}
