using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.forward
{
    public class ForwardOnbar_Info : IForwardKLineBarInfo
    {
        private IKLineData_Extend klineData;

        private int finishedBarPos;

        /// <summary>
        /// OnBar事件执行时完成一个bar的k线数据
        /// </summary>
        public IKLineData_Extend KLineData
        {
            get { return klineData; }
        }

        /// <summary>
        /// 前进器前进完成的Bar
        /// </summary>
        public int BarPos
        {
            get { return finishedBarPos; }
        }

        public KLinePeriod KLinePeriod
        {
            get
            {
                return KLineData.Period;
            }
        }

        public IKLineBar KLineBar
        {
            get
            {
                return KLineData.GetBar(BarPos);
            }
        }

        public ForwardOnbar_Info(IKLineData_Extend klineData, int finishedBarPos)
        {
            this.klineData = klineData;
            this.finishedBarPos = finishedBarPos;
        }

        public override string ToString()
        {
            return KLineBar.ToString();
        }
    }
}
