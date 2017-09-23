﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy
{
    public class StrategyResult : IStrategyResult
    {
        private IList<IStrategyResult_Single> list = new List<IStrategyResult_Single>();

        public IList<IStrategyResult_Single> StrategyResults
        {
            get
            {
                return list;
            }
        }
    }
}
