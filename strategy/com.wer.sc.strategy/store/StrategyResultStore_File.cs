using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.strategy;
using System.IO;
using com.wer.sc.utils;
using System.Xml;

namespace com.wer.sc.strategy.store
{
    /// <summary>
    /// 策略结果保存方式：
    /// --20180307
    ///     --双顶_20170101_20171001
    ///         --双顶_20170101_20171001.strategyresult
    ///         --双顶_20170101_20171001.结果集1.queryresult
    ///         --双顶_20170101_20171001.结果集2.queryresult
    ///             --rb
    ///                 双顶_rb_20170101_20171001.strategyresult_code
    ///                 双顶_rb_20170101_20171001.strategyresult_code.shape
    ///                 双顶_rb_20170101_20171001.strategyresult_code.trader
    ///             --ma
    ///             --m
    /// --20180308
    ///     --双顶_20170101_20171001
    ///     --双顶_20170101_20171001
    /// </summary>
    public class StrategyResultStore_File : IStrategyResultStore
    {
        private StrategyDataPathUtils dataPathUtils;

        public StrategyResultStore_File(StrategyDataPathUtils dataPathUtils)
        {
            this.dataPathUtils = dataPathUtils;
        }

        public IList<int> GetAllSavedDays()
        {
            string strategyResultPath = dataPathUtils.GetStrategyResultPath();
            string[] paths = Directory.GetDirectories(strategyResultPath);
            List<int> days = new List<int>(paths.Length);
            for (int i = 0; i < paths.Length; i++)
            {
                string path = paths[i];
                string daystr = path.Substring(path.LastIndexOf('\\') + 1);
                int day;
                bool isInt = int.TryParse(daystr, out day);
                if (isInt)
                    days.Add(day);
            }
            return days;
        }

        public IStrategyResult LoadStrategyResult(int day, string resultName)
        {
            string resultPath = dataPathUtils.GetStrategyResultFilePath(day, resultName);
            StrategyResult result = new StrategyResult();
            result.Load(resultPath);
            //XmlDocument doc = new XmlDocument();
            //doc.Load(resultPath);
            //result.Load(doc.DocumentElement);
            IList<string> codes = LoadStrategyResultCodes(day, resultName);
            for (int i = 0; i < codes.Count; i++)
            {
                IStrategyResult_CodePeriod result_Code = LoadStrategyResult_CodePeriod(day, resultName, codes[i]);
                result.AddStrategyResult_Code(result_Code);
            }
            return result;
        }

        public IList<string> LoadStrategyResultCodes(int day, string resultName)
        {
            string resultPath = dataPathUtils.GetStrategyResultPath(day, resultName);
            return GetSubDirs(resultPath);
        }

        private static IList<string> GetSubDirs(string resultPath)
        {
            DirectoryInfo dir = new DirectoryInfo(resultPath);
            DirectoryInfo[] subDirs = dir.GetDirectories();
            string[] arr = new string[subDirs.Length];
            for (int i = 0; i < subDirs.Length; i++)
            {
                arr[i] = subDirs[i].Name;
            }
            return arr;
        }

        public IList<string> LoadStrategyResultNames(int day)
        {
            string resultPath = dataPathUtils.GetStrategyResultPath(day);
            return GetSubDirs(resultPath);
        }

        public IStrategyResult_CodePeriod LoadStrategyResult_CodePeriod(int day, string resultName, string code)
        {
            string resultPath = dataPathUtils.GetStrategyResult_CodeFilePath(day, resultName, code);
            StrategyResult_CodePeriod result = new StrategyResult_CodePeriod();
            result.Load(resultPath);
            return result;
        }

        public bool ExistStrategyResult(int day, string resultName)
        {
            string resultPath = dataPathUtils.GetStrategyResultPath(day, resultName);
            return File.Exists(resultPath);
        }

        public bool ExistStrategyResult_CodePeriod(int day, string resultName, string code)
        {
            string resultPath = dataPathUtils.GetStrategyResult_CodeFilePath(day, resultName, code);
            return File.Exists(resultPath);
        }

        public void Save(int day, IStrategyResult strategyResult)
        {
            string path = dataPathUtils.GetStrategyResultFilePath(day, strategyResult.Name);
            FileUtils.EnsureParentDirExist(path);
            strategyResult.Save(path);
        }

        public void SaveQueryResult(int day, IStrategyResult strategyResult)
        {
            string path = dataPathUtils.GetStrategyResultFilePath(day, strategyResult.Name);
            FileInfo f = new FileInfo(path);
            string name = f.Name;
            string title = name.Substring(0, name.LastIndexOf('.'));

            IList<IStrategyQueryResult> queryResults = strategyResult.StrategyQueryResultManager.GetQueryResults();
            for (int i = 0; i < queryResults.Count; i++)
            {
                IStrategyQueryResult queryResult = queryResults[i];
                string querypath = f.Directory.FullName + "\\" + title + "." + queryResult.Name + ".queryresult";
                queryResult.Save(querypath);
            }
        }

        public void Save(int day, string resultName, IStrategyResult_CodePeriod strategyResult)
        {
            string path = dataPathUtils.GetStrategyResult_CodeFilePath(day, resultName, strategyResult.CodePeriod.Code);
            FileUtils.EnsureParentDirExist(path);
            strategyResult.Save(path);
        }
    }
}
