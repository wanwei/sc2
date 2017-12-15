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

namespace com.wer.sc.ui.comp.test
{
    public partial class FormStrategyTreeComponent : Form
    {
        public FormStrategyTreeComponent()
        {
            InitializeComponent();

            strategyTreeComponent1.TreeStrategy.DoubleClick += TreeStrategy_DoubleClick;
        }

        private void TreeStrategy_DoubleClick(object sender, EventArgs e)
        {
            TreeView treeView = (TreeView)sender;
            TreeNode node = treeView.SelectedNode;
            object obj = node.Tag;
            if (obj is IStrategyInfo)
            {
                MessageBox.Show(obj.ToString());
            }
        }
    }
}
