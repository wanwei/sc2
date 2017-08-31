using com.wer.sc.utils;
using com.wer.sc.utils.param;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace com.wer.sc.comp.param
{
    public partial class FormParameters : Form
    {
        private List<IParameterControl> parameterControls = new List<IParameterControl>();

        private IParameters parameters;

        public IParameters Parameters
        {
            get
            {
                return parameters;
            }
        }

        public FormParameters(IParameters parameters)
        {
            InitializeComponent();
            this.parameters = parameters;
            Init(parameters);
        }

        private CompParameters compParameters;

        private void Init(IParameters parameters)
        {
            compParameters = new CompParameters();
            compParameters.Parameters = parameters;
            this.panel1.Controls.Add(compParameters);
            this.Height = compParameters.Height + 100;
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            try
            {
                this.parameters = compParameters.Parameters;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}