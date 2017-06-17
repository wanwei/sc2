using com.wer.sc.data;
using com.wer.sc.data.utils;
using com.wer.sc.mockdata;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.reader.cache
{
    [TestClass]
    public class TestCodes_Cache
    {
        [TestMethod]
        public void TestGetAllCodes()
        {
            List<CodeInfo> codes = CsvUtils_Code.Load(TestCaseManager.GetTestCasePath(GetType(), "Codes"));
            CodeInfoCache cache = new CodeInfoCache(codes);

            List<CodeInfo> newcodes = cache.GetAllCodes();
            AssertUtils.AssertEqual_List("Codes", GetType(), newcodes);
        }

        [TestMethod]
        public void TestGetAllCodesByVariety()
        {
            List<CodeInfo> codes = CsvUtils_Code.Load(TestCaseManager.GetTestCasePath(GetType(), "Codes"));
            CodeInfoCache cache = new CodeInfoCache(codes);

            List<CodeInfo> newcodes = cache.GetCodesByCatelog("m", 20160101);
            //AssertUtils.PrintLineList(newcodes);         
            AssertUtils.AssertEqual_List("Codes_M_20160101", GetType(), newcodes);
        }
    }
}
