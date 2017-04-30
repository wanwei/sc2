using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace com.wer.sc.utils.ui.test
{
    public partial class FormLog : Form
    {
        public FormLog()
        {
            InitializeComponent();
        }

        private void btPrintLog_Click(object sender, EventArgs e)
        {
            LogHelper.Info(GetType(), "LogInfo");
            LogHelper.Info(GetType(), new ApplicationException("LogInfo_Exception"));
            LogHelper.Warn(GetType(), "WarnInfo");
            LogHelper.Warn(GetType(), new ApplicationException("WarnInfo_Exception"));
            LogHelper.Error(GetType(), "ErrorInfo");
            LogHelper.Error(GetType(), new ApplicationException("ErrorInfo_Exception"));
            LogHelper.Fatal(GetType(), "FatalInfo");
            LogHelper.Fatal(GetType(), new ApplicationException("FatalInfo_Exception"));
        }
    }
}
