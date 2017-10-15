namespace com.wer.sc.data.reader
{
    /// <summary>
    /// K线历史数据读取器
    /// </summary>
    public interface IKLineDataReader
    {
        /// <summary>
        /// 得到一张合约的所有K线数据
        /// </summary>
        /// <param name="code"></param>
        /// <param name="period"></param>
        /// <returns></returns>
        IKLineData GetAllData(string code, KLinePeriod period);

        /// <summary>
        /// 得到一张合约一段时间的K线数据
        /// </summary>
        /// <param name="code"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="period"></param>
        /// <returns></returns>
        IKLineData GetData(string code, int startDate, int endDate, KLinePeriod period);

        /// <summary>
        /// 得到一张合约一段时间的K线数据
        /// </summary>
        /// <param name="code"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="minBeforeBarCount"></param>
        /// <param name="minAfterBarCount"></param>
        /// <param name="period"></param>
        /// <returns></returns>
        IKLineData GetData(string code, int startDate, int endDate, int minBeforeBarCount, int minAfterBarCount, KLinePeriod period);

        IKLineData_Extend GetData_Extend(string code, int startDate, int endDate, int minBeforeBarCount, int minAfterBarCount, KLinePeriod period);

        /// <summary>
        /// 得到上个交易日收盘价，如果之前没有交易日，则返回当日的开盘价
        /// </summary>
        /// <param name="code"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        float GetLastEndPrice(string code, int date);

        /// <summary>
        /// 得到历史数据里的第一个日子
        /// </summary>
        /// <param name="code"></param>
        /// <param name="period"></param>
        /// <returns></returns>
        int GetFirstDate(string code, KLinePeriod period);

        /// <summary>
        /// 得到历史数据里最后的一个日子
        /// </summary>
        /// <param name="code"></param>
        /// <param name="period"></param>
        /// <returns></returns>
        int GetLastDate(string code, KLinePeriod period);

        /// <summary>
        /// 得到K线时间信息
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        KLineDataTimeInfo GetKLineDataTimeInfo(string code, int startDate, int endDate, KLinePeriod klinePeriod);
    }
}