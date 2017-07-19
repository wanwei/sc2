using com.wer.sc.data;
using com.wer.sc.data.reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.comp.graphic
{
    public class GraphicData_Candle : IGraphicData_Candle
    {
        private String code;

        private IKLineData data;

        private int startIndex = 200;

        private int endIndex = 300;

        private int blockMount;

        private KLinePeriod period;

        public GraphicData_Candle(IKLineData klineData, int startIndex, int endIndex)
        {
            ChangeData(klineData);
            this.startIndex = startIndex;
            this.endIndex = endIndex;
        }

        public void ChangeData(IKLineData klineData)
        {
            this.data = klineData;
            this.code = klineData.Code;
            this.period = klineData.Period;
        }

        public IKLineData GetKLineData()
        {
            return data;
        }

        public IKLineBar GetCurrentChart()
        {
            return new KLineBar_KLineData(data, endIndex);
        }

        private void InitIndex()
        {
            startIndex = endIndex - blockMount + 1;
            startIndex = startIndex < 0 ? 0 : startIndex;
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
                this.endIndex = endIndex < 0 ? 0 : endIndex;
                InitIndex();
            }
        }

        public string Code
        {
            get
            {
                return code;
            }
        }

        public KLinePeriod Period
        {
            get
            {
                return period;
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

        //public float CurrentTime
        //{
        //    get
        //    {
        //        return currentTime;
        //    }
        //}

        //public event DataChangeHandler DataChange;

    }
}
