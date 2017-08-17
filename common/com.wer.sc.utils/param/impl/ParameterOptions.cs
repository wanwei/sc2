using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace com.wer.sc.utils.param.impl
{
    /// <summary>
    /// 参数选项
    /// </summary>
    public class ParameterOptions : IParameterOptions
    {
        private ParameterType parameterType;

        private List<IParameterOption> options = new List<IParameterOption>();

        public ParameterOptions(ParameterType parameterType)
        {
            this.parameterType = parameterType;
        }

        public ParameterOptions(ParameterType parameterType, object[] values) : this(parameterType)
        {
            for (int i = 0; i < values.Length; i++)
            {
                this.AddOption(values[i]);
            }
        }

        /// <summary>
        /// 参数类型
        /// </summary>
        public ParameterType OptionType { get { return parameterType; } }

        public void AddOption(object value)
        {
            this.options.Add(new ParameterOption(parameterType, value));
        }

        /// <summary>
        /// 清空所有options
        /// </summary>
        public void ClearOptions()
        {
            this.options.Clear();
        }

        /// <summary>
        /// 参数的所有options
        /// </summary>
        public List<IParameterOption> Options
        {
            get
            {
                return this.options;
            }
        }

        public void Save(XmlElement xmlElem)
        {
            xmlElem.SetAttribute("type", parameterType.ToString());
            for (int i = 0; i < options.Count; i++)
            {
                IParameterOption option = options[i];
                XmlElement elemOption = xmlElem.OwnerDocument.CreateElement("option");
                option.Save(elemOption);
                xmlElem.AppendChild(elemOption);
            }
        }

        public void Load(XmlElement xmlElem)
        {
            this.parameterType = (ParameterType)Enum.Parse(typeof(ParameterType), xmlElem.GetAttribute("type"));
            XmlNodeList nodes = xmlElem.ChildNodes;
            for (int i = 0; i < nodes.Count; i++)
            {
                XmlNode node = nodes[i];
                if (node is XmlElement)
                {
                    XmlElement elem = (XmlElement)node;
                    this.options.Add(ParameterOption.CreateOption(elem));
                }
            }
        }
    }
}