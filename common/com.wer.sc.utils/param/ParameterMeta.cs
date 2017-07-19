using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace com.wer.sc.utils.param
{
    public class ParameterMeta //: IXmlExchange
    {


        public const char TYPE_BOOLEAN = ObjectUtils.TYPE_BOOLEAN;

        public const char TYPE_INTEGER = ObjectUtils.TYPE_INTEGER;

        public const char TYPE_DOUBLE = ObjectUtils.TYPE_DOUBLE;

        public const char TYPE_STRING = ObjectUtils.TYPE_STRING;

        public const char TYPE_FLOAT = ObjectUtils.TYPE_FLOAT;

        public const char TYPE_LONG = ObjectUtils.TYPE_LONG;

        private String key;

        private String caption;

        private char type;

        private Object defaultValue;

        private Object option;

        public ParameterMeta(String key, String caption, char type) : this(key, caption, type, null)
        {

        }

        public ParameterMeta(String key, String caption, char type, Object defaultValue) : this(key, caption, type, defaultValue, null)
        {

        }

        public ParameterMeta(String key, String caption, char type, Object defaultValue, Object options)
        {
            this.key = key;
            this.caption = caption;
            this.type = type;
            this.defaultValue = defaultValue;
            //setOption(options);
        }

        public String getKey()
        {
            return key;
        }

        public void setKey(String key)
        {
            this.key = key;
        }

        public String getCaption()
        {
            return caption;
        }

        public void setCaption(String caption)
        {
            this.caption = caption;
        }

        public char getType()
        {
            return type;
        }

        public void setType(char type)
        {
            this.type = type;
        }

        public Object getDefaultValue()
        {
            return defaultValue;
        }

        public void setDefaultValue(Object defaultValue)
        {
            this.defaultValue = defaultValue;
        }

        /**
         * option对象
         * @return
         */
        public Object getOption()
        {
            return option;
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
