using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.store
{
    /// <summary>
    /// 品种保存接口
    /// </summary>
    public interface IInstrumentStore
    {
        /// <summary>
        /// 保存所有品种
        /// </summary>
        /// <param name="codes"></param>
        void Save(List<CodeInfo> codes);

        /// <summary>
        /// 装载所有品种
        /// </summary>
        /// <returns></returns>
        List<CodeInfo> Load();

        /// <summary>
        /// 删掉所有品种
        /// </summary>
        void Delete();
    }
}
