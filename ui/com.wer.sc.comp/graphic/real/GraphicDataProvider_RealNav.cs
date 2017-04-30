using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.data;

namespace com.wer.sc.comp.graphic.real
{
    public class GraphicDataProvider_RealNav : IGraphicDataProvider_Real
    {
        private DataReaderFactory dataReaderFac;

        private IDataNavigate3 dataNavigate;

        public GraphicDataProvider_RealNav(DataReaderFactory dataReaderFac, IDataNavigate3 dataNavigate)
        {
            this.dataReaderFac = dataReaderFac;
            this.dataNavigate = dataNavigate;
        }

        public string Code
        {
            get
            {
                return this.dataNavigate.Code;
            }
        }

        public int CurrentIndex
        {
            get
            {
                return this.dataNavigate.CurrentRealIndex;
            }
        }

        public double CurrentTime
        {
            get
            {
                return this.dataNavigate.CurrentTime;
            }
        }

        public ITimeLineBar GetCurrentChart()
        {
            return new TimeLineBar_TimeLineData(GetRealData(), CurrentIndex);
        }

        public ITimeLineData GetRealData()
        {
            return dataNavigate.CurrentRealData;
        }
    }
}
