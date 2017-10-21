using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy
{
    public class StrategyResult_Single : IStrategyQueryResult_Single
    {
        private string code;

        private double time;

        private string name;

        private string description;

        public StrategyResult_Single(string code, double time)
        {
            this.code = code;
            this.time = time;
        }

        public StrategyResult_Single(string code, double time, string name, string description)
        {
            this.code = code;
            this.time = time;
            this.name = name;
            this.description = description;
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

        public string Name
        {
            get
            {
                return name;
            }
        }

        public string Description
        {
            get
            {
                return description;
            }
        }

    }
}
