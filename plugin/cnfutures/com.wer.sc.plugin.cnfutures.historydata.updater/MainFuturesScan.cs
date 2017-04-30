using com.wer.sc.data;
using com.wer.sc.plugin.cnfutures.historydata.dataloader;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin.cnfutures.historydata.datapreparer
{
    public class MainFuturesScan
    {
        private string srcDataPath;

        private IDataLoader dataLoader_CodeInfo;

        public MainFuturesScan(IDataLoader dataLoader_CodeInfo)
        {
            this.dataLoader_CodeInfo = dataLoader_CodeInfo;
        }

        public MainFutures Scan(String variety, List<int> openDates)
        {
            MainFutures mainF = new MainFutures();
            mainF.Variety = variety;
            List<MainFuturesPeriod> mainFutures = mainF.mainFutures;
            List<CodeInfo> codes = dataLoader_CodeInfo.LoadInstruments(variety.ToUpper());
            String lastMainCode = "";
            for (int i = 0; i < openDates.Count; i++)
            {
                int openDate = openDates[i];
                String mainCode = GetMainCode(srcDataPath, codes, openDate);
                if (mainCode == null)
                    continue;
                if (!mainCode.Equals(lastMainCode))
                {
                    bool addNew = true;
                    if (mainFutures.Count > 0)
                    {
                        MainFuturesPeriod lastMain = mainFutures[mainFutures.Count - 1];
                        lastMain.End = openDates[i - 1];
                        if (mainFutures.Count > 1 && lastMain.Last < 25)
                        {
                            MainFuturesPeriod lastMain2 = mainFutures[mainFutures.Count - 2];
                            if (lastMain2.Code.Equals(mainCode))
                            {
                                mainFutures.RemoveAt(mainFutures.Count - 1);
                                addNew = false;
                            }
                        }
                    }
                    if (addNew)
                    {
                        MainFuturesPeriod main = new MainFuturesPeriod();
                        main.Code = mainCode;
                        main.Start = openDate;
                        mainFutures.Add(main);
                    }
                }
                lastMainCode = mainCode;
            }
            MainFuturesPeriod lMain = mainFutures[mainFutures.Count - 1];
            lMain.End = openDates[openDates.Count - 1];

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

    public class MainFutures
    {
        public List<MainFuturesPeriod> mainFutures = new List<MainFuturesPeriod>();

        public String Variety;

        public String GetMainFutureCode(int date)
        {
            for (int i = 0; i < mainFutures.Count; i++)
            {
                MainFuturesPeriod period = mainFutures[i];
                if (period.IsBetween(date))
                    return period.Code;
            }
            return "";
        }
    }

    public class MainFuturesPeriod
    {
        public String Code;

        public int Start;

        public int End;

        public int Last
        {
            get { return End - Start; }
        }

        public bool IsBetween(int date)
        {
            return date >= Start && date <= End;
        }

        override
        public String ToString()
        {
            return Code + "," + Start + "," + End;
        }
    }
}