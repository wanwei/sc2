using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.comp.graphic
{
    /// <summary>
    /// 组合画图器，可以将多个图横向或纵向的依次画出来
    /// </summary>
    public class GraphicDrawer_Compound : GraphicDrawer_Abstract
    {
        private List<GraphicDrawer_Abstract> drawers = new List<GraphicDrawer_Abstract>();

        private List<float> drawerWidths = new List<float>();

        private List<bool> isFixs = new List<bool>();

        private bool isVertical = true;

        public bool IsVertical
        {
            get
            {
                return isVertical;
            }

            set
            {
                isVertical = value;
            }
        }

        public void AddGraph(GraphicDrawer_Abstract drawer, float percent)
        {
            AddGraph(drawer, percent, false);
        }

        public void AddGraph(GraphicDrawer_Abstract drawer, float percent, bool isFix)
        {
            drawers.Add(drawer);
            drawerWidths.Add(percent);
            isFixs.Add(isFix);
        }

        public override void DrawGraph(Graphics graphic)
        {
            float sumPercent = 0;
            float sumFix = 0;
            for (int i = 0; i < drawerWidths.Count; i++)
            {
                bool isFix = isFixs[i];
                if (isFix)
                    sumFix += drawerWidths[i];
                else
                    sumPercent += drawerWidths[i];
            }

            if (IsVertical)
                DrawVertical(graphic, sumPercent, sumFix);
            else
                DrawHorizonal(graphic, sumPercent, sumFix);
        }

        private void DrawVertical(Graphics graphic, float sumPercent, float sumFix)
        {
            int x = DisplayRect.X;
            int y = DisplayRect.Y;
            int width = DisplayRect.Width;
            int height = DisplayRect.Height - (int)sumFix;
            for (int i = 0; i < drawers.Count; i++)
            {
                GraphicDrawer_Abstract drawer = drawers[i];
                float percent = drawerWidths[i];
                bool isFix = isFixs[i];
                int currentheight;
                if (isFix)
                    currentheight = (int)percent;
                else
                    currentheight = (int)(percent / sumPercent * DisplayRect.Height);
                drawer.SetDrawRect(new Rectangle(x, y, width, currentheight));
                y += currentheight;
                drawer.DrawGraph(graphic);
            }
        }

        private void DrawHorizonal(Graphics graphic, float sumPercent, float sumFix)
        {
            int x = DisplayRect.X;
            int y = DisplayRect.Y;
            int height = DisplayRect.Height;
            int width = DisplayRect.Width - (int)sumFix;
            for (int i = 0; i < drawers.Count; i++)
            {
                GraphicDrawer_Abstract drawer = drawers[i];
                float percent = drawerWidths[i];
                bool isFix = isFixs[i];
                int currentWidth;
                if (isFix)
                    currentWidth = (int)percent;
                else
                    currentWidth = (int)(percent / sumPercent * DisplayRect.Width);
                drawer.SetDrawRect(new Rectangle(x, y, currentWidth, height));
                x += currentWidth;
                drawer.DrawGraph(graphic);
            }
        }
    }
}