using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.comp.graphic.shape
{
    public class ShapePainter
    {
        private ShapePainter_Label shapePainter_Label = new ShapePainter_Label();

        public void Paint(Graphics g, Rectangle displayRect, IShape shape)
        {
            switch (shape.ShapeType)
            {
                case ShapeType.Label:
                    shapePainter_Label.Paint(g, displayRect, shape);
                    break;
            }
        }
    }

    interface IShapePainter
    {
        void Paint(Graphics g, Rectangle displayRect, IShape shape);
    }

    class ShapePainter_Label : IShapePainter
    {
        public void Paint(Graphics g, Rectangle displayRect, IShape shape)
        {
            Shape_Label label = (Shape_Label)shape;

            float x1 = label.X + displayRect.X;
            float y1 = label.Y + displayRect.Y;
            Font font = label.Font != null ? label.Font : new Font("宋体", 14.2f, FontStyle.Regular);
            StringFormat sf = label.StringFormat != null ? label.StringFormat : new StringFormat(StringFormatFlags.LineLimit);
            g.DrawString(label.Text, font, new SolidBrush(label.Color), new PointF(x1, y1), sf);
        }
    }
}
