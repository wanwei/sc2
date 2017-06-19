using com.wer.sc.data.update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.store
{
    /// <summary>
    /// 数据更新保存器
    /// </summary>
    public interface IUpdateInfoStore
    {
        /// <summary>
        /// 保存数据更新信息
        /// </summary>
        void Save(UpdatedDataInfo updatedDataInfo);

        /// <summary>
        /// 装载数据更新信息
        /// </summary>
        /// <returns></returns>
        UpdatedDataInfo Load();
    }
}
