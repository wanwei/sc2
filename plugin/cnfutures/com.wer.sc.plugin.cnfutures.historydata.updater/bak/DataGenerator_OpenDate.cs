using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.cnfutures.generator
{
    public class DataGenerator_OpenDate
    {
        private List<int> openDates;

        private String configPath;

        private String dataPath;

        public DataGenerator_OpenDate(String configPath, String dataPath)
        {
            this.configPath = configPath;
            this.dataPath = dataPath;
        }

        public void Generate()
        {
            List<int> openDates = GetOpenDates();
            String[] openDateStr = new String[openDates.Count];
            for (int i = 0; i < openDates.Count; i++)
            {
                openDateStr[i] = openDates[i].ToString(); ;
            }
            File.WriteAllLines(configPath + "\\opendates.csv", openDateStr);
        }

        public List<int> GetOpenDates()
        {
            if (this.openDates != null)
                return this.openDates;
            String path = dataPath + "\\DL";
            this.openDates = GetOpenDates(path);
            return this.openDates;
        }

        public static List<int> GetOpenDates(string path)
        {
            if (!Directory.Exists(path))
                return new List<int>();
            String[] dirs = Directory.GetDirectories(path);
            List<int> openDates = new List<int>();
            foreach (String dir in dirs)
            {
                int openDate;
                int index = dir.LastIndexOf('\\');
                bool isInt = int.TryParse(dir.Substring(index + 1), out openDate);
                if (isInt)
                    openDates.Add(openDate);
            }
            return openDates;
        }
    }
}
