﻿using System;
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
    public abstract class GraphicDrawer_Abstract
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
            DrawGraph();
        }

        #endregion

        #region 画图

        private Object drawObj = new Object();
        private Boolean drawing = false;

        public void DrawGraph()
        {
            if (drawing)
                return;
            DrawGraphSync();
        }

        private void DrawGraphSync()
        {
            lock (drawObj)
            {
                try
                {
                    drawing = true;
                    BufferedGraphicsContext currentContext = BufferedGraphicsManager.Current;
                    BufferedGraphics myBuffer = currentContext.Allocate(control.CreateGraphics(), control.DisplayRectangle);
                    Graphics g = myBuffer.Graphics;

                    DrawGraph(g);
                    if (AfterGraphicDraw != null)
                        AfterGraphicDraw(this, new GraphicRefreshArgs(g));

                    myBuffer.Render();
                    myBuffer.Dispose();
                    drawing = false;
                }
                catch (Exception e)
                {                    
                    throw new ApplicationException(e.StackTrace, e);
                }
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
        public abstract void DrawGraph(Graphics graphic);

        public event AfterGraphicDrawHandler AfterGraphicDraw;

        #endregion
    }

    public delegate void AfterGraphicDrawHandler(object sender, GraphicRefreshArgs e);

    public class GraphicRefreshArgs
    {
        public Graphics Graphic;

        public GraphicRefreshArgs(Graphics Graphic)
        {
            this.Graphic = Graphic;
        }
    }
}