using System.Collections.Generic;
using System.Drawing;

namespace com.wer.sc.comp.graphic
{
    /// <summary>
    /// 切换画图器
    /// 类似tab页控件，可自由切换显示其中一个画图器
    /// </summary>
    public class GraphicDrawer_Switch : GraphicDrawer_Abstract
    {
        private List<GraphicDrawer_Abstract> drawers = new List<GraphicDrawer_Abstract>();

        public List<GraphicDrawer_Abstract> Drawers
        {
            get
            {
                return drawers;
            }
        }
        private int currentIndex;

        public int CurrentIndex
        {
            get
            {
                return currentIndex;
            }

            set
            {
                currentIndex = value;
            }
        }

        public void Switch(int index)
        {
            for (int i = 0; i < drawers.Count; i++)
            {
                if (i == index)
                    Drawers[i].IsEnable = true;
                else
                    Drawers[i].IsEnable = false;
            }
            this.CurrentIndex = index;
        }

        public override void DrawGraph(Graphics graphic)
        {
            GraphicDrawer_Abstract drawer = drawers[CurrentIndex];
            Rectangle rect = this.DisplayRect;
            drawer.SetDrawRect(rect);
            drawer.DrawGraph(graphic);
        }
    }
}