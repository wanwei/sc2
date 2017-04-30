using com.wer.sc.data;
using com.wer.sc.data.reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin.cnfutures.historydata.dataprovider
{
    public interface IDataProvider
    {
        /// <summary>
        /// 装载tick数据
        /// </summary>
        /// <param name="code"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        ITickData LoadTickData(string code, int date);

        /// <summary>
        /// 得到所有交易日数据
        /// </summary>
        /// <returns></returns>
        ITradingDayReader LoadTradingDayReader();
    }
}
