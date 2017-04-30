using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.ana
{    
    [AttributeUsage(AttributeTargets.Class)]
    public class ZbRegisterAttribute : Attribute
    {
        private String name;

        public ZbRegisterAttribute(String name)
        {
            this.name = name;
        }

        public String Name
        {
            get
            {
                return name;
            }
        }
    }
}
