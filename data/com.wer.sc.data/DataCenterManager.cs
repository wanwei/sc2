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

namespace com.wer.sc.data
{
    /// <summary>
    /// 数据中心管理
    /// 可以注册多个数据中心
    /// </summary>
    public class DataCenterManager
    {
        private string defaultId;

        private List<DataCenterInfo> dataCenterInfos = new List<DataCenterInfo>();

        private Dictionary<string, DataCenterInfo> dic_Uri_Config = new Dictionary<string, DataCenterInfo>();

        private Dictionary<string, DataCenterInfo> dic_ID_Config = new Dictionary<string, DataCenterInfo>();

        private string configFilePath;

        private DataCenterManager(string configFilePath)
        {
            this.configFilePath = configFilePath;
            RefreshByConfig();
        }

        private void RefreshByConfig()
        {
            dataCenterInfos.Clear();
            dic_Uri_Config.Clear();
            dic_ID_Config.Clear();
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
            this.defaultId = root.GetAttribute("Default");
            XmlNodeList xmlNode_DataCenter = root.GetElementsByTagName("DataCenter");
            foreach (XmlNode node in xmlNode_DataCenter)
            {
                XmlElement elem = (XmlElement)node;
                DataCenterInfo config = new DataCenterInfo();
                config.LoadXml(elem);
                dataCenterInfos.Add(config);
                dic_Uri_Config.Add(config.Uri.ToUpper(), config);
                dic_ID_Config.Add(config.Id.ToUpper(), config);
            }
        }

        /// <summary>
        /// 创建数据装载保存器
        /// </summary>
        /// <param name="dataCenterUri"></param>
        /// <returns></returns>
        public DataCenter GetDataCenterByUri(string dataCenterUri)
        {
            if (dataCenterUri == null)
                return null;
            dataCenterUri = dataCenterUri.Replace(@"\", @"\");
            string upperUri = dataCenterUri.ToUpper();
            if (!dic_Uri_Config.ContainsKey(upperUri))
                return null;
            DataCenterInfo config = dic_Uri_Config[upperUri];
            DataCenter dc = DataCenter.Create(config);
            if (dc != null)
                return dc;
            throw new ArgumentException("传入的数据中心地址不正确:" + dataCenterUri);
        }

        public DataCenter GetDefaultDataCenter()
        {
            return GetDataCenterById(defaultId);
        }

        public DataCenter GetDataCenterById(string id)
        {
            string upperId = id.ToUpper();
            if (!dic_ID_Config.ContainsKey(upperId))
                return null;
            DataCenterInfo config = dic_ID_Config[upperId];
            DataCenter dc = DataCenter.Create(config);
            if (dc != null)
                return dc;
            throw new ArgumentException("传入的数据中心ID不存在:" + id);
        }

        /// <summary>
        /// 注册一个数据中心
        /// </summary>
        /// <param name="config"></param>
        public void RegisterDataCenter(DataCenterInfo config)
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
        public List<DataCenterInfo> GetAllConfig()
        {
            return this.dataCenterInfos;
        }

        public List<DataCenterInfo> GetConfigs(MarketType marketType)
        {
            List<DataCenterInfo> configs = new List<DataCenterInfo>();
            for (int i = 0; i < dataCenterInfos.Count; i++)
            {
                DataCenterInfo config = dataCenterInfos[i];
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