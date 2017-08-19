using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace com.wer.sc.utils.param.impl
{
    public class ParameterObject : IXmlExchange
    {
        private ParameterType parameterType;

        private object value;

        public ParameterType ParameterType
        {
            get
            {
                return parameterType;
            }
        }


        public virtual object Value
        {
            get
            {
                return value;
            }

            set
            {
                this.value = value;
            }
        }

        protected ParameterObject() { }

        public ParameterObject(ParameterType type)
        {
            this.parameterType = type;
        }

        public ParameterObject(ParameterType type, object value)
        {
            this.parameterType = type;
            this.value = value;
        }

        public virtual void Save(XmlElement xmlElem)
        {
            xmlElem.SetAttribute("type", parameterType.ToString());
            if (this.value != null)
                xmlElem.SetAttribute("value", value.ToString());
        }

        public static ParameterObject Create(XmlElement xmlElem)
        {
            ParameterObject obj = new ParameterObject();
            obj.Load(xmlElem);
            return obj;
        }

        public virtual void Load(XmlElement xmlElem)
        {
            this.parameterType = (ParameterType)Enum.Parse(typeof(ParameterType), xmlElem.GetAttribute("type"));
            string valuestr = xmlElem.GetAttribute("value");
            if (valuestr != null)
                this.value = Parse(valuestr, ParameterType);
        }

        public static Object Parse(string str, ParameterType type)
        {
            switch (type)
            {
                case ParameterType.BOOLEAN:
                    return bool.Parse(str);
                case ParameterType.FLOAT:
                    return float.Parse(str);
                case ParameterType.INTEGER:
                    return int.Parse(str);
                case ParameterType.LIST:
                    return null;
                case ParameterType.OBJECT:
                    return str;
                case ParameterType.STRING:
                    return str;
            }
            return str;
        }

        public override string ToString()
        {
            return XmlUtils.ToString(this, "parameter");
        }
    }
}