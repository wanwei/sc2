using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.ana
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class ModelPointsAttribute : Attribute
    {
        private Color color;

        private int width;

        public ModelPointsAttribute(string color) : this(color, 2)
        {
        }

        public ModelPointsAttribute(string color, int width)
        {
            this.color = ColorUtils.GetColor(color);
            this.width = width;
        }

        public Color Color
        {
            get
            {
                return color;
            }
        }

        public int Width
        {
            get
            {
                return width;
            }
        }
    }
}
