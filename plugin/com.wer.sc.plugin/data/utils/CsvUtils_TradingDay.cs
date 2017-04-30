using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.utils
{
    public class CsvUtils_TradingDay
    {
        public static void Save(string path, List<int> data)
        {
            string[] contents = new string[data.Count];
            for (int i = 0; i < contents.Length; i++)
            {
                contents[i] = data[i].ToString();
            }
            FileUtils.EnsureParentDirExist(path);
            File.WriteAllLines(path, contents);
        }

        public static List<int> Load(string path)
        {
            if (!File.Exists(path))
                return new List<int>();
            String[] lines = File.ReadAllLines(path);
            return LoadByLines(lines);
        }

        public static List<int> LoadByContent(string content)
        {
            string[] lines = content.Split('\r');
            return LoadByLines(lines);
        }

        public static List<int> LoadByLines(string[] lines)
        {
            List<int> data = new List<int>(lines.Length);
            for (int i = 0; i < lines.Length; i++)
            {
                String line = lines[i].Trim();
                if (line.Equals(""))
                    continue;
                int openDate;
                bool isInt = int.TryParse(line, out openDate);
                if (isInt)
                    data.Add(openDate);
            }
            return data;
        }
    }
}
