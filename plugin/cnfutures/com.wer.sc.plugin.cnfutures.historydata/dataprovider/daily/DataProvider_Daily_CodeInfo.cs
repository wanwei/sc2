using com.wer.sc.data;
using com.wer.sc.plugin.cnfutures.config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin.cnfutures.historydata.dataprovider.daily
{
    public class DataProvider_Daily_CodeInfo
    {
        string srcDataPath;
        string pluginPath;

        private DataLoader_Variety dataLoader_Variety;

        public DataProvider_Daily_CodeInfo(string srcDataPath, string pluginPath)
        {
            this.srcDataPath = srcDataPath;
            this.pluginPath = pluginPath;
            this.dataLoader_Variety = new DataLoader_Variety(pluginPath);
        }
        private List<CodeInfo> allCodes;

        public List<CodeInfo> GetCodeInfo()
        {
            if (allCodes != null)
                return allCodes;
            allCodes = GenerateCodes();
            return allCodes;
        }

        public List<CodeInfo> GenerateCodes()
        {
            List<CodeInfo> codes = new List<CodeInfo>();
            HashSet<string> hashset = new HashSet<string>();
            LoopDic(srcDataPath, codes, hashset);
            codes.Sort(new CodeInfoComparer());
            GenerateIndexCodes(codes);
            return codes;
        }

        private void GenerateIndexCodes(List<CodeInfo> codes)
        {
            List<CodeInfo> indexCodes = new List<CodeInfo>();
            HashSet<string> set_Variety = new HashSet<string>();
            for (int i = 0; i < codes.Count; i++)
            {
                CodeInfo codeInfo = codes[i];
                string code = codeInfo.Code;
                CodeIdParser parser = new CodeIdParser(code);
                string variety = parser.VarietyId;
                if (!set_Variety.Contains(variety))
                {
                    indexCodes.Add(CodeInfoUtils.GetCodeInfo(variety + "0000", dataLoader_Variety));
                    indexCodes.Add(CodeInfoUtils.GetCodeInfo(variety + "MI", dataLoader_Variety));
                    set_Variety.Add(variety);
                }
            }
            codes.AddRange(indexCodes);
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
