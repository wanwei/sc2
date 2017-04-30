using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.comp.graphic
{
    public class PolyLineArray
    {
        public float[] Data;

        public Color color;

        public float Width = 1f;

        public PolyLineArray()
        {

        }

        public PolyLineArray(float[] data, Color color)
        {
            this.Data = data;
            this.color = color;
        }

        public PolyLineArray(float[] data, Color color, float width)
        {
            this.Data = data;
            this.color = color;
            this.Width = width;
        }
    }
}
