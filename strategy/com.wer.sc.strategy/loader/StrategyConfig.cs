using com.wer.sc.utils.param;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace com.wer.sc.strategy.loader
{
    public class StrategyConfig
    {
        private string className;

        private string name;

        private string desc;

        private IParameters parameters;

        public string ClassName
        {
            get
            {
                return className;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
        }

        public string Desc
        {
            get
            {
                return desc;
            }
        }

        public IParameters Parameters
        {
            get
            {
                return parameters;
            }
        }

        public void Load(string file)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(file);
            LoadXml(doc.DocumentElement);
        }

        public void LoadXml(XmlElement xmlElem)
        {
            this.name = xmlElem.GetAttribute("name");
            this.className = xmlElem.GetAttribute("class");
            this.desc = xmlElem.GetAttribute("desc");
            XmlNodeList nodeList = xmlElem.GetElementsByTagName("parameters");
            if (nodeList.Count != 0)
            {
                this.parameters = ParameterFactory.CreateParameters();
                this.parameters.Load((XmlElement)nodeList[0]);
            }          
        }
    }
}
