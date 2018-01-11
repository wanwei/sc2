using com.wer.sc.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy.common.platform
{
    public class Platform
    {
        private double recordHeight;

        private int recordCount;

        private IKLineData klineData;

        private int startIndex;

        private int endIndex;

        public Platform(IKLineData klineData, int startIndex, int endIndex)
        {
            this.klineData = klineData;
            this.startIndex = startIndex;
            this.endIndex = endIndex;
        }

        
    }
}
