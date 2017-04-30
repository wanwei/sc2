using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace com.wer.sc.data.updater
{
    public partial class FormChooseUpdater : Form
    {
        public FormChooseUpdater()
        {
            InitializeComponent();

            List<string> names = UpdateConfig.GetAllUpdatePackageName();
            for (int i = 0; i < names.Count; i++)
            {
                this.cbUpdaters.Items.Add(names[i]);
            }
            this.cbUpdaters.SelectedIndex = 0;
        }

        private DataUpdaterPackageConfigInfo package;

        private void btOK_Click(object sender, EventArgs e)
        {
            string value = this.cbUpdaters.Text;
            this.package = UpdateConfig.GetUpdatePackage(value);

            FormDataUpdater form = new FormDataUpdater(package);
            form.ShowDialog();
            //this.DialogResult = DialogResult.OK;
            //FormDataUpdater3 form = new FormDataUpdater3(package);
            //form.ShowDialog();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("更新器装载失败：\r\n" + ex.Message);
            //}
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            //this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
