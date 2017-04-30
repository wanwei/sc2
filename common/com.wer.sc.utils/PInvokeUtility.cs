using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace com.wer.sc.utils
{
    public class PInvokeUtility
    {
        static Encoding encodingGB2312 = Encoding.GetEncoding(936);

        public static string GetUnicodeString(byte[] str)
        {

            if (str == null)
            {
                return "";
            }

            byte[] unicodeStr = Encoding.Convert(encodingGB2312, Encoding.Unicode, str);

            return Encoding.Unicode.GetString(unicodeStr).TrimEnd('\0');
        }

        internal static T GetObjectFromIntPtr<T>(IntPtr handler)
        {
            try
            {
                if (handler == IntPtr.Zero)
                {
                    return default(T);
                }
                else
                {
                    return (T)Marshal.PtrToStructure(handler, typeof(T));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal static object GetObjectFromIntPtr(Type t, IntPtr handler)
        {
            try
            {
                if (handler == IntPtr.Zero)
                {
                    return null;
                }
                else
                {
                    return Marshal.PtrToStructure(handler, t);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
