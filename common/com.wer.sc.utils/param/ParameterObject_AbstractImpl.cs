using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace com.wer.sc.utils.param
{
    //public class ParameterObjectImpl : ParameterObject
    //{
    //    protected Dictionary<String, Object> mapParams = new Dictionary<String, Object>();

    //    private Dictionary<String, ParameterMeta> mapMetas;

    //    public ParameterObjectImpl()
    //    {
    //        initDefaultValue();
    //    }

    //    private void initDefaultValue()
    //    {
    //        List<ParameterMeta> metas = GetParameterMetas();
    //        if (metas == null)
    //            return;
    //        foreach (ParameterMeta meta in metas)
    //            internalSetParameter(meta.getKey(), meta.getDefaultValue());
    //        doParameterChange(mapParams);
    //    }

    //    public void SetParameter(Dictionary<String, Object> parameters)
    //    {
    //        if (parameters != null)
    //        {
    //            foreach (string key in parameters.Keys)
    //            {
    //                internalSetParameter(key, parameters[key]);
    //            }
    //            doParameterChange(mapParams);
    //        }
    //    }

    //    public void SetParameter(String key, Object parameterValue)
    //    {
    //        internalSetParameter(key, parameterValue);
    //        doParameterChange(mapParams);
    //    }

    //    private void internalSetParameter(String key, Object parameterValue)
    //    {
    //        char type = getParameterMetaMap().get(key).getType();
    //        Object value = parameterValue;
    //        if (value is KeyValue)
    //            value = ((KeyValue)value).Key;
    //        this.mapParams.Add(key, ObjectUtils.Object2Object(value, type));
    //    }

    //    public Object GetParameter(String key)
    //    {
    //        return mapParams[key];
    //    }

    //    public override string ToString()
    //    {
    //        return getDescription();
    //    }

    //    public Dictionary<String, Object> GetParameterValues()
    //    {
    //        return mapParams;
    //    }

    //    public void ClearParameter()
    //    {
    //        mapParams.Clear();
    //        doParameterChange(mapParams);
    //    }

    //    public Dictionary<String, ParameterMeta> GetParameterMetaMap()
    //    {
    //        if (mapMetas == null)
    //        {
    //            List<ParameterMeta> metas = GetParameterMetas();
    //            mapMetas = new Dictionary<String, ParameterMeta>();
    //            foreach (ParameterMeta meta in metas)
    //            {
    //                mapMetas.Add(meta.getKey(), meta);
    //            }
    //        }
    //        return mapMetas;
    //    }

    //    public void SaveToXml(XmlElement elem)
    //    {
    //        Dictionary<String, ParameterMeta> map = GetParameterMetaMap();
    //        foreach (string key in mapParams.Keys)
    //        {
    //            ParameterMeta meta = map[key];
    //            Object value = mapParams[key];

    //            Element paramelem = elem.addElement("param");
    //            paramelem.addAttribute("key", key);
    //            paramelem.addAttribute("value", ObjectUtils.object2String(value));
    //            paramelem.addAttribute("type",
    //                    meta == null ? String.valueOf(ObjectUtils.objectType(value)) : String.valueOf(meta.getType()));
    //        }
    //    }

    //    /**
    //     * 从一个xml元素里装载数据
    //     * @param node
    //     */
    //    public void LoadFromXml(XmlElement elem)
    //    {
    //        List <?> elems = elem.elements();
    //        for (int i = 0; i < elems.size(); i++)
    //        {
    //            Element subelem = (Element)elems.get(i);
    //            if (subelem.getName().Equals("param"))
    //            {
    //                String key = subelem.attributeValue("key");
    //                String typestr = subelem.attributeValue("type");
    //                String value = subelem.attributeValue("value");
    //                char type = (typestr != null && typestr.length() > 0) ? typestr.charAt(0) : ObjectUtils.TYPE_STRING;
    //                mapParams.put(key, ObjectUtils.string2Object(value, type));
    //            }
    //        }
    //    }

    //    public override bool Equals(Object obj)
    //    {
    //        if (!this.GetType().Equals(obj.GetType()))
    //            return false;
    //        ParameterObject_AbstractImpl recognizer = (ParameterObject_AbstractImpl)obj;
    //        return MapUtils.mapEquals(this.mapParams, recognizer.mapParams);
    //    }

    //    public int hashCode()
    //    {
    //        int result = 1;
    //        result = 31 * result + this.getClass().getSimpleName().hashCode();
    //        for (Entry<String, Object> entry : mapParams.entrySet())
    //        {
    //            result = 31 * result + (entry.getKey() + entry.getValue()).hashCode();
    //        }
    //        return result;
    //    }

    //    public String GetDescription()
    //    {
    //        return MapUtils.toString(mapParams);
    //    }

    //    public ParameterObject CloneParam()
    //    {
    //        try
    //        {
    //            //ParameterObject_AbstractImpl obj = Activator.CreateInstance(GetType());
    //            //obj.mapParams.a(mapParams);
    //        }
    //        catch (Exception e)
    //        {

    //        }

    //        //return obj;
    //        return null;
    //    }

    //    /**
    //     * 该方法用于给基类重载，用于参数修改时通知子类
    //     * 比如Recognizer_PriceIncrease，当参数发生修改时要将map内的值重新初始化识别器对象
    //     * 
    //     * 该方法执行的时候对象还没有被初始化，所以有时一些在该方法里需要修改的对象需要在该方法内初始化，详见 {@link Recognizer_PriceIncrease#doParameterChange(Map)}里的注释
    //     * @param mapParameterValue
    //     */
    //    public void doParameterChange(Dictionary<String, Object> mapParameterValue)
    //    {

    //    }

    //    //public int getValue_Int(String key)
    //    //{
    //    //    return NumberUtils.toInt(mapParams.get(key), 0);
    //    //}

    //    //public int getValue_Int(String key, int defaultValue)
    //    //{
    //    //    return NumberUtils.toInt(mapParams.get(key), defaultValue);
    //    //}

    //    //public boolean getValue_Boolean(String key)
    //    //{
    //    //    Object obj = mapParams.get(key);
    //    //    if (obj == null)
    //    //        return false;
    //    //    return BooleanUtils.toBoolean(obj.toString());
    //    //}

    //    //public double getValue_Double(String key)
    //    //{
    //    //    return NumberUtils.toDouble(mapParams.get(key), 0);
    //    //}

    //    //public double getValue_Double(String key, double defaultValue)
    //    //{
    //    //    return NumberUtils.toDouble(mapParams.get(key), defaultValue);
    //    //}

    //    //public String getValue_String(String key)
    //    //{
    //    //    return StringUtils.obj2Str(mapParams.get(key), "");
    //    //}

    //    //public String getValue_String(String key, String defaultValue)
    //    //{
    //    //    return StringUtils.obj2Str(mapParams.get(key), defaultValue);
    //    //}
    //}
}
