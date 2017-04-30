using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.ana
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ModelRegisterAttribute : Attribute
    {
        private String name;

        public ModelRegisterAttribute(String name)
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
