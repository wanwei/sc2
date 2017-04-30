using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.store
{
    /// <summary>
    /// 交易时间保存
    /// </summary>
    public interface ITradingSessionStore
    {
        /// <summary>
        /// 保存一个品种的所有交易时间数据
        /// </summary>
        /// <param name="code"></param>
        /// <param name="codes"></param>
        void Save(string code, List<TradingSession> codes);

        /// <summary>
        /// 装载一个品种的交易时间数据
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        List<TradingSession> Load(string code);

        /// <summary>
        /// 删除一个品种的交易时间数据
        /// </summary>
        /// <param name="code"></param>
        void Delete(string code);
    }
}
