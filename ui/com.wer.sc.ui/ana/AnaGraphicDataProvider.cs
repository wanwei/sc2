using com.wer.sc.comp.graphic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.data;

namespace com.wer.sc.ui.ana
{
    public class AnaGraphicDataProvider : IGraphicDataProvider_Candle
    {
        private KLineDataWrap klineDataWrap;

        public KLineDataWrap KlineDataWrap
        {
            get
            {
                return klineDataWrap;
            }
        }

        public AnaGraphicDataProvider(KLineDataWrap data)
        {
            this.klineDataWrap = data;
        }
        private int startIndex = 220;

        private int endIndex = 300;

        private int blockMount = 80;
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
                if (endIndex == value)
                    return;
                endIndex = value;
                startIndex = endIndex - blockMount + 1;
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
                startIndex = endIndex - blockMount + 1;
            }
        }

        public string Code
        {
            get
            {
                return klineDataWrap.Code;
            }
        }

        public float CurrentTime
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public KLinePeriod Period
        {
            get
            {
                return klineDataWrap.Period;
            }
        }

        public event DataChangeHandler DataChange;

        public IKLineBar GetCurrentChart()
        {
            return new KLineBar_KLineData(klineDataWrap.KlineData, endIndex);
        }

        public IKLineData GetKLineData()
        {
            return klineDataWrap.KlineData;
        }
        
        public void ChangeData(string code, int startDate, int endDate, KLinePeriod period)
        {
            throw new NotImplementedException();
        }

        public void ChangeData(IKLineData klineData)
        {
            throw new NotImplementedException();
        }
    }
}
