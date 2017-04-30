using com.wer.sc.data.provider;
using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace com.wer.sc.data.cnfutures.generator
{
    /// <summary>
    /// 数据生成
    /// </summary>
    public class DataGenerator_TickData
    {
        public const int PROGRESS_PERIOD = 10;
        private bool isCancel = false;

        public bool IsCancel
        {
            get
            {
                return isCancel;
            }

            set
            {
                isCancel = value;
            }
        }


        private TargetPathConfig targetPathConfig;

        private SrcPathConfig srcPathConfig;

        private String[] varieties;

        private List<int> openDates;

        private DataProvider_CodeInfo provider_CodeInfo;

        private DataProvider_OpenDate provider_OpenDate;

        private DataProvider_OpenTime provider_OpenTime;

        private DataProvider_TickData provider_TickData;

        private DataGenerator_Normal generator_Normal;

        private DataGenerator_Index generator_Index;

        private DataGenerator_Main generator_Main;

        public DataGenerator_TickData(String srcPath, String targetPath, String[] varieties)
        {            
            String configPath = ScConfig.Instance.ScPath + "\\com.wer.sc.data.cnfutures\\";
            //生成开盘日数据
            DataGenerator_OpenDate generator_OpenDate = new DataGenerator_OpenDate(configPath, srcPath);
            generator_OpenDate.Generate();

            this.varieties = varieties;
            if (this.varieties == null)
                varieties = provider_CodeInfo.GetVarieties().ToArray();

            provider_CodeInfo = new DataProvider_CodeInfo(configPath);
            provider_OpenDate = new DataProvider_OpenDate(configPath);
            provider_OpenTime = new DataProvider_OpenTime(configPath);
            provider_TickData = new DataProvider_TickData(targetPath);

            this.openDates = provider_OpenDate.GetOpenDates();

            this.targetPathConfig = new TargetPathConfig(targetPath);
            this.srcPathConfig = new SrcPathConfig(srcPath, provider_CodeInfo);

            this.generator_Normal = new DataGenerator_Normal(srcPath, provider_CodeInfo, provider_OpenTime);
            this.generator_Index = new DataGenerator_Index(targetPath, provider_CodeInfo, provider_OpenTime, provider_TickData);
            this.generator_Main = new DataGenerator_Main(targetPath, provider_CodeInfo, provider_TickData);
        }

        public void Generate()
        {
            Thread thread1 = new Thread(new ThreadStart(GenerateInternal));
            thread1.Start();
        }

        private void GenerateInternal()
        {
            GenerateInfo generateInfo = DataPrepare();
            if (IsCancel)
                return;
            Generate(generateInfo);
        }

        #region prepare

        private GenerateInfo DataPrepare()
        {
            GenerateInfo generate = GetGeneraters();

            if (AfterPrepared != null)
                AfterPrepared(generate);
            return generate;
        }

        private GenerateInfo GetGeneraters()
        {
            GenerateInfo generateInfo = new GenerateInfo();
            for (int i = 0; i < varieties.Length; i++)
            {
                generateInfo.generates.Add(GetGenerater(varieties[i]));
            }
            return generateInfo;
        }

        private GenerateInfo_Variety GetGenerater(String variety)
        {
            GenerateInfo_Variety g = new GenerateInfo_Variety();
            g.variety = variety;
            //以13结尾的表示指数，如M13表示豆粕指数。只有当日指数生成了，才认为当日数据已全部生成
            String indexCode = variety + "13";
            List<int> updatedDates = targetPathConfig.GetUpdatedDates(indexCode);
            List<int> dates = new List<int>();
            for (int i = 0; i < openDates.Count; i++)
            {
                int date = openDates[i];
                if (!updatedDates.Contains(date))
                    dates.Add(date);
            }
            g.dates = dates;
            return g;
        }

        #endregion

        #region generate

        private void Generate(GenerateInfo generate)
        {
            for (int i = 0; i < generate.generates.Count; i++)
            {
                Generate_Variety(generate.generates[i]);
                if (IsCancel)
                    return;
            }
            AfterGenerated(new GeneratedArgs());
        }

        private void Generate_Variety(GenerateInfo_Variety generate_variety)
        {
            List<int> dates = generate_variety.dates;
            for (int i = 0; i < dates.Count; i++)
            {
                if (IsCancel)
                    return;
                int date = dates[i];
                Generate(generate_variety.variety, date);

                int nextIndex = i + 1;
                if (nextIndex >= dates.Count)
                    continue;
                if ((nextIndex) % PROGRESS_PERIOD == 0)
                {
                    if (AfterGeneratedPeriod != null)
                    {
                        GeneratedPeriodArgs args = new GeneratedPeriodArgs();
                        args.nextStartDate = date;
                        int endIndex = i + PROGRESS_PERIOD;
                        endIndex = endIndex >= dates.Count ? dates.Count - 1 : endIndex;
                        args.nextEndDate = dates[endIndex];
                        args.variety = generate_variety.variety;
                        AfterGeneratedPeriod(args);
                    }
                }
            }
        }

        private void Generate(String variety, int date)
        {
            List<CodeInfo> codes = provider_CodeInfo.GetCodes(variety);
            //mi结尾是主连，13结尾是指数
            for (int i = 0; i < codes.Count; i++)
            {
                CodeInfo code = codes[i];
                String upperCode = code.Code.ToUpper();
                if (upperCode.EndsWith("MI") || upperCode.EndsWith("13"))
                    continue;
                GenerateNormal(code.Code, date);
            }
            GenerateMain(variety, date);
            GenerateIndex(variety, date);
        }

        private void GenerateNormal(String code, int date)
        {
            TickData data = generator_Normal.Generate(code, date);
            if (data == null)
                return;
            String path = targetPathConfig.GetFilePath(code, date);
            WriteTickData(data, path);
        }

        private static void WriteTickData(TickData data, string path)
        {
            string[] contents = new string[data.Length];
            for (int i = 0; i < data.Length; i++)
            {
                data.BarPos = i;
                contents[i] = data.ToString();
            }

            DirectoryInfo dir = new FileInfo(path).Directory;
            if (!dir.Exists)
                Directory.CreateDirectory(dir.FullName);
            File.WriteAllLines(path, contents);
        }

        private void GenerateMain(String variety, int date)
        {
            TickData data = generator_Main.Generate(variety, date);
            String path = targetPathConfig.GetFilePath(variety + "MI", date);
            WriteTickData(data, path);
        }

        private void GenerateIndex(string variety, int date)
        {
            TickData data = generator_Index.Generate(variety, date);
            String path = targetPathConfig.GetFilePath(variety + "13", date);
            WriteTickData(data, path);
        }

        #endregion

        public AfterPreparedHandler AfterPrepared;

        public AfterGeneratedPeriodHandler AfterGeneratedPeriod;

        public AfterGeneratedHandler AfterGenerated;
    }

    public delegate void AfterPreparedHandler(GenerateInfo generateInfo);
    public delegate void AfterGeneratedPeriodHandler(GeneratedPeriodArgs args);
    public delegate void AfterGeneratedHandler(GeneratedArgs args);


    public class GeneratedArgs
    {
    }

    public class GeneratedPeriodArgs
    {
        public String variety;

        public int generatedStartDate;

        public int generatedEndDate;

        public int nextStartDate;

        public int nextEndDate;
    }

    public class GenerateInfo
    {
        public List<GenerateInfo_Variety> generates = new List<GenerateInfo_Variety>();

        public int GetPeriodCount()
        {
            int max = 0;
            for (int i = 0; i < generates.Count; i++)
            {
                max += generates[i].GetCalcPeriodCount();
            }
            return max;
        }
    }

    public class GenerateInfo_Variety
    {
        public String variety;

        public List<int> dates;

        public int GetCalcPeriodCount()
        {
            int progress = DataGenerator_TickData.PROGRESS_PERIOD;
            if (dates.Count % progress == 0)
                return (dates.Count / progress);
            return (dates.Count / progress) + 1;
        }
    }

    public class SrcPathConfig
    {
        private String srcPath;
        private DataProvider_CodeInfo dataReader_CodeInfo;
        public SrcPathConfig(String srcPath, DataProvider_CodeInfo dataReader_CodeInfo)
        {
            this.srcPath = srcPath;
            this.dataReader_CodeInfo = dataReader_CodeInfo;
        }

        public String GetCodePath(String code, int date)
        {
            String path = srcPath + "\\" + dataReader_CodeInfo.GetBelongMarket(code) + "\\" + code + "_" + date + ".csv";
            return path;
        }
    }

    public class TargetPathConfig
    {
        private String rootPath;

        public TargetPathConfig(String rootPath)
        {
            this.rootPath = rootPath;
        }

        public String GetPath(String code)
        {
            return rootPath + "\\" + code + "\\";
        }

        public String GetFilePath(String code, int date)
        {
            return GetPath(code) + code + "_" + date + ".csv";
        }

        public List<int> GetUpdatedDates(String code)
        {
            String path = GetPath(code);
            return GetOpenDates(path);
        }

        public static List<int> GetOpenDates(string path)
        {
            if (!Directory.Exists(path))
                return new List<int>();
            String[] files = Directory.GetFiles(path);
            List<int> openDates = new List<int>();
            foreach (String file in files)
            {
                int openDate;
                int index = file.LastIndexOf('_');
                bool isInt = int.TryParse(file.Substring(index + 1, 8), out openDate);
                if (isInt)
                    openDates.Add(openDate);
            }
            return openDates;
        }
    }
}
