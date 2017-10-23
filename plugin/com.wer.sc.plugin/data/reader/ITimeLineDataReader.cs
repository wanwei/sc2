using System.Collections.Generic;

namespace com.wer.sc.data.reader
{
    /// <summary>
    /// 分时线历史数据读取器
    /// </summary>
    public interface ITimeLineDataReader
    {
        /// <summary>
        /// 读取一天的分时线数据
        /// </summary>
        /// <param name="code"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        ITimeLineData GetData(string code, int date);

        /// <summary>
        /// 读取一天的分时线数据
        /// </summary>
        /// <param name="code"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        ITimeLineData_Extend GetData_Extend(string code, int date);

        /// <summary>
        /// 读取一段时间的分时线数据
        /// </summary>
        /// <param name="code"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        List<ITimeLineData> GetData(string code, int startDate, int endDate);
    }
}