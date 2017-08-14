using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.draw
{
    public class PriceShape_Line : PriceShape
    {
        public PriceShape_Point StartPoint;

        public PriceShape_Point EndPoint;

        public float Width;

        private Color color;

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

        public PriceShapeType GetShapeType()
        {
            return PriceShapeType.Line;
        }
    }
}
