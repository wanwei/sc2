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
            CodePeriodPackageInfo codePackageInfo = new CodePeriodPackageInfo();
            codePackageInfo.CodeChooseMethod = CodeChooseMethod.Normal;
            codePackageInfo.Codes.Add("RB1705");
            codePackageInfo.Codes.Add("RB1709");
            codePackageInfo.Codes.Add("RB1801");
            codePackageInfo.Start = 20170101;
            codePackageInfo.End = 20180101;

            IDataReader dataReader = TestDataCenter.Instance.DataReader;
            CodePeriodFactory fac = new CodePeriodFactory(dataReader);
            ICodePeriodPackage codePackage = fac.CreateCodePeriodPackage(codePackageInfo);

            Assert.AreEqual(3, codePackage.CodePeriods.Count);
            Assert.AreEqual(20170101, codePackage.CodePeriods[0].StartDate);
        }

        [TestMethod]
        public void TestCodePackageCreate_MainContract()
        {
            CodePeriodPackageInfo codePackageInfo = new CodePeriodPackageInfo();
            codePackageInfo.CodeChooseMethod = CodeChooseMethod.Maincontract;
            codePackageInfo.Codes.Add("RB");
            codePackageInfo.Codes.Add("M");
            codePackageInfo.Codes.Add("A");
            codePackageInfo.Start = 20170101;
            codePackageInfo.End = 20180101;

            IDataReader dataReader = TestDataCenter.Instance.DataReader;
            CodePeriodFactory fac = new CodePeriodFactory(dataReader);
            ICodePeriodPackage codePackage = fac.CreateCodePeriodPackage(codePackageInfo);
            Console.WriteLine(codePackage);
        }

        [TestMethod]
        public void TestCodePackageCreate_Catelog()
        {
            CodePeriodPackageInfo codePackageInfo = new CodePeriodPackageInfo();
            codePackageInfo.CodeChooseMethod = CodeChooseMethod.Catelog;            
            codePackageInfo.Codes.Add("RB");
            codePackageInfo.Codes.Add("M");
            codePackageInfo.Codes.Add("A");
            codePackageInfo.Start = 20170101;
            codePackageInfo.End = 20180101;

            IDataReader dataReader = TestDataCenter.Instance.DataReader;
            CodePeriodFactory fac = new CodePeriodFactory(dataReader);
            ICodePeriodPackage codePackage = fac.CreateCodePeriodPackage(codePackageInfo);
            Console.WriteLine(codePackage);
        }
    }
}
