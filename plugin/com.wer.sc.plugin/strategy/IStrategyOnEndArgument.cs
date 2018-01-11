using com.wer.sc.data.reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy
{
    public interface IStrategyOnEndArgument
    {

        /// <summary>
        /// 得到当前数据
        /// </summary>
        IRealTimeData_Code CurrentData { get; }

        /// <summary>
        /// 得到其它数据
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        IRealTimeData_Code GetOtherData(string code);

    }
}
