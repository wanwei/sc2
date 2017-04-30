using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin
{
    /// <summary>
    /// SC的插件是以Assembly的形式出现的，该类会显示一个Assembly里的所有插件信息
    /// 插件的Assembly信息
    /// </summary>
    public class PluginAssembly
    {
        /// <summary>
        /// 程序集的名称
        /// </summary>
        private String assemblyName;

        /// <summary>
        /// 程序集完整路径
        /// </summary>
        private String fullPath;

        /// <summary>
        /// 程序集里的所有插件
        /// </summary>
        private List<PluginInfo> plugins = new List<PluginInfo>();

        private PluginAssembly()
        {

        }

        public string AssemblyName
        {
            get
            {
                return assemblyName;
            }
        }

        public string FullPath
        {
            get
            {
                return fullPath;
            }
        }

        public List<PluginInfo> Plugins
        {
            get
            {
                return plugins;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(assemblyName).Append(",").Append(fullPath);
            for (int i = 0; i < plugins.Count; i++)
            {
                sb.Append("\r\n").Append(plugins[i]);
            }
            return sb.ToString();
        }

        public static PluginAssembly Create(string path)
        {
            if (IsAssemblyRef(path))
                return null;
            try
            {
                PluginAssembly pluginAssembly = new PluginAssembly();
                Assembly ass = Assembly.LoadFrom(path);
                if (ass == null)
                    return null;

                pluginAssembly.fullPath = path;
                pluginAssembly.assemblyName = ass.GetName().Name;

                Type[] types = ass.GetTypes();
                for (int i = 0; i < types.Length; i++)
                {
                    Type classType = types[i];
                    Type pluginType = GetTypePlugin(classType);
                    if (pluginType == null)
                        continue;
                    string[] arr = GetPluginNameDesc(classType);
                    string pluginID = arr == null ? "" : arr[0];
                    string pluginName = arr == null ? "" : arr[1];
                    string pluginDesc = arr == null ? "" : arr[2];
                    //PluginInfo pluginInfo = new PluginInfo( classType, pluginType, pluginID, pluginName, pluginDesc);
                    //pluginAssembly.plugins.Add(pluginInfo);
                }
                return pluginAssembly;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        private static bool IsAssemblyRef(String path)
        {
            string[] refDll = new string[] {
                "com.wer.sc.plugin.dll",
                "com.wer.sc.utils.dll",
                "log4net.dll" };
            for (int i = 0; i < refDll.Length; i++)
            {
                if (path.ToLower().EndsWith(refDll[i]))
                    return true;
            }
            return false;
        }

        public static string[] GetPluginNameDesc(Type modelType)
        {
            Type type = typeof(PluginAttribute);
            if (!modelType.IsDefined(type, false))
                return null;
            var attributes = modelType.GetCustomAttributes();
            foreach (var attribute in attributes)
            {
                if (attribute.GetType() == type)
                {
                    string id = (String)attribute.GetType().GetProperty("ID").GetValue(attribute);
                    String name = (String)attribute.GetType().GetProperty("Name").GetValue(attribute);
                    string desc = (String)attribute.GetType().GetProperty("Desc").GetValue(attribute);
                    return new string[] { id, name, desc };
                }
            }
            return null;
        }

        private static Type[] plugintypes = GetPluginTypes();

        private static Type[] GetPluginTypes()
        {
            Type[] types = new Type[3];
            types[0] = typeof(IPlugin_HistoryData);
            types[1] = typeof(IPlugin_Market);
            //types[1] = typeof(IPlugin_MarketData);
            //types[2] = typeof(IPlugin_MarketTrader);
            //types[2] = typeof(IStrategy);
            return types;
        }

        private static Type GetTypePlugin(Type t)
        {
            for (int i = 0; i < plugintypes.Length; i++)
            {
                Type pluginType = plugintypes[i];
                if (t.GetInterface(pluginType.Name) != null)
                    return pluginType;
            }
            return null;
        }

    }
}
