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

namespace com.wer.sc.ui.comp.test
{
    public partial class FormParameterSetting : Form
    {
        private IParameters parameters;

        public FormParameterSetting(IParameters parameters)
        {
            InitializeComponent();
            this.parameters = parameters;
        }
    }
}
