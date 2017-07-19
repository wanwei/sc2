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
    public partial class FormStrategy : Form
    {
        public FormStrategy()
        {
            InitializeComponent();
            compStrategyTree1.TreeStrategy.MouseDoubleClick += TreeStrategy_MouseDoubleClick;
        }

        private void TreeStrategy_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            object tag = compStrategyTree1.TreeStrategy.SelectedNode.Tag;
            if (tag != null)
                MessageBox.Show(tag.ToString());
        }
    }
}
