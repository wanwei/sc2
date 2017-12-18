using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.graphic.shape
{
    public class PriceShape_Label : PriceShape
    {
        private string text;

        private PriceShape_Point point;

        private StringFormat stringFormat;

        private Font font;

        private Color color;

        public PriceShape_Point Point
        {
            get
            {
                return point;
            }

            set
            {
                point = value;
            }
        }

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

        public PriceShapeType GetShapeType()
        {
            return PriceShapeType.Label;
        }
    }
}
