using com.wer.sc.plugin.cnfutures.historydata.dataloader.biaopuyonghua;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin.cnfutures.historydata.dataloader
{
    public class DataLoaderFactory
    {
        public static IDataLoader CreateDataLoader(DataSourceType dataSource, PluginHelper pluginhelper, string srcDataPath, string csvDataPath)
        {
            switch (dataSource)
            {
                case DataSourceType.TaoBao1:
                    return new DataProvider_BiaoPuYongHua(pluginhelper, srcDataPath);
            }
            return null;
        }
    }
}
