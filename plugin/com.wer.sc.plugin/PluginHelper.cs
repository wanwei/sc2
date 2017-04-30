using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin
{
    public class PluginHelper
    {
        private string pluginDirPath;

        private PluginInfo pluginInfo;

        public PluginHelper(PluginInfo pluginInfo)
        {
            this.pluginInfo = pluginInfo;

            this.pluginDirPath = new FileInfo(pluginInfo.PluginPath).DirectoryName;
        }

        public PluginInfo PluginInfo
        {
            get
            {
                return pluginInfo;
            }
        }

        public string PluginDirPath
        {
            get
            {
                return pluginDirPath;
            }
        }
    }
}
