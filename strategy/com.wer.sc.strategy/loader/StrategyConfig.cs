using com.wer.sc.utils.param;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace com.wer.sc.strategy.loader
{
    public abstract class StrategyConfig
    {
        internal string strategyPath;

        private string className;

        private string name;

        private string desc;

        private bool isError;

        private string errorInfo;

        private IParameters parameters;

        /// <summary>
        /// 策略对应的类
        /// </summary>
        public string ClassName
        {
            get
            {
                return className;
            }
        }

        /// <summary>
        /// 策略的名称
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
        }

        /// <summary>
        /// 策略的描述
        /// </summary>
        public string Description
        {
            get
            {
                return desc;
            }
        }

        /// <summary>
        /// 策略的默认参数
        /// </summary>
        public IParameters Parameters
        {
            get
            {
                return parameters;
            }
        }

        public bool IsError
        {
            get
            {
                return isError;
            }

            set
            {
                isError = value;
            }
        }

        public string ErrorInfo
        {
            get
            {
                return errorInfo;
            }

            set
            {
                errorInfo = value;
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

        public string StrategyPath
        {
            get
            {
                return strategyPath;
            }
        }
    }
}
