using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace com.wer.sc.ui
{
    public partial class FormLog : Form
    {
        private bool closeForm = false;

        private UILogger logger;

        public FormLog(UILogger logger)
        {
            InitializeComponent();
            this.logger = logger;
            this.logger.Bind(this.textBox1);
        }

        public bool CloseForm
        {
            get
            {
                return closeForm;
            }

            set
            {
                closeForm = value;
            }
        }

        private void btClear_Click(object sender, EventArgs e)
        {
            this.logger.Clear();
        }

        private void FormLog_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.logger.Bind(null);
            this.logger = null;
        }
    }
}
