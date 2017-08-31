using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace com.wer.sc.utils.param.impl
{
    public class Parameters : IParameters
    {
        private Dictionary<string, IParameter> dic_Key_Parameter = new Dictionary<string, IParameter>();

        private List<IParameter> parameters = new List<IParameter>();

        public void AddParameter(string key, string caption, string desc, ParameterType parameterType)
        {
            if (this.dic_Key_Parameter.ContainsKey(key))
                return;
            Parameter pm = new Parameter(key, caption, desc, parameterType);
            AddParameter(pm);
        }

        private void AddParameter(Parameter pm)
        {
            this.parameters.Add(pm);
            this.dic_Key_Parameter.Add(pm.Key, pm);
        }

        public void AddParameter(string key, string caption, string desc, ParameterType parameterType, object defaultValue)
        {
            Parameter pm = new Parameter(key, caption, desc, parameterType, defaultValue);
            AddParameter(pm);
        }

        public void AddParameter(string key, string caption, string desc, ParameterType parameterType, object defaultValue, IParameterOptions options)
        {
            Parameter pm = new Parameter(key, caption, desc, parameterType, defaultValue, options);
            AddParameter(pm);
        }

        public void AddParameterRange(List<IParameter> parameters)
        {
            this.parameters.AddRange(parameters);
        }

        public void ClearParameters()
        {
            this.parameters.Clear();
            this.dic_Key_Parameter.Clear();
        }

        public IParameters CloneParam()
        {
            throw new NotImplementedException();
        }

        public List<IParameter> GetAllParameters()
        {
            return parameters;
        }

        public int Count
        {
            get { return parameters.Count; }
        }

        public IParameter GetParameter(int index)
        {
            return this.parameters[index];
        }

        public IParameter GetParameter(string key)
        {
            if (dic_Key_Parameter.ContainsKey(key))
                return dic_Key_Parameter[key];
            return null;
        }

        public object GetParameterValue(string key)
        {
            IParameter param = GetParameter(key);
            if (param == null)
                return null;
            return param.Value;
        }

        public Dictionary<string, object> GetParameterValues()
        {
            Dictionary<string, object> dic_Key_Value = new Dictionary<string, object>();
            foreach (string key in dic_Key_Parameter.Keys)
            {
                dic_Key_Value.Add(key, dic_Key_Parameter[key].Value);
            }
            return dic_Key_Value;
        }

        public void RemoveParameter(string key)
        {
            if (!this.dic_Key_Parameter.ContainsKey(key))
                return;
            IParameter param = this.dic_Key_Parameter[key];
            this.dic_Key_Parameter.Remove(key);
            this.parameters.Remove(param);
        }

        public void SetParameterValue(Dictionary<string, object> parameters)
        {
            foreach (string key in parameters.Keys)
            {
                SetParameterValue(key, parameters[key]);
            }
        }

        public void SetParameterValue(string key, object parameterValue)
        {
            if (!this.dic_Key_Parameter.ContainsKey(key))
                return;            
            this.dic_Key_Parameter[key].Value = parameterValue;
        }

        public void Save(XmlElement xmlElem)
        {
            for (int i = 0; i < parameters.Count; i++)
            {
                XmlElement elem = xmlElem.OwnerDocument.CreateElement("parameter");
                xmlElem.AppendChild(elem);
                parameters[i].Save(elem);
            }
        }

        public void Load(XmlElement xmlElem)
        {
            XmlNodeList nodes = xmlElem.ChildNodes;
            for (int i = 0; i < nodes.Count; i++)
            {
                XmlNode node = nodes[i];
                if (node is XmlElement)
                {
                    Parameter param = Parameter.CreateParam((XmlElement)node);
                    AddParameter(param);
                }
            }
        }

        public override string ToString()
        {
            return XmlUtils.ToString(this, "parameters");
        }
    }
}
