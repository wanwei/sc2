using com.wer.sc.data;
using com.wer.sc.mockdata;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin.cnfutures.config
{
    [TestClass]
    public class TestCodeInfoUtils
    {
        //[TestMethod]
        //public void TestSplitCodeId()
        //{
        //    string[] variety = CodeInfoUtils.SplitCodeId("ax01");
        //    Assert.AreEqual("A", variety[0]);
        //    Assert.AreEqual("X", variety[1]);
        //    Assert.AreEqual("01", variety[2]);

        //    variety = CodeInfoUtils.SplitCodeId("a0910");
        //    Assert.AreEqual("A", variety[0]);
        //    Assert.AreEqual("", variety[1]);
        //    Assert.AreEqual("0901", variety[2]);

        //    variety = CodeInfoUtils.SplitCodeId("AMI");
        //    Assert.AreEqual("A", variety[0]);
        //    Assert.AreEqual("", variety[1]);
        //    Assert.AreEqual("MI", variety[2]);

        //    variety = CodeInfoUtils.SplitCodeId("PIMI");
        //    Assert.AreEqual("PI", variety[0]);
        //    Assert.AreEqual("", variety[1]);
        //    Assert.AreEqual("MI", variety[2]);

        //    variety = CodeInfoUtils.SplitCodeId("y01");
        //    Assert.AreEqual("Y", variety[0]);
        //    Assert.AreEqual("", variety[1]);
        //    Assert.AreEqual("01", variety[2]);

        //    variety = CodeInfoUtils.SplitCodeId("y1001");
        //    Assert.AreEqual("Y", variety[0]);
        //    Assert.AreEqual("", variety[1]);
        //    Assert.AreEqual("1001", variety[2]);
        //}


        [TestMethod]
        public void TestGetVariety()
        {
            string variety = CodeInfoUtils.GetVariety("ax01");
            Assert.AreEqual("A", variety);
            variety = CodeInfoUtils.GetVariety("a0910");
            Assert.AreEqual("A", variety);

            variety = CodeInfoUtils.GetVariety("AMI");
            Assert.AreEqual("A", variety);
            variety = CodeInfoUtils.GetVariety("PIMI");
            Assert.AreEqual("PI", variety);

            variety = CodeInfoUtils.GetVariety("y01");
            Assert.AreEqual("Y", variety);
            variety = CodeInfoUtils.GetVariety("y1001");
            Assert.AreEqual("Y", variety);
        }

        [TestMethod]
        public void TestGetSimpleCodeId()
        {
            string simpleCodeId = CodeInfoUtils.GetSimpleCodeId("m0000");
            Assert.AreEqual("M13", simpleCodeId);

            simpleCodeId = CodeInfoUtils.GetSimpleCodeId("mmi");
            Assert.AreEqual("MMI", simpleCodeId);

            simpleCodeId = CodeInfoUtils.GetSimpleCodeId("a1403");
            Assert.AreEqual("AX03", simpleCodeId);

            simpleCodeId = CodeInfoUtils.GetSimpleCodeId("a1503");
            Assert.AreEqual("AY03", simpleCodeId);
        }

        [TestMethod]
        public void TestGetComplexCodeId()
        {
            string complexCodeId = CodeInfoUtils.GetComplexCodeId("m13", 20100101);
            Assert.AreEqual("M0000", complexCodeId);

            complexCodeId = CodeInfoUtils.GetComplexCodeId("mmi", 20100101);
            Assert.AreEqual("MMI", complexCodeId);

            complexCodeId = CodeInfoUtils.GetComplexCodeId("m03", 20040103);
            Assert.AreEqual("M0403", complexCodeId);
            complexCodeId = CodeInfoUtils.GetComplexCodeId("m03", 20040303);
            Assert.AreEqual("M0403", complexCodeId);
            complexCodeId = CodeInfoUtils.GetComplexCodeId("m03", 20040325);
            Assert.AreEqual("M0503", complexCodeId);

            complexCodeId = CodeInfoUtils.GetComplexCodeId("m03", 20140103);
            Assert.AreEqual("M1403", complexCodeId);
            complexCodeId = CodeInfoUtils.GetComplexCodeId("m03", 20140303);
            Assert.AreEqual("M1403", complexCodeId);
            complexCodeId = CodeInfoUtils.GetComplexCodeId("m03", 20140325);
            Assert.AreEqual("M1503", complexCodeId);

            complexCodeId = CodeInfoUtils.GetComplexCodeId("ax03", 20140103);
            Assert.AreEqual("A1403", complexCodeId);
            complexCodeId = CodeInfoUtils.GetComplexCodeId("ax03", 20140325);
            Assert.AreEqual("A1603", complexCodeId);
            complexCodeId = CodeInfoUtils.GetComplexCodeId("ax03", 20040103);
            Assert.AreEqual("A0403", complexCodeId);
            complexCodeId = CodeInfoUtils.GetComplexCodeId("ax03", 20040325);
            Assert.AreEqual("A0603", complexCodeId);

            complexCodeId = CodeInfoUtils.GetComplexCodeId("ay03", 20140125);
            Assert.AreEqual("A1503", complexCodeId);
            complexCodeId = CodeInfoUtils.GetComplexCodeId("ay03", 20140325);
            Assert.AreEqual("A1503", complexCodeId);
        }

        [TestMethod]
        public void TestGetEndDay()
        {
            int endDay = CodeInfoUtils.GetEndDay(2016, 6);
            Assert.AreEqual(20160617, endDay);

            endDay = CodeInfoUtils.GetEndDay(2015, 6);
            Assert.AreEqual(20150619, endDay);

            endDay = CodeInfoUtils.GetEndDay(2016, 9);
            Assert.AreEqual(20160916, endDay);
        }

        [TestMethod]
        public void TestGetCodeInfo()
        {
            string path = @"D:\SCWORK\DEV\SC2\bin\Debug\plugin\cnfutures";
            DataLoader_Variety dataLoader_Variety = new DataLoader_Variety(path);
            CodeInfo codeInfo = CodeInfoUtils.GetCodeInfo("m1501", dataLoader_Variety);
            Assert.AreEqual("M1501,豆粕1501,M,豆粕,20140119,20150116,DL,M1501,M01", codeInfo.ToString());

            codeInfo = CodeInfoUtils.GetCodeInfo("mmi", dataLoader_Variety);
            //Console.WriteLine(codeInfo);
            Assert.AreEqual("MMI,豆粕MI,M,豆粕,0,0,DL,MMI,MMI", codeInfo.ToString());

            codeInfo = CodeInfoUtils.GetCodeInfo("m0000", dataLoader_Variety);
            Assert.AreEqual("M0000,豆粕0000,M,豆粕,0,0,DL,M0000,M13", codeInfo.ToString());

            codeInfo = CodeInfoUtils.GetCodeInfo("FG701", dataLoader_Variety);
            Assert.AreEqual("FG1701,玻璃1701,FG,玻璃,20160117,20170120,ZZ,FG701,FG01", codeInfo.ToString());
        }

    }
}
