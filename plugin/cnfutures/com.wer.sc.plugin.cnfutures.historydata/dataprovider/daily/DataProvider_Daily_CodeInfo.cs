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

            GenerateIndexCodes(codes);           
            codes.Sort(new CodeInfoComparer());            
            return codes;
        }

        private void GenerateIndexCodes(List<CodeInfo> codes)
        {
            string[] keies = this.dic_Variety_Start.Keys.ToArray<string>();
            for (int i = 0; i < keies.Length; i++)
            {
                string variety = keies[i];
                int start = dic_Variety_Start[variety];

                CodeInfo codeInfo = CodeInfoUtils.GetCodeInfo(variety + "0000", dataLoader_Variety);
                codeInfo.Start = start;
                codes.Add(codeInfo);

                CodeInfo codeInfo2 = CodeInfoUtils.GetCodeInfo(variety + "MI", dataLoader_Variety);
                codeInfo2.Start = start;
                codes.Add(codeInfo2);
            }           
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

            int tradingDay = int.Parse(fileName.Substring(endIndex + 1, 8));
            SetVarietyStart(parser.VarietyId, tradingDay);

            if (!hashset.Contains(codeId))
            {
                hashset.Add(codeId);
                CodeInfo codeInfo = CodeInfoUtils.GetCodeInfo(codeId, dataLoader_Variety);                
                if (codeInfo == null)
                    return;
                codeInfo.Start = tradingDay;
                codes.Add(codeInfo);
            }
        }

        private Dictionary<string, int> dic_Variety_Start = new Dictionary<string, int>();

        private void SetVarietyStart(string variety, int date)
        {
            if (dic_Variety_Start.ContainsKey(variety))
            {
                int currentStart = dic_Variety_Start[variety];
                if (currentStart > date)
                    dic_Variety_Start[variety] = date;
            }
            else
                dic_Variety_Start.Add(variety, date);
        }
    }
}
