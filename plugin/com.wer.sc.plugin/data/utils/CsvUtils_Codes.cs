using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.utils
{
    public class CsvUtils_Code
    {
        public static void Save(string path, List<CodeInfo> data)
        {
            string[] contents = new string[data.Count];
            for (int i = 0; i < contents.Length; i++)
            {
                contents[i] = data[i].ToString();
            }
            FileUtils.EnsureParentDirExist(path);
            File.WriteAllLines(path, contents);
        }

        public static List<CodeInfo> Load(string path)
        {
            if (!File.Exists(path))
                return new List<CodeInfo>();
            String[] lines = File.ReadAllLines(path);
            return LoadByLines(lines);
        }

        public static List<CodeInfo> LoadByContent(string content)
        {
            string[] lines = content.Split('\r');
            return LoadByLines(lines);
        }

        public static List<CodeInfo> LoadByLines(string[] lines)
        {
            List<CodeInfo> data = new List<CodeInfo>(lines.Length);
            for (int i = 0; i < lines.Length; i++)
            {
                String line = lines[i].Trim();
                if (line.Equals(""))
                    continue;
                String[] dataArr = line.Split(',');
                CodeInfo codeInfo = new CodeInfo();
                codeInfo.Code = dataArr[0];
                codeInfo.Name = dataArr[1];
                codeInfo.Catelog = dataArr[2];
                if (dataArr.Length > 3)
                {
                    codeInfo.CatelogName = dataArr[3];
                    codeInfo.Start = int.Parse(dataArr[4]);
                    codeInfo.End = int.Parse(dataArr[5]);
                    codeInfo.Exchange = dataArr[6];
                }
                data.Add(codeInfo);
            }
            return data;
        }
    }
}