using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.store
{
    /// <summary>
    /// 交易日数据存储
    /// </summary>
    public interface ITradingDayStore
    {
        /// <summary>
        /// 保存所有交易日
        /// </summary>
        /// <param name="tradingDays"></param>
        void Save(List<int> tradingDays);

        /// <summary>
        /// 装载所有交易日
        /// </summary>
        /// <returns></returns>
        List<int> Load();

        /// <summary>
        /// 删除所有交易日数据
        /// </summary>
        void Delete();
    }
}
