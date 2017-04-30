using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.receiver2
{
    public class LogUtils
    {
        private List<String> logList = new List<string>();
        private DelegateOnWriteLog onWriteLog;
        public List<string> LogList
        {
            get
            {
                return logList;
            }
        }

        public void WriteLog(object content)
        {
            string log = DateTime.Now.ToString() + ": " + content.ToString();
            LogList.Add(log);
            if (onWriteLog != null)
                onWriteLog(this, log);
        }

        public DelegateOnWriteLog OnWriteLog
        {
            get { return onWriteLog; }
            set { onWriteLog = value; }
        }
    }

    public delegate void DelegateOnWriteLog(object sender, Object log);

}
