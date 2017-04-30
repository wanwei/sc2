using System.Collections.Generic;

namespace com.wer.sc.data
{
    /// <summary>
    /// 该接口表示了一段完整的K线数据，可以表示股票或期货软件里图形显示的K线
    /// 
    /// K线数据可以导航，导航是通过设置BarPos完成的，通过改变BarPos，能够取得BarPos对应的柱子数据
    /// 
    /// 该接口实现了IKLineBar，用于得到当前的K线柱子数据（即BarPos指定的K线柱子）
    /// </summary>
    public interface IKLineData : IKLineBar
    {
        /// <summary>
        /// 得到K线图上
        /// 在k线模型里就是当前正在计算的位置
        /// </summary>
        int BarPos { get; set; }      
        
        /// <summary>
        /// 得到当前指定的K线柱子
        /// </summary>
        /// <returns></returns>
        IKLineBar GetCurrentBar();

        /// <summary>
        /// 得到指定的K线柱子
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        IKLineBar GetBar(int index);

        /// <summary>
        /// 得到K线周期，1分钟、5分钟、日线.....
        /// </summary>
        KLinePeriod Period { get; set; }

        /// <summary>
        /// 得到一段K线数据
        /// </summary>
        /// <param name="startPos"></param>
        /// <param name="endPos"></param>
        /// <returns></returns>
        IKLineData GetRange(int startPos, int endPos);

        /// <summary>
        /// 得到一段子K线数据，该方法和GetRange区别是生成的新k线和老K线公用K线
        /// </summary>
        /// <param name="startPos"></param>
        /// <param name="endPos"></param>
        /// <returns></returns>
        IKLineData Sub(int startPos, int endPos);

        /// <summary>
        /// 将startPos到endPos的数据聚合成一个Chart
        /// 可用于将1分钟k线数据转换成5分钟数据
        /// </summary>
        /// <param name="startPos"></param>
        /// <param name="endPos"></param>
        /// <returns></returns>
        IKLineBar GetAggrKLineBar(int startPos, int endPos);

        /// <summary>
        /// k线总长度
        /// </summary>
        int Length { get; }

        #region 完整数据信息
        /// <summary>
        /// 得到K线的所有时间列表
        /// </summary>
        IList<double> Arr_Time { get; }

        /// <summary>
        /// 得到K线的所有开始价格
        /// </summary>
        IList<float> Arr_Start { get; }

        IList<float> Arr_High { get; }

        IList<float> Arr_Low { get; }

        IList<float> Arr_End { get; }

        IList<int> Arr_Mount { get; }

        IList<float> Arr_Money { get; }

        IList<int> Arr_Hold { get; }

        /// <summary>
        /// 得到每个k线的振幅数组
        /// </summary>
        IList<float> Arr_Height { get; }

        /// <summary>
        /// 得到每个k线的振幅百分比数组
        /// </summary>
        IList<float> Arr_HeightPercent { get; }

        IList<float> Arr_BlockHigh { get; }

        IList<float> Arr_BlockLow { get; }

        IList<float> Arr_BlockHeight { get; }

        IList<float> Arr_BlockHeightPercent { get; }

        IList<float> Arr_UpPercent { get; }

        #endregion

        /// <summary>
        /// 得到指定时间对应的Index
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        int IndexOfTime(double time);

        string ToString(int index);
    }
}