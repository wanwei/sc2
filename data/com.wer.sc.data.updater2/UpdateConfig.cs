using com.wer.sc.plugin;
using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace com.wer.sc.data.updater
{
    public class UpdateConfig
    {
        /// <summary>
        /// 得到所有更新包的名称
        /// </summary>
        /// <returns></returns>
        public static List<String> GetAllUpdatePackageName()
        {
            string configFile = @"config\dataupdate.config";
            XmlDocument doc = new XmlDocument();
            doc.Load(configFile);

            List<String> packageNames = new List<string>();
            XmlNodeList nodes = doc.GetElementsByTagName("DataUpdaterPackage");
            for (int i = 0; i < nodes.Count; i++)
            {
                XmlNode node = nodes[i];
                if (node is XmlElement)
                {
                    packageNames.Add(((XmlElement)node).GetAttribute("name"));
                }
            }
            return packageNames;
        }

        /// <summary>
        /// 根据更新包的名称找到更新包
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static DataUpdaterPackageConfigInfo GetUpdatePackage(string name)
        {
            string configFile = @"config\dataupdate.config";
            XmlDocument doc = new XmlDocument();
            doc.Load(configFile);

            XmlNodeList nodes = doc.GetElementsByTagName("DataUpdaterPackage");
            for (int i = 0; i < nodes.Count; i++)
            {
                XmlNode node = nodes[i];
                if (node is XmlElement)
                {
                    XmlElement elem = (XmlElement)node;
                    string packageName = elem.GetAttribute("name");
                    if (packageName == name)
                    {
                        DataUpdaterPackageConfigInfo config = new DataUpdaterPackageConfigInfo();
                        config.LoadConfig((XmlElement)node);
                        return config;
                    }
                }
            }

            return null;
        }
    }

    public class DataUpdaterPackageConfigInfo
    {
        private string name;

        private List<DataUpdaterConfigInfo> dataUpdaters = new List<DataUpdaterConfigInfo>();

        public void LoadConfig(XmlElement elem)
        {
            this.name = elem.GetAttribute("name");
            XmlNodeList nodes = elem.GetElementsByTagName("DataUpdater");
            for (int i = 0; i < nodes.Count; i++)
            {
                XmlNode node = nodes[i];
                if (node is XmlElement)
                {
                    DataUpdaterConfigInfo config = new DataUpdaterConfigInfo();
                    config.LoadConfig((XmlElement)node);
                    dataUpdaters.Add(config);
                }
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
        }

        public List<DataUpdaterConfigInfo> DataUpdaters
        {
            get
            {
                return dataUpdaters;
            }
        }
    }

    public class DataUpdaterConfigInfo
    {
        private string name;
        private string className;
        private string pluginName;

        private IPlugin_DataUpdater dataUpdater;

        private Dictionary<string, object> dic_Arguments = new Dictionary<string, object>();

        public DataUpdaterConfigInfo()
        {

        }

        private IPlugin_DataUpdater GetDataUpdater(DataUpdaterConfigInfo updaterInfo)
        {
            if (updaterInfo.pluginName != null && updaterInfo.pluginName != "")
            {
                IPluginMgr pluginMgr = PluginMgrFactory.DefaultPluginMgr;
                PluginInfo pluginInfo = pluginMgr.GetPlugin(updaterInfo.PluginName);
                if (pluginInfo != null)
                {
                    IPlugin_DataUpdater dataUpdater = pluginMgr.GetPluginObject<IPlugin_DataUpdater>(pluginInfo);
                    foreach (string key in dic_Arguments.Keys)
                    {
                        dataUpdater.SetValue(key, dic_Arguments[key]);
                    }
                    return dataUpdater;
                }
                LogHelper.Warn(GetType(), "数据更新配置：‘" + updaterInfo.name + "’配置的更新插件" + updaterInfo.pluginName + "找不到对应插件");
            }

            string clsName = updaterInfo.ClassName;
            if (clsName != null && clsName != "")
            {
                Type type = Type.GetType(clsName);
                if (type == null)
                {
                    LogHelper.Warn(GetType(), "");
                }
                IPlugin_DataUpdater dataUpdater = (IPlugin_DataUpdater)Activator.CreateInstance(type);
                foreach(string key in dic_Arguments.Keys)
                {
                    dataUpdater.SetValue(key, dic_Arguments[key]);
                }
                return dataUpdater;
            }
            LogHelper.Warn(GetType(), "数据更新配置：‘" + updaterInfo.name + "’没有配置任何更新类");
            return null;
        }

        public void LoadConfig(XmlElement elem)
        {
            this.name = elem.GetAttribute("name");
            this.pluginName = elem.GetAttribute("plugin");
            this.className = elem.GetAttribute("class");

            XmlNodeList nodes = elem.GetElementsByTagName("add");
            for (int i = 0; i < nodes.Count; i++)
            {
                XmlNode node = nodes[i];
                if (node is XmlElement)
                {
                    XmlElement addElem = (XmlElement)node;
                    AddValue(addElem);
                }
            }
            this.dataUpdater = this.GetDataUpdater(this);
        }

        private void AddValue(XmlElement elem)
        {
            string key = elem.GetAttribute("key");
            string value = elem.GetAttribute("value");
            string type = elem.GetAttribute("type");
            this.dic_Arguments.Add(key, GetValue(type, value));
        }

        private object GetValue(string type, string value)
        {
            if (type.Equals("string"))
                return value;
            if (type.Equals("bool"))
                return bool.Parse(value);
            if (type.Equals("int"))
                return int.Parse(value);
            if (type.Equals("double"))
                return double.Parse(value);
            return value;
        }

        public string Name
        {
            get
            {
                return name;
            }
        }

        public string ClassName
        {
            get
            {
                return className;
            }
        }

        public string PluginName
        {
            get
            {
                return pluginName;
            }

        }

        public Dictionary<string, object> Arguments
        {
            get
            {
                return dic_Arguments;
            }
        }

        public IPlugin_DataUpdater DataUpdater
        {
            get
            {
                return dataUpdater;
            }
        }
    }
}