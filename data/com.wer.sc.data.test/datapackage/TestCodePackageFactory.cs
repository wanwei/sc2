using com.wer.sc.data.codeperiod;
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
            CodePeriodListChooser codePackageInfo = new CodePeriodListChooser();
            codePackageInfo.CodeChooseMethod = CodeChooseMethod.Normal;
            codePackageInfo.Codes.Add("RB1705");
            codePackageInfo.Codes.Add("RB1709");
            codePackageInfo.Codes.Add("RB1801");
            codePackageInfo.Start = 20170101;
            codePackageInfo.End = 20180101;

            IDataReader dataReader = TestDataCenter.Instance.DataReader;
            CodePeriodFactory fac = new CodePeriodFactory(dataReader);
            ICodePeriodList codePackage = fac.CreateCodePeriodList(codePackageInfo);

            Assert.AreEqual(3, codePackage.CodePeriods.Count);
            Assert.AreEqual(20170101, codePackage.CodePeriods[0].StartDate);
        }

        [TestMethod]
        public void TestCodePackageCreate_MainContract()
        {
            CodePeriodListChooser codePackageInfo = new CodePeriodListChooser();
            codePackageInfo.CodeChooseMethod = CodeChooseMethod.Maincontract;
            codePackageInfo.Codes.Add("RB");
            codePackageInfo.Codes.Add("M");
            codePackageInfo.Codes.Add("A");
            codePackageInfo.Start = 20170101;
            codePackageInfo.End = 20180101;

            IDataReader dataReader = TestDataCenter.Instance.DataReader;
            CodePeriodFactory fac = new CodePeriodFactory(dataReader);
            ICodePeriodList codePackage = fac.CreateCodePeriodList(codePackageInfo);
            Console.WriteLine(codePackage);
        }

        [TestMethod]
        public void TestCodePackageCreate_Catelog()
        {
            CodePeriodListChooser codePackageInfo = new CodePeriodListChooser();
            codePackageInfo.CodeChooseMethod = CodeChooseMethod.Catelog;            
            codePackageInfo.Codes.Add("RB");
            codePackageInfo.Codes.Add("M");
            codePackageInfo.Codes.Add("A");
            codePackageInfo.Start = 20170101;
            codePackageInfo.End = 20180101;

            IDataReader dataReader = TestDataCenter.Instance.DataReader;
            CodePeriodFactory fac = new CodePeriodFactory(dataReader);
            ICodePeriodList codePackage = fac.CreateCodePeriodList(codePackageInfo);
            Console.WriteLine(codePackage);
        }
    }
}
