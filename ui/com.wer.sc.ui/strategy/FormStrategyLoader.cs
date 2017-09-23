using com.wer.sc.strategy;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace com.wer.sc.ui.strategy
{
    public partial class FormStrategyLoader : Form
    {
        private StrategyInfo strategy;

        public FormStrategyLoader()
        {
            InitializeComponent();
            this.compStrategyTree1.TreeStrategy.NodeMouseDoubleClick += TreeStrategy_NodeMouseDoubleClick;
        }

        public StrategyInfo SelectedStrategy
        {
            get
            {
                return strategy;
            }
        }

        private void TreeStrategy_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode treeNode = e.Node;
            object tag = treeNode.Tag;
            if (tag == null || (!(tag is StrategyInfo)))            
                return;

            this.strategy = (StrategyInfo)tag;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btRefresh_Click(object sender, EventArgs e)
        {
            StrategyMgrFactory.DefaultPluginMgr.Refresh();
            this.compStrategyTree1.RefreshTree();
        }
    }
}
