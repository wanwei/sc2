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

        private string catelogName;

        private int start;

        private int end;

        private string exchange;

        private string serverCode;

        private string shortCode;

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

        public string CatelogName
        {
            get
            {
                return catelogName;
            }

            set
            {
                catelogName = value;
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

        public string ServerCode
        {
            get
            {
                return serverCode;
            }

            set
            {
                serverCode = value;
            }
        }

        public string ShortCode
        {
            get
            {
                return shortCode;
            }

            set
            {
                shortCode = value;
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

        public CodeInfo(String code, String name, String catelog, string catelogName, int start, int end, string exchange, string servercode)
        {
            this.Code = code;
            this.Name = name;
            this.Catelog = catelog;
            this.CatelogName = catelogName;
            this.start = start;
            this.end = end;
            this.exchange = exchange;
            this.serverCode = servercode;
        }

        override
         public String ToString()
        {
            return Code + "," + Name + "," + Catelog + "," + CatelogName + ","
                + start + "," + end + "," + exchange + "," + serverCode + "," + shortCode;
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