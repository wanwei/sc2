﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.forward
{
    /// <summary>
    /// 前进周期
    /// </summary>
    public class ForwardPeriod
    {
        private bool isTickForward;

        private KLinePeriod klineForwardPeriod;

        public ForwardPeriod(bool isTickForward, KLinePeriod klineForwardPeriod)
        {
            this.isTickForward = isTickForward;
            this.klineForwardPeriod = klineForwardPeriod;
        }

        /// <summary>
        /// 是否是以tick为周期前进
        /// </summary>
        public bool IsTickForward
        {
            get
            {
                return isTickForward;
            }
        }

        /// <summary>
        /// 如果是以K线为周期前进，则这是其前进周期
        /// </summary>
        public KLinePeriod KlineForwardPeriod
        {
            get
            {
                return klineForwardPeriod;
            }
        }
    }
}