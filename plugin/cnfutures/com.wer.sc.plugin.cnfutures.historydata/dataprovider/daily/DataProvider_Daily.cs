using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.data;
using com.wer.sc.data.reader;
using com.wer.sc.plugin.cnfutures.config;
using com.wer.sc.data.cnfutures;
using com.wer.sc.plugin.cnfutures.historydata.dataprovider.jinshuyuan;
using System.IO;

namespace com.wer.sc.plugin.cnfutures.historydata.dataprovider.daily
{
    public class DataProvider_Daily : IDataProvider
    {
        private string srcDataPath;

        private DataProvider_Daily_CodeInfo dataLoader_Daily_CodeInfo;

        private DataProvider_Daily_TradingDay dataLoader_TradingDay;

        private DataLoader_Variety dataLoader_Variety;

        public DataProvider_Daily(string srcDataPath, string pluginPath)
        {
            this.srcDataPath = srcDataPath;
            this.dataLoader_Daily_CodeInfo = new DataProvider_Daily_CodeInfo(srcDataPath, pluginPath);
            this.dataLoader_TradingDay = new DataProvider_Daily_TradingDay(srcDataPath);
            this.dataLoader_Variety = new DataLoader_Variety(pluginPath);
        }

        public List<CodeInfo> GetNewCodes()
        {
            return dataLoader_Daily_CodeInfo.GetCodeInfo();
            //List<CodeInfo> codes = new List<CodeInfo>();
            //HashSet<string> set_CodeIds = new HashSet<string>();
            //DirectoryInfo dir = new DirectoryInfo(srcDataPath);
            //DirectoryInfo[] subdirs = dir.GetDirectories();
            //for (int i = 0; i < subdirs.Length; i++)
            //{
            //    string path = subdirs[i].FullName;
            //    GetNewCodes(path, set_CodeIds, codes);
            //}
            //return codes;
        }

        //private void GetNewCodes(string path, HashSet<String> existCodes, List<CodeInfo> codes)
        //{
        //    DirectoryInfo dir = new DirectoryInfo(path);
        //    FileInfo[] files = dir.GetFiles();
        //    for (int i = 0; i < files.Length; i++)
        //    {
        //        string filename = files[i].Name;
        //        string codeId = filename.Substring(0, filename.IndexOf('_'));
        //        string varietyId = CodeInfoUtils.GetVariety(codeId);
        //        VarietyInfo variety = dataLoader_Variety.GetVariety(varietyId);
        //        string name = "";
        //        //CodeInfo code = new CodeInfo(codeId,name,varietyId,
        //    }
        //}

        public List<int> GetNewTradingDays()
        {
            return dataLoader_TradingDay.GetTradingDays();
        }

        public ITickData LoadTickData(string code, int date)
        {
            string path = srcDataPath + "\\" + date + "\\" + code + "_" + date + ".csv";
            return DataProvider_JinShuYuan_TickData.GetTickData(path);
        }

        public List<AppointUpdate> GetAppointUpdate()
        {
            return null;
        }
    }
}
