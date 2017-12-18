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
    public class StrategyAssemblyConfig
    {
        private List<StrategyConfig> allStrategyConfigs = new List<StrategyConfig>();

        //保存策略的所有路径
        private List<string> strategyPaths = new List<string>();

        //得到路径下所有子路径
        private Dictionary<string, List<string>> dic_Path_SubPath = new Dictionary<string, List<string>>();

        //得到路径下所有策略
        private Dictionary<string, List<StrategyConfig>> dic_Path_Strategies = new Dictionary<string, List<StrategyConfig>>();

        //Assembly的路径
        private string path;

        //Assembly的全路径
        private string fullPath;

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

        public List<StrategyConfig> StrategyConfigs
        {
            get { return allStrategyConfigs; }
        }

        public void Load(string file)
        {
            this.path = new FileInfo(file).Directory.FullName;
            XmlDocument doc = new XmlDocument();
            doc.Load(file);
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
            XmlNodeList nodes = elem_Assembly.GetElementsByTagName("Strategy");
            foreach (XmlNode node in nodes)
            {
                XmlElement xmlElem_Strategy = (XmlElement)node;
                StrategyConfig config = new StrategyConfig();
                config.LoadXml(xmlElem_Strategy);
                this.allStrategyConfigs.Add(config);
            }
        }

    }
}
