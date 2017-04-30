using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.reader
{
    /// <summary>
    /// 交易时段获取接口，该接口
    /// </summary>
    public interface ITradingSessionReader_Instrument
    {
        /// <summary>
        /// 得到该读取器读取的Instrument的id
        /// </summary>
        /// <returns></returns>
        string GetInstrument();

        /// <summary>
        /// 得到当日的交易时间
        /// </summary>
        /// <param name="code"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        TradingSession GetTradingSession(int date);

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
        /// <param name="forward"></param>
        /// <returns></returns>
        int GetRecentTradingDay(double time, bool forward);

        /// <summary>
        /// 验证该时间是否是当日的开盘时间
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        bool IsStartTime(double time);
    }
}