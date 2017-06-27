﻿using com.wer.sc.data.reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy
{
    public interface IImportStrategy
    {
        void OnBar(IRealTimeDataReader currentData);

        void OnTick(IRealTimeDataReader currentData);
    }
}
