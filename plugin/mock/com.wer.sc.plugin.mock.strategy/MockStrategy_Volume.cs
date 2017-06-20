﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.data;
using com.wer.sc.strategy;
using com.wer.sc.data.reader;

namespace com.wer.sc.plugin.mock.strategy
{
    [Strategy("MOCK.STRATEGY.VOLUME","量能过滤", "量能过滤，测试专用")]
    public class MockStrategy_Volume : IStrategy
    {      
        public StrategyReferedPeriods GetStrategyPeriods()
        {
            throw new NotImplementedException();
        }

        public void StrategyEnd()
        {
            throw new NotImplementedException();
        }

        public void StrategyStart()
        {
            throw new NotImplementedException();
        }

        public void OnBar(IRealTimeDataReader dataReader)
        {
            throw new NotImplementedException();
        }

        public void OnTick(IRealTimeDataReader dataReader)
        {
            throw new NotImplementedException();
        }
    }
}
