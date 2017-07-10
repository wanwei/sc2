using com.wer.sc.data;
using com.wer.sc.data.reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.comp.graphic
{
    public class GraphicDataProvider_CandleDefault : IGraphicDrawer_Chart_Candle
    {
        private String code;

        private int blockMount;

        private IKLineData data;

        private float currentTime;

        private KLinePeriod period;

        private int startIndex = 200;

        private int endIndex = 300;

        public GraphicDataProvider_CandleDefault()
        {
          
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

        public float CurrentTime
        {
            get
            {
                return currentTime;
            }

            set
            {
                currentTime = value;
                InitIndex();
            }
        }

        public event DataChangeHandler DataChange;

    }
}
