using com.wer.sc.plugin.cnstock.historydata.download.sina;
using com.wer.sc.utils.update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin.cnstock.historydata.dataupdater
{
    public class Step_SinaDownload : IStep
    {
        public int ProgressStep
        {
            get
            {
                return 50;
            }
        }

        public string StepDesc
        {
            get
            {
                return "更新新浪数据";
            }
        }

        public string Proceed()
        {
            Download_Sina download = new Download_Sina(DataConst.SINAPATH);
            download.DownloadDates();
            download.Download("sh600516");
            download.Download("sh600019");
            download.Download("sh601155");
            download.Download("sz002110");
            download.Download("sz000830");
            download.Download("sh601318");
            download.Download("sz000932");
            return "新浪数据更新完毕";
        }
    }
}
