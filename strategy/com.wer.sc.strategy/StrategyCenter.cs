using com.wer.sc.data;
using com.wer.sc.strategy.loader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy
{
    public class StrategyCenter : IStrategyCenter
    {
        private static StrategyCenter strategyCenter;

        private DataCenter dataCenter;

        private StrategyExecutorFactory strategyExecutorFactory;

        private IStrategyAssemblyMgr strategyAssemblyMgr;

        public StrategyCenter(DataCenter dataCenter)
        {
            this.dataCenter = dataCenter;
            this.strategyExecutorFactory = new StrategyExecutorFactory(dataCenter);
            this.strategyAssemblyMgr = StrategyMgrFactory.DefaultPluginMgr;
        }

        public static IStrategyCenter Default
        {
            get
            {
                if (strategyCenter == null)
                    strategyCenter = new StrategyCenter(DataCenter.Default);
                return strategyCenter;
            }
        }

        public static IStrategyCenter CreateStrategyCenter(DataCenter dataCenter)
        {
            return new StrategyCenter(dataCenter);
        }

        public IStrategyExecutorFactory GetStrategyExecutorFactory()
        {
            return strategyExecutorFactory;
        }

        public IStrategyAssemblyMgr GetStrategyMgr()
        {
            return strategyAssemblyMgr;
        }
    }
}