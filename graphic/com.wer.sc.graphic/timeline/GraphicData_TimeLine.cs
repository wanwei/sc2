using com.wer.sc.data;
using com.wer.sc.data.reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.graphic.timeline
{
    public class GraphicData_TimeLine : IGraphicData_TimeLine
    {
        private ITimeLineData data;

        public GraphicData_TimeLine(ITimeLineData timeLineData)
        {
            this.data = timeLineData;
        }

        public string Code
        {
            get
            {
                return data.Code;
            }
        }

        public int CurrentIndex
        {
            get
            {
                return data.BarPos;
            }
        }

        public double CurrentTime
        {
            get
            {
                return data.Time;
            }
        }

        public ITimeLineBar GetCurrentChart()
        {
            return data.GetCurrentBar();
        }

        public ITimeLineData GetRealData()
        {
            return data;
        }

        public void ChangeData(TimeLineData timeLineData)
        {
            this.data = timeLineData;
            if (OnGraphicDataChange != null)
                OnGraphicDataChange(this, new GraphicDataChangeArgument());
        }

        public event DelegateOnGraphicDataChange OnGraphicDataChange;

    }
}
