using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.utils;
using System.IO;

namespace com.wer.sc.strategy
{
    public abstract class StrategyQueryResultAbstract : IStrategyQueryResult
    {
        public abstract ObjectType[] DataTypes
        {
            get;            
        }

        public abstract string Name
        {
            get;
        }

        public abstract IList<IStrategyQueryResultRow> Rows
        {
            get;
        }

        public abstract string[] Titles
        {
            get;
        }

        public virtual void AddRow(string code, double time, object[] values)
        {
            
        }

        public void RemoveRow(string code, double time)
        {
            
        }

        protected string SaveToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < Titles.Length; i++)
            {
                if (i != 0)
                    sb.Append(",");
                sb.Append(Titles[i]).Append("|");
                sb.Append(DataTypes[i]);
            }
            for (int i = 0; i < Rows.Count; i++)
            {
                sb.Append("\r\n").Append(Rows[i]);
            }
            return sb.ToString();
        }

        public void Save(string path)
        {
            File.WriteAllText(path, SaveToString());
        }

        public void Load(string path)
        {

        }
    }
}
