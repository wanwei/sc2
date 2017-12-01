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
    public interface IChartComponentInfo
    {
        /// <summary>
        /// 得到当前显示图形用到的数据包
        /// </summary>
        IDataPackage_Code DataPackage { get; }

        /// <summary>
        /// 得到当前Index
        /// </summary>
        int CurrentIndex { get; }

        /// <summary>
        /// 得到当前时间
        /// </summary>
        double CurrentTime { get; }

        /// <summary>
        /// 得到当前显示的图形
        /// </summary>
        ChartType ChartType { get; }

        /// <summary>
        /// 当前显示的K线周期
        /// </summary>
        KLinePeriod KLinePeriod { get; }

        /// <summary>
        /// 第一个显示的Index
        /// 如果是K线，是第一个显示的K线index
        /// 如果是分时线，显示0
        /// </summary>
        int ShowStartIndex { get; }

        /// <summary>
        /// 最后一个显示的Index
        /// 如果是K线，是最后一个显示的K线index
        /// 如果是分时线，则返回显示的最后一个分时线Index
        /// </summary>
        int ShowEndIndex { get; }

        /// <summary>
        /// 当前十字星选中的时间，不显示十字星的情况下返回-1
        /// </summary>
        int CrossSelectedIndex { get; }

        /// <summary>
        /// 检查数据
        /// </summary>
        /// <returns></returns>
        bool CheckData();
    }
}