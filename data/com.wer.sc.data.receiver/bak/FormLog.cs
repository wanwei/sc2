using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace com.wer.sc.data.receiver2
{
    public partial class FormLog : Form
    {
        private LogUtils logUtils;

        private int textCount;

        public FormLog(LogUtils logUtils)
        {
            InitializeComponent();
            BindLogUtils(logUtils);
        }

        private void BindLogUtils(LogUtils logUtils)
        {
            this.logUtils = logUtils;
            for (int i = 0; i < logUtils.LogList.Count; i++)
            {
                this.tbLog.AppendText(logUtils.LogList[i] + "\r\n");
            }
            logUtils.OnWriteLog = OnWriteLog;
        }

        public void OnWriteLog(object sender, Object log)
        {
            //
            AppendText(log.ToString());
            //this.tbLog.AppendText(log + "\r\n");            
        }

        private void AppendText(string text)
        {
            if (!this.IsHandleCreated)
                return;
            if (this.tbLog.InvokeRequired)
            {
                AppendTextCallback d = new AppendTextCallback(AppendText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                if (!tbLog.IsDisposed)
                {
                    //if(this.tbLog.)
                    this.tbLog.AppendText(text + "\r\n");
                    textCount++;
                }
            }
        }

        delegate void AppendTextCallback(string text);
    }
}
