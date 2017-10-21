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
    public delegate void DelegateOnTick(object sender, ForwardOnTickArgument argument);

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

    public class ForwardOnTickArgument
    {
        public ITickData TickData;

        public int Index;

        public ITickBar TickBar
        {
            get { return TickData.GetBar(Index); }
        }

        public ForwardOnTickArgument(ITickData tickData, int index)
        {
            this.TickData = tickData;
            this.Index = index;
        }
    }

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
