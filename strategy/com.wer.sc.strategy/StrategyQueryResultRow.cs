using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace com.wer.sc.strategy
{
    public class StrategyQueryResultRow : IStrategyQueryResultRow
    {
        private string code;

        private double time;

        private IList<object> values;

        public StrategyQueryResultRow(string code, double time, IList<object> values)
        {
            this.code = code;
            this.time = time;
            this.values = values;
        }

        public string Code
        {
            get
            {
                return code;
            }
        }

        public double Time
        {
            get
            {
                return time;
            }
        }

        public IList<object> Data
        {
            get
            {
                return values;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(code).Append(",").Append(time);
            for(int i = 0; i < Data.Count; i++)
            {
                sb.Append(",").Append(Data[i]);
            }
            return sb.ToString();
        }
    }
}