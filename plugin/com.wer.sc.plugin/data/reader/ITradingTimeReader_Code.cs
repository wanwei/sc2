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
    public interface ITradingTimeReader_Code
    {
        /// <summary>
        /// 得到该读取器读取的Instrument的id
        /// </summary>
        /// <returns></returns>
        string GetCode();

        /// <summary>
        /// 得到当日的交易时间
        /// </summary>
        /// <param name="code"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        TradingTime GetTradingTime(int date);

        /// <summary>
        /// 得到指定时间对应的交易日期，如果该时间不在交易时间内，则返回-1
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        int GetTradingDay(double time);

        /// <summary>
        /// 得到指定时间对应的交易日期
        /// 如果该时间不交易，则返回该时间之后最近的交易日
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        int GetRecentTradingDay(double time);

        /// <summary>
        /// 得到指定时间对应的交易日期
        /// 如果该时间不交易，则通过forward参数得到交易日
        /// 如果forward为true，则返回之后的交易日
        /// 如果forward为false，则返回之前的交易日
        /// </summary>
        /// <param name="time"></param>
        /// <param name="findForward"></param>
        /// <returns></returns>
        int GetRecentTradingDay(double time, bool findForward);

        /// <summary>
        /// 得到最近的交易时间
        /// 如果time在交易时间内，则返回time
        /// </summary>
        /// <param name="time"></param>
        /// <param name="findForward"></param>
        /// <returns></returns>
        double GetRecentTradingTime(double time, bool findForward);

        /// <summary>
        /// 验证该时间是否是当日的开盘时间
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        bool IsStartTime(double time);
    }
}