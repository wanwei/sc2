using com.wer.sc.data;
using com.wer.sc.data.utils;
using com.wer.sc.plugin.cnfutures.historydata.dataloader;
using com.wer.sc.plugin.historydata;
using com.wer.sc.utils;
using com.wer.sc.utils.update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin.cnfutures.historydata.dataupdater
{
    public class Step_TickData_CodeDate : IStep
    {
        private string code;
        private int date;
        private IDataLoader dataLoader;

        public Step_TickData_CodeDate(IDataLoader dataLoader, string code, int date)
        {
            this.dataLoader = dataLoader;
            this.code = code;
            this.date = date;
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
                return "更新" + code + "在" + date + "的Tick数据";
            }
        }

        public string Proceed()
        {
            try
            {
                ITickData tickData = this.dataLoader.LoadTickData(code, date);
                if (tickData == null)
                    return code + "-" + date + "没有数据";
                string path = CsvHistoryData_PathUtils.GetTickDataPath(dataLoader.GetTargetDataPath(), code, date);
                CsvUtils_TickData.Save(path, tickData);
            }
            catch (Exception e)
            {                
                LogHelper.Error(GetType(), new ApplicationException("更新" + code + "-" + date + "的tick数据出错", e));
            }
            return code + "-" + date + "的Tick数据更新完成";
        }
    }
}
