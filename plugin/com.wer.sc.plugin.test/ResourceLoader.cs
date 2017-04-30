using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin.test
{
    public class ResourceLoader
    {
        public static String GetTestOutputPath()
        {
            return GetTestOutputPath("");
        }

        public static String GetTestOutputPath(String path)
        {
            return @"d:\sctest\plugintest\" + path;
        }
    }
}
