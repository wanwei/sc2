using System.Collections.Generic;

namespace com.wer.sc.data.reader
{
    /// <summary>
    /// TICK历史数据读取器
    /// </summary>
    public interface ITickDataReader
    {
        /// <summary>
        /// 得到一张合约单日的tick数据
        /// </summary>
        /// <param name="code"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        TickData GetTickData(string code, int date);

        /// <summary>
        /// 得到一张合约单日的tick数据，包括一些时间信息
        /// </summary>
        /// <param name="code"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        ITickData_Extend GetTickData_Extend(string code, int date);
    }
}