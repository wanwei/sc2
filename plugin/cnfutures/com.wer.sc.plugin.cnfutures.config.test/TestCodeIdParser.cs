using com.wer.sc.data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin.cnfutures.config
{
    [TestClass]
    public class TestCodeIdParser
    {
        [TestMethod]
        public void TestSplitCodeId()
        {
            CodeIdParser parser = new CodeIdParser("ax01");
            Assert.AreEqual("A", parser.VarietyId);
            Assert.AreEqual("X", parser.TwoYearTag);
            Assert.AreEqual("01", parser.Suffix);
            Assert.AreEqual(0, parser.Year);
            Assert.AreEqual(1, parser.Month);
            Assert.AreEqual(0, parser.EndDay);
            Assert.AreEqual("AX01", parser.ShortCode);     

            parser = new CodeIdParser("a0910");
            Assert.AreEqual("A", parser.VarietyId);
            Assert.AreEqual("Y", parser.TwoYearTag);
            Assert.AreEqual("0910", parser.Suffix);
            Assert.AreEqual(2009, parser.Year);
            Assert.AreEqual(10, parser.Month);
            Assert.AreEqual(20091016, parser.EndDay);
            Assert.AreEqual(20071021, parser.StartDay);
            Assert.AreEqual("AY10", parser.ShortCode);

            parser = new CodeIdParser("AMI");
            Assert.AreEqual("A", parser.VarietyId);
            Assert.AreEqual("", parser.TwoYearTag);
            Assert.AreEqual("MI", parser.Suffix);
            Assert.AreEqual(0, parser.Year);
            Assert.AreEqual(0, parser.Month);
            Assert.AreEqual(0, parser.EndDay);
            Assert.AreEqual("AMI", parser.ShortCode);

            parser = new CodeIdParser("PIMI");
            Assert.AreEqual("PI", parser.VarietyId);
            Assert.AreEqual("", parser.TwoYearTag);
            Assert.AreEqual("MI", parser.Suffix);
            Assert.AreEqual("PIMI", parser.ShortCode);

            parser = new CodeIdParser("y01");
            Assert.AreEqual("Y", parser.VarietyId);
            Assert.AreEqual("", parser.TwoYearTag);
            Assert.AreEqual("01", parser.Suffix);
            Assert.AreEqual(0, parser.Year);
            Assert.AreEqual(1, parser.Month);
            Assert.AreEqual(0, parser.EndDay);
            Assert.AreEqual("Y01", parser.ShortCode);

            parser = new CodeIdParser("y1001");
            Assert.AreEqual("Y", parser.VarietyId);
            Assert.AreEqual("", parser.TwoYearTag);
            Assert.AreEqual("1001", parser.Suffix);
            Assert.AreEqual(2010, parser.Year);
            Assert.AreEqual(1, parser.Month);
            Assert.AreEqual(20100115, parser.EndDay);
            Assert.AreEqual("Y01", parser.ShortCode);

            parser = new CodeIdParser("sr1005");
            Assert.AreEqual("SR", parser.VarietyId);
            Assert.AreEqual("X", parser.TwoYearTag);
            Assert.AreEqual("1005", parser.Suffix);
            Assert.AreEqual(2010, parser.Year);
            Assert.AreEqual(5, parser.Month);
            Assert.AreEqual(20100521, parser.EndDay);
            Assert.AreEqual(20090317, parser.StartDay);
            Assert.AreEqual("SRX05", parser.ShortCode);

            parser = new CodeIdParser("sr0905");
            Assert.AreEqual("SR", parser.VarietyId);
            Assert.AreEqual("Y", parser.TwoYearTag);
            Assert.AreEqual("0905", parser.Suffix);
            Assert.AreEqual(2009, parser.Year);
            Assert.AreEqual(5, parser.Month);
            Assert.AreEqual(20090515, parser.EndDay);
            Assert.AreEqual(20070520, parser.StartDay);
            Assert.AreEqual("SRY05", parser.ShortCode);
        }

        //[TestMethod]
        //public void TestGetSimpleCodeId()
        //{
        //    string simpleCodeId = CodeInfoUtils.GetSimpleCodeId("m0000");
        //    Assert.AreEqual("M13", simpleCodeId);

        //    simpleCodeId = CodeInfoUtils.GetSimpleCodeId("mmi");
        //    Assert.AreEqual("MMI", simpleCodeId);

        //    simpleCodeId = CodeInfoUtils.GetSimpleCodeId("a1403");
        //    Assert.AreEqual("AX03", simpleCodeId);

        //    simpleCodeId = CodeInfoUtils.GetSimpleCodeId("a1503");
        //    Assert.AreEqual("AY03", simpleCodeId);
        //}

        //[TestMethod]
        //public void TestGetEndDay()
        //{
        //    int endDay = CodeInfoUtils.GetEndDay(2016, 6);
        //    Assert.AreEqual(20160617, endDay);

        //    endDay = CodeInfoUtils.GetEndDay(2015, 6);
        //    Assert.AreEqual(20150619, endDay);

        //    endDay = CodeInfoUtils.GetEndDay(2016, 9);
        //    Assert.AreEqual(20160916, endDay);
        //}

        //[TestMethod]
        //public void TestGetCodeInfo()
        //{
        //    CodeInfo codeinfo = CodeInfoUtils.GetCodeInfo("M1501", "豆粕", CodeInfoUtils.EXCHANGE_DL);
        //    Console.WriteLine(codeinfo);
        //}
    }
}
