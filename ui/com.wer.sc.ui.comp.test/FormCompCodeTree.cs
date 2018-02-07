using com.wer.sc.mockdata;
using com.wer.sc.utils;
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
    public partial class FormCompCodeTree : Form
    {
        public FormCompCodeTree()
        {
            InitializeComponent();
            this.compCodeTree1.Init();
        }

        private void cbMultiSelect_CheckedChanged(object sender, EventArgs e)
        {
            this.compCodeTree1.MultiSelect = cbMultiSelect.Checked;
        }

        private void cbShowCatelog_CheckedChanged(object sender, EventArgs e)
        {
            this.compCodeTree1.SelectCatelog = cbShowCatelog.Checked;
        }

        private void btCatelogs_Click(object sender, EventArgs e)
        {
            List<string> catelogs = this.compCodeTree1.SelectedCatelogs;            
            MessageBox.Show(ListUtils.ToString(catelogs));
        }

        private void btCode_Click(object sender, EventArgs e)
        {
            List<string> codes = this.compCodeTree1.SelectedCodes;
            MessageBox.Show(ListUtils.ToString(codes));
        }
    }
}
