using com.wer.sc.comp.graphic.utils;
using System.Collections.Generic;
using System.Drawing;
using System;
using System.Windows.Forms;

namespace com.wer.sc.comp.graphic
{
    /// <summary>
    /// 切换画图器
    /// 类似tab页控件，可自由切换显示其中一个画图器
    /// </summary>
    public class GraphicDrawer_Switch : GraphicDrawer_Abstract, ICrossHairAttachable
    {
        private List<GraphicDrawer_Abstract> drawers = new List<GraphicDrawer_Abstract>();

        public List<GraphicDrawer_Abstract> Drawers
        {
            get
            {
                return drawers;
            }
        }

        private CrossHairDataProvider crossHairDataPrivider;

        public GraphicDrawer_Switch()
        {
            this.crossHairDataPrivider = new CrossHairDataProvider_Switch(this);
        }

        private int currentIndex = 0;

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

        public GraphicDrawer_Abstract SelectedDrawer
        {
            get { return drawers[CurrentIndex]; }
        }

        public void Switch(int index)
        {
            for (int i = 0; i < drawers.Count; i++)
            {
                GraphicDrawer_Abstract drawer = Drawers[i];
                if (i == index)
                {
                    drawer.IsEnable = true;
                }
                else
                {
                    drawer.IsEnable = false;
                }
            }
            this.CurrentIndex = index;
            this.Paint();
        }

        public override void Paint(Graphics graphic)
        {
            GraphicDrawer_Abstract drawer = drawers[CurrentIndex];
            Rectangle rect = this.DisplayRect;
            drawer.SetDrawRect(rect);
            if (drawer.Control == null)
                drawer.SetControl(this.Control);
            drawer.Paint(graphic);
        }

        public CrossHairDataProvider GetCrossHairDataProvider()
        {
            return crossHairDataPrivider;
        }

        //public override void BindControl(Control control)
        //{
        //    base.BindControl(control);
        //    //this.Switch
        //    //for (int i = 0; i < drawers.Count; i++)
        //    //    drawers[i].BindControl(control);
        //}
    }

    class CrossHairDataProvider_Switch : CrossHairDataProvider
    {
        private GraphicDrawer_Switch graphicDrawer_Switch;

        public CrossHairDataProvider_Switch(GraphicDrawer_Switch graphicDrawer_Switch)
        {
            this.graphicDrawer_Switch = graphicDrawer_Switch;
            this.graphicDrawer_Switch.AfterGraphicPaint += GraphicDrawer_Switch_AfterGraphicPaint;
            //for (int i = 0; i < graphicDrawer_Switch.Drawers.Count; i++)
            //    graphicDrawer_Switch.Drawers[i].AfterGraphicPaint += GraphicDrawer_Switch_AfterGraphicPaint;
        }

        private void GraphicDrawer_Switch_AfterGraphicPaint(object sender, GraphicRefreshArgs e)
        {
            if (AfterGraphicPaint != null)
                AfterGraphicPaint(sender, e);
        }

        private CrossHairDataProvider CurrentCrossHairDataProvider
        {
            get
            {
                GraphicDrawer_Abstract drawer = this.graphicDrawer_Switch.SelectedDrawer;
                if (drawer is ICrossHairAttachable)
                    return ((ICrossHairAttachable)drawer).GetCrossHairDataProvider();
                return null;
            }
        }

        public Control Control
        {
            get
            {
                //if (CurrentCrossHairDataProvider != null)
                //    return CurrentCrossHairDataProvider.Control;
                //return null;
                return this.graphicDrawer_Switch.Control;
            }
        }

        public CrossHairDrawer CrossDrawer
        {
            get
            {
                if (CurrentCrossHairDataProvider != null)
                    return CurrentCrossHairDataProvider.CrossDrawer;
                return null;
            }

            set
            {
                for (int i = 0; i < graphicDrawer_Switch.Drawers.Count; i++)
                {
                    GraphicDrawer_Abstract drawer = this.graphicDrawer_Switch.SelectedDrawer;
                    if (drawer is ICrossHairAttachable)
                    {
                        CrossHairDataProvider provider = ((ICrossHairAttachable)drawer).GetCrossHairDataProvider();
                        provider.CrossDrawer = value;
                    }
                }
            }
        }

        public Rectangle DrawRect
        {
            get
            {
                if (CurrentCrossHairDataProvider != null)
                    return CurrentCrossHairDataProvider.DrawRect;
                return default(Rectangle);
            }
        }

        public int[] IndexRange
        {
            get
            {
                if (CurrentCrossHairDataProvider != null)
                    return CurrentCrossHairDataProvider.IndexRange;
                return null;
            }
        }

        public void DrawSelectBlock(Graphics g)
        {
            if (CurrentCrossHairDataProvider != null)
                CurrentCrossHairDataProvider.DrawSelectBlock(g);
        }

        public Pen GetPen()
        {
            if (CurrentCrossHairDataProvider != null)
                return CurrentCrossHairDataProvider.GetPen();
            return null;
        }

        public PriceGraphicMapping PriceMapping
        {
            get
            {
                if (CurrentCrossHairDataProvider != null)
                    return CurrentCrossHairDataProvider.PriceMapping;
                return null;
            }
        }

        public event AfterGraphicPaintHandler AfterGraphicPaint;

        public bool DoMoveNext()
        {
            if (CurrentCrossHairDataProvider != null)
                return CurrentCrossHairDataProvider.DoMoveNext();
            return false;
        }

        public bool DoMovePrev()
        {
            if (CurrentCrossHairDataProvider != null)
                return CurrentCrossHairDataProvider.DoMovePrev();
            return false;
        }

        public void DoRedraw()
        {
            if (CurrentCrossHairDataProvider != null)
                CurrentCrossHairDataProvider.DoRedraw();
        }

        public void DoRedraw(Graphics g, Rectangle rect)
        {
            if (CurrentCrossHairDataProvider != null)
                CurrentCrossHairDataProvider.DoRedraw(g, rect);
        }

        public void DoSelectIndexChange(int index)
        {
            if (CurrentCrossHairDataProvider != null)
                CurrentCrossHairDataProvider.DoSelectIndexChange(index);
        }

        public Point GetCrossHairPoint(int selectIndex)
        {
            if (CurrentCrossHairDataProvider != null)
                return CurrentCrossHairDataProvider.GetCrossHairPoint(selectIndex);
            return default(Point);
        }
    }
}