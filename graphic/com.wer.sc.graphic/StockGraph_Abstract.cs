using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace com.wer.sc.graphic
{
    /// <summary>
    /// 股票画图抽象类
    /// 使用者要画k线图或分时图等都需要从该抽象类继承
    /// 该类是完整的k线或分时图，包括线部分和量能部分
    /// </summary>
    public abstract class StockGraph_Abstract
    {
        protected ColorConfig config;
        protected IGraphicData_Candle data;

        public StockGraph_Abstract(IGraphicData_Candle data, ColorConfig config)
        {
            this.config = config;
            //this.displayRectangle = config.DisplayRectangle;
        }

        private List<GraphicDrawer_Abstract> drawers = new List<GraphicDrawer_Abstract>();

        public List<GraphicDrawer_Abstract> Drawers
        {
            get { return drawers; }
        }

        public void AddGraphicDrawer(GraphicDrawer_Abstract drawer)
        {
            this.drawers.Add(drawer);
        }

        public ColorConfig ColorConfig
        {
            get { return config; }
        }

        public IGraphicData_Candle DataProvider
        {
            get { return data; }
        }

        private Rectangle displayRectangle;

        /// <summary>
        /// 得到图形的显示区域
        /// </summary>
        public Rectangle DisplayRectangle
        {
            get
            {
                return displayRectangle;
            }
        }

        /// <summary>
        /// 得到去掉外边距的区域
        /// </summary>
        public Rectangle ShowRectangle
        {
            get
            {
                GraphicPaddingInfo paddingInfo = PaddingInfo;
                Rectangle rect = new Rectangle();
                rect.X = DisplayRectangle.X + paddingInfo.PaddingLeft;
                rect.Y = DisplayRectangle.Y + paddingInfo.PaddingTop;
                rect.Width = DisplayRectangle.Width - paddingInfo.PaddingLeft - paddingInfo.PaddingRight;
                rect.Height = DisplayRectangle.Height - paddingInfo.PaddingTop - paddingInfo.PaddingBottom;
                return rect;
            }
        }

        /// <summary>
        /// 得到去掉内边距的区域
        /// </summary>
        public Rectangle DrawRectangle
        {
            get
            {
                Rectangle rect = new Rectangle();
                rect.X = ShowRectangle.X + MarginLeft;
                rect.Y = ShowRectangle.Y;
                rect.Width = ShowRectangle.Width - MarginLeft - MarginRight;
                rect.Height = ShowRectangle.Height;
                return rect;
            }
        }

        #region 供重载的事件

        /// <summary>
        /// 股票绘图控件变化事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void StockGraphic_SizeChanged(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 股票绘图控件鼠标按下事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void StockGraphic_MouseDown(object sender, MouseEventArgs e)
        {
        }

        /// <summary>
        /// 股票绘图控件鼠标移动事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void StockGraphic_MouseMove(object sender, MouseEventArgs e)
        {
        }

        /// <summary>
        /// 股票绘图控件键盘按下事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void StockGraphic_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
        }

        #endregion

        #region 父类实现，子类可重载的属性和方法

        /// <summary>
        /// 绘制整个图形，包括画坐标
        /// </summary>
        /// <param name="g"></param>
        public virtual void DrawGraph(System.Drawing.Graphics g)
        {
            if (this.DataProvider == null)
                return;

            //画整个外边框
            g.DrawRectangle(ColorConfig.Pen_FrameLine, ShowRectangle);
            //画刻度
            DrawScale(g);
            //画图形
            for (int i = 0; i < drawers.Count; i++)
            {
                GraphicDrawer_Abstract drawer = drawers[i];
                //drawer.DrawFrame(g);
                drawer.Paint(g);
            }
        }

        public const int DEFAULT_PADDING_LEFT = 60;
        public const int DEFAULT_PADDING_TOP = 0;
        public const int DEFAULT_PADDING_RIGHT = 40;
        public const int DEFAULT_PADDING_BOTTOM = 20;

        private GraphicPaddingInfo defaultPadding = new GraphicPaddingInfo(DEFAULT_PADDING_LEFT, DEFAULT_PADDING_TOP, DEFAULT_PADDING_RIGHT, DEFAULT_PADDING_BOTTOM);

        /// <summary>
        /// 该图的外边距，提供了默认实现
        /// </summary>
        public GraphicPaddingInfo PaddingInfo
        {
            get { return defaultPadding; }
        }

        private int marginLeft = 0;

        public virtual int MarginLeft
        {
            get { return marginLeft; }
        }

        private int marginRight = 0;

        public virtual int MarginRight
        {
            get { return marginRight; }
        }

        #endregion

        #region 父类提供给子类的工具方法

        /// <summary>
        /// 得到块的数量
        /// </summary>
        /// <returns></returns>
        public int BlockCount
        {
            get
            {
                if (BlockOrLine)
                    return (int)(DrawRectangle.Width / BlockWidth);
                return (int)(DrawRectangle.Width / BlockWidth + 1);
            }
        }

        /// <summary>
        /// 得到index所在的位置
        /// 如果是柱状图得到块的中间位置
        /// 如果是线图获得连接线的点
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public int CalcX(int index)
        {
            double blockWidth = BlockWidth;
            if (BlockOrLine)
                return (int)(blockWidth * index + blockWidth / 2) + DrawRectangle.X;
            return (int)(blockWidth * index) + DrawRectangle.X;
        }

        /// <summary>
        /// 根据点的位置计算被选中的chart的index
        /// 该方法被用来确定十字线鼠标滑动时选中的block
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public int CalcSelectIndex(Point point)
        {
            double distance = point.X - DrawRectangle.X;
            if (BlockOrLine)
            {
                return (int)(distance / BlockWidth);
            }
            else
            {
                return (int)((distance - BlockWidth / 2) / BlockWidth);
            }
        }

        /// <summary>
        /// 计算图中最后一个能被选中的index
        /// </summary>
        /// <returns></returns>
        public int GetLastSelectIndex()
        {
            return BlockCount - 1;
        }

        #endregion

        #region 子类必须重载提供的信息

        /// <summary>
        /// 提供每个块的宽度
        /// </summary>
        public abstract double BlockWidth
        {
            get;
        }

        /// <summary>
        /// 得到是画块状图还是画线图
        /// </summary>
        public abstract bool BlockOrLine
        {
            get;
        }

        #endregion

        #region 子类必须重载实现的方法

        /// <summary>
        /// 得到时间对应的index
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public abstract int GetIndex(String time);

        /// <summary>
        /// 看指定时间是否在图中显示
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public abstract bool isInGraphic(String time);

        /// <summary>
        /// 画刻度
        /// </summary>
        /// <param name="g"></param>
        public abstract void DrawScale(Graphics g);

        /// <summary>
        /// 得到指定的块信息
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        //public abstract SelectedPointInfo GetBlockInfo(int index);

        /// <summary>
        /// 向前进period个周期
        /// 该方法用于在图形上键盘左右按钮点击事件，用来看k线的前后图形
        /// 但是该方法向前时不能超过CurrentInfo的当前时间
        /// </summary>
        /// <param name="period"></param>
        public abstract void nextChart(int period);

        /// <summary>
        /// 该方法会用最新的CurrentInfo数据重新刷新图形
        /// 用在模拟历史数据时的时间前进
        /// </summary>
        public abstract void RefreshGraph();

        /// <summary>
        /// 得到十字星对应的点
        /// </summary>
        /// <param name="selectIndex"></param>
        /// <returns></returns>
        public abstract Point CalcCrossHairPoint(int selectIndex);
        #endregion
    }
}