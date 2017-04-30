using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.utils
{
    /// <summary>
    /// K线数据保存为CSV文件及从CSV文件装载的工具类
    /// </summary>
    public class CsvUtils_KLineData
    {
        public static void Save(string path, IKLineData data)
        {
            string[] contents = new string[data.Length];
            for (int i = 0; i < contents.Length; i++)
            {
                data.BarPos = i;
                contents[i] = data.ToString();
            }
            FileUtils.EnsureParentDirExist(path);
            File.WriteAllLines(path, contents);
        }

        public static IKLineData Load(String path)
        {
            if (!File.Exists(path))
                return null;
            String[] lines = File.ReadAllLines(path);
            return LoadByLines(lines);
        }

        public static IKLineData LoadByContent(string content)
        {
            string[] lines = content.Split('\r');
            return LoadByLines(lines);
        }

        public static IKLineData LoadByLines(string[] lines)
        {
            KLineData data = new KLineData(lines.Length);
            for (int i = 0; i < lines.Length; i++)
            {
                String line = lines[i].Trim();
                String[] dataArr = line.Split(',');
                data.arr_time[i] = double.Parse(dataArr[0]);
                data.arr_start[i] = float.Parse(dataArr[1]);
                data.arr_high[i] = float.Parse(dataArr[2]);
                data.arr_low[i] = float.Parse(dataArr[3]);
                data.arr_end[i] = float.Parse(dataArr[4]);
                data.arr_mount[i] = (int)float.Parse(dataArr[5]);
                data.arr_money[i] = float.Parse(dataArr[6]);
                data.arr_hold[i] = int.Parse(dataArr[7]);
            }
            return data;
        }
    }
}
