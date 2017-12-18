using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace com.wer.sc.graphic.utils
{
    /// <summary>
    /// 十字线
    /// 1.点击屏幕出现十字线，再次点击会消失
    /// 2.十字线选中的block或者real可以显示详细信息
    /// 3.点左右按钮可以移动十字线
    /// 4.如果在最右端，继续右移整个k线向右移动；向左同理
    /// 5.当十字线显示的时候鼠标移动，它会跟随移动
    /// 6.当出现图形切换，如分时线切换成K线，十字线保持不动，并自动切换显示新图信息
    /// 
    /// 画图方法：
    /// 1.点击屏幕：
    /// 判断之前有没有
    /// 2.
    /// 3.点左右移动十字线
    /// </summary>
    public class CrossHairDrawer
    {
        private Control control;

        private bool showCrossHair;

        private CrossHairDataProvider provider;

        private Point crossHairPoint;

        private Point lastCrossHairPoint;

        private bool enable = true;

        public bool Enable
        {
            get
            {
                return enable;
            }

            set
            {
                enable = value;
            }
        }

        public bool ShowCrossHair
        {
            get
            {
                return showCrossHair;
            }

            set
            {
                showCrossHair = value;
                ShowCursor(!value);
            }
        }

        public Point CrossHairPoint
        {
            get
            {
                return crossHairPoint;
            }
        }

        public CrossHairDrawer()
        {
        }

        public void Bind(ICrossHairAttachable crossHairAttachable)
        {
            Bind(crossHairAttachable.GetCrossHairDataProvider());
        }

        public void Bind(CrossHairDataProvider provider)
        {
            if (this.provider != null)
                UnBind();
            this.provider = provider;
            this.provider.CrossDrawer = this;
            this.control = provider.Control;
            this.control.MouseUp += Control_MouseUp;
            this.control.MouseMove += Control_MouseMove;
            this.control.PreviewKeyDown += Control_PreviewKeyDown;
            this.provider.AfterGraphicPaint += Provider_AfterGraphicPaint;
        }

        private void Provider_AfterGraphicPaint(object sender, GraphicRefreshArgs e)
        {
            if (showCrossHair)
            {
                int index = (int)provider.PriceMapping.CalcPriceX(crossHairPoint.X);
                if (index != selectIndex)
                    provider.DoSelectIndexChange(index);
            }
            //ChangeCrossPoint(crossHairPoint);
            DrawGraphic(e.Graphic);
        }

        private void Control_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (!enable)
                return;
            if (e.KeyCode == Keys.Escape)
            {
                this.ShowCrossHair = false;
                provider.DoRedraw();
                DrawGraphic();
                return;
            }

            if (e.KeyCode != Keys.Left && e.KeyCode != Keys.Right)
                return;
            if (e.KeyCode == Keys.Left)
            {
                if (!ShowCrossHair)
                {
                    this.selectIndex = (int)provider.PriceMapping.PriceRect.PriceRight;
                    this.ShowCrossHair = true;
                }
                else
                {
                    if (this.selectIndex == 0)
                        return;
                    this.selectIndex--;
                    if (this.selectIndex < provider.PriceMapping.PriceRect.PriceLeft)
                    {
                        bool isSuccess = provider.DoMovePrev();
                        if (!isSuccess)
                        {
                            this.selectIndex++;
                            return;
                        }
                    }
                }
            }
            if (e.KeyCode == Keys.Right)
            {
                if (!ShowCrossHair)
                {
                    this.selectIndex = (int)provider.PriceMapping.PriceRect.PriceLeft + 1;
                    this.ShowCrossHair = true;
                }
                else
                {
                    this.selectIndex++;
                    if (this.selectIndex > provider.PriceMapping.PriceRect.PriceRight)
                    {
                        bool isSuccess = provider.DoMoveNext();
                        if (!isSuccess)
                        {
                            this.selectIndex--;
                            return;
                        }
                    }
                }
            }
            provider.DoSelectIndexChange(this.selectIndex);
            crossHairPoint = provider.GetCrossHairPoint(this.selectIndex);
            provider.DoRedraw();
            DrawGraphic();
            //}
        }

        public void UnBind()
        {
            if (this.provider == null)
                return;
            this.control.MouseUp -= Control_MouseUp;
            this.control.MouseMove -= Control_MouseMove;
            this.control = null;
        }

        /// <summary>
        /// 设置鼠标状态的计数器（非状态）
        /// </summary>
        /// <param name="bShow">状态</param>
        /// <returns>状态技术</returns>
        [DllImport("user32.dll", EntryPoint = "ShowCursor", CharSet = CharSet.Auto)]
        public static extern int ShowCursor(bool bShow);

        private void Control_MouseUp(object sender, MouseEventArgs e)
        {
            if (!enable)
                return;
            if (e.Button == MouseButtons.Left)
            {
                Point p = e.Location;
                Rectangle rec = provider.DrawRect;
                if (!rec.Contains(p))
                {
                    return;
                }

                this.ShowCrossHair = !ShowCrossHair;
                if (!this.ShowCrossHair)
                {
                    this.selectIndex = -1;
                    provider.DoSelectIndexChange(selectIndex);
                    RemoveLastCrossPoint();
                    provider.DoRedraw();
                    return;
                }
                ChangeCrossPoint(GetMousePosition());
                //DrawGraphic();
            }
        }

        private void DoRedraw(Rectangle rect)
        {
            BufferedGraphicsContext currentContext = BufferedGraphicsManager.Current;
            BufferedGraphics myBuffer = currentContext.Allocate(control.CreateGraphics(), rect);
            Graphics g = myBuffer.Graphics;

            provider.DoRedraw(g, rect);

            myBuffer.Render();
            myBuffer.Dispose();
            currentContext.Dispose();
        }

        private DateTime lastMouseMoveTime = DateTime.Now;
        private int selectIndex;

        private void Control_MouseMove(object sender, MouseEventArgs e)
        {
            if (!enable)
                return;
            if (ShowCrossHair)
            {
                if (lastMouseMoveTime.AddTicks(50) < DateTime.Now)
                {
                    ChangeCrossPoint(GetMousePosition());
                }
            }
            else
            {
                this.selectIndex = -1;
                provider.DoSelectIndexChange(this.selectIndex);
            }
        }

        public void ChangeCrossPoint(Point point)
        {
            this.crossHairPoint = point;
            if (!provider.DrawRect.Contains(point.X, point.Y))
            {
                ShowCrossHair = false;
                provider.DoSelectIndexChange(-1);
                lastMouseMoveTime = DateTime.Now;
                provider.DoRedraw();
                return;
            }
            int index = (int)Math.Round(provider.PriceMapping.CalcPriceX(point.X));
            int[] range = provider.IndexRange;
            if (index >= range[0] && index <= range[1])
            {
                if (this.selectIndex != index)
                {
                    this.selectIndex = index;
                    provider.DoSelectIndexChange(this.selectIndex);
                }
            }
            //如果selectIndex<0，说明鼠标移出了该控件，此时应该不需要显示十字线了
            if (this.selectIndex < 0)
                ShowCrossHair = false;
            lastMouseMoveTime = DateTime.Now;
            provider.DoRedraw();
            DrawGraphic();
        }

        private Point GetMousePosition()
        {
            int x = control.PointToClient(Control.MousePosition).X;
            int y = control.PointToClient(Control.MousePosition).Y;
            return new Point(x, y);
        }

        public void DrawGraphic()
        {
            this.DrawGraphic(control.CreateGraphics());
        }

        public void DrawGraphic(Graphics g)
        {
            if (provider == null)
                return;
            if (!enable)
                return;
            if (ShowCrossHair)
            {
                Pen pen = provider.GetPen();
                if (pen == null)
                    return;
                Rectangle rec = provider.DrawRect;
                g.DrawLine(pen, new Point(crossHairPoint.X, rec.Top), new Point(crossHairPoint.X, rec.Bottom));
                g.DrawLine(pen, new Point(rec.X, crossHairPoint.Y), new Point(rec.Right, crossHairPoint.Y));

                lastCrossHairPoint = crossHairPoint;
                DrawCrossHairsPrice(g, crossHairPoint, rec);

                provider.DrawSelectBlock(g);
            }
        }

        private void DrawCross()
        {
            if (lastCrossHairPoint.X >= 0)
                RemoveLastCrossPoint();
            Graphics g = control.CreateGraphics();
            DrawCross(g);
        }

        private void DrawCross(Graphics g)
        {
            if (!showCrossHair)
                return;
            Rectangle rect = provider.DrawRect;
            Pen pen = new Pen(Color.White);
            g.DrawLine(pen, new Point(rect.X, crossHairPoint.Y), new Point(rect.Right / 2 - 2, crossHairPoint.Y));
            g.DrawLine(pen, new Point(crossHairPoint.X, rect.Top), new Point(crossHairPoint.X, rect.Bottom));
            lastCrossHairPoint = crossHairPoint;
        }

        private void RemoveLastCrossPoint()
        {
            RemoveLastCrossPoint(control.CreateGraphics(), lastCrossHairPoint);
            lastCrossHairPoint.X = -1;
            lastCrossHairPoint.Y = -1;
        }

        private void RemoveLastCrossPoint(Graphics g, Point p)
        {
            RemoveHorzonalLine(g, p);
            RemoveVerticalLine(g, p);
        }

        private void RemoveHorzonalLine(Graphics g, Point point)
        {
            Rectangle rect = provider.DrawRect;
            Rectangle drawRect = new Rectangle(rect.X, point.Y - 2, rect.Width, 4);

            BufferedGraphicsContext currentContext = BufferedGraphicsManager.Current;
            BufferedGraphics myBuffer = currentContext.Allocate(g, drawRect);
            Graphics graphic = myBuffer.Graphics;

            provider.DoRedraw(graphic, drawRect);

            myBuffer.Render();
            myBuffer.Dispose();
            currentContext.Dispose();
        }

        private void RemoveVerticalLine(Graphics g, Point point)
        {
            Rectangle rect = provider.DrawRect;
            Rectangle drawRect = new Rectangle(point.X - 2, rect.Y, 4, rect.Height);

            BufferedGraphicsContext currentContext = BufferedGraphicsManager.Current;
            BufferedGraphics myBuffer = currentContext.Allocate(g, drawRect);
            Graphics graphic = myBuffer.Graphics;

            provider.DoRedraw(graphic, drawRect);

            myBuffer.Render();
            myBuffer.Dispose();
            currentContext.Dispose();
        }

        private void DrawCrossHairsPrice(Graphics g, Point point, Rectangle rec)
        {

        }
    }

    /// <summary>
    /// 能画十字线的控件
    /// </summary>
    public interface ICrossHairAttachable
    {
        /// <summary>
        /// 得到十字线获取器
        /// </summary>
        /// <returns></returns>
        CrossHairDataProvider GetCrossHairDataProvider();
    }

    /// <summary>
    /// 交互：
    /// 1.能够
    /// 2.
    /// </summary>
    public interface CrossHairDataProvider
    {
        /// <summary>
        /// 设置和得到十字线画线控件
        /// 该属性的目的是将十字线绑定到目标控件上
        /// </summary>
        CrossHairDrawer CrossDrawer { get; set; }

        /// <summary>
        /// 得到要绘制十字线的控件
        /// </summary>
        Control Control { get; }

        /// <summary>
        /// 得到绘制十字线的边界
        /// </summary>
        Rectangle DrawRect { get; }

        /// <summary>
        /// 得到绘制十字线的画笔
        /// </summary>
        Pen GetPen();

        /// <summary>
        /// 画图事件
        /// 十字线画图器会侦听该事件，在画完控件后再画十字线
        /// </summary>
        event AfterGraphicPaintHandler AfterGraphicPaint;

        /// <summary>
        /// 获得图形价格和图形的映射类
        /// </summary>
        PriceGraphicMapping PriceMapping { get; }

        /// <summary>
        /// 得到每个index的交叉点位置
        /// </summary>
        /// <param name="selectIndex"></param>
        /// <returns></returns>
        Point GetCrossHairPoint(int selectIndex);

        /// <summary>
        /// 绘制选中K线块
        /// </summary>
        /// <param name="g"></param>
        void DrawSelectBlock(Graphics g);

        /// <summary>
        /// 执行事件
        /// </summary>
        /// <param name="index"></param>
        void DoSelectIndexChange(int index);

        /// <summary>
        /// 重新绘制整个图形
        /// </summary>
        void DoRedraw();

        /// <summary>
        /// 重新绘制，只绘制区块内的图
        /// </summary>
        /// <param name="rect"></param>
        void DoRedraw(Graphics g, Rectangle rect);

        bool DoMoveNext();

        bool DoMovePrev();

        int[] IndexRange { get; }
    }
}