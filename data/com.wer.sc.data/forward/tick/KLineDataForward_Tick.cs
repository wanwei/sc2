using com.wer.sc.data.forward.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.forward
{
    public class KLineDataForward_Tick
    {
        private ForwardDataIndeier forwardDataIndeier;

        private DataForForward_Code dataForForward;

        public KLineDataForward_Tick(DataForForward_Code dataForForward)
        {
            this.dataForForward = dataForForward;
            //this.forwardDataIndeier = new ForwardDataIndeier()
        }

        public void ForwardToday()
        {
            ITickData_Extend tickData = this.dataForForward.CurrentTickData;

            int lastMainKLineBarPos = -1;
            int mainKLineBarPos = forwardDataIndeier.GetMainKLineBarPosIfFinished(tickData.BarPos, out lastMainKLineBarPos);
            //小于0，则表示这个K线bar没有结束
            if (mainKLineBarPos < 0)
            {
                ForwardCurrentBar();
            }
            else
            {
                ForwardNextBar(lastMainKLineBarPos);
            }
        }

        private void ForwardCurrentBar()
        {
            IKLineData_Extend mainKLineData = this.dataForForward.GetMainKLineData();
            ITickBar tickBar = dataForForward.CurrentTickData.GetCurrentBar();
            ForwardCurrentBar(mainKLineData, tickBar);
            foreach (KLinePeriod period in dataForForward.ReferedKLinePeriods)
            {
                if (dataForForward.MainKLinePeriod.Equals(period))
                    continue;
                ForwardCurrentBar(dataForForward.GetKLineData(period), tickBar);
            }
        }

        private void ForwardCurrentBar(IKLineData_Extend mainKLineData, ITickBar tickBar)
        {

        }

        private void ForwardNextBar(int lastMainKLineBarPos)
        {

        }

        private void ForwardNextBar(IKLineData_Extend mainKLineData)
        {

        }

        public void ForwardNextDay()
        {

        }
    }
}
