using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace com.wer.sc.ui
{
    public class UILogger
    {
        private int maxLine;

        private List<string> logContent = new List<string>(1000);

        private TextBox textBox;

        public UILogger() : this(5000)
        {

        }

        public UILogger(int maxLine)
        {
            this.maxLine = maxLine;
        }

        public void Bind(TextBox textBox)
        {
            this.textBox = textBox;
            if (this.textBox == null)
                return;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < logContent.Count; i++)
            {
                sb.Append(logContent[i]).Append("\r\n");
            }
            this.textBox.AppendText(sb.ToString());
        }

        public void Log(string text)
        {
            if (this.logContent.Count >= maxLine)
            {
                this.logContent.RemoveAt(0);
            }
            string log = DateTime.Now + ":" + text;
            logContent.Add(log);
            if (textBox != null)
                AppendLine(log);
        }

        public void Clear()
        {
            this.logContent.Clear();
            if (this.textBox != null)
                this.textBox.Clear();
        }

        private delegate void AppendText(object txt);

        private void AppendLine(object text)
        {
            if (this.textBox.InvokeRequired)
            {
                textBox.Invoke(new AppendText(AppendLine), text);
            }
            else
            {
                if (textBox != null)
                    textBox.AppendText(text + "\r\n");
            }
        }
    }
}
