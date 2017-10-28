using com.wer.sc.mockdata;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace com.wer.sc.utils.param
{
    [TestClass]
    public class TestParameters
    {
        [TestMethod]
        public void TestAddParameter()
        {
            IParameters parameters = GetParameters();

            Console.WriteLine(parameters);
        }

        private static IParameters GetParameters()
        {
            IParameters parameters = ParameterFactory.CreateParameters();
            parameters.AddParameter("ma1", "ma1", "ma1", ParameterType.INTEGER, 5);
            parameters.AddParameter("ma2", "ma2", "ma2", ParameterType.INTEGER, 10);
            parameters.AddParameter("ma3", "ma3", "ma3", ParameterType.INTEGER, 20);
            parameters.AddParameter("ma4", "ma4", "ma4", ParameterType.INTEGER, 40);
            parameters.AddParameter("ma5", "ma5", "ma5", ParameterType.INTEGER, 60);

            IParameterOptions options = ParameterFactory.CreateParameterOptions(ParameterType.INTEGER, new object[] { 5, 10, 20, 40, 60 });
            parameters.AddParameter("test", "testc", "testd", ParameterType.INTEGER, 0, options);

            parameters.SetParameterValue("ma1", 5);
            parameters.SetParameterValue("ma2", 10);
            parameters.SetParameterValue("ma3", 20);
            parameters.SetParameterValue("ma4", 40);
            parameters.SetParameterValue("ma5", 60);
            return parameters;
        }

        [TestMethod]
        public void TestLoadParameters()
        {
            IParameters parameters = ParameterFactory.CreateParameters();
            string testCase = TestCaseManager.GetTestCasePath(GetType(), "Parameters");
            XmlDocument doc = new XmlDocument();
            doc.Load(testCase);

            parameters.Load(doc.DocumentElement);

            IParameters parameters2 = GetParameters();
            Assert.AreEqual(parameters.ToString(), parameters2.ToString());
        }
    }
}