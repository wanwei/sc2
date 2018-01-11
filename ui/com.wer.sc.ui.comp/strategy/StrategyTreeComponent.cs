using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using com.wer.sc.strategy;

namespace com.wer.sc.ui.comp.strategy
{
    public partial class StrategyTreeComponent : UserControl
    {
        private IStrategyCenter strategyCenter;

        public StrategyTreeComponent()
        {
            InitializeComponent();
            //RefreshTree();
        }

        public void RefreshTree()
        {
            if (strategyCenter == null)
                return;
            IList<IStrategyAssembly> assemblies = strategyCenter.GetStrategyMgr().GetAllStrategyAssemblies();
            if (assemblies == null)
                return;
            this.treeStrategy.Nodes.Clear();
            for (int i = 0; i < assemblies.Count; i++)
            {
                IStrategyAssembly ass = assemblies[i];
                TreeNode treeNode = this.treeStrategy.Nodes.Add(ass.Name);
                CompStrategyTreeBuilder builder = new CompStrategyTreeBuilder(treeStrategy);
                builder.AddSubNodesByAssembly(ass, treeNode);
            }
        }

        public TreeView TreeStrategy
        {
            get { return treeStrategy; }
        }

        public IStrategyCenter StrategyCenter
        {
            get
            {
                return strategyCenter;
            }

            set
            {
                strategyCenter = value;
                RefreshTree();
            }
        }
    }

    class CompStrategyTreeBuilder
    {
        private TreeView treeStrategy;

        public CompStrategyTreeBuilder(TreeView treeView)
        {
            this.treeStrategy = treeView;
        }

        public void AddSubNodesByAssembly(IStrategyAssembly ass, TreeNode treeNode)
        {
            AddSubNodes(ass, "", treeNode);
        }

        private void AddSubNodes(IStrategyAssembly assembly, string path, TreeNode treeNode)
        {
            IList<string> subPaths = assembly.GetSubPath(path);
            for (int i = 0; i < subPaths.Count; i++)
            {
                string subPath = subPaths[i];
                string pathName = subPath.Substring(subPath.LastIndexOf("\\") + 1);
                TreeNode subNode = treeNode.Nodes.Add(pathName);
                subNode.Tag = subPath;
                AddSubNodes(assembly, subPaths[i], subNode);
            }

            IList<IStrategyInfo> strategies = assembly.GetSubStrategyInfo(path);
            for (int i = 0; i < strategies.Count; i++)
            {
                IStrategyInfo strategyInfo = strategies[i];
                TreeNode subNode = treeNode.Nodes.Add(strategyInfo.Name);
                subNode.Tag = strategyInfo;
                if (strategyInfo.IsError)
                {
                    subNode.Text += ":" + strategyInfo.ErrorInfo;
                    subNode.ForeColor = Color.Red;
                }
                else
                    subNode.ForeColor = Color.Yellow;
            }
        }

        private void AddStrategies(TreeNode parentNode, List<IStrategyInfo> strategies)
        {
            for (int i = 0; i < strategies.Count; i++)
            {
                IStrategyInfo strategy = strategies[i];
                string name = strategy.Name;
                TreeNode subNode = parentNode.Nodes.Add(name);
                subNode.ForeColor = Color.Yellow;
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
