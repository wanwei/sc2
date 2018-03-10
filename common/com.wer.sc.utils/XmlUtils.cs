using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace com.wer.sc.utils
{
    public class XmlUtils
    {
        public static String ToString(IXmlExchange xmlexchange, string rootTag)
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlNode rootNode = xmlDoc.CreateElement(rootTag);
            xmlDoc.AppendChild(rootNode);

            xmlexchange.Save(xmlDoc.DocumentElement);
            return ToString(xmlDoc);
        }

        public static String ToString(IXmlExchange xmlexchange)
        {
            return ToString(xmlexchange, "root");
        }

        public static String[] ToStringArr(IXmlExchange_Multi xmlexchange, string[] rootTags)
        {
            int arrLen = xmlexchange.XmlElementCount;
            XmlElement[] elemArr = new XmlElement[arrLen];
            for (int i = 0; i < arrLen; i++)
            {
                XmlDocument xmlDoc = new XmlDocument();
                XmlNode rootNode = xmlDoc.CreateElement(rootTags[i]);
                xmlDoc.AppendChild(rootNode);
                elemArr[i] = (XmlElement)rootNode;
            }
            xmlexchange.Save(elemArr);
            string[] strArr = new string[arrLen];
            for (int i = 0; i < arrLen; i++)
            {
                XmlElement elem = elemArr[i];
                strArr[i] = ToString(elem.OwnerDocument);
            }
            return strArr;
        }

        public static String ToString(IXmlExchange_Multi xmlexchange, string[] rootTags)
        {
            string[] arr = ToStringArr(xmlexchange, rootTags);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < arr.Length; i++) {
                if (i != 0)
                    sb.Append("\r\n");
                sb.Append(arr[i]);
            }
            return sb.ToString();
        }

        public static String ToString(IXmlExchange_Multi xmlexchange_Multi)
        {
            string[] rootTags = new string[xmlexchange_Multi.XmlElementCount];
            for(int i = 0; i < rootTags.Length; i++)
            {
                rootTags[i] = "root";
            }
            return ToString(xmlexchange_Multi, rootTags);
        }

        public static string ToString(XmlDocument xmlDoc)
        {
            MemoryStream stream = new MemoryStream();
            XmlTextWriter writer = new XmlTextWriter(stream, null);
            writer.Formatting = Formatting.Indented;
            xmlDoc.Save(writer);
            StreamReader sr = new StreamReader(stream, System.Text.Encoding.UTF8);
            stream.Position = 0;
            string xmlString = sr.ReadToEnd();
            sr.Close();
            stream.Close();
            return xmlString;
        }
    }

    public interface IXmlExchange
    {
        /// <summary>
        /// 将数据保存为xml
        /// </summary>
        /// <param name="xmlElem"></param>
        void Save(XmlElement xmlElem);

        /// <summary>
        /// 从xml装载数据
        /// </summary>
        /// <param name="xmlElem"></param>
        void Load(XmlElement xmlElem);
    }

    public abstract class XmlExchange_Abstract : IXmlExchange
    {
        public abstract void Load(XmlElement xmlElem);

        public abstract void Save(XmlElement xmlElem);

        public override string ToString()
        {
            return XmlUtils.ToString(this);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public interface IXmlExchange_Multi
    {
        /// <summary>
        /// 将数据保存为xml
        /// </summary>
        /// <param name="xmlElems"></param>
        void Save(IList<XmlElement> xmlElems);

        /// <summary>
        /// 从xml装载数据
        /// </summary>
        /// <param name="xmlElems"></param>
        void Load(IList<XmlElement> xmlElems);

        /// <summary>
        /// 获得保存的Element数量
        /// </summary>
        int XmlElementCount { get; }
    }
}
