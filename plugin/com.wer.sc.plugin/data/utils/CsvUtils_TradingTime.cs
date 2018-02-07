using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.utils
{
    public class CsvUtils_TradingTime
    {
        public static void Save(string path, IList<ITradingTime> data)
        {
            if (data == null)
                return;
            string[] contents = new string[data.Count];
            for (int i = 0; i < contents.Length; i++)
            {
                contents[i] = data[i].SaveToString();
            }
            FileUtils.EnsureParentDirExist(path);
            File.WriteAllLines(path, contents);
        }

        public static List<ITradingTime> Load(string path)
        {
            if (!File.Exists(path))
                return null;
            String[] lines = File.ReadAllLines(path);
            return LoadByLines(lines);
        }

        public static List<ITradingTime> LoadByContent(string content)
        {
            string[] lines = content.Split('\r');
            return LoadByLines(lines);
        }

        public static List<ITradingTime> LoadByLines(string[] lines)
        {
            List<ITradingTime> data = new List<ITradingTime>(lines.Length);
            for (int i = 0; i < lines.Length; i++)
            {
                String line = lines[i].Trim();
                if (line.Equals(""))
                    continue;
                TradingTime startTime = new TradingTime();
                startTime.LoadFromString(line);
                data.Add(startTime);
            }
            return data;
        }
    }
}
