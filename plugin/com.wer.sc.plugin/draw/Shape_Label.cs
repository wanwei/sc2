using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.draw
{
    public class PriceShape_Label : PriceShape
    {
        private string txt;

        public string Txt
        {
            get
            {
                return txt;
            }

            set
            {
                txt = value;
            }
        }

        public PriceShapeType GetShapeType()
        {
            return PriceShapeType.Label;
        }

        public Color Color
        {
            get
            {
                return Color.Red;
            }
        }
    }
}
