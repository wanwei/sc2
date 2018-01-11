using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.data;

namespace com.wer.sc.strategy.common.platform
{
    public class PlatformLooper : IDataLooper
    {
        private int length_Long = 100;

        private int length_Short = 30;

        private IKLineData klineData;

        private Platform currentPlatform;

        public PlatformLooper(IKLineData klineData)
        {
            this.klineData = klineData;
        }

        public void Loop(int barPos)
        {
            if (barPos < length_Short)
                return;

            
        }
    }
}