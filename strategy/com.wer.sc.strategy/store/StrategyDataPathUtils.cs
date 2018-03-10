using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy.store
{
    /// --20180307
    ///     --双顶_174421_20170101_20171001
    ///         --双顶_174421_20170101_20171001.strategyresult
    ///             --rb
    ///                 双顶_174421_20170101_20171001_rb.strategyresult_code
    ///                 双顶_174421_20170101_20171001_rb.strategyresult_code.shape
    ///                 双顶_174421_20170101_20171001_rb.strategyresult_code.trader
    ///             --ma
    ///             --m
    /// --20180308
    ///     --双顶_20170101_20171001
    ///     --双顶_20170101_20171001
    public class StrategyDataPathUtils
    {
        private String dataPath;

        public StrategyDataPathUtils(String dataPath)
        {
            this.dataPath = RealPath(dataPath);
        }

        public string GetStrategyResultPath()
        {
            return dataPath + "\\STRATEGYRESULT\\";
        }

        public string GetStrategyResultPath(int day)
        {
            return GetStrategyResultPath() + day + "\\";
        }

        public string GetStrategyResultPath(int day, string strategyName)
        {
            return GetStrategyResultPath(day) + strategyName + "\\";
        }

        public string GetStrategyResultFilePath(int day, string strategyName)
        {
            return GetStrategyResultPath(day, strategyName) + strategyName + ".strategyresult";
        }

        public string GetStrategyResult_CodeFilePath(int day, string strategyName, string code)
        {
            return GetStrategyResultPath(day) + strategyName + "\\" + code + "\\" + strategyName + "_" + code + ".strategyresult_code";
        }

        private String RealPath(String path2)
        {
            String path = path2;
            if (!path.EndsWith("\\") || !path.EndsWith("/"))
                path += "\\";
            return path;
        }
    }
}
