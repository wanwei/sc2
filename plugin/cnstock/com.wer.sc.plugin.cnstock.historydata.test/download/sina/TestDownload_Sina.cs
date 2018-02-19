using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin.cnstock.historydata.download.sina
{
    [TestClass]
    public class TestDownload_Sina
    {
        [TestMethod]
        public void Test()
        {
            Download_Sina download = new Download_Sina(@"E:\Demo\DATASRC\Sina");
            //download.Download("");
            //download.DownloadDates();
            download.Download("sh600516");
            download.Download("sh600019");
            download.Download("sh601155");
            download.Download("sz002110");
            download.Download("sz000830");
            download.Download("sh601318");
            download.Download("sz000932");
        }
    }
}