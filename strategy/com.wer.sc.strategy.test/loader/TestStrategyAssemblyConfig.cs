using com.wer.sc.mockdata;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy.loader
{
    [TestClass]
    public class TestStrategyAssemblyConfig
    {
        [TestMethod]
        public void TestConfigLoad()
        {
            string fileName = "strategy.common.strategyconfig";
            string filePath = TestCaseManager.GetTestCasePath(typeof(TestStrategyAssemblyConfig), fileName);
            StrategyAssembly config = new StrategyAssembly();
            config.Load(filePath);
            Assert.AreEqual("com.wer.sc.strategy.common", config.AssemblyName);
            Assert.AreEqual("基础策略测试", config.Description);
            Assert.AreEqual("基础策略", config.Name);

            IList<IStrategyInfo> strategies = config.GetSubStrategyInfo("");
            Assert.AreEqual(1, strategies.Count);
            Assert.AreEqual("mock测试", strategies[0].Name);
            Assert.AreEqual("com.wer.sc.strategy.common.test.Strategy_Mock", strategies[0].ClassName);

            IList<string> paths = config.GetSubPath("");
            Assert.AreEqual(4, paths.Count);
            Assert.AreEqual("\\均线", paths[0]);
            Assert.AreEqual("\\转折点查找", paths[1]);
            Assert.AreEqual("\\量能分析", paths[2]);
            Assert.AreEqual("\\平台分析", paths[3]);

            Assert.AreEqual("\\平台分析\\子平台分析", config.GetSubPath("\\平台分析")[0]);

            IList<IStrategyInfo> subStrategies = config.GetSubStrategyInfo("\\均线");
            Assert.AreEqual(1, subStrategies.Count);
            Assert.AreEqual("均线策略", subStrategies[0].Name);
        }

        [TestMethod]
        public void TestAssemblyCreate()
        {
            string fileName = "strategy.common.strategyconfig";
            string filePath = TestCaseManager.GetTestCasePath(typeof(TestStrategyAssemblyConfig), fileName);
            //StrategyAssembly config = new StrategyAssembly();
            //config.Load(filePath);

            StrategyAssembly strategyAssembly = StrategyAssembly.Create(filePath);
            IStrategyInfo strategyInfo = strategyAssembly.GetStrategyInfo("com.wer.sc.strategy.common.ma.Strategy_MultiMa");
            Console.WriteLine(strategyInfo);
        }
    }
}
