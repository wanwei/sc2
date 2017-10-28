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
        private List<StrategyConfig> strategyConfigs = new List<StrategyConfig>();

        private string path;

        private string fullPath;

        private string name;

        private string description;

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
            get { return strategyConfigs; }
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
                this.strategyConfigs.Add(config);
            }
        }

    }
}
