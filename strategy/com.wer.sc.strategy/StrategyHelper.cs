﻿using com.wer.sc.strategy.draw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy
{
    public class StrategyHelper : IStrategyHelper
    {
        private StrategyResult results = new StrategyResult();

        private IDrawHelper drawHelper;

        private IStrategyTrader_Code strategyTrader_Code;

        public StrategyHelper(IDrawHelper drawHelper)
        {
            this.drawHelper = drawHelper;
        }

        /// <summary>
        /// 得到画图帮助接口
        /// </summary>
        public IDrawHelper DrawHelper
        {
            get
            {
                return drawHelper;
            }
        }

        public IStrategyTrader_Code Trader
        {
            get
            {
                return strategyTrader_Code;
            }
            set
            {
                strategyTrader_Code = value;
            }
        }

        public IStrategyResult Results
        {
            get
            {
                return results;
            }
        }


        public void AddStrategyResult(IStrategyResult_Single strategyResult)
        {
            this.results.StrategyResults.Add(strategyResult);
        }

        public void AddStrategyResult(string code, double time, string name, string desc)
        {
            IStrategyResult_Single result = new StrategyResult_Single(code, time, name, desc);
            this.results.StrategyResults.Add(result);
        }
    }
}
