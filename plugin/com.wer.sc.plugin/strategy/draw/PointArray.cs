using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.comp.graphic
{
    public class PointArray
    {
        public float[] Data;

        public Color color;

        public float Width = 5;

        public PointArray()
        {

        }
        public PointArray(float[] data, Color color)
        {
            this.Data = data;
            this.color = color;
        }

        public PointArray(float[] data, Color color, float Width)
        {
            this.Data = data;
            this.Width = Width;
            this.color = color;
        }
    }
}
