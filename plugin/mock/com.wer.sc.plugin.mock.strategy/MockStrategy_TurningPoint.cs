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
    [Strategy("MOCK.STRATEGY.TURNINGPOINT","转折点查找", "转折点查找，测试专用")]
    public class MockStrategy_TurningPoint : StrategyAbstract
    {
        public string Description
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string Name
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override StrategyReferedPeriods GetStrategyPeriods()
        {
            throw new NotImplementedException();
        }

        public override void StrategyEnd()
        {
            throw new NotImplementedException();
        }

        public override void StrategyStart()
        {
            throw new NotImplementedException();
        }

        public override void OnBar(IRealTimeDataReader dataReader)
        {
            throw new NotImplementedException();
        }

        public override void OnTick(IRealTimeDataReader dataReader)
        {
            throw new NotImplementedException();
        }
    }
}
