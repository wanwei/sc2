using com.wer.sc.data.reader;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.datapackage
{
    [TestClass]
    public class TestCodePackageFactory
    {
        [TestMethod]
        public void TestCodePackageCreate_Normal()
        {
            CodePackageInfo codePackageInfo = new CodePackageInfo();
            codePackageInfo.ChoosedByCatelog = false;
            codePackageInfo.ChoosedByMainContract = false;
            codePackageInfo.Codes.Add("RB1705");
            codePackageInfo.Codes.Add("RB1709");
            codePackageInfo.Codes.Add("RB1801");
            codePackageInfo.Start = 20170101;
            codePackageInfo.End = 20180101;

            IDataReader dataReader = TestDataCenter.Instance.DataReader;
            CodePackageFactory fac = new CodePackageFactory(dataReader);
            CodePackage codePackage = fac.CreateCodePackage(codePackageInfo);

            Assert.AreEqual(3, codePackage.Codes.Count);
            Assert.AreEqual(20170101, codePackage.StartDate);
        }

        [TestMethod]
        public void TestCodePackageCreate_MainContract()
        {
            CodePackageInfo codePackageInfo = new CodePackageInfo();
            codePackageInfo.ChoosedByCatelog = false;
            codePackageInfo.ChoosedByMainContract = true;
            codePackageInfo.Codes.Add("RB");
            codePackageInfo.Codes.Add("M");
            codePackageInfo.Codes.Add("A");
            codePackageInfo.Start = 20170101;
            codePackageInfo.End = 20180101;

            IDataReader dataReader = TestDataCenter.Instance.DataReader;
            CodePackageFactory fac = new CodePackageFactory(dataReader);
            CodePackage codePackage = fac.CreateCodePackage(codePackageInfo);
            Console.WriteLine(codePackage);
        }

        [TestMethod]
        public void TestCodePackageCreate_Catelog()
        {
            CodePackageInfo codePackageInfo = new CodePackageInfo();
            codePackageInfo.ChoosedByCatelog = true;
            codePackageInfo.ChoosedByMainContract = false;
            codePackageInfo.Codes.Add("RB");
            codePackageInfo.Codes.Add("M");
            codePackageInfo.Codes.Add("A");
            codePackageInfo.Start = 20170101;
            codePackageInfo.End = 20180101;

            IDataReader dataReader = TestDataCenter.Instance.DataReader;
            CodePackageFactory fac = new CodePackageFactory(dataReader);
            CodePackage codePackage = fac.CreateCodePackage(codePackageInfo);
            Console.WriteLine(codePackage);
        }
    }
}
