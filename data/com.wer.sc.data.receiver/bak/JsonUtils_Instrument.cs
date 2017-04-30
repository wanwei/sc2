using com.wer.sc.data.market;
using com.wer.sc.plugin.market;
using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.receiver2
{
    public class JsonUtils_Instrument
    {
        public static void Save(string path, List<InstrumentInfo> data)
        {
            FileUtils.EnsureParentDirExist(path);
            File.WriteAllText(path, JsonUtils.ToJsJson(data));
        }

        public static List<InstrumentInfo> Load(string path)
        {
            string content = File.ReadAllText(path);
            return JsonUtils.FromJsonTo<List<InstrumentInfo>>(content);
        }

        //public static List<InstrumentInfo> LoadByContent(string content)
        //{
        //    string[] lines = content.Split('\r');
        //    return LoadByLines(lines);
        //}

        //public static List<InstrumentInfo> LoadByLines(string[] lines)
        //{
        //    List<InstrumentInfo> data = new List<InstrumentInfo>(lines.Length);
        //    for (int i = 0; i < lines.Length; i++)
        //    {
        //        String line = lines[i].Trim();
        //        if (line.Equals(""))
        //            continue;
        //        String[] dataArr = line.Split(',');
        //        //CodeInfo code = new CodeInfo(dataArr[0], dataArr[1], dataArr[2]);
        //        InstrumentInfo instrument = new InstrumentInfo();

        //        data.Add(instrument);
        //    }
        //    return data;
        //}
    }
}
