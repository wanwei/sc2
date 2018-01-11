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

namespace com.wer.sc.ui.comp.strategy
{
    public partial class FormStrategyLoader : Form
    {
        private IStrategyInfo strategy;

        public FormStrategyLoader()
        {
            InitializeComponent();
            this.ShowIcon = false;
            this.strategyTreeComponent1.StrategyCenter = StrategyCenter.Default;
            this.strategyTreeComponent1.TreeStrategy.NodeMouseDoubleClick += TreeStrategy_NodeMouseDoubleClick;
        }

        public IStrategyInfo SelectedStrategy
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
            if (tag == null || (!(tag is IStrategyInfo)))
                return;
            this.strategy = (IStrategyInfo)tag;
            if (strategy.IsError)
            {
                MessageBox.Show("选中策略有错误");
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btRefresh_Click(object sender, EventArgs e)
        {
            //StrategyMgrFactory.DefaultPluginMgr.Refresh();
            //this.compStrategyTree1.RefreshTree();
        }
    }
}
