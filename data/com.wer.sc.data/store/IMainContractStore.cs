using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.store
{
    public interface IMainContractStore
    {
        /// <summary>
        /// 保存所有品种
        /// </summary>
        /// <param name="mainContractInfo"></param>
        void Save(IList<MainContractInfo> mainContractInfo);

        /// <summary>
        /// 装载所有品种
        /// </summary>
        /// <returns></returns>
        IList<MainContractInfo> Load();

        /// <summary>
        /// 删掉所有品种
        /// </summary>
        void Delete();
    }
}
