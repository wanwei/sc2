using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.datapackage
{
    public interface IDataPackage_CodeInfo
    {
        /// <summary>
        /// 得到股票或期货的ID
        /// </summary>
        string Code { get; }

        /// <summary>
        /// 得到开始日期
        /// </summary>
        int StartDate { get; }

        /// <summary>
        /// 得到结束日期
        /// </summary>
        int EndDate { get; }
    }
}
