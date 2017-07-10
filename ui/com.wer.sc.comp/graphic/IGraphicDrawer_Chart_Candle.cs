using com.wer.sc.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.comp.graphic
{
    /// <summary>
    /// 蜡烛图数据提供接口
    /// </summary>
    public interface IGraphicDrawer_Chart_Candle
    {
        /// <summary>
        /// 得到单个合约ID
        /// </summary>
        String Code { get; }

        /// <summary>
        /// 获取数据周期
        /// </summary>
        KLinePeriod Period { get; }

        /// <summary>
        /// 获得当前画的K线数据
        /// </summary>
        /// <returns></returns>
        IKLineData GetKLineData();

        /// <summary>
        /// 得到当前的Charts
        /// </summary>
        /// <returns></returns>
        IKLineBar GetCurrentChart();

        /// <summary>
        /// 得到当前画的第一个bar在KLineData里的index
        /// </summary>
        int StartIndex { get; }

        /// <summary>
        /// 得到和设置当前画的最后一个bar在KLineData里的index
        /// </summary>
        int EndIndex { get; set; }

        /// <summary>
        /// 获取当前时间
        /// </summary>
        float CurrentTime { get; }

        /// <summary>
        /// 设置或获取屏幕上显示的K线数量
        /// </summary>
        int BlockMount { get; set; }

        /// <summary>
        /// 修改当前的K线数据
        /// </summary>
        /// <param name="klineData"></param>
        void ChangeData(IKLineData klineData);

        //event DataChangeHandler DataChange;
    }

    public delegate void DataChangeHandler(object sender, DataChangeArgs e);

    public class DataChangeArgs
    {

    }
}