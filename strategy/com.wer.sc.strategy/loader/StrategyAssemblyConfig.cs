using com.wer.sc.strategy.loader;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace com.wer.sc.strategy.loader
{
    /// <summary>
    /// 一个Assembly包策略的配置
    /// </summary>
    public abstract class StrategyAssemblyConfig
    {
        //索引所有策略信息
        private Dictionary<String, IStrategyInfo> dic_ClsName_Strategies = new Dictionary<string, IStrategyInfo>();

        //记录所有的策略信息
        private List<IStrategyInfo> allStrategyInfos = new List<IStrategyInfo>();

        //保存策略的所有路径
        //private List<string> strategyPaths = new List<string>();

        //得到路径下所有子路径
        private Dictionary<string, List<string>> dic_Path_SubPath = new Dictionary<string, List<string>>();

        //得到路径下所有策略
        private Dictionary<string, List<IStrategyInfo>> dic_Path_Strategies = new Dictionary<string, List<IStrategyInfo>>();

        //Assembly的路径
        private string path;

        //Assembly的全路径
        private string fullPath;

        //配置文件路径
        private string configPath;

        //该包的名称
        private string name;

        //Assembly的描述
        private string description;

        //Assembly的名称
        private string assemblyName;

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

        public string ConfigPath
        {
            get
            {
                return configPath;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
        }

        public string Description
        {
            get
            {
                return description;
            }
        }

        public List<IStrategyInfo> GetAllStrategies()
        {
            return this.allStrategyInfos;
        }

        public IStrategyInfo GetStrategyInfo(string strategyClsName)
        {
            if (dic_ClsName_Strategies.ContainsKey(strategyClsName))
                return dic_ClsName_Strategies[strategyClsName];
            return null;
        }

        public IList<IStrategyInfo> GetSubStrategyInfo(string path)
        {
            if (this.dic_Path_Strategies.ContainsKey(path))
                return dic_Path_Strategies[path];
            return null;
        }

        public IList<string> GetSubPath(string path)
        {
            if (this.dic_Path_SubPath.ContainsKey(path))
                return dic_Path_SubPath[path];
            return null;
        }

        public void Load(string file)
        {
            this.path = new FileInfo(file).Directory.FullName;
            XmlDocument doc = new XmlDocument();
            doc.Load(file);
            this.configPath = file;
            LoadXml(doc.DocumentElement);
        }

        private void LoadXml(XmlElement xmlElem)
        {
            XmlElement elem_Assembly = (XmlElement)xmlElem.GetElementsByTagName("Assembly")[0];
            string fileName = elem_Assembly.GetAttribute("file");
            this.assemblyName = fileName.Substring(0, fileName.LastIndexOf('.'));
            this.fullPath = this.path + "\\" + fileName;            
            this.name = elem_Assembly.GetAttribute("name");
            this.description = elem_Assembly.GetAttribute("description");
            LoadSubNodes(elem_Assembly, "");
        }

        private void LoadSubNodes(XmlNode parentnode, string path)
        {
            XmlNodeList nodes = parentnode.ChildNodes;
            this.dic_Path_Strategies.Add(path, new List<IStrategyInfo>());
            this.dic_Path_SubPath.Add(path, new List<string>());
            foreach (XmlNode node in nodes)
            {
                XmlElement xmlNode = (XmlElement)node;
                if (xmlNode.Name.Equals("Path"))
                {
                    string subPath = path + "\\" + xmlNode.GetAttribute("name");
                    this.dic_Path_SubPath[path].Add(subPath);
                    LoadSubNodes(xmlNode, subPath);
                }
                else if (xmlNode.Name.Equals("Strategy"))
                {
                    StrategyInfo config = new StrategyInfo();
                    config.strategyPath = path;
                    config.strategyAssembly = (IStrategyAssembly)this;
                    config.LoadXml(xmlNode);
                    this.allStrategyInfos.Add(config);
                    this.dic_ClsName_Strategies.Add(config.ClassName, config);
                    this.dic_Path_Strategies[path].Add(config);
                }
            }
        }
    }
}
