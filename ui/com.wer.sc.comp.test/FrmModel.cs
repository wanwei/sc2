using com.wer.sc.comp.ana;
using com.wer.sc.plugin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace com.wer.sc.comp.test
{
    public partial class FrmModel : Form
    {
        private Plugin_KLineModel model;
        private CompKLineModels modelComp;

        public FrmModel()
        {
            InitializeComponent();
            modelComp = new CompKLineModels();
            modelComp.Bind(treeView1);
        }

        private void treeView1_DoubleClick(object sender, EventArgs e)
        {
            this.model = this.modelComp.CreateSelectModel();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        public Plugin_KLineModel Model
        {
            get { return model; }
        }
    }
}