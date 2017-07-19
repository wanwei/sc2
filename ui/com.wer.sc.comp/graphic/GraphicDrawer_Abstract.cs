using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;

namespace com.wer.sc.comp.graphic
{
    /// <summary>
    /// 画图器
    /// 用于画单个图形，比如说k线图、k线图的量、k线图的macd线、分时图、分时图的量等
    /// </summary>
    public abstract class GraphicDrawer_Abstract : IGraphicDrawer
    {
        #region 通用属性

        private bool isEnable = true;
        public virtual bool IsEnable
        {
            get
            {
                return isEnable;
            }

            set
            {
                isEnable = value;
            }
        }

        private Rectangle displayRect;

        /// <summary>
        /// 设置和获取显示区域
        /// 显示区域指最终的画K线图、分时图等的区域
        /// </summary>
        public Rectangle DisplayRect
        {
            get
            {
                return displayRect;
            }

            set
            {
                displayRect = value;
            }
        }

        private Rectangle frameRect;

        /// <summary>
        /// 设置和获取边框区域
        /// 边框区域指k线图或分时图的边框
        /// </summary>
        public Rectangle FrameRect
        {
            get
            {
                return frameRect;
            }

            set
            {
                frameRect = value;
            }
        }

        private GraphicMarginInfo margin = new GraphicMarginInfo(0, 20, 60, 0);

        public GraphicMarginInfo MarginInfo
        {
            get { return margin; }
            set { margin = value; }
        }

        private GraphicPaddingInfo padding = new GraphicPaddingInfo(0, 0, 0, 0);

        public GraphicPaddingInfo Padding
        {
            get
            {
                return padding;
            }
            set
            {
                padding = value;
            }
        }
        public void SetDrawRect(Rectangle rect)
        {
            frameRect = new Rectangle(rect.X + margin.MarginLeft, rect.Y + margin.MarginTop, rect.Width - margin.MarginLeft - margin.MarginRight, rect.Height - margin.MarginTop - margin.MarginBottom);
            displayRect = new Rectangle(frameRect.X + padding.PaddingLeft, frameRect.Y + padding.PaddingTop, frameRect.Width - padding.PaddingLeft - padding.PaddingRight, frameRect.Height - padding.PaddingTop - padding.PaddingBottom);
        }

        private ColorConfig colorConfig = new ColorConfig();

        public ColorConfig ColorConfig
        {
            get
            {
                return colorConfig;
            }

            set
            {
                colorConfig = value;
            }
        }

        public Control Control
        {
            get
            {
                return control;
            }
        }

        #endregion

        #region 绑定控件

        internal Control control;

        public virtual void BindControl(Control control)
        {
            if (this.control != null)
                UnBindControl();
            this.control = control;
            control.Paint += Control_Paint;
            control.SizeChanged += Control_SizeChanged;
        }

        public virtual void UnBindControl()
        {
            this.control.Paint -= Control_Paint;
            this.control.SizeChanged -= Control_SizeChanged;
            this.control = null;
        }

        private void Control_SizeChanged(object sender, EventArgs e)
        {
            if (!isEnable)
                return;

            DrawGraphBind();
        }

        private void Control_Paint(object sender, PaintEventArgs e)
        {
            if (!isEnable)
                return;
            DrawGraphBind();
        }

        private void DrawGraphBind()
        {
            if (control == null)
                return;
            Rectangle rect = control.DisplayRectangle;
            SetDrawRect(rect);
            Paint();
        }

        #endregion

        #region 画图

        private Object drawObj = new Object();
        private Boolean drawing = false;

        public void Paint()
        {
            if (drawing)
                return;
            PaintSync();
        }

        private void PaintSync()
        {
            lock (drawObj)
            {
                try
                {
                    drawing = true;
                    BufferedGraphicsContext currentContext = BufferedGraphicsManager.Current;
                    BufferedGraphics myBuffer = currentContext.Allocate(control.CreateGraphics(), control.DisplayRectangle);
                    Graphics g = myBuffer.Graphics;

                    Paint(g);
                    if (AfterGraphicPaint != null)
                        AfterGraphicPaint(this, new GraphicRefreshArgs(g));

                    myBuffer.Render();
                    myBuffer.Dispose();
                    drawing = false;
                }
                //catch (Exception e)
                //{
                //    throw new ApplicationException(e.StackTrace, e);
                //}
                finally
                {
                    drawing = false;
                }
            }
        }

        /// <summary>
        /// 画图
        /// 抽象方法
        /// </summary>
        /// <param name="graphic"></param>
        public abstract void Paint(Graphics graphic);

        public event AfterGraphicPaintHandler AfterGraphicPaint;

        #endregion

        #region 画附加图

        //private List<PricePolyLine> polyLines = new List<PricePolyLine>();

        //public void AddPolyLine(PricePolyLine polyLine)
        //{
        //    this.polyLines.Add(polyLine);
        //}

        //public void AddPolyLines(List<PricePolyLine> polyLines)
        //{
        //    this.polyLines.AddRange(polyLines);
        //}

        //private List<PolyLineList> polyLineList = new List<PolyLineList>();

        //public void AddPolyLine(PolyLineList polyLine)
        //{
        //    this.polyLineList.Add(polyLine);
        //}

        //public void AddPolyLines(List<PolyLineList> polyLines)
        //{
        //    this.polyLineList.AddRange(polyLines);
        //}


        //public void ClearPolyLine()
        //{
        //    polyLines.Clear();
        //    polyLineList.Clear();
        //}

        //private void DrawPolyLine(Graphics g)
        //{
        //    for (int i = 0; i < polyLines.Count; i++)
        //    {
        //        DrawPolyLine(g, polyLines[i]);
        //    }
        //    for (int i = 0; i < polyLineList.Count; i++)
        //    {
        //        DrawPolyLine(g, polyLineList[i]);
        //    }
        //}

        //private void DrawPolyLine(Graphics g, PolyLineList line)
        //{
        //    int startIndex = DataProvider.StartIndex;
        //    int endIndex = DataProvider.EndIndex;

        //    Pen pen = new Pen(line.color, line.Width);
        //    List<PricePoint> data = line.Data;
        //    for (int i = 1; i < data.Count; i++)
        //    {
        //        PricePoint lastpoint = data[i - 1];
        //        PricePoint point = data[i];
        //        if (lastpoint.X >= DataProvider.StartIndex && point.X <= DataProvider.EndIndex)
        //        {

        //            float x1 = PriceMapping.CalcX(lastpoint.X);
        //            float y1 = PriceMapping.CalcY(lastpoint.Y);
        //            float x2 = PriceMapping.CalcX(point.X);
        //            float y2 = PriceMapping.CalcY(point.Y);
        //            g.DrawLine(pen, x1, y1, x2, y2);
        //        }
        //    }
        //}

        //private void DrawPolyLine(Graphics g, PricePolyLine line)
        //{
        //    int startIndex = DataProvider.StartIndex;
        //    int endIndex = DataProvider.EndIndex;

        //    Pen pen = new Pen(line.color, line.Width);
        //    float[] data = line.Data;
        //    endIndex = endIndex >= data.Length ? data.Length - 1 : endIndex;
        //    for (int i = startIndex + 1; i <= endIndex; i++)
        //    {
        //        float x1 = PriceMapping.CalcX(i - 1);
        //        float y1 = PriceMapping.CalcY(data[i - 1]);
        //        float x2 = PriceMapping.CalcX(i);
        //        float y2 = PriceMapping.CalcY(data[i]);
        //        g.DrawLine(pen, x1, y1, x2, y2);
        //    }
        //}

        //private List<PointArray> points = new List<PointArray>();

        //public void AddPoint(PointArray polyLine)
        //{
        //    points.Add(polyLine);
        //}

        //public void AddPoints(List<PointArray> polyLine)
        //{
        //    points.AddRange(polyLine);
        //}

        //private List<PointList> pointLists = new List<PointList>();

        //public void AddPoint(PointList polyLine)
        //{
        //    pointLists.Add(polyLine);
        //}

        //public void AddPoints(List<PointList> polyLine)
        //{
        //    pointLists.AddRange(polyLine);
        //}

        //public void ClearPoints()
        //{
        //    points.Clear();
        //    pointLists.Clear();
        //}

        //private void DrawPoint(Graphics g)
        //{
        //    for (int i = 0; i < points.Count; i++)
        //    {
        //        DrawPoint(g, points[i]);
        //    }
        //    for (int i = 0; i < pointLists.Count; i++)
        //    {
        //        DrawPoint(g, pointLists[i]);
        //    }
        //}

        //private void DrawPoint(Graphics g, PointArray points)
        //{
        //    int startIndex = DataProvider.StartIndex;
        //    int endIndex = DataProvider.EndIndex;

        //    float[] data = points.Data;
        //    endIndex = endIndex >= data.Length ? data.Length - 1 : endIndex;

        //    Brush b = new SolidBrush(points.color);
        //    float w = points.Width / 2;
        //    for (int i = startIndex; i <= endIndex; i++)
        //    {
        //        if (data[i] <= 0)
        //            continue;
        //        float x1 = PriceMapping.CalcX(i);
        //        float y1 = PriceMapping.CalcY(data[i]);
        //        g.FillEllipse(b, x1 - w, y1 - w, points.Width, points.Width);
        //    }
        //}

        //private void DrawPoint(Graphics g, PointList points)
        //{
        //    List<PricePoint> data = points.Data;
        //    Brush b = new SolidBrush(points.color);
        //    float w = points.Width / 2;
        //    for (int i = 0; i < data.Count; i++)
        //    {
        //        PricePoint point = data[i];
        //        if (point.X >= DataProvider.StartIndex && point.X <= DataProvider.EndIndex)
        //        {
        //            float x1 = PriceMapping.CalcX(point.X);
        //            float y1 = PriceMapping.CalcY(point.Y);
        //            g.FillEllipse(b, x1 - w, y1 - w, points.Width, points.Width);
        //        }
        //    }
        //}

        #endregion

    }    
}