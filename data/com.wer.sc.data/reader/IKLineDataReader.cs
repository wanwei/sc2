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

        IKLineData GetData(string code, int startDate, int endDate, int minBeforeBarCount, int minAfterBarCount, KLinePeriod period);

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
    }
}