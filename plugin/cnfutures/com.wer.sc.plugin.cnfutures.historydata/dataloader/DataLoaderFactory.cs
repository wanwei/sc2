using com.wer.sc.plugin.cnfutures.historydata.dataprovider;
using com.wer.sc.plugin.cnfutures.historydata.dataprovider.biaopuyonghua;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin.cnfutures.historydata.dataloader
{
    public class DataLoaderFactory
    {
        /// <summary>
        /// 创建一个数据装载器
        /// </summary>
        /// <param name="pluginhelper"></param>
        /// <param name="srcDataPath"></param>
        /// <param name="targetDataPath"></param>
        /// <param name="dataProvider"></param>
        /// <returns></returns>
        public static IDataLoader CreateDataLoader(PluginHelper pluginhelper, IDataProvider dataProvider, string srcDataPath, string targetDataPath)
        {
            return new DataLoader(pluginhelper, dataProvider, srcDataPath, targetDataPath);
        }
    }
}
