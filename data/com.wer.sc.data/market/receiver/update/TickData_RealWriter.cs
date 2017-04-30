using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace com.wer.sc.data.market.receiver
{
    /// <summary>
    /// Tick数据接收器，负责接收当日的Tick文件，并将Tick数据写入文件
    /// 
    /// TODO 创建时，如果当前已经有tick数据，则将装载当前tick数据
    /// 
    /// tick写入规则
    /// 
    /// M05
    ///     20150104.csv
    ///     20150105.csv
    ///     ...
    /// M09 
    ///     20150104.csv
    ///     ...
    /// 
    /// </summary>
    public class TickData_RealWriter
    {
        private Dictionary<string, ITickData> dicTickData = new Dictionary<string, ITickData>();

        private Dictionary<string, int> dicIndex = new Dictionary<string, int>();

        private string path;

        private WriterQueue writerQueue = new WriterQueue();

        private int date;

        private System.Timers.Timer writeTimer;

        public int Date
        {
            get
            {
                return date;
            }
        }

        public TickData_RealWriter(string path, int date, int interval)
        {
            this.path = path;
            this.date = date;

            //每1分钟写入一次
            writeTimer = new System.Timers.Timer(interval * 1000);
            writeTimer.Elapsed += WriteTimer_Elapsed;
            writeTimer.AutoReset = true;//设置是执行一次（false）还是一直执行(true)；
            writeTimer.Enabled = true;//是否执行System.Timers.Timer.Elapsed事件；
        }

        private void WriteTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            List<string> codes = dicTickData.Keys.ToList<string>();
            List<int[]> startEndList = new List<int[]>();
            for (int i = 0; i < codes.Count; i++)
            {
                string code = codes[i];
                ITickData tickData = dicTickData[code];
                int startIndex = GetCurrentWriteBeginIndex(code);
                int endIndex = tickData.Length - 1;
                SetLastWriteEndIndex(code, endIndex);
                Push2Queue(code, startIndex, endIndex);
            }
            writerQueue.Flush();
        }

        public void Receive(ITickData tickData)
        {
            string code = tickData.Code;
            if (!dicTickData.ContainsKey(code))
            {
                dicTickData.Add(code, tickData);
                return;
            }
        }

        private DelegateOnTickDataReceived onTickDataReceived;

        public DelegateOnTickDataReceived OnTickDataReceived
        {
            get { return onTickDataReceived; }
            set { this.onTickDataReceived = value; }
        }

        private int GetCurrentWriteBeginIndex(string code)
        {
            if (dicIndex.ContainsKey(code))
                return dicIndex[code] + 1;
            return 0;
        }

        private void SetLastWriteEndIndex(string code, int index)
        {
            if (dicIndex.ContainsKey(code))
                dicIndex[code] = index;
            else
            {
                dicIndex.Add(code, index);
            }
        }

        public static string GetTickBarPath(string path, string code, int date)
        {
            return path + "\\" + date + "\\tick\\" + code + "_" + date + ".csv";
        }

        private void Push2Queue(string code, int startIndex, int endIndex)
        {
            if (endIndex < startIndex)
                return;
            string tickpath = GetTickBarPath(path, code, date);
            ITickData tickBars = dicTickData[code];
            writerQueue.PushWriter(new SingleTickBarWriter(tickpath, tickBars, startIndex, endIndex));
        }

        public void Dispose()
        {
            this.writeTimer.Stop();
            this.dicIndex.Clear();
            this.dicTickData.Clear();
        }
    }

    public delegate void DelegateOnTickDataReceived(object sender, ITickData tickData, ITickBar tickBar);

    public class SingleTickBarWriter : IFileWriter
    {
        private string path;

        private ITickData tickData;

        private int startIndex;

        private int endIndex;

        public SingleTickBarWriter(string path, ITickData tickData, int startIndex, int endIndex)
        {
            this.path = path;
            this.tickData = tickData;
            this.startIndex = startIndex;
            this.endIndex = endIndex;
        }

        public void Write()
        {
            FileUtils.EnsureParentDirExist(path);
            StreamWriter sw = File.AppendText(path);
            try
            {
                for (int i = startIndex; i <= endIndex; i++)
                {
                    sw.WriteLine(tickData.GetBar(i));
                }
            }
            finally
            {
                sw.Close();
            }
        }
    }

    public interface IFileWriter
    {
        void Write();
    }

    /// <summary>
    /// 写入队列
    /// </summary>
    public class WriterQueue
    {
        private Queue<IFileWriter> writerQueue = new Queue<IFileWriter>();

        private bool isWriting = false;

        private Object lockObj = new object();

        public void PushWriter(IFileWriter writer)
        {
            this.writerQueue.Enqueue(writer);
        }

        public void Flush()
        {
            if (!isWriting)
            {
                new Thread(Write).Start();
            }
        }

        public void Write()
        {
            lock (lockObj)
            {
                isWriting = true;
                if (writerQueue.Count == 0)
                {
                    isWriting = false;
                    return;
                }
                IFileWriter writer = writerQueue.Dequeue();
                while (writer != null)
                {
                    writer.Write();
                    if (writerQueue.Count == 0)
                        break;
                    writer = writerQueue.Dequeue();
                }
                isWriting = false;
            }
        }
    }
}
