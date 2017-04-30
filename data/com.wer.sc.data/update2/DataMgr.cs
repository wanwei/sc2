using com.wer.sc.plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.update
{
    public class DataMgr
    {
        private List<DataProviderWrap> providers;

        private Dictionary<String, DataProviderWrap> dicProviders;


        public DataMgr()
        {
            List<Plugin_DataProvider> pluginproviders = new List<Plugin_DataProvider>();
            List<PluginInfo2> plugs = PluginMgr2.Instance.Load();
            for (int i = 0; i < plugs.Count; i++)
            {
                pluginproviders.AddRange(plugs[i].GetProviders());
            }

            this.providers = new List<DataProviderWrap>();
            this.dicProviders = new Dictionary<string, DataProviderWrap>();
            for (int i = 0; i < pluginproviders.Count; i++)
            {
                Plugin_DataProvider provider = pluginproviders[i];
                DataProviderWrap providerWrap = new DataProviderWrap(provider);
                providers.Add(providerWrap);
                dicProviders.Add(provider.GetName(), providerWrap);
            }
        }

        /// <summary>
        /// 得到所有的数据提供者
        /// </summary>
        /// <returns></returns>
        public List<DataProviderWrap> GetProviders()
        {
            return providers;
        }

        public DataProviderWrap GetProvider(String name)
        {
            if (dicProviders.ContainsKey(name))
                return dicProviders[name];
            return null;
        }

        private Dictionary<String, DataUpdate_Old> dicDataUpdate = new Dictionary<string, DataUpdate_Old>();

        public DataUpdate_Old GetDataUpdate(String providerName)
        {
            if (dicDataUpdate.ContainsKey(providerName))
                return dicDataUpdate[providerName];
            DataUpdate_Old update = new DataUpdate_Old(GetProvider(providerName));
            dicDataUpdate.Add(providerName, update);
            return update;
        }
    }
}
