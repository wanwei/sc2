using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.comp.graphic
{
    public class PointList
    {
        public List<PricePoint> Data;

        public Color color;

        public float Width = 5;

        public PointList(List<PricePoint> data, Color color)
        {
            this.Data = data;
            this.color = color;
        }

        public PointList(List<PricePoint> data, Color color, float Width)
        {
            this.Data = data;
            this.color = color;
            this.Width = Width;
        }
    }
}
