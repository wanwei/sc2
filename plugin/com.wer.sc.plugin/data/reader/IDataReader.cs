using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.reader
{
    /// <summary>
    /// 数据读取工厂
    /// </summary>
    public interface IDataReader
    {
        /// <summary>
        /// 创建一个品种信息读取器
        /// </summary>
        /// <returns></returns>
        ICodeReader CodeReader { get; }

        /// <summary>
        /// 创建一个交易日读取器
        /// </summary>
        /// <returns></returns>
        ITradingDayReader TradingDayReader { get; }

        /// <summary>
        /// 创建一个某品种的交易日读取器
        /// TODO 确认实现方式
        /// 停牌怎么算？
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        ITradingDayReader GetTradingDayReader(string code);

        /// <summary>
        /// 创建一个品种的交易时间读取器
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        ITradingSessionReader_Code CreateTradingSessionReader(string code);

        /// <summary>
        /// 创建K线读取器
        /// </summary>
        /// <returns></returns>
        IKLineDataReader KLineDataReader { get; }

        /// <summary>
        /// 创建分时线读取器
        /// </summary>
        /// <returns></returns>
        ITimeLineDataReader TimeLineDataReader { get; }

        /// <summary>
        /// 创建tick数据读取器
        /// </summary>
        /// <returns></returns>
        ITickDataReader TickDataReader { get; }
    }
}