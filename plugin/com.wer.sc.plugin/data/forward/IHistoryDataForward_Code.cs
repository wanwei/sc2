using com.wer.sc.data;
using com.wer.sc.data.datapackage;
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
    public interface IHistoryDataForward_Code : IRealTimeDataReader_Code
    {
        /// <summary>
        /// 跳转到指定时间
        /// </summary>
        /// <param name="time"></param>
        void NavigateTo(double time);

        /// <summary>
        /// 前进
        /// </summary>
        /// <returns></returns>
        bool Forward();

        /// <summary>
        /// 得到前进时的主K线，如果是以tick前进，则返回空
        /// </summary>
        /// <returns></returns>
        IKLineData GetKLineData();

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
        /// 得到起始日期
        /// </summary>
        int StartDate { get; }

        /// <summary>
        /// 得到结束日期
        /// </summary>
        int EndDate { get; }

        /// <summary>
        /// 得到数据包
        /// </summary>
        IDataPackage_Code DataPackage { get; }

        #region 当前的前进信息

        /// <summary>
        /// 是否不能再前进了
        /// </summary>
        bool IsEnd { get; }

        /// <summary>
        /// 是否是一天的开始
        /// </summary>
        bool IsDayStart { get; }

        /// <summary>
        /// 是否是一天的结束
        /// </summary>
        bool IsDayEnd { get; }

        /// <summary>
        /// 是否是一个交易时段的开始
        /// </summary>
        bool IsTradingTimeStart { get; }

        /// <summary>
        /// 是否是一段交易时间的结束
        /// </summary>
        bool IsTradingTimeEnd { get; }

        #endregion

        /// <summary>
        /// 前进器接收到一个新tick时，触发该事件
        /// 只有当前进器以tick为周期前进时才执行该事件
        /// </summary>
        event DelegateOnTick OnTick;

        /// <summary>
        /// 前进器在主K线的bar完全生成后后执行该事件
        /// 在前进器是tick或K线周期都会执行陔事件，但触发时机略有不同
        /// 注意如果前进器是以tick为周期前进，则到下一个bar生成的那一刻该委托才会触发
        /// 如果前进器是以K线为周期前进，那么在bar结束的时候触发该委托。
        /// 
        /// 如果在前进器有多个K线，那么onbar的时候只会执行那么bar完全生成的K线
        /// 比如现在时间是20170904.090500，那么该事件只会触发1分钟和5分钟的K线返回，15分钟不会触发
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
    /// 在K线的bar完全生成后后执行该委托
    /// 注意如果前进器是以tick为周期前进，则到下一个bar生成的那一刻该委托才会触发
    /// 如果前进器是以K线为周期前进，那么在bar结束的时候触发该委托。
    /// 
    /// 如果在前进器有多个K线，那么onbar的时候只会执行那么bar完全生成的K线
    /// 比如现在时间是20170904.090500，那么该事件只会触发1分钟和5分钟的K线返回，15分钟不会触发
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="arguments"></param>
    public delegate void DelegateOnBar(object sender, ForwardOnBarArgument arguments);

    /// <summary>
    /// 
    /// </summary>
    public class ForwardOnBarArgument
    {
        private IList<ForwardOnbar_Info> klineData_BarFinished;

        public ForwardOnBarArgument(IList<ForwardOnbar_Info> barFinishedInfo)
        {
            this.klineData_BarFinished = barFinishedInfo;
        }

        public IList<ForwardOnbar_Info> ForwardOnBar_Infos
        {
            get
            {
                return klineData_BarFinished;
            }
        }

        public ForwardOnbar_Info MainForwardOnBar_Info
        {
            get { return klineData_BarFinished[0]; }
        }
    }

    public class ForwardOnbar_Info
    {
        /// <summary>
        /// OnBar事件执行时完成一个bar的k线数据
        /// </summary>
        public IKLineData KlineData;

        /// <summary>
        /// 前进器前进完成的Bar
        /// </summary>
        public int FinishedBarPos;

        public KLinePeriod KLinePeriod
        {
            get
            {
                return KlineData.Period;
            }
        }

        public IKLineBar KLineBar
        {
            get
            {
                return KlineData.GetBar(FinishedBarPos);
            }
        }

        public ForwardOnbar_Info(IKLineData klineData, int finishedBarPos)
        {
            this.KlineData = klineData;
            this.FinishedBarPos = finishedBarPos;
        }
    }
}