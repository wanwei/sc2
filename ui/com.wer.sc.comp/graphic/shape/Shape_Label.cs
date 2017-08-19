using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.comp.graphic.shape
{
    public class Shape_Label:IShape
    {
        private string text;

        private int x;

        private int y;

        private StringFormat stringFormat;

        private Font font;

        private Color color;

        public string Text
        {
            get
            {
                return text;
            }

            set
            {
                text = value;
            }
        }

        public int X
        {
            get
            {
                return x;
            }

            set
            {
                x = value;
            }
        }

        public int Y
        {
            get
            {
                return y;
            }

            set
            {
                y = value;
            }
        }

        public StringFormat StringFormat
        {
            get
            {
                return stringFormat;
            }

            set
            {
                stringFormat = value;
            }
        }

        public Font Font
        {
            get
            {
                return font;
            }

            set
            {
                font = value;
            }
        }

        public Color Color
        {
            get
            {
                return color;
            }

            set
            {
                color = value;
            }
        }

        public ShapeType ShapeType
        {
            get
            {
                return ShapeType.Label;
            }
        }
    }
}