using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace com.wer.sc.utils.ui
{
    public partial class FormException : Form
    {
        public FormException(Exception e)
        {
            InitializeComponent();
            this.label1.Text = e.Message;
            this.textBox1.Text = e.StackTrace;
        }
    }
}
