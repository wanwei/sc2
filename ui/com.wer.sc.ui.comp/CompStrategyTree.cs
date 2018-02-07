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
            RefreshTree();
        }

        public void RefreshTree()
        {
            try
            {
                IList<IStrategyAssembly> assemblies = null;// StrategyMgrFactory.DefaultPluginMgr.GetAllStrategyAssemblies();
                if (assemblies == null)
                    return;
                this.treeStrategy.Nodes.Clear();
                for (int i = 0; i < assemblies.Count; i++)
                {
                    IStrategyAssembly ass = assemblies[i];
                    TreeNode treeNode = this.treeStrategy.Nodes.Add(ass.AssemblyName);
                    CompStrategyTreeBuilder builder = new CompStrategyTreeBuilder(treeStrategy);
                    builder.AddSubNodesByAssembly(treeNode, ass);
                }
            }
            catch (Exception e)
            {

            }
        }

        public TreeView TreeStrategy
        {
            get { return treeStrategy; }
        }
    }

    class CompStrategyTreeBuilder
    {
        private TreeView treeStrategy;

        public CompStrategyTreeBuilder(TreeView treeView)
        {
            this.treeStrategy = treeView;
        }

        public void AddSubNodesByAssembly(TreeNode treeNode, IStrategyAssembly ass)
        {
            List<IStrategyInfo> strategies = ass.GetAllStrategies();
            InitPathStrategies(strategies);

            List<string> pathList = dic_Path_Strategies.Keys.ToList();
            pathList.Sort();
            for (int i = 0; i < pathList.Count; i++)
            {
                string path = pathList[i];
                List<IStrategyInfo> strategiesInPath = dic_Path_Strategies[path];
                TreeNode subNode = treeNode.Nodes.Add(path);
                AddStrategies(subNode, strategiesInPath);
            }
        }

        private void AddStrategies(TreeNode parentNode, List<IStrategyInfo> strategies)
        {
            for (int i = 0; i < strategies.Count; i++)
            {
                IStrategyInfo strategy = strategies[i];
                string name = strategy.Name;
                TreeNode subNode = parentNode.Nodes.Add(name);
                subNode.ForeColor = Color.White;
                subNode.Tag = strategy;
            }
        }

        private Dictionary<string, List<IStrategyInfo>> dic_Path_Strategies = new Dictionary<string, List<IStrategyInfo>>();

        private void InitPathStrategies(List<IStrategyInfo> strategies)
        {
            for (int i = 0; i < strategies.Count; i++)
            {
                IStrategyInfo strategy = strategies[i];
                string path = strategy.StrategyPath;
                if (!dic_Path_Strategies.ContainsKey(path))
                {
                    List<IStrategyInfo> strategiesInPath = new List<IStrategyInfo>();
                    dic_Path_Strategies.Add(path, strategiesInPath);
                    strategiesInPath.Add(strategy);
                }
                else
                {
                    dic_Path_Strategies[path].Add(strategy);
                }
            }
        }

    }
}
