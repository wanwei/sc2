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
    public class TestDownload_Tick
    {
        [TestMethod]
        public void TestSina_Tick()
        {
            List<string[]> arr = Download_Tick.Request("sz000002", 20180209);
            for (int i = 0; i < arr.Count; i++)
            {
                Console.WriteLine(ListUtils.ToString(arr[i]));
            }
        }
    }
}