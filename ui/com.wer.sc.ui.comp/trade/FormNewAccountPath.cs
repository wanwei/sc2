using com.wer.sc.data.account;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace com.wer.sc.ui.comp.trade
{
    public partial class FormNewAccountPath : Form
    {
        private IAccountManager accountManager;

        private string parentPath;

        private string path;

        private bool pathCreated = false;

        public bool PathCreated
        {
            get { return pathCreated; }
        }

        public FormNewAccountPath(IAccountManager accountManager, string parentPath)
        {
            InitializeComponent();
            this.accountManager = accountManager;
            this.parentPath = parentPath;
        }

        public string Path
        {
            get
            {
                return path;
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
                return;
            }
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    string newpath = textBox1.Text.Trim();
                    this.pathCreated = this.accountManager.NewPath(parentPath + "\\" + newpath);
                    this.path = newpath;
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void FormTime_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }
    }
}