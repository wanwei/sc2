using com.wer.sc.plugin;
using com.wer.sc.utils.update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.utils.ui.update
{
    public interface IMultiUpdater
    {
        /// <summary>
        /// 得到所有更新器名称
        /// </summary>
        /// <returns></returns>
        List<String> GetDataUpdaterNames();

        /// <summary>
        /// 得到所有的更新步骤获取器
        /// </summary>
        /// <returns></returns>
        List<IUpdateStepGetter> GetDataUpdaters();
    }
}
