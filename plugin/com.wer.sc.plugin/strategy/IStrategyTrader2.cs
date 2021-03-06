﻿using com.wer.sc.data.account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy
{
    public interface IStrategyTrader
    {
        IList<string> GetAllCodes();

        IStrategyTrader_Code GetStrategyTrader(string code);

        IAccount Account { get; }
    }
}