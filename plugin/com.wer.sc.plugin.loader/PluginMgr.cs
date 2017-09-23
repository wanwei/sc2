using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin
{
    /// <summary>
    /// 插件管理器
    /// </summary>
    public class PluginMgr : IPluginMgr
    {
        //按插件ID索引的插件实例
        private Dictionary<string, object> dic_Id_PluginObject = new Dictionary<string, object>();
        //按插件ID索引，key是插件ID，value是实现了该接口的插件
        private Dictionary<string, PluginInfo> dic_Id_Plugin = new Dictionary<string, PluginInfo>();
        //所有的插件
        private List<PluginInfo> pluginInfos = new List<PluginInfo>();
        //按市场分类插件
        private Dictionary<MarketType, List<PluginInfo>> dic_Market_Plugins = new Dictionary<MarketType, List<PluginInfo>>();
        //按插件类型分类插件
        private Dictionary<Type, List<PluginInfo>> dic_Type_Plugins = new Dictionary<Type, List<PluginInfo>>();

        private string path;

        internal PluginMgr(string path)
        {
            this.path = path;
        }

        public List<PluginInfo> GetAllPlugins()
        {
            return this.pluginInfos;
        }

        public List<PluginInfo> GetPlugins(Type type)
        {
            List<PluginInfo> plugins;
            dic_Type_Plugins.TryGetValue(type, out plugins);
            return plugins;
        }

        /// <summary>
        /// 根据插件类型得到对应的插件
        /// </summary>
        /// <param name="type">对应PluginInfo里的类型</param>
        /// <returns></returns>
        public List<PluginInfo> GetPlugins(MarketType market, Type type)
        {
            List<PluginInfo> plugins;
            dic_Market_Plugins.TryGetValue(market, out plugins);
            if (plugins == null)
                return null;
            List<PluginInfo> result = new List<PluginInfo>();
            for (int i = 0; i < plugins.Count; i++)
            {
                PluginInfo plugin = plugins[i];
                if (plugin.PluginType == type)
                    result.Add(plugin);
            }
            return result;
        }

        /// <summary>
        /// 得到插件
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PluginInfo GetPlugin(string id)
        {
            PluginInfo pluginInfo;
            bool b = dic_Id_Plugin.TryGetValue(id, out pluginInfo);
            return b ? pluginInfo : null;
        }

        public T CreatePluginObject<T>(PluginInfo pluginInfo)
        {
            Type[] parameters = { typeof(PluginHelper) };
            System.Reflection.ConstructorInfo ci = pluginInfo.PluginClassType.GetConstructor(parameters);
            if (ci != null)
            {
                PluginHelper pluginHelper = new PluginHelper(pluginInfo);
                return (T)Activator.CreateInstance(pluginInfo.PluginClassType, pluginHelper);
            }
            //Console.WriteLine(pluginInfo.PluginClassType);
            return (T)Activator.CreateInstance(pluginInfo.PluginClassType);
        }

        public T CreatePluginObject<T>(string pluginId)
        {
            PluginInfo pluginInfo = GetPlugin(pluginId);
            if (pluginInfo == null)
                return default(T);
            return CreatePluginObject<T>(pluginInfo);
        }

        public T GetPluginObject<T>(PluginInfo pluginInfo)
        {
            if (dic_Id_PluginObject.ContainsKey(pluginInfo.PluginID))
            {
                return (T)dic_Id_PluginObject[pluginInfo.PluginID];
            }
            T t = CreatePluginObject<T>(pluginInfo);
            dic_Id_PluginObject.Add(pluginInfo.PluginID, t);
            return t;
        }

        public T GetPluginObject<T>(string pluginId)
        {
            PluginInfo pluginInfo = GetPlugin(pluginId);
            if (pluginInfo == null)
                return default(T);
            return GetPluginObject<T>(pluginInfo);
        }

        public void Load()
        {
            if (!Directory.Exists(path))
                return;
            PluginScan scan = new PluginScan();
            this.pluginInfos.AddRange(scan.Scan(this.path));
            foreach (PluginInfo plugin in pluginInfos)
            {
                AddPlugin2Dic(plugin);
            }
        }

        private void AddPlugin2Dic(PluginInfo pluginInfo)
        {
            AddDic_Id_Plugin(pluginInfo);
            AddDic_Type_Plugins(pluginInfo);
            AddDic_Market_Plugins(pluginInfo);
        }

        private void AddDic_Id_Plugin(PluginInfo pluginInfo)
        {
            string id = pluginInfo.PluginID;
            if (this.dic_Id_Plugin.ContainsKey(id))
            {
                //TODO 写入日志，重复插件ID
            }
            else
            {
                this.dic_Id_Plugin.Add(id, pluginInfo);
            }
        }

        private void AddDic_Type_Plugins(PluginInfo pluginInfo)
        {
            Type type = pluginInfo.PluginType;
            if (this.dic_Type_Plugins.ContainsKey(type))
            {
                this.dic_Type_Plugins[type].Add(pluginInfo);
            }
            else
            {
                List<PluginInfo> plugins = new List<PluginInfo>();
                plugins.Add(pluginInfo);
                this.dic_Type_Plugins.Add(type, plugins);
            }
        }

        private void AddDic_Market_Plugins(PluginInfo pluginInfo)
        {
            MarketType type = pluginInfo.MarketType;
            if (this.dic_Market_Plugins.ContainsKey(type))
            {
                this.dic_Market_Plugins[type].Add(pluginInfo);
            }
            else
            {
                List<PluginInfo> plugins = new List<PluginInfo>();
                plugins.Add(pluginInfo);
                this.dic_Market_Plugins.Add(type, plugins);
            }
        }
    }
}
