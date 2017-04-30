using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.reader
{
    /// <summary>
    /// 交易时段明细读取器，和ITradingSessionReader不同之处：
    /// 1.该接口能得到每段连续交易时间，ITradingSessionReader能得到开盘和收盘时间
    /// 2.该接口得到的时间不是fulltime，只从小时开始，如.090000表示9点
    /// </summary>
    public interface ITradingTimeReader
    {
        /// <summary>
        /// 得到指定开盘时间
        /// </summary>
        /// <param name="code"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        List<double[]> GetTradingTime(String code, int date);
    }
}