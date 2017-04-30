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
    public class TickBarWriter
    {
        private const int WRITEPERIOD = 200;

        private Dictionary<string, List<ITickBar>> dicTickBar = new Dictionary<string, List<ITickBar>>();

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

        public TickBarWriter(string path, int date)
        {
            this.path = path;
            this.date = date;

            //每1分钟写入一次
            writeTimer = new System.Timers.Timer(60000);
            writeTimer.Elapsed += WriteTimer_Elapsed;
            writeTimer.AutoReset = true;//设置是执行一次（false）还是一直执行(true)；
            writeTimer.Enabled = true;//是否执行System.Timers.Timer.Elapsed事件；
        }

        private void WriteTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            List<string> codes = dicTickBar.Keys.ToList<string>();
            List<int[]> startEndList = new List<int[]>();
            for (int i = 0; i < codes.Count; i++)
            {

                string code = codes[i];
                List<ITickBar> tickBars = dicTickBar[code];
                int startIndex = GetCurrentWriteBeginIndex(code);
                int endIndex = tickBars.Count - 1;
                SetLastWriteEndIndex(code, endIndex);
                Push2Queue(code, startIndex, endIndex);
            }
            writerQueue.Flush();
        }

        public void Write(ITickBar tickBar)
        {
            string code = tickBar.Code;
            if (!dicTickBar.ContainsKey(code))
            {
                List<ITickBar> tickBarList = new List<ITickBar>();
                ((TickBar)tickBar).Mount = tickBar.TotalMount;
                ((TickBar)tickBar).Add = tickBar.Hold;
                tickBarList.Add(tickBar);
                dicTickBar.Add(code, tickBarList);
                return;
            }

            List<ITickBar> tickBars = dicTickBar[code];
            ITickBar lastTick = tickBars[tickBars.Count - 1];
            ((TickBar)tickBar).Mount = tickBar.TotalMount - lastTick.TotalMount;
            ((TickBar)tickBar).Add = tickBar.Hold - lastTick.Hold;
            tickBars.Add(tickBar);
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
            List<ITickBar> tickBars = dicTickBar[code];
            writerQueue.PushWriter(new SingleTickBarWriter(tickpath, tickBars, startIndex, endIndex));
        }
    }

    public class SingleTickBarWriter : IFileWriter
    {
        private string path;

        private List<ITickBar> tickBars;

        private int startIndex;

        private int endIndex;

        public SingleTickBarWriter(string path, List<ITickBar> tickBars, int startIndex, int endIndex)
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
                    sw.WriteLine(tickBars[i]);
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
