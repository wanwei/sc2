using com.wer.sc.data;
using com.wer.sc.data.reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.forward
{
    /// <summary>
    /// 单支合约的历史数据前进器
    /// </summary>
    public interface IHistoryDataForward_Code : IRealTimeDataReader
    {
        /// <summary>
        /// 得到前进时的主K线，如果是以tick前进，则返回空
        /// </summary>
        /// <returns></returns>
        IKLineData GetKLineData();

        /// <summary>
        /// 前进
        /// </summary>
        /// <returns></returns>
        bool Forward();

        /// <summary>
        /// 导航
        /// </summary>
        /// <param name="time"></param>
        void NavigateTo(double time);

        /// <summary>
        /// 自动前进
        /// </summary>
        void Play();

        /// <summary>
        /// 停止自动前进
        /// </summary>
        void Pause();

        /// <summary>
        /// 每次前进的周期
        /// </summary>
        ForwardPeriod ForwardPeriod { get; }

        /// <summary>
        /// 是否不能再前进了
        /// </summary>
        bool IsEnd { get; }

        /// <summary>
        /// 是否到一天的结束
        /// </summary>
        bool IsDayEnd { get; }

        /// <summary>
        /// 指定周期的K线是否正好走完
        /// </summary>
        /// <param name="klinePeriod"></param>
        /// <returns></returns>
        bool IsPeriodEnd(KLinePeriod klinePeriod);

        /// <summary>
        /// 得到起始日期
        /// </summary>
        int StartDate { get; }

        /// <summary>
        /// 得到结束日期
        /// </summary>
        int EndDate { get; }

        /// <summary>
        /// 
        /// </summary>
        event DelegateOnTick OnTick;

        /// <summary>
        /// 
        /// </summary>
        event DelegateOnBar OnBar;
    }

    /// <summary>
    /// 每前进一个tick执行该委托
    /// 如前进器是以tick为周期前进，每接收到一个新tick，触发该委托
    /// 如果前进器是以K线为周期前进，则不触发该委托
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="tickData"></param>
    /// <param name="index"></param>
    public delegate void DelegateOnTick(object sender, ITickData tickData, int index);

    /// <summary>
    /// 每前进一个K线的bar执行该委托
    /// 如果前进器是以tick为周期前进，则每到下一分钟开始触发该委托
    /// 如果前进器是以K线为周期前进
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="klineData"></param>
    /// <param name="index"></param>
    public delegate void DelegateOnBar(object sender, IKLineData klineData, int index);
}
