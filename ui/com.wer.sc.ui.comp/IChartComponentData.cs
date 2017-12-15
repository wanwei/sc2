using com.wer.sc.data;
using com.wer.sc.data.datapackage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.ui.comp
{
    /// <summary>
    /// 组件的数据信息
    /// </summary>
    public interface IChartComponentData
    {      
        /// <summary>
        /// 得到和设置当前组件的Code
        /// </summary>
        string Code { get; set; }

        /// <summary>
        /// 得到当前时间
        /// </summary>
        double Time { get; set; }

        /// <summary>
        /// 得到当前显示的图形
        /// </summary>
        ChartType ChartType { get; set; }

        /// <summary>
        /// 当前显示的K线周期
        /// </summary>
        KLinePeriod KLinePeriod { get; set; }

        /// <summary>
        /// 第一个显示的Index
        /// 如果是K线，是第一个显示的K线index
        /// 如果是分时线，显示0
        /// </summary>
        int ShowStartIndex { get; set; }

        /// <summary>
        /// 最后一个显示的Index
        /// 如果是K线，是最后一个显示的K线index
        /// 如果是分时线，则返回显示的最后一个分时线Index
        /// </summary>
        int ShowEndIndex { get; set; }

        /// <summary>
        /// 当前十字星选中的时间，不显示十字星的情况下返回-1
        /// </summary>
        int CrossSelectedIndex { get; set; }

        /// <summary>
        /// 检查数据
        /// </summary>
        /// <returns></returns>
        bool CheckData();
    }
}