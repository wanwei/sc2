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
    public class ModelLinesAttribute : Attribute
    {
        private String name;

        private Color color;

        private int width;

        public ModelLinesAttribute(string color) : this(color, 2)
        {
        }

        public ModelLinesAttribute(string color, int width)
        {
            this.color = ColorUtils.GetColor(color);
            this.width = width;
        }

        public ModelLinesAttribute(string name, string color, int width)
        {
            this.name = name;
            this.color = ColorUtils.GetColor(color);
            this.width = width;
        }

        public string Name
        {
            get
            {
                return name;
            }
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