using com.wer.sc.data.utils;
using com.wer.sc.plugin.cnstock.historydata.download.sina;
using com.wer.sc.utils.update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin.cnstock.historydata.dataupdater
{
    public class Step_TradingDay : IStep
    {
        public int ProgressStep
        {
            get
            {
                return 1;
            }
        }

        public string StepDesc
        {
            get
            {
                return "更新交易日数据";
            }
        }

        public string Proceed()
        {
            Download_Sina download = new Download_Sina(DataConst.SINAPATH);
            List<int> tradingDays = download.GetTradingDays();
            string targetPath = DataConst.CSVPATH + @"\tradingdays.csv";
            CsvUtils_TradingDay.Save(targetPath, tradingDays);
            return "更新完成交易日数据";
        }
    }
}
