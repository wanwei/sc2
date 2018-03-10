using com.wer.sc.data;
using com.wer.sc.strategy.loader;
using com.wer.sc.strategy.store;
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

        private IStrategyExecutorPool strategyExecutorPool;

        private IStrategyResultStore strategyResultStore;

        public StrategyCenter(DataCenter dataCenter)
        {
            this.dataCenter = dataCenter;
            this.strategyExecutorFactory = new StrategyExecutorFactory(this);
            this.strategyAssemblyMgr = StrategyMgrFactory.DefaultPluginMgr;
            this.strategyExecutorPool = new StrategyExecutorPool();
            Uri uri = new Uri(dataCenter.DataCenterInfo.Uri);
            this.strategyResultStore = new StrategyResultStore_File(new StrategyDataPathUtils(uri.LocalPath));
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

        public IDataCenter BelongDataCenter
        {
            get
            {
                return this.dataCenter;
            }
        }

        public IStrategyExecutorFactory GetStrategyExecutorFactory()
        {
            return strategyExecutorFactory;
        }

        public IStrategyAssemblyMgr GetStrategyMgr()
        {
            return strategyAssemblyMgr;
        }

        public IStrategyExecutorPool GetStrategyExecutorPool()
        {
            return strategyExecutorPool;
        }

        public IStrategyResultStore StrategyResultStore
        {
            get
            {
                return strategyResultStore;
            }
        }
    }
}