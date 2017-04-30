using com.wer.sc.data;
using com.wer.sc.data.reader;
using com.wer.sc.data.utils;
using com.wer.sc.plugin.cnfutures.historydata.dataloader;
using com.wer.sc.plugin.historydata;
using com.wer.sc.plugin.historydata.utils;
using com.wer.sc.utils.update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin.cnfutures.historydata.dataupdater
{
    public class Step_TradingSession : IStep
    {
        private string code;

        private List<int> dates;

        private IDataLoader dataLoader;

        //private IDataLoader dataLoader2;

        public Step_TradingSession(string code, IDataLoader dataLoader)
        {
            this.code = code;
            this.dataLoader = dataLoader;
        }

        public int ProgressStep
        {
            get
            {
                return 10;
            }
        }

        public string StepDesc
        {
            get
            {
                return "更新" + code + "的开盘时间";
            }
        }

        public string Proceed()
        {
            List<TradingSession> result = GetAllDayStartTimes();
            if (result == null)
                return code + "的开盘时间已经是最新的，不需要更新";
            string path = CsvHistoryData_PathUtils.GetTradingSessionPath(dataLoader.CsvDataPath, code);
            CsvUtils_TradingSession.Save(path, result);
            return "更新完成" + code + "的开盘时间";
        }

        /// <summary>
        /// 得到该合约的所有开盘时间，如果返回空，则表示现在数据已经是最新的
        /// </summary>
        /// <returns></returns>
        public List<TradingSession> GetAllDayStartTimes()
        {
            return dataLoader.LoadTradingSessions(this.code);
        }
      
        public override string ToString()
        {
            return StepDesc;
        }
    }
}