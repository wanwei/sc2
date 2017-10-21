using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy
{
    public class StrategyAssemblyScan
    {
        public IList<IStrategyAssembly> Scan(String path)
        {
            if (!Directory.Exists(path))
                return new List<IStrategyAssembly>();
            String[] files = Directory.GetFiles(path, "*.dll", SearchOption.AllDirectories);

            List<IStrategyAssembly> plugins = new List<IStrategyAssembly>();
            for (int i = 0; i < files.Length; i++)
            {
                string file = files[i];
                if (IsIgnoreDll(file))
                    continue;
                StrategyAssembly strategyAssembly = GetAssembly(file);
                if (strategyAssembly != null && strategyAssembly.GetAllStrategies().Count > 0)
                    plugins.Add(strategyAssembly);
            }
            return plugins;
        }

        public static StrategyAssembly GetAssembly(string file)
        {
            return StrategyAssembly.Create(file);
        }

        private static string[] ignoreDll = GetIgnoreDll();

        private static string[] GetIgnoreDll()
        {
            string[] strs = new string[] {
                "com.wer.sc.plugin.dll",
                "com.wer.sc.utils.dll",
                "log4net.dll" };
            return strs;
        }

        private static bool IsIgnoreDll(string file)
        {
            for (int i = 0; i < ignoreDll.Length; i++)
            {
                if (file.EndsWith(ignoreDll[i]))
                    return true;
            }
            return false;
        }
    }
}
