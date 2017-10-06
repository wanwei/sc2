using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.utils
{
    public class TextExchangeUtils
    {
        public static void Write<T>(string path, IList<T> list)
        {
            string[] contents = new string[list.Count];
            for (int i = 0; i < list.Count; i++)
            {
                T t = list[i];
                if (t is ITextExchange)
                {
                    string text = ((ITextExchange)t).SaveToString();
                    contents[i] = text;
                }
            }
            FileUtils.EnsureParentDirExist(path);
            File.WriteAllLines(path, contents);
        }

        public static List<T> Load<T>(string path, Type type)
        {
            if (!File.Exists(path))
                return new List<T>();
            String[] lines = File.ReadAllLines(path);
            List<T> data = new List<T>(lines.Length);
            for (int i = 0; i < lines.Length; i++)
            {
                T t = (T)Activator.CreateInstance(type);
                String line = lines[i].Trim();
                if (line.Equals(""))
                    continue;
                ((ITextExchange)t).LoadFromString(line);
                data.Add(t);
            }
            return data;
        }
    }

    /// <summary>
    /// 本接口专用于实体类和文本交互，如将BonusInfo保存成","分隔的字符串，并且能从该
    /// </summary>
    public interface ITextExchange
    {
        String SaveToString();

        void LoadFromString(String content);
    }
}
