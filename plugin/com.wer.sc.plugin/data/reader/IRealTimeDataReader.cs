using com.wer.sc.data.reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.reader
{
    /// <summary>
    /// 市场上的实时数据读取器
    /// 该读取器能够得到市场上指定品种的信息
    /// </summary>
    public interface IRealTimeDataReader
    {
        /// <summary>
        /// 得到侦听的所有股票ID
        /// </summary>
        IList<string> ListenedCodes { get; }

        /// <summary>
        /// 得到当前时间
        /// </summary>
        double Time { get; }

        /// <summary>
        /// 实时时间改变事件，每当时间改变该事件会触发
        /// </summary>
        event DelegateOnRealTimeChanged OnRealTimeChanged;

        /// <summary>
        /// 得到一支股票或期货的实时数据
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        IRealTimeData_Code GetRealTimeData(String code);
    }

    public delegate void DelegateOnRealTimeChanged(Object sender, RealTimeChangedArgument argument);

    public class RealTimeChangedArgument
    {
        //private string prevCode;

        //private string code;

        private double prevTime;

        private double time;

        private bool tradingDayChanged;

        private IRealTimeDataReader realTimeDataReader;

        public RealTimeChangedArgument(double prevTime, double time, IRealTimeDataReader realTimeDataReader)
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

        public IRealTimeDataReader RealTimeDataReader
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