using com.wer.sc.data;
using com.wer.sc.data.utils;
using com.wer.sc.plugin.cnfutures.config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin.cnfutures.historydata.dataupdater.generator
{
    public class MainFuturesScan
    {
        private string csvPath;

        private CacheUtils_CodeInfo cache;

        public MainFuturesScan(string csvPath, List<CodeInfo> codes)
        {
            this.cache = new CacheUtils_CodeInfo(codes);
            this.csvPath = csvPath;
        }

        public MainFutures Scan(String variety, IList<int> openDates)
        {
            MainFutures mainF = new MainFutures();
            mainF.Variety = variety;
            List<MainContractInfo> mainFutures = mainF.mainFutures;
            List<CodeInfo> codes = cache.GetCodesByCatelog(variety);
            String lastMainCode = "";
            for (int i = 0; i < openDates.Count; i++)
            {
                int openDate = openDates[i];
                String mainCode = GetMainCode(csvPath, codes, openDate);
                if (mainCode == null)
                    continue;
                if (!mainCode.Equals(lastMainCode))
                {
                    bool addNew = true;
                    if (mainFutures.Count > 0)
                    {
                        //主合约改变条件：
                        //
                        MainContractInfo lastMain = mainFutures[mainFutures.Count - 1];
                        lastMain.End = openDates[i - 1];
                        if (mainFutures.Count > 1 && lastMain.Last < 25)
                        {
                            MainContractInfo lastMain2 = mainFutures[mainFutures.Count - 2];
                            if (lastMain2.Code.Equals(mainCode))
                            {
                                mainFutures.RemoveAt(mainFutures.Count - 1);
                                addNew = false;
                            }
                        }
                    }
                    if (addNew)
                    {
                        MainContractInfo main = new MainContractInfo();
                        main.Variety = variety;
                        main.Code = mainCode;
                        main.Start = openDate;
                        mainFutures.Add(main);
                    }
                }
                lastMainCode = mainCode;
            }
            if (mainFutures.Count != 0)
            {
                MainContractInfo lMain = mainFutures[mainFutures.Count - 1];
                lMain.End = openDates[openDates.Count - 1];
            }
            return mainF;
        }

        private String GetMainCode(String path, List<CodeInfo> codes, int date)
        {
            //int maxHold = 0;
            long max = 0;
            String mainCode = null;
            for (int i = 0; i < codes.Count; i++)
            {
                String code = codes[i].Code;
                if (code.Contains("MI") || code.Contains("0000"))
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
            return path + "\\" + code + "\\tick\\" + code + "_" + date + ".csv";
        }
    }

    public class MainFutures
    {
        public List<MainContractInfo> mainFutures = new List<MainContractInfo>();

        public String Variety;

        public String GetMainFutureCode(int date)
        {
            for (int i = 0; i < mainFutures.Count; i++)
            {
                MainContractInfo period = mainFutures[i];
                if (period.IsBetween(date))
                    return period.Code;
            }
            return "";
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < mainFutures.Count; i++)
            {
                sb.Append(mainFutures[i]).Append("\r\n");
            }
            return sb.ToString();
        }
    }
}
