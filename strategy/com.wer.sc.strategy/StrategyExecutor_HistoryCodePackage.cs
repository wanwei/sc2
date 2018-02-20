using com.wer.sc.data;
using com.wer.sc.data.datapackage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace com.wer.sc.strategy
{
    /// <summary>
    /// 将一个策略在多个上验证
    /// </summary>
    public class StrategyExecutor_HistoryCodePackage
    {
        private int threadCount = 5;

        private ICodePeriodPackageInfo codePackageInfo;

        private ICodePeriodPackage codePackage;

        private List<StrategyExecutor_History> executors = new List<StrategyExecutor_History>();

        private IStrategyExecutorFactory_History executorFactory;

        public int ThreadCount
        {
            get
            {
                return threadCount;
            }

            set
            {
                threadCount = value;
            }
        }

        public StrategyExecutor_HistoryCodePackage(IDataCenter dataCenter, ICodePeriodPackageInfo codePackageInfo)
        {
            this.codePackageInfo = codePackageInfo;
            ICodePeriodFactory fac = dataCenter.CodePackageFactory;
            this.codePackage = fac.CreateCodePeriodPackage(codePackageInfo);
            this.executorFactory = StrategyCenter.Default.GetStrategyExecutorFactory_History();
        }

        public void Execute()
        {
            //if (this.codePackage.IsMainContract)
            //{
            //    Execute_MainContract();
            //}
            //else
            //{
            //    Execute_Normal();
            //}
        }

        private void Execute_MainContract()
        {
            //ThreadPool.SetMaxThreads(threadCount, threadCount);
            //for (int i = 0; i < codePackage.MainContracts.Varieties.Count; i++)
            //{
            //    ICodePeriod_MainContract mainContract = codePackage.MainContracts.Varieties[i];
            //    ThreadPool.QueueUserWorkItem(Run_Normal, 100);
            //}            
        }

        private void Run_Normal(object state)
        {
            string code = null;
            int startDate = 0;
            int endDate = 0;
            StrategyReferedPeriods referedPeriods = null;
            StrategyForwardPeriod forwardPeriod = null;
            IStrategyExecutor executor = executorFactory.CreateExecutor(code, startDate, endDate, referedPeriods, forwardPeriod);
        }

        private void Execute_Normal()
        {
            //ThreadPool.SetMaxThreads(threadCount, threadCount);
            //for (int i = 0; i < codePackage.MainContracts.Varieties.Count; i++)
            //{
            //    ICodePeriod_MainContract mainContract = codePackage.MainContracts.Varieties[i];
            //    ThreadPool.QueueUserWorkItem(Run_Normal, 100);
            //}
        }
    }
}
