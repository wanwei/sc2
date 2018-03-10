using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy
{
    public class StrategyQueryResult : IStrategyQueryResult
    {
        private string name;

        private string[] titles;

        private ObjectType[] dataTypes;

        private IList<IStrategyQueryResultRow> rows = new List<IStrategyQueryResultRow>();

        public StrategyQueryResult()
        {
        }

        public StrategyQueryResult(string name, string[] titles, ObjectType[] dataTypes)
        {
            this.name = name;
            this.titles = titles;
            this.dataTypes = dataTypes;
        }

        public string Name
        {
            get
            {
                return this.name;
            }
        }

        public string[] Titles { get { return titles; } }

        public ObjectType[] DataTypes { get { return dataTypes; } }

        public IList<IStrategyQueryResultRow> Rows
        {
            get
            {
                return rows;
            }
        }


        /// <summary>
        /// 添加行
        /// </summary>
        /// <param name="code"></param>
        /// <param name="time"></param>
        /// <param name="values"></param>
        public void AddRow(string code, double time, object[] values)
        {
            StrategyQueryResultRow row = new StrategyQueryResultRow(code, time, values);
            this.rows.Add(row);
        }

        /// <summary>
        /// 删除行
        /// </summary>
        /// <param name="code"></param>
        /// <param name="time"></param>
        public void RemoveRow(string code, double time)
        {
            //TODO
        }

        public string SaveToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < titles.Length; i++)
            {
                if (i != 0)
                    sb.Append(",");
                sb.Append(titles[i]).Append("|");
                sb.Append(dataTypes[i]);
            }
            for (int i = 0; i < rows.Count; i++)
            {
                sb.Append("\r\n").Append(rows[i]);
            }
            return sb.ToString();
        }

        public void LoadFromString(string content)
        {

        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="path"></param>
        public void Save(string path)
        {
            File.WriteAllText(path, SaveToString());
        }

        public void Load(string path)
        {
            int endIndex = path.LastIndexOf('.');
            int startIndex = path.LastIndexOf('.', endIndex - 1) + 1;
            this.name = path.Substring(startIndex, endIndex - startIndex);

            string[] lines = File.ReadAllLines(path);
            string[] titleArr = lines[0].Split(',');
            this.titles = new string[titleArr.Length];
            this.dataTypes = new ObjectType[titleArr.Length];

            for (int i = 0; i < titleArr.Length; i++)
            {
                string headStr = titleArr[i];
                string[] arr = headStr.Split('|');
                titles[i] = arr[0];
                dataTypes[i] = (ObjectType)EnumUtils.Parse(typeof(ObjectType), arr[1]);
            }

            for (int i = 1; i < lines.Length; i++)
            {
                string line = lines[i];
                string[] arr = line.Split(',');
                this.rows.Add(ParseRow(dataTypes, arr));
            }
        }

        private IStrategyQueryResultRow ParseRow(ObjectType[] objTypes, string[] arr)
        {
            string code = arr[0];
            double time = double.Parse(arr[1]);
            object[] values = new object[arr.Length - 2];
            for (int i = 2; i < arr.Length; i++)
            {
                values[i - 2] = ObjectUtils.String2Object(arr[i], objTypes[i - 2]);
            }
            return new StrategyQueryResultRow(code, time, values);
        }
    }
}