using com.wer.sc.data;
using com.wer.sc.data.utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin.cnfutures.config
{
    public class CodeInfoLoader
    {
        private PathUtils pathUtils;

        public CodeInfoLoader(string pluginPath)
        {
            this.pathUtils = new PathUtils(pluginPath);
        }

        public List<CodeInfo> LoadCodes()
        {
            if (File.Exists(pathUtils.GeneratedCodesPath))
                return CsvUtils_Code.Load(pathUtils.GeneratedCodesPath);
            return null;
        }
    }
}
