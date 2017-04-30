using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.utils
{
    public class ScConfig
    {
        private string scPath;

        private ScConfig()
        {
            this.scPath = Environment.CurrentDirectory;
        }

        private static ScConfig _instance = new ScConfig();

        public static ScConfig Instance
        {
            get { return _instance; }
        }

        public string ScPath
        {
            get
            {
                return scPath;
            }
        }
    }
}
