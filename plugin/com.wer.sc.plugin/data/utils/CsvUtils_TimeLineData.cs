using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.utils
{
    public class CsvUtils_TimeLineData
    {
        public static void Save(string path, ITimeLineData data)
        {
            string[] contents = new string[data.Length + 1];
            contents[0] = data.YesterdayEnd.ToString();
            for (int i = 1; i < contents.Length; i++)
            {
                data.BarPos = i - 1;
                contents[i] = data.ToString();
            }
            FileUtils.EnsureParentDirExist(path);
            File.WriteAllLines(path, contents);
        }

        public static ITimeLineData Load(string path)
        {
            String[] lines = File.ReadAllLines(path);
            return LoadKLineData(lines);
        }

        public static ITimeLineData LoadKLineData(string[] lines)
        {
            float yesterday = float.Parse(lines[0]);
            TimeLineData data = new TimeLineData(yesterday, lines.Length - 1);
            for (int i = 0; i < data.Length; i++)
            {
                String line = lines[i + 1].Trim();
                String[] dataArr = line.Split(',');
                data.arr_time[i] = double.Parse(dataArr[0]);
                data.arr_price[i] = float.Parse(dataArr[1]);
                data.arr_mount[i] = int.Parse(dataArr[2]);
                data.arr_hold[i] = int.Parse(dataArr[3]);
            }
            return data;
        }
    }
}
