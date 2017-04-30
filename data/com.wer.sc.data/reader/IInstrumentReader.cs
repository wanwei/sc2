using System.Collections.Generic;

namespace com.wer.sc.data.reader
{
    /// <summary>
    /// 股票信息读取器
    /// </summary>
    public interface IInstrumentReader
    {
        /// <summary>
        /// 是否包含code
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        bool Contain(string code);

        List<string> GetAllCatelogs();

        List<CodeInfo> GetAllInstruments();

        CodeInfo GetInstrument(string code);

        List<string> GetInstrumentsByCatelog(string catelog);

        void Refresh();
    }
}