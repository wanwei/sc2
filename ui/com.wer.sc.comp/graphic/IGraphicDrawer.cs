using com.wer.sc.comp.graphic.shape;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace com.wer.sc.comp.graphic
{
    /// <summary>
    /// 绘图接口
    /// </summary>
    public interface IGraphicDrawer
    {
        /// <summary>
        /// 设置或获取当前画图是否在可用状态
        /// </summary>
        bool IsEnable { get; set; }

        /// <summary>
        /// 获取具体图像的绘制区域
        /// 显示区域指最终的画K线图、分时图等的区域
        /// </summary>
        Rectangle DisplayRect { get; }

        /// <summary>
        /// 设置和获取整幅图像的绘制区域
        /// 该区域包括内边框区域
        /// </summary>
        Rectangle FrameRect { get; set; }

        /// <summary>
        /// 设置或获取该图像的外边距
        /// </summary>
        GraphicMarginInfo MarginInfo { get; set; }

        /// <summary>
        /// 设置或获取该图像的内边距
        /// </summary>
        GraphicPaddingInfo Padding { get; set; }

        /// <summary>
        /// 绑定控件
        /// </summary>
        /// <param name="control"></param>
        void BindControl(Control control);

        /// <summary>
        /// 解绑控件
        /// </summary>
        void UnBindControl();

        /// <summary>
        /// 将图像绘制到屏幕上
        /// </summary>
        void Paint();

        /// <summary>
        /// 图像绘制完事件
        /// </summary>
        event AfterGraphicPaintHandler AfterGraphicPaint;

        void DrawShape(IShape shape);
    }

    public delegate void AfterGraphicPaintHandler(object sender, GraphicRefreshArgs e);

    public class GraphicRefreshArgs
    {
        public Graphics Graphic;

        public GraphicRefreshArgs(Graphics Graphic)
        {
            this.Graphic = Graphic;
        }
    }
}