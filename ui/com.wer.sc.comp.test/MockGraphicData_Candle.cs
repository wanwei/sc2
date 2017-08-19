using com.wer.sc.comp.graphic;
using com.wer.sc.data;
using com.wer.sc.data.reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.comp
{
    public class MockGraphicData_Candle : IGraphicData_Candle
    {
        private String code;

        private int blockMount;

        private IDataReader dataReader;

        private IKLineData data;

        private float currentTime;

        private KLinePeriod period;

        private int startIndex = 200;

        private int endIndex = 300;

        //private int startDate = 20100101;

        //private int endDate = 20150101;

        public MockGraphicData_Candle():this("m1505", 20150101, 20150110, KLinePeriod.KLinePeriod_1Minute)
        {

        }

        public MockGraphicData_Candle(String code, int startDate, int endDate, KLinePeriod period)
        {
            this.dataReader = DataReaderFactory.CreateDataReader(@"E:\SCDATA\CNFUTURES");
            this.data = dataReader.KLineDataReader.GetData(code, startDate, endDate, period);
            this.code = code;
            this.period = period;
        }

        public void ChangeData(IKLineData klineData)
        {
            this.data = klineData;
            this.code = klineData.Code;
            this.period = klineData.Period;
        }


        //public void ChangeData(String code, int startDate, int endDate, KLinePeriod period)
        //{
        //    this.data = fac.KLineDataReader.GetData(code, startDate, endDate, period);
        //    this.startDate = startDate;
        //    this.endDate = endDate;
        //    ChangeData(data);
        //}

        public IKLineData GetKLineData()
        {
            return data;
        }

        public IKLineBar GetCurrentChart()
        {
            return new KLineBar_KLineData(data, endIndex);
        }
        private void InitCharts()
        {
            if (code == null || period == null)
                return;
            // this.data = fac.KLineDataReader.GetData(code, startDate, endDate, period);
        }

        private void InitIndex()
        {
            startIndex = endIndex - blockMount + 1;
        }

        public int StartIndex
        {
            get
            {
                return startIndex;
            }
        }

        public int EndIndex
        {
            get
            {
                return endIndex;
            }
            set
            {
                this.endIndex = value;
                InitIndex();
            }
        }

        public string Code
        {
            get
            {
                return code;
            }

            set
            {
                if (this.code != null && this.code.Equals(value))
                    return;
                this.code = value;
                InitCharts();
            }
        }

        public KLinePeriod Period
        {
            get
            {
                return period;
            }

            set
            {
                if (this.period != null && period.Equals(value))
                    return;
                period = value;
                InitCharts();
            }
        }

        public int BlockMount
        {
            get
            {
                return blockMount;
            }

            set
            {
                if (blockMount == value)
                    return;
                blockMount = value;
                InitIndex();
            }
        }

        public float CurrentTime
        {
            get
            {
                return currentTime;
            }

            set
            {
                currentTime = value;
                EndIndex = findEndIndex(currentTime);
            }
        }

        private int findEndIndex(float currentTime)
        {
            bool isSmaller = data.Arr_Time[0] < currentTime;
            if (!isSmaller)
                return 0;
            for (int i = 0; i < data.Length; i++)
            {
                if (data.Arr_Time[i] >= currentTime)
                    return i;
            }
            return data.Length - 1;
        }

        public event DataChangeHandler DataChange;
        public event DelegateOnGraphicDataChange OnGraphicDataChange;
    }
}
