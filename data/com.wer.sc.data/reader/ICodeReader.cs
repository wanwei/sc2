using System.Collections.Generic;

namespace com.wer.sc.data.reader
{
    /// <summary>
    /// 股票信息读取器
    /// </summary>
    public interface ICodeReader
    {
        /// <summary>
        /// 是否包含code
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        bool Contain(string code);

        /// <summary>
        /// 得到所有分类
        /// </summary>
        /// <returns></returns>
        List<string> GetAllCatelogs();

        /// <summary>
        /// 得到所有合约
        /// </summary>
        /// <returns></returns>
        List<CodeInfo> GetAllCodes();

        /// <summary>
        /// 根据代码得到对应合约
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        CodeInfo GetCodeInfo(string code);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="catelog"></param>
        /// <returns></returns>
        List<string> GetCodesByCatelog(string catelog);

        void Refresh();
    }
}