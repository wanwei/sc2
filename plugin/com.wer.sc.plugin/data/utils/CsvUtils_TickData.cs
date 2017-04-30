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
    /// 一天的Tick数据保存为CSV文件及从CSV文件装载的工具类
    /// </summary>
    public class CsvUtils_TickData
    {
        public static void Save(string path, ITickData data)
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

        public static ITickData Load(string path)
        {
            if (!File.Exists(path))
                return null;
            String[] lines = File.ReadAllLines(path);
            return LoadByLines(lines);
        }

        public static ITickData LoadByContent(string content)
        {
            string[] lines = content.Split('\r');
            return LoadByLines(lines);
        }

        public static ITickData LoadByLines(string[] lines)
        {
            TickData data = new TickData(lines.Length);
            for (int i = 0; i < lines.Length; i++)
            {
                String line = lines[i].Trim();
                if (line.Equals(""))
                    continue;
                String[] dataArr = line.Split(',');
                if (dataArr.Length < 5)
                    continue;
                data.arr_time[i] = double.Parse(dataArr[0]);
                data.arr_price[i] = float.Parse(dataArr[1]);
                data.arr_mount[i] = int.Parse(dataArr[2]);
                data.arr_totalMount[i] = int.Parse(dataArr[3]);
                data.arr_add[i] = int.Parse(dataArr[4]);
                data.arr_buyPrice[i] = (int)float.Parse(dataArr[5]);
                data.arr_buyMount[i] = int.Parse(dataArr[6]);
                data.arr_sellPrice[i] = (int)float.Parse(dataArr[7]);
                data.arr_sellMount[i] = int.Parse(dataArr[8]);
                data.arr_isBuy[i] = dataArr[9].Equals("1");
            }
            return data;
        }
    }
}
