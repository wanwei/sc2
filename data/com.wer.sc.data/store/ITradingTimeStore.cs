using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.store
{
    public interface ITradingTimeStore
    {
        /// <summary>
        /// 保存一个品种的所有交易时间数据
        /// </summary>
        /// <param name="code"></param>
        /// <param name="codes"></param>
        void Save(string code, IList<ITradingTime> codes);

        /// <summary>
        /// 装载一个品种的交易时间数据
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        IList<ITradingTime> Load(string code);

        /// <summary>
        /// 删除一个品种的交易时间数据
        /// </summary>
        /// <param name="code"></param>
        void Delete(string code);
    }
}
