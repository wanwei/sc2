using com.wer.sc.data.reader;

namespace com.wer.sc.data.cache
{
    /// <summary>
    /// CODE数据
    /// </summary>
    public interface IDataCache_Code
    {
        string Code { get; }

        int StartDate { get; }

        int EndDate { get; }

        /// <summary>
        /// 得到指定时间对应的开盘日
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        int GetOpenDate(double time);
        
        ITradingDayReader GetOpenDateReader();

        int MaxCacheDateCount { get; set; }

        IDataCache_CodeDate GetCache_CodeDate(int date);

        IDataCache_CodeDate GetCache_CodeDate(double time);
    }
}