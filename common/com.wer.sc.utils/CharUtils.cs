using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.utils
{
    public class CharUtils
    {
        public const int OBJTYPE_STRING = 1;

        public const int OBJTYPE_INT = 2;

        public const int OBJTYPE_LONG = 3;

        public const int OBJTYPE_FLOAT = 4;

        public const int OBJTYPE_DOUBLE = 5;

        public const int OBJTYPE_BOOLEAN = 6;

        public const int OBJTYPE_OBJECT = 7;

        public const String OBJTYPE_STRING_STR = "STRING";

        public const String OBJTYPE_INT_STR = "INT";

        public const String OBJTYPE_LONG_STR = "LONG";

        public const String OBJTYPE_FLOAT_STR = "FLOAT";

        public const String OBJTYPE_DOUBLE_STR = "DOUBLE";

        public const String OBJTYPE_BOOLEAN_STR = "BOOLEAN";

        public const String OBJTYPE_OBJECT_STR = "OBJECT";

        public static int strType2IntType(String type)
        {
            if (type.Equals(OBJTYPE_STRING_STR))
                return OBJTYPE_STRING;
            if (type.Equals(OBJTYPE_INT_STR))
                return OBJTYPE_INT;
            if (type.Equals(OBJTYPE_LONG_STR))
                return OBJTYPE_LONG;
            if (type.Equals(OBJTYPE_FLOAT_STR))
                return OBJTYPE_FLOAT;
            if (type.Equals(OBJTYPE_DOUBLE_STR))
                return OBJTYPE_DOUBLE;
            if (type.Equals(OBJTYPE_BOOLEAN_STR))
                return OBJTYPE_BOOLEAN;
            if (type.Equals(OBJTYPE_OBJECT_STR))
                return OBJTYPE_OBJECT;
            return -1;
        }

        public static int classType2IntType(Type cls)
        {
            if (cls == typeof(float))
                return OBJTYPE_FLOAT;
            if (cls == typeof(double))
                return OBJTYPE_DOUBLE;
            if (cls == typeof(int))
                return OBJTYPE_INT;
            if (cls == typeof(long))
                return OBJTYPE_LONG;
            if (cls == typeof(Boolean) || cls == typeof(bool))
                return OBJTYPE_BOOLEAN;
            if (cls == typeof(string) || cls == typeof(String))
                return OBJTYPE_STRING;
            return OBJTYPE_OBJECT;
        }

        public static Object getObjectByString(String objstr, int type)
        {
            if (type == OBJTYPE_STRING)
                return objstr;
            if (type == OBJTYPE_INT)
                return int.Parse(objstr);
            if (type == OBJTYPE_LONG)
                return long.Parse(objstr);
            if (type == OBJTYPE_FLOAT)
                return float.Parse(objstr);
            if (type == OBJTYPE_DOUBLE)
                return Double.Parse(objstr);
            if (type == OBJTYPE_BOOLEAN)
                return Boolean.Parse(objstr);
            if (type == OBJTYPE_OBJECT)
                return objstr;
            return null;
        }

        public static int compareObj(Object o1, Object o2)
        {
            Type cls = o1.GetType();
            Type cls2 = o2.GetType();
            if (!cls.Equals(cls2))
                return 0;
            if (cls == typeof(float))
                return ((float)o1).CompareTo((float)o2);
            if (cls == typeof(double))
                return ((Double)o1).CompareTo((Double)o2);
            if (cls == typeof(int))
                return ((int)o1).CompareTo((int)o2);
            if (cls == typeof(long))
                return ((long)o1).CompareTo((long)o2);
            if (cls == typeof(Boolean))
                return ((Boolean)o1).CompareTo((Boolean)o2);
            if (cls == typeof(String))
                return ((String)o1).CompareTo((String)o2);
            return 0;
        }

        public static Object getObjectByString(String objstr, Type type)
        {
            return getObjectByString(objstr, classType2IntType(type));
        }

        public static Object getObjectByString(String objstr, String objtype)
        {
            return getObjectByString(objstr, strType2IntType(objtype));
        }

        public static Type getClassByType(int objtype)
        {
            switch (objtype)
            {
                case OBJTYPE_STRING:
                    return typeof(String);
                case OBJTYPE_INT:
                    return typeof(int);
                case OBJTYPE_LONG:
                    return typeof(long);
                case OBJTYPE_FLOAT:
                    return typeof(float);
                case OBJTYPE_DOUBLE:
                    return typeof(Double);
                case OBJTYPE_BOOLEAN:
                    return typeof(Boolean);
                case OBJTYPE_OBJECT:
                    return typeof(Object);
                default:
                    break;
            }
            return null;
        }

        public static Type getClassByType(String objtype)
        {
            return getClassByType(strType2IntType(objtype));
        }

        public static MethodInfo getMethod(String className, String method, int[] argumentTypes)
        {
            Type[] args = new Type[argumentTypes.Length];
            for (int i = 0; i < args.Length; i++)
            {
                args[i] = getClassByType(argumentTypes[i]);
            }
            return getMethod(className, method, args);
        }

        public static MethodInfo getMethod(String className, String method, String[] argumentTypes)
        {
            Type[] args = new Type[argumentTypes.Length];
            for (int i = 0; i < args.Length; i++)
            {
                args[i] = getClassByType(argumentTypes[i]);
            }
            return getMethod(className, method, args);
        }

        private static MethodInfo getMethod(String className, String method, Type[] args)
        {
            try
            {
                Type c = Type.GetType(className);
                return c.GetMethod(method, args);
            }
            catch (Exception e)
            {
                // TODO Auto-generated catch block
                // e.Message
                LogHelper.Error(typeof(CharUtils), e);
            }
            return null;
        }

        public static String getClassField(Type cls, int fieldindex)
        {
            return getFieldByMethod(cls.GetMethods()[fieldindex]);
        }

        public static String[] getClassFields(Type cls)
        {
            MethodInfo[] methods = cls.GetMethods();
            List<String> fields = new List<String>();
            for (int i = 0; i < methods.Length; i++)
            {
                String fieldname = getFieldByMethod(methods[i]);
                if (!StringUtils.IsEmpty(fieldname))
                    fields.Add(fieldname);
            }
            return fields.ToArray();
        }

        public static Object[][] getClassFieldsWithType(Type cls)
        {
            MethodInfo[] methods = cls.GetMethods();
            List<Type> types = new List<Type>();
            List<String> fields = new List<String>();
            for (int i = 0; i < methods.Length; i++)
            {
                String fieldname = getFieldByMethod(methods[i]);
                if (!StringUtils.IsEmpty(fieldname))
                {
                    fields.Add(fieldname);
                    //types.Add(methods[i].GetParameters()[0]);
                }
            }
            Object[][] result = new Object[fields.Count()][];
            for (int i = 0; i < fields.Count(); i++)
            {
                result[i][0] = fields[i];
                result[i][1] = classType2IntType(types[i]);
            }
            return result;
        }

        private static String getFieldByMethod(MethodInfo method)
        {
            String fieldname = null;
            String name = method.Name;
            if (name.StartsWith("set"))
                fieldname = name.Substring(3, name.Length - 3);
            return fieldname;
        }

        public static Boolean containField(Object obj, String field)
        {
            FieldInfo fieldInfo = obj.GetType().GetField(field);
            return fieldInfo != null;
        }

        public static Object getValueFromObject(Object obj, String field)
        {
            FieldInfo fieldInfo = obj.GetType().GetField(field);
            return fieldInfo.GetValue(obj);
        }

        public static void setValue2Object(Object obj, String field, Object value)
        {
            FieldInfo fieldInfo = obj.GetType().GetField(field);
            if (fieldInfo != null)
                fieldInfo.SetValue(obj, value);
        }

        public static Dictionary<String, Object> getAllObjectFieldValue(Object obj)
        {
            Dictionary<String, Object> map = new Dictionary<String, Object>();
            FieldInfo[] fieldInfos = obj.GetType().GetFields();
            for (int i = 0; i < fieldInfos.Length; i++)
            {
                FieldInfo fieldInfo = fieldInfos[i];
                map.Add(fieldInfo.Name, fieldInfo.GetValue(obj));
            }
            return map;
        }

        public static String saveObjectToString(Object obj)
        {
            StringBuilder result = new StringBuilder();
            FieldInfo[] fieldInfos = obj.GetType().GetFields();
            //String[] fields = getClassFields(obj.GetType());
            for (int i = 0; i < fieldInfos.Length; i++)
            {
                //FieldInfo fieldInfo = fieldInfos[i];
                //fieldInfo
                //Object field = getValueFromObject(obj, fields[i]);
                //result.Append(field);
                //if (i != fields.Length - 1)
                //    result.Append(",");
            }
            return result.ToString();
        }

        public static void loadObjFromString(Object obj, String str)
        {
            String[] arr = str.Split(',');
            Object[][] objs = getClassFieldsWithType(obj.GetType());
            if (arr.Length != objs.Length)
                throw new ApplicationException("解析字符串和对象不相符");

            for (int i = 0; i < arr.Length; i++)
            {
                String field = objs[i][0].ToString();
                Object value = getObjectByString(arr[i], (int)objs[i][1]);
                setValue2Object(obj, field, value);
            }
        }

        public static String[] saveObjectToStringArr(Object obj)
        {
            String[] fields = getClassFields(obj.GetType());
            String[] result = new String[fields.Length];
            for (int i = 0; i < fields.Length; i++)
            {
                Object field = getValueFromObject(obj, fields[i]);
                result[i] = field.ToString();
            }
            return result;
        }

        public static String saveObjectFieldToString(Object obj, int fieldindex)
        {
            String field = getClassField(obj.GetType(), fieldindex);
            return getValueFromObject(obj, field).ToString();
        }

        public static void loadObjFromStringArr(Object obj, String[] arr)
        {
            Object[][] objs = getClassFieldsWithType(obj.GetType());
            if (arr.Length != objs.Length)
                throw new ApplicationException("解析字符串和对象不相符");

            for (int i = 0; i < arr.Length; i++)
            {
                String field = objs[i][0].ToString();
                Object value = getObjectByString(arr[i], (int)objs[i][1]);
                setValue2Object(obj, field, value);
            }
        }
    }
}
