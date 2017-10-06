using com.wer.sc.data.reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.reader
{
    public interface IRealTimeDataReader
    {
        /// <summary>
        /// 得到一支股票或期货的实时数据
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        IRealTimeDataReader_Code GetRealTimeData(String code);
    }
}
