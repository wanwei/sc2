using com.wer.sc.data;
using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.utils
{
    public class CsvUtils_TradingSession
    {
        public static void Save(string path, List<TradingSession> data)
        {
            if (data == null)
                return;
            string[] contents = new string[data.Count];
            for (int i = 0; i < contents.Length; i++)
            {
                contents[i] = data[i].ToString();
            }
            FileUtils.EnsureParentDirExist(path);
            File.WriteAllLines(path, contents);
        }

        public static List<TradingSession> Load(string path)
        {
            if (!File.Exists(path))
                return null;
            String[] lines = File.ReadAllLines(path);
            return LoadByLines(lines);
        }

        public static List<TradingSession> LoadByContent(string content)
        {
            string[] lines = content.Split('\r');
            return LoadByLines(lines);
        }

        public static List<TradingSession> LoadByLines(string[] lines)
        {
            List<TradingSession> data = new List<TradingSession>(lines.Length);
            for (int i = 0; i < lines.Length; i++)
            {
                String line = lines[i].Trim();
                if (line.Equals(""))
                    continue;
                String[] dataArr = line.Split(',');
                TradingSession startTime = new TradingSession();
                startTime.TradingDay = int.Parse(dataArr[0]);
                startTime.StartTime = double.Parse(dataArr[1]);
                startTime.EndTime = double.Parse(dataArr[2]);
                data.Add(startTime);
            }
            return data;
        }
    }
}
