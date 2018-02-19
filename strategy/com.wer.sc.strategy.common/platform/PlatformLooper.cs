using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.data;

namespace com.wer.sc.strategy.common.platform
{
    /// <summary>
    /// 查找一个平台，有以下参数：
    /// 1.平台K线最短长度
    /// 2.平台高度
    /// 3.平台突出次数
    /// </summary>
    public class PlatformLooper : IDataLooper
    {
        private int param_Length = 60;

        private double param_Height = 0.5;

        private double param_OutPercent = 2;

        private IKLineData klineData;        

        private Platform currentPlatform;

        public int Param_Length
        {
            get
            {
                return param_Length;
            }

            set
            {
                param_Length = value;
            }
        }

        public double Param_Height
        {
            get
            {
                return param_Height;
            }

            set
            {
                param_Height = value;
            }
        }

        public double Param_OutPercent
        {
            get
            {
                return param_OutPercent;
            }

            set
            {
                param_OutPercent = value;
            }
        }

        public PlatformLooper(IKLineData klineData)
        {
            this.klineData = klineData;
        }

        private void Loop(int startBarPos, int endBarPos)
        {
            double start = klineData.GetBar(startBarPos).End;
            double high = 0;
            double low = double.MaxValue;

            for (int i = startBarPos; i <= endBarPos; i++)
            {
                IKLineBar bar = klineData.GetBar(i);
                if (bar.High > high)
                    high = bar.High;
                if (bar.Low < low)
                    low = bar.Low;

            }
        }

        public void Loop(int barPos)
        {
            if (barPos < Param_Length)
                return;
        }
    }
}