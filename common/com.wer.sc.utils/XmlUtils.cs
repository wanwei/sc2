﻿using System;
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
        public static String ToString(IXmlExchange xmlexchange)
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlNode rootNode = xmlDoc.CreateElement("root");
            xmlDoc.AppendChild(rootNode);

            xmlexchange.Save(xmlDoc.DocumentElement);
            return ToString(xmlDoc);
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
}