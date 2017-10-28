using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy.loader
{
    public class StrategyAssemblyScan
    {
        public IList<IStrategyAssembly> Scan(String path)
        {
            if (!Directory.Exists(path))
                return new List<IStrategyAssembly>();

            String[] configfiles = Directory.GetFiles(path, "*.strategyconfig", SearchOption.AllDirectories);
            List<IStrategyAssembly> plugins = new List<IStrategyAssembly>();
            for (int i = 0; i < configfiles.Length; i++)
            {
                string file = configfiles[i];
                StrategyAssemblyConfig config = new StrategyAssemblyConfig();
                config.Load(file);
                StrategyAssembly strategyAssembly = StrategyAssembly.Create(config);
                plugins.Add(strategyAssembly);
            }
            return plugins;
        }        
    }
}
