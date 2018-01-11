using com.wer.sc.data;
using com.wer.sc.data.forward;
using com.wer.sc.data.navigate;
using com.wer.sc.strategy.loader;
using com.wer.sc.utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy
{
    [TestClass]
    public class TestStrategyMgr
    {
        private const string STRATEGY_MA = "com.wer.sc.plugin.mock.strategy.MockStrategy_Ma";
        private const string STRATEGY_TURNINGPOINT = "com.wer.sc.plugin.mock.strategy.MockStrategy_TurningPoint";
        private const string STRATEGY_VOLUME = "com.wer.sc.plugin.mock.strategy.MockStrategy_Volume";

        string assemblyName1 = "com.wer.sc.plugin.mock.strategy.20170115";
        string assemblyName2 = "com.wer.sc.plugin.mock.strategy.20170207";

        private static IStrategyAssemblyMgr GetStrategyMgr()
        {
            string path = GetStrategyPath();
            IStrategyAssemblyMgr strategyMgr = StrategyMgrFactory.CreatePluginMgr(path);
            return strategyMgr;
        }

        private static string GetStrategyPath()
        {
            string path = ScConfig.Instance.ScPath;
            path = path.Replace("Debug", "Mock") + "\\strategy\\";
            return path;
        }

        [TestMethod]
        public void TestStrategyScan()
        {
            IStrategyAssemblyMgr strategyMgr = GetStrategyMgr();
            IList<IStrategyAssembly> assemblies = strategyMgr.GetAllStrategyAssemblies();
            Assert.AreEqual(2, assemblies.Count);

            string assemblyName = "com.wer.sc.plugin.mock.strategy.20170115";
            IStrategyAssembly assembly1 = strategyMgr.GetStrategyAssembly(assemblyName);

            Assert.AreEqual(GetStrategyPath() + assemblyName + ".dll", assembly1.FullPath);
            Assert.AreEqual(assemblyName, assembly1.AssemblyName);    
            Assert.AreEqual(4, assembly1.GetAllStrategies().Count);
        }

        private void AssertMockStrategy_Ma(IStrategyInfo strategy)
        {
            //Assert.AreEqual(STRATEGY_MA, strategy.StrategyID);
            Assert.AreEqual("MA指标", strategy.Name);
            Assert.AreEqual("MA指标，测试专用", strategy.Description);
            Assert.AreEqual("com.wer.sc.plugin.mock.strategy.MockStrategy_Ma", strategy.StrategyClassType.FullName);
        }

        private void AssertMockStrategy_TurningPoint(IStrategyInfo strategy)
        {
            //Assert.AreEqual(STRATEGY_TURNINGPOINT, strategy.StrategyID);
            Assert.AreEqual("转折点查找", strategy.Name);
            Assert.AreEqual("转折点查找，测试专用", strategy.Description);
            Assert.AreEqual("com.wer.sc.plugin.mock.strategy.MockStrategy_TurningPoint", strategy.StrategyClassType.FullName);
        }

        private void AssertMockStrategy_Volume(IStrategyInfo strategy)
        {
            //Assert.AreEqual(STRATEGY_VOLUME, strategy.StrategyID);
            Assert.AreEqual("量能过滤", strategy.Name);
            Assert.AreEqual("量能过滤，测试专用", strategy.Description);
            Assert.AreEqual("com.wer.sc.plugin.mock.strategy.MockStrategy_Volume", strategy.StrategyClassType.FullName);
        }

        private void AssertMockStrategy_Complex(IStrategyInfo strategy)
        {
            //Assert.AreEqual("MOCK.STRATEGY.COMPLEX.REAL", strategy.StrategyID);
            Assert.AreEqual("复杂策略", strategy.Name);
            Assert.AreEqual("复杂策略，测试专用", strategy.Description);
            Assert.AreEqual("com.wer.sc.plugin.mock.strategy.complex.MockStrategy_Real", strategy.StrategyClassType.FullName);
        }

        private void AssertMockStrategy_Ma_Object1(IStrategy strategyObject)
        {
            ForwardReferedPeriods periods = strategyObject.GetReferedPeriods();
            Assert.AreEqual(false, periods.UseTickData);
            Assert.AreEqual(2, periods.UsedKLinePeriods.Count);
            Assert.AreEqual(KLinePeriod.KLinePeriod_1Minute, periods.UsedKLinePeriods[0]);
            Assert.AreEqual(KLinePeriod.KLinePeriod_5Minute, periods.UsedKLinePeriods[1]);
        }

        private void AssertMockStrategy_Ma_Object2(IStrategy strategyObject)
        {
            ForwardReferedPeriods periods = strategyObject.GetReferedPeriods();
            Assert.AreEqual(true, periods.UseTickData);
            Assert.AreEqual(3, periods.UsedKLinePeriods.Count);
            Assert.AreEqual(KLinePeriod.KLinePeriod_1Minute, periods.UsedKLinePeriods[0]);
            Assert.AreEqual(KLinePeriod.KLinePeriod_5Minute, periods.UsedKLinePeriods[1]);
            Assert.AreEqual(KLinePeriod.KLinePeriod_15Minute, periods.UsedKLinePeriods[2]);
        }
        [TestMethod]
        public void TestStrategyAssembly()
        {
            IStrategyAssemblyMgr strategyMgr = GetStrategyMgr();
            IStrategyAssembly assembly1 = strategyMgr.GetStrategyAssembly(assemblyName1);

            //string path1 = "com.wer.sc.plugin.mock.strategy";
            //string path2 = "com.wer.sc.plugin.mock.zb";
            //string path3 = "com.wer.sc.plugin.mock.strategy.complex";

            IList<IStrategyInfo> strategyInfos = null;//assembly1.GetSubStrategies("");
            //Assert.AreEqual(2, strategyInfos.Count);
            AssertMockStrategy_TurningPoint(assembly1.GetStrategyInfo(STRATEGY_TURNINGPOINT));
            AssertMockStrategy_Volume(assembly1.GetStrategyInfo(STRATEGY_VOLUME));
            AssertMockStrategy_Ma(assembly1.GetStrategyInfo(STRATEGY_MA));

            //strategyInfos = assembly1.GetSubStrategies(path2);
            //Assert.AreEqual(1, strategyInfos.Count);
            //AssertMockStrategy_Ma(strategyInfos[0]);

            //IList<string> subPath2 = assembly1.GetSubPath(path1);
            //Assert.AreEqual(1, subPath2.Count);
            //Assert.AreEqual(path3, subPath2[0]);
            //Assert.AreEqual(null, assembly1.GetSubPath(path2));

            //strategyInfos = assembly1.GetSubStrategies(path3);
            //AssertMockStrategy_Complex(strategyInfos[0]);

            //IStrategyAssembly assembly2 = strategyMgr.GetStrategyAssembly(assemblyName2);
            //Assert.AreEqual(3, assembly2.GetAllStrategies().Count);
        }

        //[TestMethod]
        //public void TestStrategyAssembly()
        //{
        //    IStrategyAssemblyMgr strategyMgr = GetStrategyMgr();
        //    IStrategyAssembly assembly1 = strategyMgr.GetStrategyAssembly(assemblyName1);

        //    string path1 = "com.wer.sc.plugin.mock.strategy";
        //    string path2 = "com.wer.sc.plugin.mock.zb";
        //    string path3 = "com.wer.sc.plugin.mock.strategy.complex";

        //    //IList<String> rootPaths = assembly1.GetRootPath();
        //    //Assert.AreEqual(1, rootPaths.Count);
        //    //Assert.AreEqual("com.wer.sc.plugin.mock", rootPaths[0]);
        //    //IList<String> firstSubPaths = assembly1.GetSubPath(rootPaths[0]);
        //    //Assert.AreEqual(2, firstSubPaths.Count);
        //    ////TODO 
        //    //Assert.AreEqual(path1, firstSubPaths[1]);
        //    //Assert.AreEqual(path2, firstSubPaths[0]);
        //    string strategy1 = "com.wer.sc.plugin.mock.strategy.MockStrategy_TurningPoint";             

        //    IList<IStrategyInfo> strategyInfos = assembly1.GetSubStrategies("");
        //    //Assert.AreEqual(2, strategyInfos.Count);
        //    AssertMockStrategy_TurningPoint(assembly1.GetStrategy(strategy1));
        //    AssertMockStrategy_Volume(strategyInfos[1]);

        //    strategyInfos = assembly1.GetSubStrategies(path2);
        //    Assert.AreEqual(1, strategyInfos.Count);
        //    AssertMockStrategy_Ma(strategyInfos[0]);

        //    IList<string> subPath2 = assembly1.GetSubPath(path1);
        //    Assert.AreEqual(1, subPath2.Count);
        //    Assert.AreEqual(path3, subPath2[0]);
        //    Assert.AreEqual(null, assembly1.GetSubPath(path2));

        //    strategyInfos = assembly1.GetSubStrategies(path3);
        //    AssertMockStrategy_Complex(strategyInfos[0]);

        //    IStrategyAssembly assembly2 = strategyMgr.GetStrategyAssembly(assemblyName2);
        //    Assert.AreEqual(3, assembly2.GetAllStrategies().Count);
        //}

        [TestMethod]
        public void TestCreateStrategyObject()
        {
            IStrategyAssemblyMgr strategyMgr = GetStrategyMgr();
            IStrategyAssembly assembly1 = strategyMgr.GetStrategyAssembly(assemblyName1);
            IStrategy strategy_ma = assembly1.CreateStrategy(STRATEGY_MA);
            AssertMockStrategy_Ma_Object1(strategy_ma);

            IStrategyAssembly assembly2 = strategyMgr.GetStrategyAssembly(assemblyName2);
            IStrategy strategy_ma2 = assembly2.CreateStrategy(STRATEGY_MA);
            AssertMockStrategy_Ma_Object2(strategy_ma2);
        }
    }
}
