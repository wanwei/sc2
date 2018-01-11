using com.wer.sc.utils.param;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace com.wer.sc.strategy.loader
{
    /// <summary>
    /// 一个策略的配置信息
    /// </summary>
    public class StrategyInfo : StrategyConfig, IStrategyInfo
    {
        //该策略所属的策略包，在装载时设置
        internal IStrategyAssembly strategyAssembly;
        //该策略的实现类Type
        internal Type strategyType;

        public Type StrategyClassType
        {
            get
            {
                return strategyType;
            }
        }

        public IStrategyAssembly StrategyAssembly
        {
            get
            {
                return strategyAssembly;
            }
        }

        public IStrategyData CreateStrategyData()
        {
            return strategyAssembly.CreateStrategyData(this.ClassName);
        }

        public IStrategy CreateStrategy()
        {
            return strategyAssembly.CreateStrategy(this.ClassName);
        }
    }
}