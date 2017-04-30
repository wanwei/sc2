using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin.data
{
    /// <summary>
    /// 交易时段明细，该类会记录所有中间休息停盘时间
    /// 如 
    /// 
    /// </summary>
    public class TradingTime
    {
        private int tradingDay;

        private List<double[]> tradingPeriods = new List<double[]>();

        public TradingTime(int tradingDay)
        {
            this.tradingDay = tradingDay;
        }

        public int TradingDay
        {
            get
            {
                return tradingDay;
            }
        }

        /// <summary>
        /// 得到所有交易明细
        /// 如下：
        /// </summary>
        public List<double[]> TradingPeriods
        {
            get
            {
                return tradingPeriods;
            }
        }
    }
}