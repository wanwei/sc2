using com.wer.sc.graphic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy.common.zigzag
{
    public class ZigzagDrawer
    {
        public static void DrawZigzagPoints(IShapeDrawer_PriceRect drawHelper, List<ZigzagPoint> points, int width)
        {
            DrawZigzagPoints(drawHelper, points, Color.Red, Color.Green, width);
        }

        public static void DrawZigzagPoints(IShapeDrawer_PriceRect drawHelper, List<ZigzagPoint> points, Color colorHigh, Color colorLow, int width)
        {
            for (int i = 0; i < points.Count; i++)
            {
                ZigzagPoint point = points[i];
                float price = point.IsHigh ? point.GetBar().High : point.GetBar().Low;
                Color color = point.IsHigh ? colorHigh : colorLow;
                drawHelper.DrawPoint(new graphic.shape.PriceShape_Point(point.BarPos, price, width, color));
            }
        }
    }
}
