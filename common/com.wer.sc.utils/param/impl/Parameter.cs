using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace com.wer.sc.utils.param.impl
{
    public class Parameter : ParameterObject, IParameter
    {
        private String key;

        private String caption;

        private string description;

        private Object defaultValue;

        private IParameterOptions options;

        public string Key
        {
            get
            {
                return key;
            }
        }

        public string Caption
        {
            get
            {
                return caption;
            }
        }
        public string Description
        {
            get
            {
                return description;
            }
        }

        public object DefaultValue
        {
            get
            {
                return defaultValue;
            }
        }

        public IParameterOptions Options
        {
            get
            {
                return options;
            }
        }

        protected Parameter()
        {

        }

        public Parameter(String key, String caption, string description, ParameterType type) : this(key, caption, description, type, null)
        {

        }

        public Parameter(String key, String caption, string description, ParameterType type, Object defaultValue) : this(key, caption, description, type, defaultValue, null)
        {

        }

        public Parameter(String key, String caption, string description, ParameterType type, Object defaultValue, IParameterOptions options) : base(type)
        {
            this.key = key;
            this.caption = caption;
            this.description = description;
            this.defaultValue = defaultValue;
            this.options = options;
        }


        public override object Value
        {
            get
            {
                object value = base.Value;
                if (value == null)
                    return DefaultValue;
                return base.Value;
            }

            set
            {
                base.Value = value;
            }
        }
        public override void Save(XmlElement xmlElem)
        {
            base.Save(xmlElem);
            xmlElem.SetAttribute("key", key);
            xmlElem.SetAttribute("caption", caption);
            if (description != null)
                xmlElem.SetAttribute("description", description);
            xmlElem.SetAttribute("defaultValue", StringUtils.obj2Str(defaultValue, ""));
            if (options != null)
            {
                XmlElement elemOption = xmlElem.OwnerDocument.CreateElement("option");
                this.options.Save(elemOption);
                xmlElem.AppendChild(elemOption);
            }
        }

        public override void Load(XmlElement xmlElem)
        {
            base.Load(xmlElem);
            this.key = xmlElem.GetAttribute("key");
            this.caption = xmlElem.GetAttribute("caption");
            this.description = xmlElem.GetAttribute("description");
            this.defaultValue = Parse(xmlElem.GetAttribute("defaultValue"), this.ParameterType);
            XmlNodeList nodes = xmlElem.GetElementsByTagName("option");
            if (nodes != null && nodes.Count > 0)
            {
                this.options = new ParameterOptions(this.ParameterType);
                this.options.Load((XmlElement)nodes[0]);
            }
        }

        public static Parameter CreateParam(XmlElement xmlElem)
        {
            Parameter obj = new Parameter();
            obj.Load(xmlElem);
            return obj;
        }

        //public void setOption(Object option)
        //{
        //    if (option is KeyValue[])
        //    {
        //        KeyValue[] keyValues = (KeyValue[])option;
        //        foreach (KeyValue keyvalue in keyValues)
        //        {
        //            keyvalue.setKey(ObjectUtils.Object2Object(keyvalue.getKey(), this.type));
        //        }
        //    }
        //    else if (option is Object[])
        //    {
        //        Object[] op = (Object[])option;
        //        for (int i = 0; i < op.Length; i++)
        //        {
        //            op[i] = ObjectUtils.Object2Object(op[i], type);
        //        }
        //    }
        //    else if (option != null)
        //        throw new ScException("传入的option不合法");
        //    this.option = option;
        //}

        //public String toString()
        //{
        //    return key + UtilsConst.getSeparator_Normal() + caption + UtilsConst.getSeparator_Normal() + type
        //            + UtilsConst.getSeparator_Normal() + StringUtils.obj2Str(option, "");
        //}

        //public void saveToXml(XmlElement elem)
        //{
        //    elem.SetAttribute("key", key);
        //    elem.SetAttribute("type", type.ToString());
        //    elem.SetAttribute("title", caption);
        //    elem.SetAttribute("defaultvalue", StringUtils.obj2Str(defaultValue, ""));
        //    elem.SetAttribute("option", getOptionStr());
        //}

        //private String getOptionStr()
        //{
        //    StringBuilder sb = new StringBuilder();
        //    if (option is KeyValue[])
        //    {
        //        KeyValue[] keyValues = (KeyValue[])option;
        //        for (int i = 0; i < keyValues.Length; i++)
        //        {
        //            if (i != 0)
        //                sb.Append(";");
        //            KeyValue keyvalue = keyValues[i];
        //            sb.Append(keyvalue.getKey()).Append(":").Append(keyvalue.getValue());
        //        }
        //    }
        //    else if (option is Object[])
        //    {
        //        Object[] op = (Object[])option;
        //        for (int i = 0; i < op.Length; i++)
        //        {
        //            if (i != 0)
        //                sb.Append(";");
        //            sb.Append(ObjectUtils.Object2String(op[i]));
        //        }
        //    }
        //    return sb.ToString();
        //}

        //public void loadFromXml(Element elem)
        //{

        //}
    }
}