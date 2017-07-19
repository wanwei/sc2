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
            for (int i = 0; i < strategies.Count; i++)
            {
                StrategyInfo strategy = strategies[i];
                string name = strategy.StrategyName;
                TreeNode subNode = treeNode.Nodes.Add(name);
                subNode.Tag = strategy;
            }
        }

        public TreeView TreeStrategy
        {
            get { return treeStrategy; }
        }
    }
}
