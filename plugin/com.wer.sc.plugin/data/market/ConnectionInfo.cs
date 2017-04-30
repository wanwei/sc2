using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.market
{
    /// <summary>
    /// 市场连接信息
    /// 市场根据该类连接到指定服务器
    /// </summary>
    public class ConnectionInfo : ICloneable
    {
        public Dictionary<string, string> Data = new Dictionary<string, string>();

        /// <summary>
        /// 连接ID
        /// </summary>
        public string Id
        {
            get
            {
                string id = null;
                Data.TryGetValue("ID", out id);
                return id;
            }
        }

        /// <summary>
        /// 设置或获取连接名称
        /// </summary>
        public string Name
        {
            get
            {
                string name = null;
                Data.TryGetValue("NAME", out name);
                return name;
            }
        }

        /// <summary>
        /// 设置或获取连接描述
        /// </summary>
        public string Description
        {
            get
            {
                string desc = null;
                Data.TryGetValue("DESC", out desc);
                return desc;
            }
        }

        public void AddValue(string key, string value)
        {
            if (key == null)
                return;
            this.Data.Add(key.ToUpper(), value);
        }

        public string GetValue(string key)
        {
            if (key == null)
                return null;
            return this.Data[key.ToUpper()];
        }

        public bool ContainsKey(string key)
        {
            return Data.ContainsKey(key);
        }

        public static ConnectionInfo LoadFromPath(string path)
        {
            String[] lines = File.ReadAllLines(path);
            return LoadByLines(lines);
        }

        public static ConnectionInfo LoadFrom(string txt)
        {
            string[] lines = txt.Split('\r');
            return LoadByLines(lines);
            //return JsonUtils.FromJsonTo<ConnectionInfo>(txt);
        }

        private static ConnectionInfo LoadByLines(string[] lines)
        {
            ConnectionInfo conn = new ConnectionInfo();
            for (int i = 0; i < lines.Length; i++)
            {
                String line = lines[i].Trim();
                if (line.Equals(""))
                    continue;
                int splitIndex = line.IndexOf(':');
                if (splitIndex < 0)
                    continue;
                //String[] dataArr = line.Split(':');
                string key = line.Substring(0, splitIndex).ToUpper();
                string value = line.Substring(splitIndex + 1);
                conn.Data.Add(key, value);
            }
            return conn;
        }

        public override string ToString()
        {
            string[] keies = Data.Keys.ToArray<string>();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < keies.Length; i++)
            {
                string key = keies[i];
                if (i != 0)
                    sb.Append("\r\n");
                sb.Append(key).Append(":").Append(Data[key]);
            }
            return sb.ToString();
            //return JsonUtils.ToJsJson(this);
        }

        public object Clone()
        {
            ConnectionInfo conn = new ConnectionInfo();
            string[] keies = Data.Keys.ToArray<string>();
            for (int i = 0; i < keies.Length; i++)
            {
                string key = keies[i];
                conn.Data.Add(key, Data[key]);
            }
            return conn;
        }
    }
}
