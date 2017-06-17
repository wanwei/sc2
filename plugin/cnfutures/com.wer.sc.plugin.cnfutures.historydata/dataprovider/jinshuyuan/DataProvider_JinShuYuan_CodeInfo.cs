using com.wer.sc.data;
using com.wer.sc.data.utils;
using com.wer.sc.plugin.cnfutures.config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin.cnfutures.historydata.dataprovider.jinshuyuan
{
    public class DataProvider_JinShuYuan_CodeInfo
    {
        private List<CodeInfo> allCodes;

        private string srcDataPath;

        private string pluginPath;

        private DataLoader_Variety dataLoader_Variety;

        public DataProvider_JinShuYuan_CodeInfo(string srcDataPath, string pluginPath)
        {
            this.srcDataPath = srcDataPath;
            this.pluginPath = pluginPath;
            this.dataLoader_Variety = new DataLoader_Variety(pluginPath);
        }

        public List<CodeInfo> GetCodeInfo()
        {
            if (allCodes != null)
                return allCodes;
            string path = pluginPath + "\\jinshuyuan.instruments.csv";
            if (File.Exists(path))
                allCodes = CsvUtils_Code.Load(path);
            else
                allCodes = GenerateCodes();

            List<VarietyInfo> varieties = dataLoader_Variety.GetAllVarieties();
            for (int i = 0; i < varieties.Count; i++)
            {
                VarietyInfo variety = varieties[i];
                CodeInfo codeInfo = new CodeInfo(variety.Code + "0000", variety.Name + "0000", variety.Code, variety.Name, 0, 0, variety.Exchange, variety.Code + "13");
                allCodes.Add(codeInfo);
                CodeInfo codeInfo2 = new CodeInfo(variety.Code + "MI", variety.Name + "MI", variety.Code, variety.Name, 0, 0, variety.Exchange, variety.Code + "MI");
                allCodes.Add(codeInfo2);
            }

            allCodes.Sort(new CodeInfoComparer());
            return allCodes;
        }

        public List<CodeInfo> GenerateCodes()
        {
            List<CodeInfo> codes = new List<CodeInfo>();
            HashSet<string> hashset = new HashSet<string>();
            LoopDic(srcDataPath, codes, hashset);
            allCodes = codes;
            return codes;
        }

        private void LoopDic(string path, List<CodeInfo> codes, HashSet<string> hashset)
        {
            string[] files = Directory.GetFiles(path);
            for (int i = 0; i < files.Length; i++)
                GetCodeId(files[i], codes, hashset);

            string[] dirs = Directory.GetDirectories(path);
            for (int i = 0; i < dirs.Length; i++)
                LoopDic(dirs[i], codes, hashset);
        }

        private void GetCodeId(string fileName, List<CodeInfo> codes, HashSet<string> hashset)
        {
            int startIndex = fileName.LastIndexOf('\\');
            int endIndex = fileName.LastIndexOf('_');
            string codeId = fileName.Substring(startIndex + 1, endIndex - startIndex - 1);
            if (codeId.IndexOf("主力连续") >= 0)
                return;
            CodeIdParser parser = new CodeIdParser(codeId);
            if (parser.Suffix.Length == 3)
            {
                codeId = parser.VarietyId + "1" + parser.Suffix;
            }

            if (!hashset.Contains(codeId))
            {
                hashset.Add(codeId);
                CodeInfo codeInfo = CodeInfoUtils.GetCodeInfo(codeId, dataLoader_Variety);
                if (codeInfo == null)
                    return;
                codes.Add(codeInfo);
            }
        }
    }
}