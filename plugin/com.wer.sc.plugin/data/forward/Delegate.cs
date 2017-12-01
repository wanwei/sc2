using com.wer.sc.data.reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.forward
{

    /// <summary>
    /// 每前进一个tick执行该委托
    /// 如前进器是以tick为周期前进，每接收到一个新tick，触发该委托
    /// 如果前进器是以K线为周期前进，则不触发该委托
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="argument"></param>
    public delegate void DelegateOnTick(object sender, IForwardOnTickArgument argument);

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
    public delegate void DelegateOnBar(object sender, IForwardOnBarArgument arguments);

    /// <summary>
    /// 历史回测时每跳一个tick触发事件的参数
    /// </summary>
    public interface IForwardOnTickArgument
    {
        /// <summary>
        /// 当前回测的股票或期货合约
        /// </summary>
        string Code { get; }

        /// <summary>
        /// 当前回测触发事件所处的历史时间
        /// </summary>
        double Time { get; }

        IForwardTickInfo TickInfo { get; }

        /// <summary>
        /// 获得回测的股票或期货在当前历史时间的实时数据
        /// </summary>
        IRealTimeDataReader_Code CurrentData { get; }

        /// <summary>
        /// 获得其它股票或期货在当前历史时间的实时数据
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        IRealTimeDataReader_Code GetOtherData(string code);
    }

    /// <summary>
    /// 历史回测的当前tick信息
    /// </summary>
    public interface IForwardTickInfo
    {
        /// <summary>
        /// 
        /// </summary>
        ITickData_Extend TickData { get; }

        /// <summary>
        /// 
        /// </summary>
        int Index { get; }

        /// <summary>
        /// 
        /// </summary>
        ITickBar TickBar { get; }
    }

    /// <summary>
    /// 
    /// </summary>
    public interface IForwardOnBarArgument
    {
        /// <summary>
        /// 得到当前回归测试的股票或期货合约
        /// </summary>
        String Code { get; }

        double Time { get; }

        IList<IForwardKLineBarInfo> AllFinishedBars
        {
            get;
        }

        IForwardKLineBarInfo MainBar
        {
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        IRealTimeDataReader_Code CurrentData { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        IRealTimeDataReader_Code GetOtherData(string code);
    }

    public interface IForwardKLineBarInfo
    {
        /// <summary>
        /// OnBar事件执行时完成一个bar的k线数据
        /// </summary>
        IKLineData_Extend KLineData { get; }

        /// <summary>
        /// 前进器前进完成的Bar
        /// </summary>
        int BarPos { get; }

        /// <summary>
        /// K线周期
        /// </summary>
        KLinePeriod KLinePeriod { get; }

        /// <summary>
        /// K线柱
        /// </summary>
        IKLineBar KLineBar { get; }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void DelegateOnNavigateTo(Object sender, DataNavigateEventArgs e);

    /// <summary>
    /// 
    /// </summary>
    public class DataNavigateEventArgs : System.EventArgs
    {
        private double time;

        private double prevTime;

        public double Time
        {
            get
            {
                return time;
            }
        }

        public double PrevTime
        {
            get
            {
                return prevTime;
            }
        }


        public DataNavigateEventArgs(double prevTime, double time)
        {
            this.prevTime = prevTime;
            this.time = time;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(prevTime).Append(",").Append(time);
            return sb.ToString();
        }
    }
}
