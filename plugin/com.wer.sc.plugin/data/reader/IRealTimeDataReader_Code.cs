using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.reader
{
    /// <summary>
    /// 单支股票或期货的实时数据读取，能得到实时的K线、tick和分时线数据
    /// 这个读取器和IHistoryDataReader...的区别：
    /// IHistoryDataReader...读取器如IHistoryDataReader_KLine，它读取数据的时候都是固定时段的历史K线数据
    /// 如获取的1分钟K线数据肯定是：09:00:00,09:01:00,09:02:00....
    /// 该读取器是读取最新的数据，如获取的1分钟K线数据可能是：09:00:00,09:01:00,09:02:22
    /// 最小精确到tick数据。
    /// 该读取器看到的数据和用期货和股票软件看到的数据是一样的。
    /// </summary>
    public interface IRealTimeDataReader_Code
    {
        /// <summary>
        /// 得到代码
        /// </summary>
        /// <returns></returns>
        string Code { get; }

        /// <summary>
        /// 得到当前时间
        /// </summary>
        double Time { get; }

        /// <summary>
        /// 得到指定周期的K线
        /// </summary>
        /// <param name="period"></param>
        /// <returns></returns>
        IKLineData GetKLineData(KLinePeriod period);

        /// <summary>
        /// 确定现在是否是一个周期的结束点
        /// </summary>
        /// <param name="period"></param>
        /// <returns></returns>
        bool IsPeriodEnd(KLinePeriod period);

        /// <summary>
        /// 到周期结束还有多少秒
        /// </summary>
        /// <param name="period"></param>
        /// <returns></returns>
        //int SecondToPeriodEnd(KLinePeriod period);

        /// <summary>
        /// 得到当前的分时线
        /// </summary>
        /// <returns></returns>
        ITimeLineData GetTimeLineData();

        /// <summary>
        /// 得到今日的TICK数据
        /// </summary>
        /// <returns></returns>
        ITickData GetTickData();

        event DelegateOnRealTimeChanged OnRealTimeChanged;
    }

    public delegate void DelegateOnRealTimeChanged(Object sender, RealTimeChangedArgument argument);

    public class RealTimeChangedArgument
    {
        private double prevTime;

        private double time;

        private bool tradingDayChanged;

        private IRealTimeDataReader_Code realTimeDataReader;

        public RealTimeChangedArgument(double prevTime, double time, IRealTimeDataReader_Code realTimeDataReader)
        {
            this.prevTime = prevTime;
            this.time = time;
            this.realTimeDataReader = realTimeDataReader;
        }

        public double PrevTime
        {
            get
            {
                return prevTime;
            }
        }

        public double Time
        {
            get
            {
                return time;
            }
        }

        public IRealTimeDataReader_Code RealTimeDataReader
        {
            get
            {
                return realTimeDataReader;
            }
        }

        public bool TradingDayChanged
        {
            get
            {
                return tradingDayChanged;
            }            
        }
    }
}