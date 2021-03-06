﻿using com.wer.sc.data;
using com.wer.sc.data.utils;
using com.wer.sc.plugin.cnfutures.config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin.cnfutures.historydata.dataprovider.biaopuyonghua
{
    /// <summary>
    /// 
    /// </summary>
    public class DataProvider_BiaoPuYongHua_CodeInfo
    {
        private string srcDataPath;

        private string pluginPath;

        private DataLoader_Variety dataLoader_Variety;

        private List<CodeInfo> codes;

        public DataProvider_BiaoPuYongHua_CodeInfo(string srcDataPath, string pluginPath)
        {
            this.srcDataPath = srcDataPath;
            this.pluginPath = pluginPath;
            this.dataLoader_Variety = new DataLoader_Variety(pluginPath);
        }

        public List<CodeInfo> GetNewCodes()
        {
            if (codes != null)
                return codes;
            string path = pluginPath + "\\config\\biaopuyonghua.instruments.csv";
            if (File.Exists(path))
                codes = CsvUtils_Code.Load(path);
            else
                codes = GenerateCodes();
            return codes;
        }

        public List<CodeInfo> GenerateCodes()
        {
            List<CodeInfo> codes = new List<CodeInfo>();
            HashSet<string> set = new HashSet<string>();
            LoopByExchange(srcDataPath + "\\DL", codes, set);
            LoopByExchange(srcDataPath + "\\SQ", codes, set);
            LoopByExchange(srcDataPath + "\\ZZ", codes, set);

            string[] varieties = this.dic_Variety_Start.Keys.ToArray<String>();
            for (int i = 0; i < varieties.Length; i++)
            {
                string variety = varieties[i];                
                int start = this.dic_Variety_Start[variety];

                string indexId = variety + "0000";
                CodeInfo codeInfo = CodeInfoUtils.GetCodeInfo(indexId, dataLoader_Variety);
                codeInfo.Start = start;
                codes.Add(codeInfo);

                string miId = variety + "MI";
                codeInfo = CodeInfoUtils.GetCodeInfo(miId, dataLoader_Variety);
                codeInfo.Start = start;
                codes.Add(codeInfo);
            }

            //for (int i = 0; i < indexCodes.Count; i++)
            //{
            //    CodeInfo codeInfo = indexCodes[i];
            //    if (dic_Variety_Start.ContainsKey(codeInfo.Catelog))
            //        codeInfo.Start = dic_Variety_Start[codeInfo.Catelog];
            //}

            codes.Sort(new CodeInfoComparer());
            return codes;
        }

        private void LoopByExchange(string path, List<CodeInfo> codes, HashSet<string> set_Codes)
        {
            DirectoryInfo dir = new DirectoryInfo(path);
            DirectoryInfo[] subDirs = dir.GetDirectories();
            for (int i = 0; i < subDirs.Length; i++)
            {
                DirectoryInfo subDir = subDirs[i];
                Loop(subDir.FullName, int.Parse(subDir.Name), codes, set_Codes);
            }
        }

        private void Loop(string path, int tradingDay, List<CodeInfo> codes, HashSet<string> set_Codes)
        {
            DirectoryInfo dir = new DirectoryInfo(path);
            FileInfo[] files = dir.GetFiles();

            for (int i = 0; i < files.Length; i++)
            {
                string fileName = files[i].Name;
                string oldCodeId = fileName.Substring(0, fileName.IndexOf('_'));
                CodeIdParser parser = new CodeIdParser(oldCodeId);
                //if (parser.Suffix == "MI")
                //    continue;
                string complexCodeId = CodeInfoUtils.GetComplexCodeId(parser, tradingDay);
                if (complexCodeId == null)
                    continue;
                CodeIdParser parser2 = new CodeIdParser(complexCodeId);
                if (parser2.Suffix == null)
                    continue;
                if (parser2.Suffix.Length < 2 || parser2.Suffix.Length > 4)
                    continue;
                //string varietyId = CodeInfoUtils.GetVariety(oldCodeId);
                //string[] splitCodeArr = CodeInfoUtils.SplitCodeId(oldCodeId);
                //string newCodeId = GetCodeId(tradingDay, splitCodeArr);
                if (set_Codes.Contains(complexCodeId))
                    continue;
                set_Codes.Add(complexCodeId);
                CodeInfo codeInfo = CodeInfoUtils.GetCodeInfo(complexCodeId, dataLoader_Variety);
                if (codeInfo == null)
                    continue;
                if (!codeInfo.Code.EndsWith("0000") && !codeInfo.Code.EndsWith("MI"))
                {
                    codeInfo.Start = tradingDay;
                    SetVarietyStart(parser2.VarietyId, tradingDay);
                }
                else
                {
                    //indexCodes.Add(codeInfo);
                    continue;
                }
                codes.Add(codeInfo);
            }
        }

        private List<CodeInfo> indexCodes = new List<CodeInfo>();

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

        private string GetCodeId(int tradingDay, string[] splitCodeArr)
        {
            //双年合约
            if (splitCodeArr[1] != "")
            {
                return "";
            }
            return "";
        }
    }
}