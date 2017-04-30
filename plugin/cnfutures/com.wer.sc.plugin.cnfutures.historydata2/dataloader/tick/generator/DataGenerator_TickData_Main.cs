using com.wer.sc.data;
using com.wer.sc.data.cnfutures;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin.cnfutures.historydata.dataloader.tick.generator
{
    public class DataGenerator_TickData_Main
    {
        private String csvDataPath;

        private IDataLoader dataLoader;

        public DataGenerator_TickData_Main(IDataLoader dataLoader, string csvDataPath)
        {
            this.csvDataPath = dataLoader.CsvDataPath;
            this.dataLoader = dataLoader;
        }

        public ITickData Generate(String variety, int date)
        {
            List<CodeInfo> codes = this.dataLoader.LoadInstruments(variety);
            List<CodeInfo> anaCodes = new List<CodeInfo>();
            for (int i = 0; i < codes.Count; i++)
            {
                CodeInfo c = codes[i];
                String code = c.Code.ToUpper();
                if (code.EndsWith("MI") || code.EndsWith("13"))
                    continue;
                anaCodes.Add(c);
            }
            String mainCode = GetMainCode(csvDataPath, anaCodes, date);
            return GetAdjustedTickData(mainCode, date);
        }

        private ITickData GetAdjustedTickData(string code, int date)
        {
            return dataLoader.LoadUpdatedTickData(code, date);
        }

        private String GetMainCode(String path, List<CodeInfo> codes, int date)
        {
            //int maxHold = 0;
            long max = 0;
            String mainCode = null;
            for (int i = 0; i < codes.Count; i++)
            {
                String code = codes[i].Code;
                if (code.Contains("MI") || code.Contains("13"))
                    continue;
                String p = GetPath(path, code, date);
                //if (!File.Exists(p))
                //    continue;
                //int hold = GetHold(p);
                //if (hold > maxHold)
                //{
                //    maxHold = hold;
                //    mainCode = code;
                //}

                //以前是按照持仓量来判断，但是有时候持仓量最大的成交量不大，这里还是倾向于用成交量最大的作为主连
                FileInfo f = new FileInfo(p);
                if (!f.Exists)
                    continue;
                long l = f.Length;
                if (l > max)
                {
                    max = l;
                    mainCode = code;
                }
            }
            return mainCode;
        }
        private int GetHold(String path)
        {
            IEnumerable<String> lines = File.ReadLines(path);
            int cnt = 0;
            foreach (String line in lines)
            {
                if (cnt == 0)
                {
                    int startIndex = findHoldStart(line);
                    int endIndex = line.IndexOf(',', startIndex);
                    return int.Parse(line.Substring(startIndex, endIndex - startIndex));
                }
                cnt++;
            }

            return -1;
        }

        private int findHoldStart(String line)
        {
            int startIndex = line.IndexOf(',', 0) + 1;
            startIndex = line.IndexOf(',', startIndex) + 1;
            startIndex = line.IndexOf(',', startIndex) + 1;
            startIndex = line.IndexOf(',', startIndex) + 1;
            return startIndex;
        }

        private String GetPath(String path, String code, int date)
        {
            return path + "\\" + code + "\\" + code + "_" + date + ".csv";
        }
    }
}
