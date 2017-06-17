using com.wer.sc.plugin.cnfutures.config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin.cnfutures.historydata.dataprovider.biaopuyonghua
{
    public class GenerateCodeIpoDate
    {
        private DataLoader_InstrumentInfo dataLoader;

        private string dataPath;

        public GenerateCodeIpoDate(string dataPath, DataLoader_InstrumentInfo dataLoader)
        {
            this.dataPath = dataPath;
            this.dataLoader = dataLoader;
        }

        public int GetIpoDate(String oldCodeId)
        {
            
            return -1;
        }
    }
}