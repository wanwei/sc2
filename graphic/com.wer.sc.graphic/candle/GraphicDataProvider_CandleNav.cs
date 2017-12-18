using com.wer.sc.data;
using com.wer.sc.data.navigate;
using com.wer.sc.data.reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.comp.graphic
{
    /// <summary>
    /// 画图的数据提供者
    /// 可自由指定时间
    /// </summary>
    public class GraphicDataProvider_CandleNav : IGraphicDataProvider_Candle
    {
        //private String code;

        private int startIndex = 200;

        private int endIndex = 300;

        private int blockMount;

        private DataReaderFactory dataReaderFac;

        //private IKLineData data;

        //private float currentTime;

        //private KLinePeriod period;

        //private CurrentKLineChartBuilder currentKLineChartBuilder;

        //private int startDate;

        //private int endDate;

        private IDataNavigate_Code dataNavigate;

        public GraphicDataProvider_CandleNav(DataReaderFactory dataReaderFac)
        {
            this.dataReaderFac = dataReaderFac;
            this.dataNavigate = new DataNavigate(dataReaderFac);
        }

        public GraphicDataProvider_CandleNav(DataReaderFactory dataReaderFac, IDataNavigate_Code dataNavigate)
        {
            this.dataReaderFac = dataReaderFac;
            this.dataNavigate = dataNavigate;            
        }        

        #region 修改数据

        /// <summary>
        /// 修改提供的数据
        /// </summary>
        /// <param name="klineData"></param>
        public void ChangeData(IKLineData klineData)
        {
            //this.data = klineData;
            //this.code = klineData.Code;
            //this.period = klineData.Period;
            //this.startDate = (int)klineData.Arr_Time[0];
            //this.endDate = (int)klineData.Arr_Time[klineData.Length - 1];
            //this.endIndex = data.Length - 1;
            //this.InitIndex();
            this.dataNavigate.Change(klineData, klineData.Arr_Time[klineData.Length - 1]);
        }

        /// <summary>
        /// 修改提供的数据
        /// </summary>
        /// <param name="code"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="period"></param>
        public void ChangeData(String code, int startDate, int endDate, KLinePeriod period)
        {
            IKLineData data = dataReaderFac.KLineDataReader.GetData(code, startDate, endDate, period);
            this.ChangeData(data);
        }

        public void ChangeCode(String code)
        {
            this.dataNavigate.ChangeCode(code);
        }

        /// <summary>
        /// 修改周期
        /// </summary>
        /// <param name="period"></param>
        public void ChangePeriod(KLinePeriod period)
        {
            this.dataNavigate.ChangePeriod(period);
        }

        public void ChangeTime(float time)
        {
            this.dataNavigate.ChangeTime(time);
        }

        #endregion

        public IKLineData GetKLineData()
        {
            return dataNavigate.CurrentKLineData;
        }

        public IKLineBar GetCurrentChart()
        {
            return new KLineBar_KLineData(GetKLineData(), dataNavigate.CurrentKLineIndex);
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
                int index = dataNavigate.CurrentKLineIndex;
                if (index != endIndex) { 
                    endIndex = index;
                    InitIndex();
                }
                return endIndex;
            }
            set
            {
                dataNavigate.ChangeIndex(value);
                this.endIndex = value;
                InitIndex();
            }
        }

        public string Code
        {
            get
            {
                return dataNavigate.CurrentKLineData.Code;
            }
        }

        public KLinePeriod Period
        {
            get
            {
                return dataNavigate.CurrentKLineData.Period;
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

        private void InitIndex()
        {
            startIndex = endIndex - blockMount + 1;
        }

        public float CurrentTime
        {
            get
            {
                return (float)dataNavigate.CurrentTime;
            }
        }

        public event DataChangeHandler DataChange;
    }
}