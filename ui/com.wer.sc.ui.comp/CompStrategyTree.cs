using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using com.wer.sc.plugin;
using com.wer.sc.strategy;
using System.Reflection;

namespace com.wer.sc.ui.comp
{
    public partial class CompStrategyTree : UserControl
    {
        public CompStrategyTree()
        {
            InitializeComponent();

            IList<IStrategyAssembly> assemblies = StrategyMgrFactory.DefaultPluginMgr.GetAllStrategyAssemblies();
            for (int i = 0; i < assemblies.Count; i++)
            {
                IStrategyAssembly ass = assemblies[i];
                TreeNode treeNode = this.treeStrategy.Nodes.Add(ass.AssemblyName);
                AddSubNodesByAssembly(treeNode, ass);
            }
        }

        private void AddSubNodesByAssembly(TreeNode treeNode, IStrategyAssembly ass)
        {
            List<StrategyInfo> strategies = ass.GetAllStrategies();
            InitPathStrategies(strategies);


            List<string> pathList = dic_Path_Strategies.Keys.ToList();
            pathList.Sort();
            for (int i = 0; i < pathList.Count; i++)
            {
                string path = pathList[i];
                List<StrategyInfo> strategiesInPath = dic_Path_Strategies[path];
                TreeNode subNode = treeNode.Nodes.Add(path);
                AddStrategies(subNode, strategiesInPath);
            }
        }

        private void AddStrategies(TreeNode parentNode, List<StrategyInfo> strategies)
        {
            for (int i = 0; i < strategies.Count; i++)
            {
                StrategyInfo strategy = strategies[i];
                string name = strategy.StrategyName;
                TreeNode subNode = parentNode.Nodes.Add(name);
                subNode.Tag = strategy;
            }
        }

        private Dictionary<string, List<StrategyInfo>> dic_Path_Strategies = new Dictionary<string, List<StrategyInfo>>();

        private void InitPathStrategies(List<StrategyInfo> strategies)
        {
            for (int i = 0; i < strategies.Count; i++)
            {
                StrategyInfo strategy = strategies[i];
                string path = strategy.StrategyPath;
                if (!dic_Path_Strategies.ContainsKey(path))
                {
                    List<StrategyInfo> strategiesInPath = new List<StrategyInfo>();
                    dic_Path_Strategies.Add(path, strategiesInPath);
                    strategiesInPath.Add(strategy);
                }
                else
                {
                    dic_Path_Strategies[path].Add(strategy);
                }
            }
        }

        public TreeView TreeStrategy
        {
            get { return treeStrategy; }
        }
    }
}
