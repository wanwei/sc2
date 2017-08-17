using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace com.wer.sc.utils.param.impl
{
    public class ParameterOption : ParameterObject, IParameterOption
    {
        protected ParameterOption() : base()
        {

        }

        public ParameterOption(ParameterType type, object value) : base(type, value)
        {
        }

        public static ParameterOption CreateOption(XmlElement xmlElem)
        {
            ParameterOption obj = new ParameterOption();
            obj.Load(xmlElem);
            return obj;
        }

        //public override bool Equals(Object value)
        //{
        //    if (value == null || (!(value is ParameterOption)))
        //        return false;
        //    ParameterOption keyvalue = (ParameterOption)value;
        //    if (keyvalue.Key == null)
        //        return this.Key == null ? true : false;
        //    return this.Key == null ? false : this.Key.Equals(keyvalue.Key);
        //}

        //public override string ToString()
        //{
        //    return StringUtils.obj2Str(Value, "");
        //}

        //public override int GetHashCode()
        //{
        //    if (this.Key == null)
        //        return 0;
        //    return this.Key.GetHashCode();
        //}

        //public int CompareTo(IParameterOption other)
        //{
        //    if (other == null)
        //        return 1;
        //    if (other.Key == null)
        //        return this.Key == null ? 0 : 1;
        //    if (this.Key == null)
        //        return -1;
        //    //return ExpUtils.compareObject(this.key, o.key);
        //    throw new NotImplementedException();
        //}
    }
}