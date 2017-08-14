using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy.draw
{
    public interface IDrawHelper
    {
        /// <summary>
        /// 画折线，如MA等
        /// </summary>
        /// <param name="line"></param>
        void DrawPolyLine(List<float> line, Color color);

        /// <summary>
        /// 画折线，如MA等
        /// </summary>
        /// <param name="polyLine"></param>
        /// <param name="color"></param>
        void DrawPolyLine(PricePolyLine polyLine);

        /// <summary>
        /// 画点
        /// </summary>
        /// <param name="points"></param>
        /// <param name="color"></param>
        void DrawPoints(List<float> points, Color color);

        void DrawPoints(List<float> points, Color color, int width);

        /// <summary>
        /// 画点
        /// </summary>
        /// <param name="points"></param>
        /// <param name="color"></param>
        void DrawPoint(PricePoint points);

        /// <summary>
        /// 写文字
        /// </summary>
        /// <param name="positions"></param>
        /// <param name="txts"></param>
        /// <param name="color"></param>
        void DrawLabels(List<float> positions, List<string> txts, Color color);

        /// <summary>
        /// 写文字
        /// </summary>
        /// <param name="label"></param>
        void DrawLabel(PriceLabel label);

        /// <summary>
        /// 画线
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="startPrice"></param>
        /// <param name="endTime"></param>
        /// <param name="endPrice"></param>
        void DrawLine(double startTime, float startPrice, double endTime, float endPrice);

        /// <summary>
        /// 画线
        /// </summary>
        /// <param name="line"></param>
        void DrawLine(PriceLine line);

        /// <summary>
        /// 刷新图像
        /// </summary>
        void Refresh();
    }
}
