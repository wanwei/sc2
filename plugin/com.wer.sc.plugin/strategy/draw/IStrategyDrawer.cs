﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy.draw
{
    /// <summary>
    /// 策略绘图器
    /// 该接口用于执行完策略的绘制图形
    /// </summary>
    public interface IStrategyDrawer
    {
        /// <summary>
        /// 写该图的title，写在该图的左上角
        /// 如：MA指标
        /// </summary>
        /// <param name="x">绘制文本的x坐标</param>
        /// <param name="text">绘制的文本</param>
        /// <param name="color">使用的颜色</param>
        void DrawTitle(int x, string text, Color color);

        /// <summary>
        /// 画折线
        /// 该方法会在每一个bar绘制折线
        /// 适用于一些指标的计算，如MA
        /// 如果points里面数小于0，则不画该点
        /// </summary>
        /// <param name="points"></param>
        void DrawPolyLine(List<float> points, Color color);

        /// <summary>
        /// 画折线
        /// 该方法在指定点画折线，适用于在图形上画直线
        /// 如Zigzag、通道线、趋势线等。
        /// </summary>
        /// <param name="polyLine"></param>
        /// <param name="color"></param>
        void DrawPolyLine(PricePolyLine polyLine);

        /// <summary>
        /// 画点，在图形的每一个bar上画点
        /// 如果points里的数小于0，则不画该点
        /// </summary>
        /// <param name="points"></param>
        /// <param name="color"></param>
        void DrawPoints(List<float> points, Color color);

        /// <summary>
        /// 画点，在图形的每一个bar上画点
        /// 如果points里的数小于0，则不画该点
        /// </summary>
        /// <param name="points"></param>
        /// <param name="color"></param>
        /// <param name="width"></param>
        void DrawPoints(List<float> points, Color color, int width);

        /// <summary>
        /// 画指定点
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

        /// <summary>
        /// 清空所有图形
        /// </summary>
        void ClearShapes();
    }
}