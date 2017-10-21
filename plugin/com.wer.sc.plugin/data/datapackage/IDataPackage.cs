using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.datapackage
{
    /// <summary>
    /// 数据包，可以获得
    /// </summary>
    public interface IDataPackage
    {
        /// <summary>
        /// 得到开始日期
        /// </summary>
        int StartDate { get; }

        /// <summary>
        /// 得到结束日期
        /// </summary>
        int EndDate { get; }

        /// <summary>
        /// 得到这段时间内的所有交易日
        /// </summary>
        /// <returns></returns>
        IList<int> GetTradingDays();

        /// <summary>
        /// 得到数据包里所有的股票或期货
        /// </summary>
        /// <returns></returns>
        IList<string> GetCodes();

        /// <summary>
        /// 得到code对应的数据包
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        IDataPackage_Code GetDataPackage(string code);
    }
}
