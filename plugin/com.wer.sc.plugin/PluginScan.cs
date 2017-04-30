using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace com.wer.sc.plugin
{
    public class PluginScan
    {
        public IList<PluginInfo> Scan(String path)
        {
            DirectoryInfo dir = new DirectoryInfo(path);
            List<PluginInfo> plugins = new List<PluginInfo>();
            Scan(plugins, dir);
            return plugins;
        }

        private void Scan(List<PluginInfo> plugins, DirectoryInfo dir)
        {
            FileInfo[] files = dir.GetFiles("*.pluginconfig");
            foreach (FileInfo file in files)
            {
                GetPluginInfos(file, plugins);
            }

            DirectoryInfo[] subDirs = dir.GetDirectories();
            foreach (DirectoryInfo subDir in subDirs)
            {
                Scan(plugins, subDir);
            }
        }

        private void GetPluginInfos(FileInfo configFile, List<PluginInfo> plugins)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(configFile.FullName);
            XmlNodeList nodes = doc.GetElementsByTagName("Assembly");
            foreach (XmlNode node in nodes)
            {
                if (!(node is XmlElement))
                    continue;
                XmlElement elem = (XmlElement)node;
                GetPluginInfos(configFile.Directory.FullName, elem, plugins);
            }
        }

        private void GetPluginInfos(string path, XmlElement elem, List<PluginInfo> plugins)
        {
            string file = elem.GetAttribute("file");
            if (file == null)
                return;
            string fullPath = path + "\\" + file;
            if (!File.Exists(fullPath))
                return;
            Assembly ass = Assembly.LoadFrom(fullPath);
            if (ass == null)
                return;
            XmlNodeList childNodes = elem.ChildNodes;
            foreach (XmlNode node in childNodes)
            {
                XmlElement elemPluginInfo = (XmlElement)node;
                if (elemPluginInfo.Name != "Plugin")
                    continue;
                string clsName = elemPluginInfo.GetAttribute("class");
                Type pluginClassType = ass.GetType(clsName);
                if (pluginClassType == null)
                    continue;
                try
                {
                    PluginInfo pluginInfo = GetPluginInfo(pluginClassType);
                    if (pluginInfo != null)
                    {
                        pluginInfo.PluginPath = fullPath;
                        plugins.Add(pluginInfo);
                        LogHelper.Info(GetType(), "扫描到插件" + pluginInfo);
                    }
                }
                catch (Exception e)
                {
                    LogHelper.Warn(GetType(), e);
                }
            }
        }

        private static PluginInfo GetPluginInfo(Type pluginClassType)
        {
            Type type = typeof(PluginAttribute);
            if (!pluginClassType.IsDefined(type, false))
                return null;
            Type pluginType = typeof(IPlugin_HistoryData);
            Type inheritType = pluginClassType.GetInterface(pluginType.FullName);
            if (inheritType == null)
            {
                pluginType = typeof(IPlugin_Market);
                inheritType = pluginClassType.GetInterface(pluginType.FullName);
                if (inheritType == null)
                {
                    pluginType = typeof(IPlugin_DataUpdater);
                    inheritType = pluginClassType.GetInterface(pluginType.FullName);
                    if (inheritType == null)
                        return null;
                }
            }

            var attributes = pluginClassType.GetCustomAttributes();
            foreach (var attribute in attributes)
            {
                if (attribute.GetType() == type)
                {
                    string id = (String)attribute.GetType().GetProperty("ID").GetValue(attribute);
                    String name = (String)attribute.GetType().GetProperty("Name").GetValue(attribute);
                    string desc = (String)attribute.GetType().GetProperty("Desc").GetValue(attribute);
                    MarketType marketType = (MarketType)attribute.GetType().GetProperty("MarketType").GetValue(attribute);
                    return new PluginInfo(pluginClassType, pluginType, id, name, desc, marketType);
                }
            }
            return null;
        }
    }
}
