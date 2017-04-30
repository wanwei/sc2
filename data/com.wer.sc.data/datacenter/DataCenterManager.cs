using com.wer.sc.data.reader;
using com.wer.sc.data.store;
using com.wer.sc.data.store.file;
using com.wer.sc.plugin;
using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace com.wer.sc.data.datacenter
{
    /// <summary>
    /// 数据中心管理
    /// 可以注册多个数据中心
    /// </summary>
    public class DataCenterManager
    {
        private List<DataCenterConfig> dataCenterConfigs = new List<DataCenterConfig>();

        private Dictionary<string, DataCenterConfig> dic_Uri_Config = new Dictionary<string, DataCenterConfig>();

        private string configFilePath;

        private DataCenterManager(string configFilePath)
        {
            this.configFilePath = configFilePath;
            RefreshByConfig();
        }

        private void RefreshByConfig()
        {
            dataCenterConfigs.Clear();
            dic_Uri_Config.Clear();
            if (!File.Exists(configFilePath))
            {
                XmlDocument newdoc = new XmlDocument();
                XmlElement elem = newdoc.CreateElement("Config");
                newdoc.AppendChild(elem);
                newdoc.Save(configFilePath);
                return;
            }

            XmlDocument doc = new XmlDocument();
            doc.Load(configFilePath);
            XmlElement root = doc.DocumentElement;
            XmlNodeList xmlNode_DataCenter = root.GetElementsByTagName("DataCenter");
            foreach (XmlNode node in xmlNode_DataCenter)
            {
                XmlElement elem = (XmlElement)node;
                DataCenterConfig config = new DataCenterConfig();
                config.LoadXml(elem);
                dataCenterConfigs.Add(config);
                dic_Uri_Config.Add(config.Uri.ToUpper(), config);
            }
        }

        /// <summary>
        /// 创建数据装载保存器
        /// </summary>
        /// <param name="dataCenterUri"></param>
        /// <returns></returns>
        public DataCenter GetDataCenter(string dataCenterUri)
        {
            if (dataCenterUri == null)
                return null;
            dataCenterUri = dataCenterUri.Replace(@"\", @"\");
            string upperUri = dataCenterUri.ToUpper();
            if (!dic_Uri_Config.ContainsKey(upperUri))
                return null;
            DataCenterConfig config = dic_Uri_Config[upperUri];

            Uri uri = new Uri(upperUri);
            if (uri.IsFile)
            {
                IDataStore dataStore = DataStoreFactory.CreateDataStore(uri.LocalPath);
                IDataReader dataReader = DataReaderFactory.CreateDataReader(uri.LocalPath);
                return new DataCenter(config, dataStore, dataReader);
            }
            throw new ArgumentException("传入的数据中心地址不正确:" + dataCenterUri);
        }

        /// <summary>
        /// 注册一个数据中心
        /// </summary>
        /// <param name="config"></param>
        public void RegisterDataCenter(DataCenterConfig config)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(configFilePath);
            XmlElement elemDataCenter = doc.CreateElement("DataCenter");
            doc.DocumentElement.AppendChild(elemDataCenter);
            config.SaveXml(elemDataCenter);
            doc.Save(configFilePath);
            RefreshByConfig();
        }

        /// <summary>
        /// 注销一个数据中心
        /// </summary>
        /// <param name="config"></param>
        public void UnRegisterDataCenter(string uri)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(configFilePath);
            XmlNodeList dataCenterNodes = doc.DocumentElement.ChildNodes;
            for (int i = 0; i < dataCenterNodes.Count; i++)
            {
                XmlNode node = dataCenterNodes[i];
                if (node is XmlElement)
                {
                    XmlElement elem = (XmlElement)node;
                    if (elem.GetAttribute("Uri") == uri)
                    {
                        doc.DocumentElement.RemoveChild(node);
                        i--;
                    }
                }
            }

            doc.Save(configFilePath);
            RefreshByConfig();
        }

        /// <summary>
        /// 装载所有的数据中心配置信息
        /// </summary>
        /// <returns></returns>
        public List<DataCenterConfig> GetAllConfig()
        {
            return this.dataCenterConfigs;
        }

        public List<DataCenterConfig> GetConfigs(MarketType marketType)
        {
            List<DataCenterConfig> configs = new List<DataCenterConfig>();
            for (int i = 0; i < dataCenterConfigs.Count; i++)
            {
                DataCenterConfig config = dataCenterConfigs[i];
                if (config.MarketType == marketType)
                    configs.Add(config);
            }
            return configs;
        }

        private static DataCenterManager _instance = new DataCenterManager(ScConfig.Instance.ScPath + "\\config\\datacenter.config");

        public static DataCenterManager Instance
        {
            get
            {
                return _instance;
            }
        }

        public string ConfigFilePath
        {
            get
            {
                return configFilePath;
            }
        }

        public static DataCenterManager Create(string configFile)
        {
            return new DataCenterManager(configFile);
        }
    }
}