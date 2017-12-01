using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy
{
    [AttributeUsage(AttributeTargets.Class)]
    public class StrategyAttribute : Attribute
    {
        private string id;

        private String name;

        private string desc;

        private string path;

        public StrategyAttribute(string id, String name, string desc)
        {
            this.id = id;
            this.name = name;
            this.desc = desc;
        }

        public StrategyAttribute(string id, String name, string desc, string path)
        {
            this.id = id;
            this.name = name;
            this.desc = desc;
            this.path = path;
        }

        public string ID
        {
            get
            {
                return id;
            }
        }

        public String Name
        {
            get
            {
                return name;
            }
        }

        public string Desc
        {
            get
            {
                return desc;
            }
        }

        public string Path
        {
            get
            {
                return path;
            }
        }
    }
}
