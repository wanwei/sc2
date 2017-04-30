using com.wer.sc.plugin.cnfutures.historydata.updater;
using com.wer.sc.utils.ui.proceed;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.cnfutures.generator
{
    [TestClass]
    public class TestStepPreparer
    {
        [TestMethod]
        public void TestStepPreparer_GetAllSteps()
        {
            StepPreparer preparer = new StepPreparer(MockDataLoader.originalDataPath, MockDataLoader.pluginDataPath, "", false);
            List<IStep> steps = preparer.GetAllSteps();
            for (int i = 0; i < steps.Count; i++)
                Console.WriteLine(steps[i]);
        }
    }
}
