using com.wer.sc.data.receiver2;
using com.wer.sc.plugin.market;
using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace com.wer.sc.data.receiver2
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
    public class DataReceiver_Tick
    {
        private const int WRITEPERIOD = 200;

        private Dictionary<string, TickData_Present> dicTickData = new Dictionary<string, TickData_Present>();

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

        public DataReceiver_Tick(string path, int date)
        {
            this.path = path;
            this.date = date;

            //每1分钟写入一次
            writeTimer = new System.Timers.Timer(30000);
            writeTimer.Elapsed += WriteTimer_Elapsed;
            writeTimer.AutoReset = true;//设置是执行一次（false）还是一直执行(true)；
            writeTimer.Enabled = true;//是否执行System.Timers.Timer.Elapsed事件；
        }

        public TickData_Present GetTickData(string code)
        {
            TickData_Present tickData;
            bool b = dicTickData.TryGetValue(code, out tickData);
            return b ? tickData : null;
        }

        private void WriteTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            List<string> codes = dicTickData.Keys.ToList<string>();
            List<int[]> startEndList = new List<int[]>();
            for (int i = 0; i < codes.Count; i++)
            {
                string code = codes[i];
                TickData_Present tickData = dicTickData[code];
                int startIndex = GetCurrentWriteBeginIndex(code);
                int endIndex = tickData.Length - 1;
                SetLastWriteEndIndex(code, endIndex);
                Push2Queue(code, startIndex, endIndex);
            }
            writerQueue.Flush();
        }

        public void Receive(ITickBar tickBar)
        {
            string code = tickBar.Code;
            if (!dicTickData.ContainsKey(code))
            {
                TickData_Present newTickData = new TickData_Present(10000);
                newTickData.Code = code;
                //List<ITickBar> tickBarList = new List<ITickBar>();
                ((TickBar)tickBar).Mount = tickBar.TotalMount;
                ((TickBar)tickBar).Add = tickBar.Hold;
                newTickData.Recieve(tickBar);
                dicTickData.Add(code, newTickData);
                if (OnTickDataReceived != null)
                    OnTickDataReceived(this, newTickData, tickBar);
                return;
            }

            TickData_Present tickData = dicTickData[code];
            ITickBar lastTick = tickData;
            ((TickBar)tickBar).Mount = tickBar.TotalMount - lastTick.TotalMount;
            ((TickBar)tickBar).Add = tickBar.Hold - lastTick.Hold;
            tickData.Recieve(tickBar);
            if (OnTickDataReceived != null)
                OnTickDataReceived(this, tickData, tickBar);
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

        //public void Flush()
        //{
        //    List<string> codes = dicTickBar.Keys.ToList<string>();

        //    for (int i = 0; i < codes.Count; i++)
        //    {
        //        string code = codes[i];
        //        List<ITickBar> tickBars = dicTickBar[code];
        //        int startIndex = GetCurrentWriteBeginIndex(code);
        //        int endIndex = tickBars.Count;
        //        Push2Queue(code, startIndex, endIndex);
        //    }            
        //}

        public static string GetTickBarPath(string path, string code, int date)
        {
            return path + "\\" + date + "\\" + code + "_" + date + ".csv";
        }

        private void Push2Queue(string code, int startIndex, int endIndex)
        {
            if (endIndex < startIndex)
                return;
            string tickpath = GetTickBarPath(path, code, date);
            TickData_Present tickBars = dicTickData[code];
            writerQueue.PushWriter(new SingleTickBarWriter(tickpath, tickBars, startIndex, endIndex));
        }
    }

    public delegate void DelegateOnTickDataReceived(object sender, ITickData tickData, ITickBar tickBar);

    public class SingleTickBarWriter : IFileWriter
    {
        private string path;

        private TickData_Present tickBars;

        private int startIndex;

        private int endIndex;

        public SingleTickBarWriter(string path, TickData_Present tickBars, int startIndex, int endIndex)
        {
            this.path = path;
            this.tickBars = tickBars;
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
                    sw.WriteLine(tickBars.GetBar(i));
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
