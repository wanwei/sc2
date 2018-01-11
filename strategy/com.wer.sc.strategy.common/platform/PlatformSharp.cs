using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy.common.platform
{
    /// <summary>
    /// 平台的尖尖
    /// </summary>
    public class PlatformSharp
    {
        private Platform platform;

        private int startIndex;

        private int endIndex;

        private bool isHigh;

        public PlatformSharp(Platform platform, int startIndex, int endIndex, bool isHigh)
        {
            this.platform = platform;
            this.startIndex = startIndex;
            this.endIndex = endIndex;
            this.isHigh = isHigh;
        }


    }
}
