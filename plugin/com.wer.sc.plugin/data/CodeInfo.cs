using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data
{
    /// <summary>
    /// 股票或期货信息的实现类
    /// </summary>
    public class CodeInfo : ICodeInfo
    {
        private string code;

        private String name;

        private string catelog;

        private int start;

        private int end;

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

        public string Catelog
        {
            get
            {
                return catelog;
            }

            set
            {
                catelog = value;
            }
        }

        public int Start
        {
            get
            {
                return start;
            }

            set
            {
                start = value;
            }
        }

        public int End
        {
            get
            {
                return end;
            }

            set
            {
                end = value;
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

        public CodeInfo()
        {

        }

        public CodeInfo(String code, String name, String catelog)
        {
            this.Code = code;
            this.Catelog = catelog;
            this.Name = name;
        }

        public CodeInfo(String code, String name, String catelog, int start, int end, string exchange)
        {
            this.Code = code;
            this.Catelog = catelog;
            this.Name = name;
            this.start = start;
            this.end = end;
            this.exchange = exchange;
        }

        override
         public String ToString()
        {
            return Code + "," + Name + "," + Catelog + "," + start + "," + end + "," + exchange;
        }

        public override bool Equals(object obj)
        {
            return this.ToString().Equals(obj.ToString());
        }

        public override int GetHashCode()
        {
            return Code.GetHashCode();
        }
    }
}