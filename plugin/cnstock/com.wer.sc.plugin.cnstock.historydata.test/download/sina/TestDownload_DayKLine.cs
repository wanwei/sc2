using com.wer.sc.utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin.cnstock.historydata.download.sina
{
    [TestClass]
    public class TestDownload_DayKLine
    {
        [TestMethod]
        public void TestDownloadKLineIndex()
        {
            List<string[]> arr = Download_DayKLine.RequestIndex("sh000001", 20180209);
            for (int i = 0; i < arr.Count; i++)
            {
                Console.WriteLine(ListUtils.ToString(arr[i]));
            }
        }

        [TestMethod]
        public void TestDownloadKLine()
        {
            List<string[]> arr = Download_DayKLine.RequestIndex("sz000002", 20171009);
            for (int i = 0; i < arr.Count; i++)
            {
                Console.WriteLine(ListUtils.ToString(arr[i]));
            }
        }
    }
}
