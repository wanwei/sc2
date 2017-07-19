using com.wer.sc.comp.graphic.timeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.data;
using com.wer.sc.comp.graphic;
using com.wer.sc.data.reader;

namespace com.wer.sc.comp
{
    public class MockGraphicData_Real : IGraphicData_TimeLine
    {
        private IDataReader fac;

        private string code = "m1505";

        private int date = 20150108;

        private ITimeLineData data;

        public MockGraphicData_Real()
        {
            fac = DataReaderFactory.CreateDataReader(@"E:\SCDATA\CNFUTURES");
            data = fac.TimeLineDataReader.GetData(code, date);
        }

        public string Code
        {
            get
            {
                return code;
            }
        }

        public int CurrentIndex
        {
            get
            {
                return 330;
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
            throw new NotImplementedException();
        }
    }
}