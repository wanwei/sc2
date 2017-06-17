using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin.cnfutures.config
{
    public class VarietyInfo
    {
        private string code;

        private string name;

        private string exchange;

        public string Code
        {
            get
            {
                return code;
            }

            set
            {
                code = value;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        public string Exchange
        {
            get
            {
                return exchange;
            }

            set
            {
                exchange = value;
            }
        }
    }
}
