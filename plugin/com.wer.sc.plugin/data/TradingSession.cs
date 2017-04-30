using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data
{
    /// <summary>
    /// 交易时段
    /// 该类记录了一天早上的开盘时间和晚上收盘时间
    /// 如 20100105,20100105.090000，20100105.150000
    /// 表示2010年1月5日，当日早上9点开盘，下午15点收盘
    /// </summary>
    public class TradingSession
    {
        public int TradingDay;

        public double StartTime;

        public double EndTime;

        public TradingSession()
        {

        }

        public TradingSession(int tradingDay, double startTime, double endTime)
        {
            this.TradingDay = tradingDay;
            this.StartTime = startTime;
            this.EndTime = endTime;
        }

        public override string ToString()
        {
            return TradingDay + "," + StartTime + "," + EndTime;
        }
    }
}
