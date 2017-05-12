using com.wer.sc.data;
using com.wer.sc.data.utils;
using System;
using System.Collections.Generic;
using System.IO;

namespace com.wer.sc.plugin.cnfutures.config
{
    /// <summary>
    /// 股票或期货信息的原始数据加载
    /// </summary>
    internal class DataLoader_InstrumentInfo2
    {
        private PathUtils pathUtils;
        private Dictionary<String, CodeInfo> dicInstruments = new Dictionary<string, CodeInfo>();
        private List<CodeInfo> instruments;
        private Dictionary<String, String> dicCatelogs = new Dictionary<string, string>();
        private List<String> catelogs = new List<string>();

        public DataLoader_InstrumentInfo2(string pluginPath)
        {
            this.pathUtils = new PathUtils(pluginPath);
            InitInstruments();
            initCatelogs();
        }

        private void InitInstruments()
        {            
            this.instruments = CsvUtils_Code.LoadByContent(File.ReadAllText(pathUtils.InstrumentPath));
            for (int i = 0; i < instruments.Count; i++)
            {
                dicInstruments.Add(instruments[i].Code, instruments[i]);
            }
        }

        private void initCatelogs()
        {
            String[] lines = File.ReadAllText(pathUtils.CatelogPath).Split('\r');
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i].Trim();
                if (line.Equals(""))
                    continue;
                String[] strs = line.Split(',');
                dicCatelogs.Add(strs[0], strs[2]);
                catelogs.Add(strs[0]);
            }
        }

        public CodeInfo GetInstrument(String instrument)
        {
            return dicInstruments[instrument];
        }

        public List<CodeInfo> GetAllInstruments()
        {
            return instruments;
        }

        /// <summary>
        /// 得到合约所属市场
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public String GetBelongMarket(String code)
        {
            return dicCatelogs[dicInstruments[code.ToUpper()].Catelog];
        }

        /// <summary>
        /// 得到合约所属品种
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public String GetVariety(String code)
        {
            return dicInstruments[code.ToUpper()].Catelog;
        }

        /// <summary>
        /// 得到品种下所有合约
        /// </summary>
        /// <param name="variety"></param>
        /// <returns></returns>
        public List<CodeInfo> GetInstruments(String variety)
        {
            List<CodeInfo> vcodes = new List<CodeInfo>();
            for (int i = 0; i < instruments.Count; i++)
            {
                CodeInfo c = instruments[i];
                if (c.Catelog.Equals(variety))
                    vcodes.Add(c);
            }
            return vcodes;
        }
    }
}
