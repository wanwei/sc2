using com.wer.sc.data;
using com.wer.sc.data.utils;
using com.wer.sc.plugin.historydata;
using com.wer.sc.utils;
using com.wer.sc.utils.update;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin.cnfutures.historydata.dataupdater
{
    public class Step_TickData_CodeDate : IStep
    {
        private CodeInfo codeInfo;
        private int date;
        private DataUpdateHelper dataUpdateHelper;

        private bool overwrite = false;

        public Step_TickData_CodeDate(DataUpdateHelper dataUpdateHelper, CodeInfo codeInfo, int date)
        {
            this.dataUpdateHelper = dataUpdateHelper;
            this.codeInfo = codeInfo;
            this.date = date;
        }

        public Step_TickData_CodeDate(DataUpdateHelper dataUpdateHelper, CodeInfo codeInfo, int date, bool overwrite) : this(dataUpdateHelper, codeInfo, date)
        {
            this.overwrite = overwrite;
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
                return "更新" + codeInfo.Code + "在" + date + "的Tick数据";
            }
        }

        public string Proceed()
        {
            try
            {
                string path = this.dataUpdateHelper.GetPath_TickData(codeInfo.Code, date);
                if (!overwrite && File.Exists(path))
                    return codeInfo.Code + "-" + date + "的Tick数据已存在";
                ITickData tickData = this.dataUpdateHelper.GetNewTickData(codeInfo.ServerCode, date);
                if (tickData == null)
                    return codeInfo.Code + "-" + date + "没有数据";
                CsvUtils_TickData.Save(path, tickData);
            }
            catch (Exception e)
            {
                LogHelper.Error(GetType(), new ApplicationException("更新" + codeInfo.Code + "-" + date + "的tick数据出错", e));
            }
            return codeInfo.Code + "-" + date + "的Tick数据更新完成";
        }
    }
}
