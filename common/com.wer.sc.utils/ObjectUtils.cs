using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.wer.sc.utils
{
    public enum ObjectType
    {
        BOOLEAN = 0,

        INTEGER = 1,

        LONG = 2,

        FLOAT = 3,

        DOUBLE = 4,

        STRING = 5,

        CUSTOM = 6
    }

    public class ObjectUtils
    {
        public static String ToString(Object obj)
        {
            if (obj == null)
                return "";
            return obj.ToString();
        }

        public static Object String2Object(String value, ObjectType type)
        {
            if (value == null || value == "")
                return null;

            switch (type)
            {
                case ObjectType.BOOLEAN:
                    return bool.Parse(value);
                case ObjectType.INTEGER:
                    return int.Parse(value);
                case ObjectType.LONG:
                    return long.Parse(value);
                case ObjectType.FLOAT:
                    return float.Parse(value);
                case ObjectType.DOUBLE:
                    return double.Parse(value);
                case ObjectType.STRING:
                    return value;
                default:
                    return null;
            }
        }

        //       public static Object string2ObjectType(String objTypeString)
        //       {
        //           objTypeString = objTypeString.Trim();
        //           if (objTypeString.Length == 1)
        //               return CharUtils.toChar(objTypeString);

        //           try
        //           {
        //               return Class.forName(objTypeString);
        //           }
        //           catch (Exception e)
        //           {
        //               return ObjectUtils.TYPE_STRING;
        //           }
        //       }

        //       public static Object string2ObjectThrowIfCanNotParse(String value, Object objType)
        //       {
        //           if (value == null)
        //               return null;
        //           if (objType is String)
        //               objType = String2ObjectType((String)objType);
        //           if (objType is Char)
        //           {
        //               char type = (Char)objType;
        //               switch (type)
        //               {
        //                   case TYPE_STRING:
        //                       return value;
        //                   case TYPE_INTEGER:
        //                       return int.Parse(value);
        //                   case TYPE_BOOLEAN:
        //                       return Boolean.Parse(value);
        //                   case TYPE_LONG:
        //                       return long.Parse(value);
        //                   case TYPE_FLOAT:
        //                       return float.Parse(value);
        //                   case TYPE_DOUBLE:
        //                       return Double.Parse(value);
        //                   case TYPE_BIGDECMAL:
        //                       return new Decimal(value);
        //                   default:
        //                       return null;
        //               }
        //           }
        //           if (objType is Class)
        //           {
        //               Class <?> cls = (Class <?>) objType;
        //               Object obj = cls.newInstance();
        //               if (obj is TextExchange)
        //               {
        //                   ((TextExchange)obj).LoadFromString(value);
        //                   return obj;
        //               }
        //               else if (obj is XmlExchange)
        //               {
        //                   XmlUtil.loadXmlExchange((XmlExchange)obj, value);
        //                   return obj;
        //               }
        //           }

        //           return null;
        //       }

        //       public static Object Object2Object(Object value, Object objType)
        //       {
        //           //TODO 为了效率，应该不全都转到str
        //           String str = StringUtils.obj2Str(value, "");
        //           return String2Object(str, objType);
        //       }

        //       public static Object String2Object(String value, Object objType)
        //       {
        //           if (value == null)
        //               return null;
        //           if (objType is String)
        //               objType = string2ObjectType((String)objType);
        //           if (objType instanceof Character) {
        //               char type = (Character)objType;
        //               switch (type)
        //               {
        //                   case TYPE_STRING:
        //                       return value;
        //                   case TYPE_INTEGER:
        //                       return NumberUtils.toInt(value);
        //                   case TYPE_LONG:
        //                       return NumberUtils.toLong(value);
        //                   case TYPE_FLOAT:
        //                       return NumberUtils.toFloat(value);
        //                   case TYPE_DOUBLE:
        //                       return NumberUtils.toDouble(value);
        //                   case TYPE_BOOLEAN:
        //                       return BooleanUtils.toBoolean(value);
        //                   case TYPE_BIGDECMAL:
        //                       return new BigDecimal(value);
        //                   default:
        //                       return null;
        //               }
        //           }
        //           if (objType instanceof Class) {
        //               Class <?> cls = (Class <?>) objType;
        //               if (objType == String.class)
        //			return value;
        //		try {
        //			Object obj = cls.newInstance();
        //			if (obj instanceof TextExchange) {
        //				((TextExchange) obj).loadFromString(value);
        //				return obj;
        //			}
        //			else if (obj instanceof XmlExchange) {
        //				XmlUtil.loadXmlExchange((XmlExchange) obj, value);
        //				return obj;
        //			}

        //		}
        //		catch (Exception e) {
        //			e.printStackTrace();
        //		}
        //	}
        //	return null;
        //}

        //   }
    }
}
