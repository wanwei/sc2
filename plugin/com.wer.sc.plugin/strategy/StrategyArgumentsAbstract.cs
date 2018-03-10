using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy
{
    /// <summary>
    /// 创建策略执行器的参数基类
    /// </summary>
    public abstract class StrategyArgumentsAbstract
    {
        private bool isSaveResult;

        private StrategyReferedPeriods referedPeriods;

        private StrategyForwardPeriod forwardPeriod;

        private StrategyTraderSetting traderSetting;

        private IStrategyHelper strategyHelper;

        /// <summary>
        /// 得到或设置策略引用的周期
        /// </summary>
        public StrategyReferedPeriods ReferedPeriods
        {
            get
            {
                return referedPeriods;
            }

            set
            {
                referedPeriods = value;
            }
        }

        /// <summary>
        /// 得到或设置策略的前进周期
        /// </summary>
        public StrategyForwardPeriod ForwardPeriod
        {
            get
            {
                return forwardPeriod;
            }

            set
            {
                forwardPeriod = value;
            }
        }

        /// <summary>
        /// 得到或设置交易设置
        /// </summary>
        public StrategyTraderSetting TraderSetting
        {
            get
            {
                return traderSetting;
            }

            set
            {
                traderSetting = value;
            }
        }

        public IStrategyHelper StrategyHelper
        {
            get
            {
                return strategyHelper;
            }

            set
            {
                strategyHelper = value;
            }
        }

        public bool IsSaveResult
        {
            get
            {
                return isSaveResult;
            }

            set
            {
                isSaveResult = value;
            }
        }
    }
}