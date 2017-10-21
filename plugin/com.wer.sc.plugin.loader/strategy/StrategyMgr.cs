using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy
{
    public class StrategyMgr : IStrategyAssemblyMgr
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

        /// <summary>
        /// 刷新所有assembly
        /// </summary>
        public void Refresh()
        {
            for (int i = 0; i < assemblies.Count; i++)
            {
                Refresh(assemblies[i]);
            }
        }

        /// <summary>
        /// 刷新一个assembly
        /// </summary>
        /// <param name="ass"></param>
        public void Refresh(IStrategyAssembly ass)
        {
            int index = this.assemblies.IndexOf(ass);
            this.assemblies.Remove(ass);
            string path = ass.FullPath;
            StrategyAssembly newAss = StrategyAssembly.Create(path);
            if (newAss != null)
                this.assemblies.Insert(index, newAss);
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
