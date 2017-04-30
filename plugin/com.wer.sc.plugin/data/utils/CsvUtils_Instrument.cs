using com.wer.sc.data.market;
using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.utils
{
    public class CsvUtils_Instrument
    {
        public static void Save(string path, List<InstrumentInfo> data)
        {
            string[] contents = new string[data.Count];
            for (int i = 0; i < contents.Length; i++)
            {
                contents[i] = data[i].ToString();
            }
            FileUtils.EnsureParentDirExist(path);
            File.WriteAllLines(path, contents);
        }

        public static List<InstrumentInfo> Load(string path)
        {
            if (!File.Exists(path))
                return new List<InstrumentInfo>();
            String[] lines = File.ReadAllLines(path);
            return LoadByLines(lines);
        }

        public static List<InstrumentInfo> LoadByContent(string content)
        {
            string[] lines = content.Split('\r');
            return LoadByLines(lines);
        }

        public static List<InstrumentInfo> LoadByLines(string[] lines)
        {
            List<InstrumentInfo> data = new List<InstrumentInfo>(lines.Length);
            for (int i = 0; i < lines.Length; i++)
            {
                String line = lines[i].Trim();
                if (line.Equals(""))
                    continue;
                InstrumentInfo code = new InstrumentInfo();
                code.LoadFromString(line);
                data.Add(code);
            }
            return data;
        }
    }
}
