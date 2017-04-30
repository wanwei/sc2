using com.wer.sc.comp.graphic.real;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.data;

namespace com.wer.sc.comp.test
{
    public class MockGraphicDataProvider_Real : IGraphicDataProvider_Real
    {
        private DataReaderFactory fac;

        private string code = "m05";

        private int date = 20150108;

        private ITimeLineData data;

        public MockGraphicDataProvider_Real()
        {
            fac = new DataReaderFactory(@"D:\SCDATA\CNFUTURES");
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
    }
}