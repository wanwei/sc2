using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.store
{
    /// <summary>
    /// tick数据存储
    /// </summary>
    public interface ITickDataStore
    {
        /// <summary>
        /// 保存tick数据，该方法会替换掉之前的数据
        /// </summary>
        /// <param name="code"></param>
        /// <param name="date"></param>
        /// <param name="data"></param>
        void Save(string code, int date, TickData data);

        /// <summary>
        /// 保存tick数据，该方法会写入新的tick数据
        /// </summary>
        /// <param name="code"></param>
        /// <param name="date"></param>
        /// <param name="data"></param>
        void Append(string code, int date, TickData data);

        /// <summary>
        /// 装载tick数据
        /// </summary>
        /// <param name="code"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        TickData Load(string code, int date);

        /// <summary>
        /// 删掉指定的tick数据
        /// </summary>
        /// <param name="code"></param>
        /// <param name="date"></param>
        void Delete(string code, int date);

        /// <summary>
        /// 得到所有
        /// </summary>
        /// <returns></returns>
        List<int> GetAllDays(string code);

        /// <summary>
        /// 是否存在该日tick数据
        /// </summary>
        /// <param name="code"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        bool Exist(string code, int date);
    }
}
