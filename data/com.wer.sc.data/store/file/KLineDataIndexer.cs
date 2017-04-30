using com.wer.sc.data.utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.store.file
{
    /// <summary>
    /// K线数据索引器，用作索引KLineDataStore保存的数据
    /// 该索引器会记录每天开盘那个K线index，所以装载数据时能快速获取以日为单位的数据。
    /// </summary>
    public class KLineDataIndexer
    {
        private String path;

        public KLineDataIndexer(String path)
        {
            this.path = path;
        }

        public KLineDataIndexResult GetIndexResult()
        {
            String indexPath = path + ".index";
            if (!File.Exists(indexPath))
                return null;
            KLineDataIndexResult result = new KLineDataIndexResult();
            String[] lines = File.ReadAllLines(indexPath);
            for (int i = 0; i < lines.Length; i++)
            {
                String line = lines[i];
                String[] arr = line.Split(',');
                result.AddIndex(int.Parse(arr[0]), int.Parse(arr[1]));
            }
            return result;
        }

        public void DoIndex()
        {
            if (!File.Exists(path))
                return;
            String indexPath = path + ".index";
            KLineDataStore_File_Single store = new KLineDataStore_File_Single(path);
            IKLineData klineData = store.LoadAll();
            List<SplitterResult> results = DaySplitter.Split(klineData);

            List<String> indeies = new List<string>(results.Count);
            for (int i = 0; i < results.Count; i++)
                indeies.Add(results[i].ToString());

            File.WriteAllLines(indexPath, indeies.ToArray());

            //FileStream file = new FileStream(path, FileMode.Open);
            //try
            //{               
            //indeies.Add(((int)time).ToString() + "," + currentIndex);
            //File.WriteAllLines(indexPath, indeies.ToArray());

            //double lastTime = GetTimeByIndex(file, 0);
            //double time = GetTimeByIndex(file, 1);
            ////KLinePeriod period = KLineData.GetPeriod(lastTime, time);

            ////算法                

            //int len = GetLength(file);
            //int currentIndex = 0;
            //bool hasNight = false;
            //for (int index = 1; index < len; index++)
            //{
            //    time = GetTimeByIndex(file, index);

            //    int date = (int)time;
            //    int lastDate = (int)lastTime;

            //    //夜盘开始，则一定是新的一天开始
            //    if (IsNightStart(time, lastTime))
            //    {
            //        indeies.Add(((int)lastTime).ToString() + "," + currentIndex.ToString());
            //        currentIndex = index;
            //        hasNight = true;
            //    }
            //    else if (hasNight)
            //    {
            //        //对于夜盘来说，如果到了第二天，则说明夜盘结束了,此时不算新的一天开始
            //        if (date != lastDate)
            //            hasNight = false;
            //    }
            //    //只要过了夜都算第二天的
            //    else if (date != lastDate)
            //    {
            //        indeies.Add(((int)lastTime).ToString() + "," + currentIndex.ToString());
            //        currentIndex = index;
            //    }

            //    lastTime = time;
            //}

            //}
            //finally
            //{
            //    //file.Close();
            //}
        }

        //public static bool IsNightStart(double time, double lastTime)
        //{
        //    //time在晚上6点之后，lasttime在晚上6点之前
        //    //且前后时间相隔超过100分钟，说明time是夜盘开始
        //    double t1 = time - (int)time;
        //    if (t1 < 0.18)
        //        return false;

        //    double lastt1 = lastTime - (int)lastTime;
        //    if (lastt1 >= 0.18)
        //        return false;

        //    TimeSpan span = TimeUtils.Substract(time, lastTime);
        //    if (span.Hours * 60 + span.Minutes > 100)
        //    {
        //        return true;
        //    }

        //    return false;
        //}

        //private int GetChangeDateType(double time, double lastTime)
        //{
        //    //-1 不修改日期、 0当日开盘、1要加1
        //    double distance = time - lastTime;
        //    if (distance >= 1)
        //        return 0;
        //    if (distance < 0.04)
        //        return -1;
        //    int date = (int)time;
        //    int lastdate = (int)lastTime;
        //    if (date == lastdate)
        //        return 1;

        //    double lastt = lastTime - (int)lastTime;

        //    //收盘时间在晚上9点后，早上8点前都认为是夜盘，夜盘记作第二天开盘
        //    if (lastt > 0.21 || lastt < 0.08)
        //        return -1;
        //    return 0;
        //}

        //private double GetTimeByIndex(FileStream file, int index)
        //{
        //    byte[] bs = new byte[8];
        //    file.Seek(index * KLineDataStore.LEN_EVERYKLINE, SeekOrigin.Begin);
        //    file.Read(bs, 0, bs.Length);
        //    return BitConverter.ToDouble(bs, 0);
        //}

        //private int GetLength(FileStream file)
        //{
        //    return (int)(file.Length / KLineDataStore.LEN_EVERYKLINE);
        //}
    }
}
