using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy
{
    public class StrategyMgr : IStrategyMgr
    {
        private IList<IStrategyAssembly> assemblies;
        private string path;

        public StrategyMgr(string path)
        {
            this.path = path;
        }

        public IList<IStrategyAssembly> GetAllStrategyAssemblies()
        {
            return assemblies;
        }

        public IStrategyAssembly GetStrategyAssembly(string assemblyName)
        {
            foreach (IStrategyAssembly ass in assemblies)
            {
                if (ass.AssemblyName == assemblyName)
                    return ass;
            }
            return null;
        }

        public IList<IStrategyAssembly> SearchStrategyInfo(string strategyName)
        {
            return null;
        }

        internal void Load()
        {
            StrategyAssemblyScan scan = new StrategyAssemblyScan();
            this.assemblies = scan.Scan(this.path);
        }
    }
}
